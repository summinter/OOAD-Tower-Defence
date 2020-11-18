using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class viewcontroller : MonoBehaviour
{
    
    public float speed= 1;
    public float mouseSpeed=90;
    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float mouse=Input.GetAxis("Mouse ScrollWheel");
        transform.Translate(new Vector3(h*speed,-mouse*mouseSpeed,v*speed)*Time.deltaTime*speed,Space.World);
    }
}
