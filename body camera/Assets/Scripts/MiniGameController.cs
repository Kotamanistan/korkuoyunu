using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class MiniGameController : MonoBehaviour
{
    public GameObject miniGamePanel; // Mini oyun paneli
    public GameObject winPanel; // Win paneli
    public Button playButton; // Play butonu
    public Image[] directionImages; // Y�n g�r�nt�leri (sa�, yukar�, sol, a�a��)
    public Sprite rightSprite; // Sa� y�n i�in g�rsel
    public Sprite upSprite; // Yukar� y�n i�in g�rsel
    public Sprite leftSprite; // Sol y�n i�in g�rsel
    public Sprite downSprite; // A�a�� y�n i�in g�rsel
    public string[] directions = { "Right", "Up", "Left", "Down" }; // Y�nler
    private string[] currentSequence; // Mevcut y�n s�ralamas�
    private int currentLevel = 0; // Mevcut a�ama
    private int currentStep = 0; // Mevcut ad�m
    private float timeLimit = 4f; // Her a�ama i�in s�re s�n�r�
    public GameObject A1;
    public GameObject A2;
    public GameObject A3;
    public GameObject A4;
    public GameObject D1;
    public GameObject D2;
    public GameObject D3;
    public GameObject D4;
    public GameObject Y1;
    public GameObject Y2;
    public GameObject Y3;
    public GameObject Y4;
    public MonoBehaviour playerController; // PlayerController script'i
    public Text levelText; // Level metin alan�
    public Text arcadetamamlamaText;
    void Start()
    {
        A1.SetActive(false);
        A2.SetActive(false);
        A3.SetActive(false);
        A4.SetActive(false);
        D1.SetActive(false);
        D2.SetActive(false);
        D3.SetActive(false);
        D4.SetActive(false);
        Y1.SetActive(false);
        Y2.SetActive(false);
        Y3.SetActive(false);
        Y4.SetActive(false);
        UpdateLevelText();

        // Play butonuna t�klama olay�n� ba�la
        playButton.onClick.AddListener(StartMiniGame);

        // Ba�lang��ta y�n g�rsellerini gizle
        foreach (var image in directionImages)
        {
            image.gameObject.SetActive(false);
        }

        // Win panelini devre d��� b�rak
        winPanel.SetActive(false);
        
    }

    void Update()
    {
        UpdateLevelText();
        if (currentLevel == 1)
        {
            A1.SetActive(true);
            D1.SetActive(false);
            A2.SetActive(true);
            D2.SetActive(false);
            A3.SetActive(true);
            D3.SetActive(false);
            A4.SetActive(true);
            D4.SetActive(false);

        }
        if (currentLevel == 2)
        {
            A1.SetActive(true);
            D1.SetActive(false);
            A2.SetActive(true);
            D2.SetActive(false);
            A3.SetActive(true);
            D3.SetActive(false);
            A4.SetActive(true);
            D4.SetActive(false);
        }
        if (currentLevel == 3)
        {

            A1.SetActive(true);
            D1.SetActive(false);
            A2.SetActive(true);
            D2.SetActive(false);
            A3.SetActive(true);
            D3.SetActive(false);
            A4.SetActive(true);
            D4.SetActive(false);

        }
        if (currentLevel == 4)
        {
            A1.SetActive(true);
            D1.SetActive(false);
            A2.SetActive(true);
            D2.SetActive(false);
            A3.SetActive(true);
            D3.SetActive(false);
            A4.SetActive(true);
            D4.SetActive(false);
        }
        
        if (currentStep == 1)
        {
            D1.SetActive(true);
            A1.SetActive(false);
        }
        else if (currentStep == 2)
        {
            D1.SetActive(true);
            D2.SetActive(true);
            A2.SetActive(false);
        }
        else if (currentStep == 3)
        {
            D1.SetActive(true);
            D2.SetActive(true);
            D3.SetActive(true);
            A3.SetActive(false);
        }
        else if (currentStep == 4)
        {
            D1.SetActive(true);
            D2.SetActive(true);
            D3.SetActive(true);
            D4.SetActive(true);
            A4.SetActive(false);
        }
        if (miniGamePanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow)) OnDirectionButtonPressed("Right");
            if (Input.GetKeyDown(KeyCode.UpArrow)) OnDirectionButtonPressed("Up");
            if (Input.GetKeyDown(KeyCode.LeftArrow)) OnDirectionButtonPressed("Left");
            if (Input.GetKeyDown(KeyCode.DownArrow)) OnDirectionButtonPressed("Down");
        }
    }

    public void StartMiniGame()
    {
        A1.SetActive(true);
        A2.SetActive(true);
        A3.SetActive(true);
        A4.SetActive(true);
        Debug.Log("Oyun Ba�lad�!");
        currentLevel = 0;
        currentStep = 0;
        miniGamePanel.SetActive(true);
        winPanel.SetActive(false);
        playButton.gameObject.SetActive(false);
        StartNextLevel();
    }

    void StartNextLevel()
    {
        if (currentLevel >= 4)
        {
            
            WinGame();
            kapanis();
            return;
        }

        currentLevel++;
        currentStep = 0;
        ShuffleDirections();
        ShowDirections();
        Invoke("EnablePlayerInput", timeLimit);
    }
    void UpdateLevelText()
    {
        // Level metnini g�ncelle
        levelText.text = "Level: " + currentLevel.ToString() + "/4";
    }
    void ShuffleDirections()
    {
        currentSequence = directions.OrderBy(x => Random.value).ToArray();
    }

    void ShowDirections()
    {
        for (int i = 0; i < directionImages.Length; i++)
        {
            directionImages[i].gameObject.SetActive(true);
            switch (currentSequence[i])
            {
                case "Right":
                    directionImages[i].sprite = rightSprite;
                    break;
                case "Up":
                    directionImages[i].sprite = upSprite;
                    break;
                case "Left":
                    directionImages[i].sprite = leftSprite;
                    break;
                case "Down":
                    directionImages[i].sprite = downSprite;
                    break;
            }
        }

        // Debug log for current sequence
        Debug.Log("G�sterilen Y�nler: " + string.Join(", ", currentSequence));
    }

    void EnablePlayerInput()
    {
        Debug.Log("Oyuncu giri�i etkinle�tirildi.");
    }

    void OnDirectionButtonPressed(string direction)
    {
        if (currentSequence[currentStep] == direction)
        {
            Debug.Log("Do�ru tu�a bas�ld�: " + direction);
            currentStep++;

            if (currentStep == directions.Length) // T�m y�nler do�ru bilindi�inde
            {
                Debug.Log("Seviye tamamland�: " + currentLevel);
                Invoke("StartNextLevel", 0.5f);
            }
        }
        else
        {
            Debug.LogError("Yanl�� tu�a bas�ld�: " + direction + ". Do�ru tu�: " + currentSequence[currentStep]);
            ShowWrongStepIndicator(currentStep + 1); // Yanl�� ad�m�n g�sterilmesi i�in fonksiyon �a�r�s�
            RestartGameWithDelay(); // RestartGame fonksiyonunu 3 saniye geciktirerek �a��r
        }
    }

    void ShowWrongStepIndicator(int step)
    {
        switch (step)
        {
            case 1:
                Y1.SetActive(true);
                break;
            case 2:
                Y2.SetActive(true);
                break;
            case 3:
                Y3.SetActive(true);
                break;
            case 4:
                Y4.SetActive(true);
                break;
            default:
                Debug.LogError("Hatal� ad�m numaras�: " + step);
                break;
        }
    }

    void RestartGame()
    {
        Debug.Log("Oyun yeniden ba�lat�l�yor.");
        currentLevel = 0;
        currentStep = 0;
        miniGamePanel.SetActive(true);
        playButton.gameObject.SetActive(true);
        // Y�n g�rsellerini gizle
        foreach (var image in directionImages)
        {
            image.gameObject.SetActive(false);
        }
        A1.SetActive(false);
        A2.SetActive(false);
        A3.SetActive(false);
        A4.SetActive(false);
        Y1.SetActive(false);
        Y2.SetActive(false);
        Y3.SetActive(false);
        Y4.SetActive(false);
        D1.SetActive(false);
        D2.SetActive(false);
        D3.SetActive(false);
        D4.SetActive(false);

    }
    void WinGame()
    {

        Debug.Log("Oyunu Kazand�n�z!");
        foreach (var image in directionImages)
        {
            image.gameObject.SetActive(false);
        }
        


        winPanel.SetActive(true);
        // �zel kazanma ekran� i�in gerekli i�lemleri burada yapabilirsiniz.
        // �rne�in, winPanel'de bir animasyon oynatmak veya bir GIF g�stermek.
        Invoke("QuitGame", 3f);


    }
    void RestartGameWithDelay()
    {
        // RestartGame fonksiyonunu 3 saniye geciktirerek �a��r
        Invoke("RestartGame", 3f);
    }
    void QuitGame()
    {
        miniGamePanel.SetActive(false);
        // Oyunu kapat
        playerController.enabled = true; // PlayerController'� etkinle�tir
        Debug.Log("Oyun kapat�l�yor...");
        Application.Quit();
    }
    void kapanis()
    {
        D1.SetActive(false);
        D2.SetActive(false);
        D3.SetActive(false);
        D4.SetActive(false);
    }

}