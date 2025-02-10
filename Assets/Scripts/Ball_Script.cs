using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ball_Script : MonoBehaviour
{
    public float Force;
    public Transform Target;
    public Slider forceUI;
    
    public Transform initialPosition;
    public TextMeshProUGUI scoreText;
    private int score = 0;




    void Start()
    {
        if (initialPosition == null)
        {
            initialPosition = new GameObject("InitialPosition").transform;
            initialPosition.position = transform.position;
        }
        UpdateScoreText();
    }

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
        ResetPosition();
    }

    private void ResetPosition()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        transform.position = initialPosition.position;
        transform.rotation = Quaternion.identity;
        IncreaseScore();
    }
    private void IncreaseScore()
    {
        score += 1;
        UpdateScoreText();
    }
    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Goal"))
        {
            Debug.Log("Goal scored");
            IncreaseScore();
        }
    }
}