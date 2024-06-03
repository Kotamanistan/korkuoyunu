using UnityEngine;

public class HideMechanic : MonoBehaviour
{
    public Transform teleportTarget; // I��nlan�lacak hedef nokta

    private Vector3 savedPosition; // Kaydedilen pozisyon
    private bool canTeleport = false; // I��nlanma kontrol�

    void Update()
    {
        // Crosshair ile "Door" etiketli objenin �st�ne 3f mesafede olup olmad���n� kontrol et
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 3f))
        {
            if (hit.collider.CompareTag("Door"))
            {
                // E tu�una bas�ld���nda
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (!canTeleport)
                    {
                        // Pozisyonu kaydet
                        savedPosition = transform.position;
                        // I��nlan
                        transform.position = teleportTarget.position;
                        canTeleport = true;
                    }
                    else
                    {
                        // Kaydedilen pozisyona ���nlan
                        transform.position = savedPosition;
                        canTeleport = false;
                    }
                }
            }
        }
    }
}