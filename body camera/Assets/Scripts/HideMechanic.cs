using UnityEngine;

public class HideMechanic : MonoBehaviour
{
    public Transform teleportTarget; // Iþýnlanýlacak hedef nokta

    private Vector3 savedPosition; // Kaydedilen pozisyon
    private bool canTeleport = false; // Iþýnlanma kontrolü

    void Update()
    {
        // Crosshair ile "Door" etiketli objenin üstüne 3f mesafede olup olmadýðýný kontrol et
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 3f))
        {
            if (hit.collider.CompareTag("Door"))
            {
                // E tuþuna basýldýðýnda
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (!canTeleport)
                    {
                        // Pozisyonu kaydet
                        savedPosition = transform.position;
                        // Iþýnlan
                        transform.position = teleportTarget.position;
                        canTeleport = true;
                    }
                    else
                    {
                        // Kaydedilen pozisyona ýþýnlan
                        transform.position = savedPosition;
                        canTeleport = false;
                    }
                }
            }
        }
    }
}