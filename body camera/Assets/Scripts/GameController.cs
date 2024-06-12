using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
    public GameObject[] arrowImages;
    public GameObject winScreen;
    public Text timerText;

    private int currentLevel = 0;
    private float timer = 4f;

    private void Start()
    {
        winScreen.SetActive(false);
        StartLevel();
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        timerText.text = timer.ToString("F1");

        if (timer <= 0f)
        {
            RestartLevel();
        }

        // Klavye ok tuþlarýna basýldýðýnda doðru oku kontrol et
        if (Input.GetKeyDown(GetArrowKey(currentLevel)))
        {
            if (currentLevel < arrowImages.Length - 1)
            {
                currentLevel++;
                StartLevel();
            }
            else
            {
                WinGame();
            }
        }

        // Oyunu Esc tuþuna basarak kapatabilme kontrolü
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    // Baþlangýçta ve her seviyenin baþýnda çaðrýlýr
    private void StartLevel()
    {
        timer = 4f;
        ShowArrowImages();
    }

    // Yanlýþ yapýldýðýnda seviyeyi yeniden baþlatýr
    private void RestartLevel()
    {
        currentLevel = 0;
        StartLevel();
    }

    // Oyunu kazandýðýnda çaðrýlýr
    private void WinGame()
    {
        winScreen.SetActive(true);
    }

    // Doðru ok iþaretlerini gösterir
    private void ShowArrowImages()
    {
        for (int i = 0; i < arrowImages.Length; i++)
        {
            arrowImages[i].SetActive(i == currentLevel);
        }
    }

    // Seviye boyunca hangi ok tuþuna basýlacaðýný belirler
    private KeyCode GetArrowKey(int level)
    {
        switch (level)
        {
            case 0:
                return KeyCode.LeftArrow;
            case 1:
                return KeyCode.RightArrow;
            case 2:
                return KeyCode.UpArrow;
            case 3:
                return KeyCode.DownArrow;
            default:
                return KeyCode.Space; // Varsayýlan olarak boþluk tuþu
        }
    }
}
