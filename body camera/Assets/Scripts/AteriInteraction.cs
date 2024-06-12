using UnityEngine;
using UnityEngine.UI;

public class AteriInteraction : MonoBehaviour
{
    public GameObject miniGameUI; // Mini oyun UI paneli
    public MonoBehaviour playerController; // PlayerController script'i
    public Button playButton; // Play butonu
    public MiniGameController miniGameController; // Mini oyun kontrol script'i
    public float interactionDistance = 3f; // Etkileþim mesafesi

    private Camera playerCamera;
    private bool isNearAteri = false;

    void Start()
    {
        // Mini oyun UI panelini baþlangýçta devre dýþý býrak
        if (miniGameUI != null)
        {
            miniGameUI.SetActive(false);
        }
        else
        {
            Debug.LogError("Mini Game UI is not assigned in the inspector.");
        }

        // Oyuncunun kamerasýný bul
        playerCamera = Camera.main;

        // Play butonuna týklama olayýný baðla
        if (playButton != null)
        {
            playButton.onClick.AddListener(StartMiniGame);
        }
        else
        {
            Debug.LogError("Play Button is not assigned in the inspector.");
        }
    }

    void Update()
    {
        // Raycast kullanarak oyuncunun baktýðý noktayý tespit et
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            // Eðer raycast arcade makinesine çarptýysa ve E tuþuna basýldýysa
            if (hit.collider.CompareTag("ArcadeMachine") && Input.GetKeyDown(KeyCode.E))
            {
                miniGameUI.SetActive(true);
                playerController.enabled = false; // PlayerController'ý devre dýþý býrak
                Cursor.lockState = CursorLockMode.None; // Mouse imlecini serbest býrak
                Cursor.visible = true; // Mouse imlecini görünür yap
            }
        }

        // ESC tuþuna basýldýðýnda mini oyun UI'sini devre dýþý býrak ve PlayerController'ý etkinleþtir
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            miniGameUI.SetActive(false);
            playerController.enabled = true; // PlayerController'ý etkinleþtir
            Cursor.lockState = CursorLockMode.Locked; // Mouse imlecini kilitle
            Cursor.visible = false; // Mouse imlecini gizle
        }
    }

    void StartMiniGame()
    {
        miniGameController.StartMiniGame();
        miniGameUI.SetActive(true);
    }
}
