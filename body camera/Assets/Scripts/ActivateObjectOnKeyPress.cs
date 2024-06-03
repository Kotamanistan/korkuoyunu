using UnityEngine;

public class ActivateObjectOnKeyPress : MonoBehaviour
{
    public GameObject objectToToggle;
    private bool isVisible = false;

    void Start()
    {
        // Oyun baþýnda objeyi gizle
        ToggleObjectVisibility(objectToToggle, false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            // Her F tuþuna basýldýðýnda görünürlüðü deðiþtir
            isVisible = !isVisible;
            ToggleObjectVisibility(objectToToggle, isVisible);
        }
    }

    void ToggleObjectVisibility(GameObject obj, bool visible)
    {
        obj.SetActive(visible);
    }
}
