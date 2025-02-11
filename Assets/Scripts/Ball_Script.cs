using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ball_Script : MonoBehaviour
{
    public float Force;
    public float maxForce = 50f; // liimit max force
    public Transform Target; //target 
    public Slider forceUI;// slider 
    public Transform initialPosition;
    public TextMeshProUGUI scoreText;
    private int score = 0; // initial score value

    void Start()
    {
        if (initialPosition == null)
        {
            initialPosition = new GameObject("InitialPosition").transform;
            initialPosition.position = transform.position;
        }
        UpdateScoreText(); // update it as a 0 value.
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Force = Mathf.Clamp(Force + Time.deltaTime * 20f, 0, maxForce); // how fast the slider filled up.
            Slider();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            Shoot();
            StartCoroutine(RespawnAfterTime(3f)); // wait for 3 seconds to put a new ball. 
        }
    }

    private void Shoot()
    {
        Vector3 ShootForce = (Target.position - transform.position).normalized;
        GetComponent<Rigidbody>().AddForce(ShootForce * Force, ForceMode.Impulse);
        ResetGauge(); // Ensure force resets after shooting
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

    private void ResetPosition()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        transform.position = initialPosition.position;
        transform.rotation = Quaternion.identity;
    }

    private void UpdateScoreText()
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
            score++;
            UpdateScoreText();
        }
    }

    IEnumerator RespawnAfterTime(float Delay)
    {
        yield return new WaitForSeconds(Delay);
        ResetPosition();
    }
}
