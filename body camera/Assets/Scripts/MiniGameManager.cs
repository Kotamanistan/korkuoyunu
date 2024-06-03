using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MiniGameManager : MonoBehaviour
{
    public GameObject playButton;
    public Image[] arrowImages; // 0: Sað, 1: Yukarý, 2: Aþaðý, 3: Sol
    public Sprite[] arrowSprites; // Sprite'ler sað, yukarý, aþaðý, sol ok için
    public GameObject winScreen;
    public GameObject miniGameUI;
    public GameObject player;

    private int currentStage = 0;
    private List<int> currentSequence;
    private int inputIndex = 0;
    private float timer = 0f;
    private float timeLimit = 4f;
    private bool gameStarted = false;
    private bool sequenceActive = false;

    private void Start()
    {
        miniGameUI.SetActive(false);
        winScreen.SetActive(false);
        playButton.SetActive(true);
        foreach (var image in arrowImages)
        {
            image.gameObject.SetActive(false);
        }
        playButton.GetComponent<Button>().onClick.AddListener(StartGame);
        Cursor.lockState = CursorLockMode.None; // Mouse imlecini serbest býrak
        Cursor.visible = true; // Mouse imlecini görünür yap
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) // Enter tuþuna basýldýðýnda StartGame fonksiyonunu çaðýr
        {
            StartGame();
        }

        if (gameStarted)
        {
            if (sequenceActive)
            {
                timer += Time.deltaTime;
                if (timer > timeLimit)
                {
                    RestartGame();
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ExitGame();
            }

            if (sequenceActive)
            {
                if (Input.GetKeyDown(KeyCode.RightArrow)) CheckInput(0);
                else if (Input.GetKeyDown(KeyCode.UpArrow)) CheckInput(1);
                else if (Input.GetKeyDown(KeyCode.DownArrow)) CheckInput(2);
                else if (Input.GetKeyDown(KeyCode.LeftArrow)) CheckInput(3);
            }
        }
    }

    private void StartGame()
    {
        playButton.SetActive(false);
        foreach (var image in arrowImages)
        {
            image.gameObject.SetActive(true);
        }
        winScreen.SetActive(false);
        gameStarted = true;
        Cursor.lockState = CursorLockMode.None; // Mouse imlecini serbest býrak
        Cursor.visible = true; // Mouse imlecini görünür yap
        NextStage();
    }

    private void NextStage()
    {
        if (currentStage == 4)
        {
            WinGame();
            return;
        }

        currentStage++;
        inputIndex = 0;
        timer = 0f;
        sequenceActive = true;

        SetButtonSequence();
        DisplaySequence();
    }

    private void SetButtonSequence()
    {
        // Rastgele bir sýra oluþtur
        currentSequence = new List<int>();
        for (int i = 0; i < 4; i++)
        {
            currentSequence.Add(Random.Range(0, arrowImages.Length));
        }
    }

    private void DisplaySequence()
    {
        // Oyuncuya sýrayý göster
        for (int i = 0; i < arrowImages.Length; i++)
        {
            arrowImages[i].sprite = arrowSprites[currentSequence[i]];
        }
    }

    private void CheckInput(int arrowIndex)
    {
        if (currentSequence[inputIndex] == arrowIndex)
        {
            inputIndex++;
            if (inputIndex == currentSequence.Count)
            {
                sequenceActive = false;
                NextStage();
            }
        }
        else
        {
            RestartGame();
        }
    }

    private void RestartGame()
    {
        currentStage = 0;
        gameStarted = false;
        sequenceActive = false;
        playButton.SetActive(true);
        Cursor.lockState = CursorLockMode.None; // Mouse imlecini serbest býrak
        Cursor.visible = true; // Mouse imlecini görünür yap
        foreach (var image in arrowImages)
        {
            image.gameObject.SetActive(false);
        }
    }

    private void WinGame()
    {
        winScreen.SetActive(true);
        foreach (var image in arrowImages)
        {
            image.gameObject.SetActive(false);
        }
        gameStarted = false;
        sequenceActive = false;
        Cursor.lockState = CursorLockMode.None; // Mouse imlecini serbest býrak
        Cursor.visible = true; // Mouse imlecini görünür yap
    }

    private void ExitGame()
    {
        miniGameUI.SetActive(false);
        player.GetComponent<PlayerController>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked; // Mouse imlecini kilitle
        Cursor.visible = false; // Mouse imlecini görünmez yap
    }
}
