using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using UnityEngine.UI; // UI bileþenlerine eriþmek için gerekli

public class PlayerController : MonoBehaviour
{
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public CinemachineVirtualCamera virtualCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    public float crouchHeight = 0.5f; // Yükseklik
    private bool isCrouching = false;
    private float originalHeight;
    public float crouchSpeedModifier = 0.5f;

    // Cinemachine noise parameters
    public float walkingAmplitude = 1f;
    public float walkingFrequency = 2f;
    public float runningAmplitude = 3f;
    public float runningFrequency = 4f;

    // Stamina parameters
    public float maxStamina = 100f;
    public float staminaDecreaseRate = 10f; // per second
    public float staminaIncreaseRate = 20f; // per second
    public float staminaRegenDelay = 4f;
    private float currentStamina;
    private float staminaRegenTimer = 0f;
    private bool canRun = true;

    public Slider staminaSlider; // UI Slider for stamina

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;

    private CinemachineBasicMultiChannelPerlin noise;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Debug.Log("Oyun baþladý. Pozisyon: " + transform.position.y);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        originalHeight = characterController.height;

        noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        // Initialize stamina
        currentStamina = maxStamina;
        staminaSlider.maxValue = maxStamina;
        staminaSlider.value = currentStamina;
    }

    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift) && canRun;
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        characterController.Move(moveDirection * Time.deltaTime);

        // Update Cinemachine noise based on movement
        if (canMove)
        {
            if (isRunning)
            {
                noise.m_AmplitudeGain = runningAmplitude;
                noise.m_FrequencyGain = runningFrequency;

                // Reduce stamina
                currentStamina -= staminaDecreaseRate * Time.deltaTime;
                if (currentStamina <= 0)
                {
                    currentStamina = 0;
                    canRun = false;
                }
                // Reset stamina regen timer
                staminaRegenTimer = 0f;
            }
            else
            {
                noise.m_AmplitudeGain = walkingAmplitude;
                noise.m_FrequencyGain = walkingFrequency;

                // Regenerate stamina if not running
                if (currentStamina < maxStamina)
                {
                    staminaRegenTimer += Time.deltaTime;
                    if (staminaRegenTimer >= staminaRegenDelay)
                    {
                        currentStamina += staminaIncreaseRate * Time.deltaTime;
                        if (currentStamina >= maxStamina)
                        {
                            currentStamina = maxStamina;
                            canRun = true;
                        }
                    }
                }
            }
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            Quaternion localRotation = Quaternion.Euler(rotationX, 0, 0);
            virtualCamera.transform.localRotation = localRotation;
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);

            // Crouch functionality
            if (Input.GetKeyDown(KeyCode.C))
            {
                isCrouching = !isCrouching;
                characterController.height = isCrouching ? crouchHeight : originalHeight;
                characterController.center = new Vector3(characterController.center.x,
                    isCrouching ? crouchHeight / 2 : originalHeight / 2, characterController.center.z);
            }

            if (isCrouching)
            {
                noise.m_AmplitudeGain *= crouchSpeedModifier;
                noise.m_FrequencyGain *= crouchSpeedModifier;
            }
        }

        // Update stamina slider
        staminaSlider.value = currentStamina;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Respawn")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
