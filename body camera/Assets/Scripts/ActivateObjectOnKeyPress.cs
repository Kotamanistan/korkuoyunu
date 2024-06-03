using UnityEngine;

public class ActivateObjectOnKeyPress : MonoBehaviour
{
    public GameObject objectToToggle;
    private bool isVisible = false;

    void Start()
    {
        // Oyun ba��nda objeyi gizle
        ToggleObjectVisibility(objectToToggle, false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            // Her F tu�una bas�ld���nda g�r�n�rl��� de�i�tir
            isVisible = !isVisible;
            ToggleObjectVisibility(objectToToggle, isVisible);
        }
    }

    void ToggleObjectVisibility(GameObject obj, bool visible)
    {
        obj.SetActive(visible);
    }
}
