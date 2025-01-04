using UnityEngine;
using System.Collections;

public class TrafficLight : MonoBehaviour
{
    public GameObject redLight;
    public GameObject greenLight;
    public GameObject grayLight;
    public Rigidbody2D ground;

    private bool isGreenLightOn = true;
    private CharController player;

    void Start()
    {
        player = GameManager.Instance.player;

        redLight.SetActive(false);
        grayLight.SetActive(false);
        greenLight.SetActive(true);
        StartCoroutine(SwitchLights());
    }

    void Update()
    {
        if (!isGreenLightOn && PlayerIsMoving())
        {
            ground.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    private IEnumerator SwitchLights()
    {
        while (true)
        {
            // Green light
            isGreenLightOn = true;
            grayLight.SetActive(false);
            redLight.SetActive(false);
            greenLight.SetActive(true);

            yield return new WaitForSeconds(3f);

            // Blink
            yield return StartCoroutine(BlinkGrayLight(1.5f));

            // Red light
            isGreenLightOn = false;
            grayLight.SetActive(false);
            redLight.SetActive(true);
            greenLight.SetActive(false);

            yield return new WaitForSeconds(3f);

            // Blink
            yield return StartCoroutine(BlinkGrayLight(1.5f));
        }
    }

    private IEnumerator BlinkGrayLight(float duration)
    {
        float elapsed = 0f;
        bool isOn = false;

        while (elapsed < duration)
        {
            elapsed += 0.2f;
            isOn = !isOn;
            grayLight.SetActive(isOn);
            yield return new WaitForSeconds(0.2f);
        }
        grayLight.SetActive(false);
    }

    private bool PlayerIsMoving()
    {
        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        Debug.Log(playerRb.velocity);
        return Mathf.Abs(playerRb.velocity.x) > 0.1f || Mathf.Abs(playerRb.velocity.y) > 0.1f;
    }
}
