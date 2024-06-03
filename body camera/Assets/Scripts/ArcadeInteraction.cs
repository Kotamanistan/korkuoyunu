using UnityEngine;

public class ArcadeInteraction : MonoBehaviour
{
    public GameObject player;
    public GameObject miniGameUI;
    public float interactionDistance = 3f;  // Etkileþim mesafesi
    private bool isPlayerNearby = false;

    void Start()
    {
        // Minik oyun UI'sini baþlangýçta deaktif yap
        miniGameUI.SetActive(false);
    }

    void Update()
    {
        // Karakterin bakýþ yönünü ve mesafesini kontrol et
        Ray ray = new Ray(player.transform.position, player.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            // Eðer hit objesi arcade makinesi ise ve E tuþuna basýlýrsa
            if (hit.collider.gameObject == gameObject && Input.GetKeyDown(KeyCode.E))
            {
                miniGameUI.SetActive(true);
                player.GetComponent<PlayerController>().enabled = false; // Karakter hareketini devre dýþý býrak
            }
        }
        else
        {
            miniGameUI.SetActive(false);
            player.GetComponent<PlayerController>().enabled = true; // Karakter hareketini tekrar etkinleþtir
        }
    }
}
