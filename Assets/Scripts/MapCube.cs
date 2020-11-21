using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class MapCube : MonoBehaviour {
    [HideInInspector]
    public bool isUpgraded = false;
    [HideInInspector]
    public GameObject turretGo;
    public TurretData turretData;
    public Material material;

    void Start () {
        material = GetComponent<Renderer> ().material;
    }
    public GameObject buildEffect;
    public void BuildTurret (TurretData turretData) {
        this.turretData=turretData;
        isUpgraded = false;
        turretGo = GameObject.Instantiate (turretData.turretPrefab, transform.position, Quaternion.identity);
        GameObject effect = GameObject.Instantiate (buildEffect, transform.position, Quaternion.identity);
        Destroy (effect, 1.5f);
    }
    public void UpgradeTurret(){
        if(isUpgraded==true)return;
        Destroy(turretGo);
        isUpgraded = true;
        turretGo = GameObject.Instantiate (turretData.turretUpgradedPrefab, transform.position, Quaternion.identity);
        GameObject effect = GameObject.Instantiate (buildEffect, transform.position, Quaternion.identity);
        Destroy (effect, 1.5f);
    }
    public void DestroyTurret(){
        Destroy(turretGo);
        isUpgraded=false;
        turretGo=null;
        turretData=null;
        GameObject effect = GameObject.Instantiate (buildEffect, transform.position, Quaternion.identity);
        Destroy (effect, 1.5f);
    }
    void OnMouseOver () {
        if (turretGo == null && EventSystem.current.IsPointerOverGameObject () == false) {
            material.color = Color.red;
        }
    }
    void OnMouseExit () {
        material.color = Color.white;
    }
}