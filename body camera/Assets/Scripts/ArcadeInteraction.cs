using UnityEngine;

public class ArcadeInteraction : MonoBehaviour
{
    public GameObject player;
    public GameObject miniGameUI;
    public float interactionDistance = 3f;  // Etkile�im mesafesi
    private bool isPlayerNearby = false;

    void Start()
    {
        // Minik oyun UI'sini ba�lang��ta deaktif yap
        miniGameUI.SetActive(false);
    }

    void Update()
    {
        // Karakterin bak�� y�n�n� ve mesafesini kontrol et
        Ray ray = new Ray(player.transform.position, player.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            // E�er hit objesi arcade makinesi ise ve E tu�una bas�l�rsa
            if (hit.collider.gameObject == gameObject && Input.GetKeyDown(KeyCode.E))
            {
                miniGameUI.SetActive(true);
                player.GetComponent<PlayerController>().enabled = false; // Karakter hareketini devre d��� b�rak
            }
        }
        else
        {
            miniGameUI.SetActive(false);
            player.GetComponent<PlayerController>().enabled = true; // Karakter hareketini tekrar etkinle�tir
        }
    }
}
