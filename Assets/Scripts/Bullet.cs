using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public int damage = 50;
    public float speed = 20;

    public GameObject explosionEffectPrefab;

    private Transform target;

    public void setTarget (Transform _target) {
        this.target = _target;
    }
    void Update () {
        if (target == null) {
            Die ();
            return;
        }
        transform.LookAt (target.position);
        transform.Translate (Vector3.forward * speed * Time.deltaTime);
    }
    void OnTriggerEnter (Collider col) {
        if (col.tag == "Enemy") {
            col.GetComponent<Enemy> ().TakeDamage (damage);
            GameObject effect = GameObject.Instantiate (explosionEffectPrefab, transform.position, transform.rotation);
            Destroy (effect, 1);
            Destroy (this.gameObject);
        }
    }
    void Die () {
        GameObject effect = GameObject.Instantiate (explosionEffectPrefab, transform.position, transform.rotation);
        Destroy (effect, 1);
        Destroy (this.gameObject);
    }
}