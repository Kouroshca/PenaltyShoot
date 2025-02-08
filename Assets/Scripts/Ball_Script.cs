using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Script : MonoBehaviour
{
    public float Force;
    public Transform Target;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
            Destroy(gameObject, 5f);

        }
    }

    private void Shoot()
    {
        Vector3 ShootForce = (Target.position - this.transform.position).normalized;
        
        GetComponent<Rigidbody>().AddForce(ShootForce * Force + new Vector3(0, 3f, 0), ForceMode.Impulse);
    }
}