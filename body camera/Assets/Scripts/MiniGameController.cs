using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class MiniGameController : MonoBehaviour
{
    public GameObject miniGamePanel; // Mini oyun paneli
    public GameObject winPanel; // Win paneli
    public Button playButton; // Play butonu
    public Image[] directionImages; // Yön görüntüleri (sað, yukarý, sol, aþaðý)
    public Sprite rightSprite; // Sað yön için görsel
    public Sprite upSprite; // Yukarý yön için görsel
    public Sprite leftSprite; // Sol yön için görsel
    public Sprite downSprite; // Aþaðý yön için görsel
    public string[] directions = { "Right", "Up", "Left", "Down" }; // Yönler
    private string[] currentSequence; // Mevcut yön sýralamasý
    private int currentLevel = 0; // Mevcut aþama
    private int currentStep = 0; // Mevcut adým
    private float timeLimit = 4f; // Her aþama için süre sýnýrý
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
    public Text levelText; // Level metin alaný
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

        // Play butonuna týklama olayýný baðla
        playButton.onClick.AddListener(StartMiniGame);

        // Baþlangýçta yön görsellerini gizle
        foreach (var image in directionImages)
        {
            image.gameObject.SetActive(false);
        }

        // Win panelini devre dýþý býrak
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
        Debug.Log("Oyun Baþladý!");
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
        // Level metnini güncelle
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
        Debug.Log("Gösterilen Yönler: " + string.Join(", ", currentSequence));
    }

    void EnablePlayerInput()
    {
        Debug.Log("Oyuncu giriþi etkinleþtirildi.");
    }

    void OnDirectionButtonPressed(string direction)
    {
        if (currentSequence[currentStep] == direction)
        {
            Debug.Log("Doðru tuþa basýldý: " + direction);
            currentStep++;

            if (currentStep == directions.Length) // Tüm yönler doðru bilindiðinde
            {
                Debug.Log("Seviye tamamlandý: " + currentLevel);
                Invoke("StartNextLevel", 0.5f);
            }
        }
        else
        {
            Debug.LogError("Yanlýþ tuþa basýldý: " + direction + ". Doðru tuþ: " + currentSequence[currentStep]);
            ShowWrongStepIndicator(currentStep + 1); // Yanlýþ adýmýn gösterilmesi için fonksiyon çaðrýsý
            RestartGameWithDelay(); // RestartGame fonksiyonunu 3 saniye geciktirerek çaðýr
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
                Debug.LogError("Hatalý adým numarasý: " + step);
                break;
        }
    }

    void RestartGame()
    {
        Debug.Log("Oyun yeniden baþlatýlýyor.");
        currentLevel = 0;
        currentStep = 0;
        miniGamePanel.SetActive(true);
        playButton.gameObject.SetActive(true);
        // Yön görsellerini gizle
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

        Debug.Log("Oyunu Kazandýnýz!");
        foreach (var image in directionImages)
        {
            image.gameObject.SetActive(false);
        }
        


        winPanel.SetActive(true);
        // Özel kazanma ekraný için gerekli iþlemleri burada yapabilirsiniz.
        // Örneðin, winPanel'de bir animasyon oynatmak veya bir GIF göstermek.
        Invoke("QuitGame", 3f);


    }
    void RestartGameWithDelay()
    {
        // RestartGame fonksiyonunu 3 saniye geciktirerek çaðýr
        Invoke("RestartGame", 3f);
    }
    void QuitGame()
    {
        miniGamePanel.SetActive(false);
        // Oyunu kapat
        playerController.enabled = true; // PlayerController'ý etkinleþtir
        Debug.Log("Oyun kapatýlýyor...");
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