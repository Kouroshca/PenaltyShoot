using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallLauncher : MonoBehaviour
{
   public GameObject ballPrefab; // Assign the ball prefab in Unity
    public Transform launchPoint; // The position where the ball will be spawned
    public float launchForce = 20f; // Adjust the force as needed
    public float respawnTime = 3f; // Time before spawning a new ball
    public Slider slider; 

    private GameObject currentBall;

    void Start()
    {
        SpawnBall(); // Spawn the first ball when the game starts
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && currentBall != null)
        {
            LaunchBall();
            Slider();
        }
    }

    private void LaunchBall()
    {
        Rigidbody rb = currentBall.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero; // Reset velocity
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = false; // Ensure physics is enabled

            Vector3 launchDirection = transform.forward + new Vector3(0, 0.2f, 0); // Slightly upwards
            rb.AddForce(launchDirection * launchForce, ForceMode.Impulse);

            StartCoroutine(RespawnBallAfterDelay());
        }
    }

    void Slider()
    {
        slider.value = launchForce;
    }
    IEnumerator RespawnBallAfterDelay()
    {
        yield return new WaitForSeconds(respawnTime);

        // Destroy old ball and spawn a new one
        if (currentBall != null)
        {
            Destroy(currentBall);
        }
        SpawnBall();
    }

    private void SpawnBall()
    {
        currentBall = Instantiate(ballPrefab, launchPoint.position, Quaternion.identity);
        currentBall.GetComponent<Rigidbody>().isKinematic = true; // Prevent movement until launched
    }

}
