using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BulidManager : MonoBehaviour {
    public TurretData laserTurretData;
    public TurretData missileTurretData;
    public TurretData standardTurretData;

    //UI
    private TurretData selectedTurretData;
    //物体
    public MapCube selectedMapCube;
    public GameObject upgradeCanvas;
    public Button buttonUpgrade;

    private int money = 1000;

    public Animator moneyAnimator;

    public Text moneyText;
    private Animator upgradeCanvasAnimator;
    public void ChangeMoney (int change = 0) {
        money += change;
        moneyText.text = "$" + money;
    }
    void Start () {
        upgradeCanvasAnimator = upgradeCanvas.GetComponent<Animator> ();
        switch(EnemySpawner.waveInt){
            case 1:
                money=500;
                break;
            case 2:
                money=800;
                break;
            case 3:
                money=1000;
                break;
            case 4:
                money=600;
                break;
        }
    }
    void Update () {
        if (Input.GetMouseButtonDown (0)) {
            if (EventSystem.current.IsPointerOverGameObject () == false) {
                Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
                RaycastHit hit;
                bool isCollider = Physics.Raycast (ray, out hit, 1000, LayerMask.GetMask ("MapCube"));
                if (isCollider) {
                    MapCube mapCube = hit.collider.GetComponent<MapCube> ();
                    if (selectedTurretData != null && mapCube.turretGo == null) {
                        if (money > selectedTurretData.cost) {
                            ChangeMoney (-selectedTurretData.cost);
                            mapCube.BuildTurret (selectedTurretData);
                        } else {
                            moneyAnimator.SetTrigger ("Flicker");
                        }
                    } else if (mapCube.turretGo != null) {
                        selectedMapCube = mapCube;
                        if (mapCube == selectedMapCube && upgradeCanvas.activeInHierarchy) {
                            StartCoroutine (HideUpgradeUI ());
                        } else {
                            ShowUpgradeUI (mapCube.transform.position, mapCube.isUpgraded);
                        }
                    }

                }
            }
        }
    }

    public void OnLaserSelected (bool isOn) {
        if (isOn) {
            selectedTurretData = laserTurretData;
        }
    }

    public void OnMissileSelected (bool isOn) {
        if (isOn) {
            selectedTurretData = missileTurretData;
        }
    }
    public void OnStandardSelected (bool isOn) {
        if (isOn) {
            selectedTurretData = standardTurretData;
        }
    }
    void ShowUpgradeUI (Vector3 pos, bool isDisableUpgrade = false) {
        StopCoroutine("HideUpgradeUI");
        upgradeCanvas.SetActive(false);
        upgradeCanvas.SetActive(true);
        buttonUpgrade.interactable = !isDisableUpgrade;
        upgradeCanvas.transform.position = pos;
    }
    IEnumerator HideUpgradeUI () {
        upgradeCanvasAnimator.SetTrigger("Hide");
        yield return new WaitForSeconds (0.3f);
        upgradeCanvas.SetActive (false);
    }
    public void OnUpgradeButtonDown () {
        if(money>=selectedMapCube.turretData.costUpgraded){
            ChangeMoney(-selectedMapCube.turretData.costUpgraded);
            selectedMapCube.UpgradeTurret();
        }else{
            moneyAnimator.SetTrigger ("Flicker");
        }
        StartCoroutine(HideUpgradeUI());
 
    }
    public void OnDestroyButtonDown () {
        selectedMapCube.DestroyTurret();
        StartCoroutine(HideUpgradeUI());
    }
}