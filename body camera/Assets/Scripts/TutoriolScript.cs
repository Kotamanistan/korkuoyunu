using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class TutoriolScript : MonoBehaviour
{
    public GameObject[] sayfalar; // Sayfalar� tutmak i�in bir dizi
    private int currentPageIndex = 0; // Ge�erli sayfan�n dizideki dizini
    public Button ileriButton; // �leri tu�unu temsil eden buton
    public Button geriButton; // Geri tu�unu temsil eden buton

    void Start()
    {
        // Ba�lang��ta sadece ilk sayfay� aktif et
        for (int i = 0; i < sayfalar.Length; i++)
        {
            if (i == currentPageIndex)
                sayfalar[i].SetActive(true);
            else
                sayfalar[i].SetActive(false);
        }

        // �leri ve geri butonlar�n� dinleyici fonksiyonlara ba�la
        ileriButton.onClick.AddListener(NextPage);
        geriButton.onClick.AddListener(PreviousPage);

        // �lk sayfadaysak geri butonunu devre d��� b�rak
        geriButton.interactable = false;
    }

    // �leri gitme i�levi
    public void NextPage()
    {
        if (currentPageIndex < sayfalar.Length - 1)
        {
            // Mevcut sayfay� pasifle�tir ve bir sonraki sayfay� etkinle�tir
            sayfalar[currentPageIndex].SetActive(false);
            currentPageIndex++;
            sayfalar[currentPageIndex].SetActive(true);

            // E�er son sayfadaysak ileri butonunu devre d��� b�rak
            if (currentPageIndex == sayfalar.Length - 1)
                ileriButton.interactable = false;

            // Geri butonunu etkinle�tir
            geriButton.interactable = true;
        }
    }

    // Geri gitme i�levi
    public void PreviousPage()
    {
        if (currentPageIndex > 0)
        {
            // Mevcut sayfay� pasifle�tir ve bir �nceki sayfay� etkinle�tir
            sayfalar[currentPageIndex].SetActive(false);
            currentPageIndex--;
            sayfalar[currentPageIndex].SetActive(true);

            // E�er ilk sayfadaysak geri butonunu devre d��� b�rak
            if (currentPageIndex == 0)
                geriButton.interactable = false;

            // �leri butonunu etkinle�tir
            ileriButton.interactable = true;
        }
    }
}
