using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball_Script : MonoBehaviour
{
    public float Force;
    public Transform Target;
    public Slider forceUI;
 
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space)) // it will fill slider dependin on pressure
        {
            Force++; // hold the button to add force to the ball
            Slider();
        }
        
        
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Shoot();
            StartCoroutine(Wait());

        }

        
    }


    private void Shoot()
    {
        Vector3 ShootForce = (Target.position - this.transform.position).normalized;
        Debug.Log("Shoot");
       // GetComponent<Rigidbody>().AddForce(ShootForce * Force + new Vector3(0, 3f, 0), ForceMode.Impulse);
        GetComponent<Rigidbody>().AddForce(ShootForce * Force, ForceMode.Impulse);
    }
    private void Slider()
    {
        forceUI.value = Force;
    }

    public void ResetGauge() 
    {
        Force = 0;
        forceUI.value = 0;
    }

    IEnumerator Wait() 
    {
        yield return new WaitForSeconds(1.5f);
        ResetGauge();
    }
}