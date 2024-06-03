using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CrosshairInteraction : MonoBehaviour
{
    public Image doorImage;
    public Image drinkImage;
    public Image keyImage;
    public Camera mainCamera; // Main camera reference
    public float maxDistance = 10f; // Max distance for raycast
    public LayerMask interactableLayer; // Layer mask for interactable objects

    void Start()
    {
        // Ensure all images are initially hidden
        doorImage.gameObject.SetActive(false);
        drinkImage.gameObject.SetActive(false);
        keyImage.gameObject.SetActive(false);
    }

    void Update()
    {
        // Hide all images by default
        doorImage.gameObject.SetActive(false);
        drinkImage.gameObject.SetActive(false);
        keyImage.gameObject.SetActive(false);

        // Perform a raycast from the center of the screen, only considering the specified layer
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance, interactableLayer))
        {
            // Check the tag of the hit object
            if (hit.collider.CompareTag("Door"))
            {
                doorImage.gameObject.SetActive(true);
            }
            else if (hit.collider.CompareTag("Soda"))
            {
                drinkImage.gameObject.SetActive(true);
            }
            else if (hit.collider.CompareTag("MemoryObject"))
            {
                keyImage.gameObject.SetActive(true);
            }
        }
    }
}
