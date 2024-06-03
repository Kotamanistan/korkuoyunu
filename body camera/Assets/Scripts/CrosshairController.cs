using UnityEngine;
using UnityEngine.UI;

public class CrosshairController : MonoBehaviour
{
    public Transform handTransform;
    public float interactDistance = 3f;
    public LayerMask interactableLayer;
    public Image memoryUIImage;
    public PlayerController playerController;
    public HealthSystem healthSystem;

    private GameObject heldObject = null;
    private bool sodaUsed = false;
    private bool memoryObjectHeld = false;

    void Start()
    {
        memoryUIImage.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && heldObject == null)
        {
            TryPickUpObject();
        }

        if (Input.GetMouseButtonDown(0) && memoryObjectHeld && heldObject != null)
        {
            ToggleMemoryUI();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (memoryObjectHeld)
            {
                HideMemoryUI();
                playerController.enabled = true;
                SetMaxHealth();
                Destroy(heldObject);
                memoryObjectHeld = false;
            }
        }

        if (heldObject != null && heldObject.CompareTag("Soda"))
        {
            if (Input.GetMouseButtonDown(0) && !sodaUsed)
            {
                UseSoda();
            }
        }
    }

    void TryPickUpObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactableLayer))
        {
            if (hit.collider.CompareTag("MemoryObject"))
            {
                PickUpMemoryObject(hit.collider.gameObject);
            }
            else if (hit.collider.CompareTag("Soda") && !sodaUsed)
            {
                PickUpObject(hit.collider.gameObject);
            }
        }
    }

    void PickUpObject(GameObject obj)
    {
        heldObject = obj;
        heldObject.transform.position = handTransform.position;
        heldObject.transform.parent = handTransform;
        heldObject.GetComponent<Rigidbody>().isKinematic = true;
    }

    void PickUpMemoryObject(GameObject obj)
    {
        heldObject = obj;
        heldObject.transform.position = handTransform.position;
        heldObject.transform.parent = handTransform;
        heldObject.GetComponent<Rigidbody>().isKinematic = true;
        memoryObjectHeld = true;
        ShowMemoryUI();
        playerController.enabled = false;
    }

    void ToggleMemoryUI()
    {
        if (!memoryUIImage.gameObject.activeSelf)
        {
            ShowMemoryUI();
            playerController.enabled = false;
        }
        else
        {
            HideMemoryUI();
            playerController.enabled = true;
        }
    }

    void ShowMemoryUI()
    {
        memoryUIImage.gameObject.SetActive(true);
    }

    void HideMemoryUI()
    {
        memoryUIImage.gameObject.SetActive(false);
    }

    void SetMaxHealth()
    {
        healthSystem.currentHealth = healthSystem.maxHealth;
    }

    void UseSoda()
    {
        healthSystem.currentHealth = Mathf.Min(healthSystem.currentHealth + 50, healthSystem.maxHealth);
        DropObject();
        sodaUsed = true;
    }

    void DropObject()
    {
        heldObject.transform.parent = null;
        heldObject.GetComponent<Rigidbody>().isKinematic = false;
        heldObject = null;
        memoryObjectHeld = false;
        HideMemoryUI();
        playerController.enabled = true;
    }
}
