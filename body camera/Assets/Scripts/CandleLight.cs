using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleLight : MonoBehaviour
{
    public Light candleLight;
    public KeyCode toggleKey = KeyCode.F;
    public float minIntensity = 0.8f;
    public float maxIntensity = 1.2f;
    public float flickerSpeed = 0.1f; // Titreþim hýzýný kontrol eden deðiþken
    private bool isLit = false;
    private CharacterController characterController;
    private float nextFlickerTime; // Bir sonraki titreþim zamaný

    void Start()
    {
        candleLight.enabled = false;
        characterController = GetComponentInParent<CharacterController>();
        nextFlickerTime = Time.time;
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            isLit = !isLit;
            candleLight.enabled = isLit;
        }

        if (isLit)
        {
            FlickerLight();
            AdjustLightIntensityBasedOnMovement();
        }
    }

    void FlickerLight()
    {
        if (Time.time >= nextFlickerTime)
        {
            candleLight.intensity = Random.Range(minIntensity, maxIntensity);
            nextFlickerTime = Time.time + flickerSpeed; // Bir sonraki titreþim zamaný
        }
    }

    void AdjustLightIntensityBasedOnMovement()
    {
        if (characterController != null)
        {
            float movementSpeed = characterController.velocity.magnitude;
            candleLight.intensity += Mathf.PingPong(Time.time * flickerSpeed, 0.2f) * movementSpeed;
        }
    }
}
