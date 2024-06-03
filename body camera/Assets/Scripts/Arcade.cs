using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionDistance = 5f;
    public Camera playerCamera;
    public GameObject interactionUI;
    public string targetTag = "Arcade";

    void Start()
    {
        // Oyun baþladýðýnda interactionUI'yi gizle
        if (interactionUI != null)
        {
            interactionUI.SetActive(false);
        }
    }

    void Update()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            if (hit.collider.CompareTag(targetTag))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactionUI.SetActive(true);
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            interactionUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}