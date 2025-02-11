using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class Timer : MonoBehaviour
{
        public float gameDuration = 60f; // Total time in seconds
    private float timeRemaining;
    private bool gameActive = true;

    public TextMeshProUGUI timerText; // Assign a UI Text element in Unity
    public Ball_Script ballScript; // Reference to the Ball script
    private Rigidbody ballRigidbody;

    void Start()
    {
        timeRemaining = gameDuration;
        UpdateTimerUI();
        StartCoroutine(Countdown());

        // get the ball and rigid body.
        GameObject ball = GameObject.FindGameObjectWithTag("Ball");
        if (ball != null)
        {
            ballRigidbody = ball.GetComponent<Rigidbody>();
        }
    }

    IEnumerator Countdown()
    {
        while (timeRemaining > 0)
        {
            yield return new WaitForSeconds(1f);
            timeRemaining--;
            UpdateTimerUI();
        }
        
        EndGame();
    }

    private void UpdateTimerUI()
    {
        if (timerText != null)
        {
            timerText.text = "Time: " + Mathf.Ceil(timeRemaining);
        }
    }

    private void EndGame()
    {
        gameActive = false;

        // Stop ball movement
        if (ballRigidbody != null)
        {
            ballRigidbody.velocity = Vector3.zero;
            ballRigidbody.angularVelocity = Vector3.zero;
        }

        if (timerText != null)
        {
            timerText.text = "Game Over!";
        }
    }
}
