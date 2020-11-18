using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {
    private List<GameObject> enemys = new List<GameObject> ();
    void OnTriggerEnter (Collider col) {
        if (col.tag == "Enemy") {
            enemys.Add (col.gameObject);
        }
    }

    void OnTriggerExit (Collider col) {
        enemys.Remove (col.gameObject);
    }

    public float attackRateTime = 1;
    private float timer = 0;
    public GameObject bulletPrefab;
    public Transform firePosition;
    public Transform head;

    public bool useLaser = false;
    public float damageRate = 70;
    public LineRenderer laserRender;
    public GameObject laserEffect;

    void Start () {
        timer = attackRateTime;
    }
    void Update () {
        if (enemys.Count > 0 && enemys[0] != null) {
            Vector3 targetPosition = enemys[0].transform.position;
            targetPosition.y = head.position.y;
            head.LookAt (targetPosition);
        }
        if (useLaser == false) {
            timer += Time.deltaTime;
            if (enemys.Count > 0 && timer >= attackRateTime) {
                timer = 0;
                Attack ();
            }
        } else if (enemys.Count > 0) {
            if (enemys[0] == null) {
                enemys = UpdateEnemys (enemys);
            }
            if (enemys.Count > 0) {
                if (laserRender.enabled == false)
                    laserRender.enabled = true;
                laserEffect.SetActive(true);
                laserRender.SetPositions (new Vector3[] { firePosition.position, enemys[0].transform.position });
                enemys[0].GetComponent<Enemy>().TakeDamage(damageRate*Time.deltaTime);
                laserEffect.transform.position=enemys[0].transform.position;
                Vector3 pos=transform.position;
                pos.y=enemys[0].transform.position.y;
                laserEffect.transform.LookAt(pos);
            }
        } else {
            laserEffect.SetActive(false);
            laserRender.enabled = false;
        }
    }
    void Attack () {
        if (enemys[0] == null)
            enemys = UpdateEnemys (enemys);
        if (enemys.Count == 0) {
            timer = attackRateTime;
            return;
        }
        GameObject bullet = GameObject.Instantiate (bulletPrefab, firePosition.position, firePosition.rotation);
        bullet.GetComponent<Bullet> ().setTarget (enemys[0].transform);
    }
    List<GameObject> UpdateEnemys (List<GameObject> enemys) {
        List<GameObject> newEnemys = new List<GameObject> ();
        foreach (GameObject item in enemys) {
            if (item != null) {
                newEnemys.Add (item);
            }
        }
        return newEnemys;
    }
}