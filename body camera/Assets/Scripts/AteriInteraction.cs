using UnityEngine;
using UnityEngine.UI;

public class AteriInteraction : MonoBehaviour
{
    public GameObject miniGameUI; // Mini oyun UI paneli
    public MonoBehaviour playerController; // PlayerController script'i
    public Button playButton; // Play butonu
    public MiniGameController miniGameController; // Mini oyun kontrol script'i
    public float interactionDistance = 3f; // Etkile�im mesafesi

    private Camera playerCamera;
    private bool isNearAteri = false;

    void Start()
    {
        // Mini oyun UI panelini ba�lang��ta devre d��� b�rak
        if (miniGameUI != null)
        {
            miniGameUI.SetActive(false);
        }
        else
        {
            Debug.LogError("Mini Game UI is not assigned in the inspector.");
        }

        // Oyuncunun kameras�n� bul
        playerCamera = Camera.main;

        // Play butonuna t�klama olay�n� ba�la
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
        // Raycast kullanarak oyuncunun bakt��� noktay� tespit et
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            // E�er raycast arcade makinesine �arpt�ysa ve E tu�una bas�ld�ysa
            if (hit.collider.CompareTag("ArcadeMachine") && Input.GetKeyDown(KeyCode.E))
            {
                miniGameUI.SetActive(true);
                playerController.enabled = false; // PlayerController'� devre d��� b�rak
                Cursor.lockState = CursorLockMode.None; // Mouse imlecini serbest b�rak
                Cursor.visible = true; // Mouse imlecini g�r�n�r yap
            }
        }

        // ESC tu�una bas�ld���nda mini oyun UI'sini devre d��� b�rak ve PlayerController'� etkinle�tir
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            miniGameUI.SetActive(false);
            playerController.enabled = true; // PlayerController'� etkinle�tir
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
