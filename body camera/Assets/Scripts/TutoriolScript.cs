using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class TutoriolScript : MonoBehaviour
{
    public GameObject[] sayfalar; // Sayfalarý tutmak için bir dizi
    private int currentPageIndex = 0; // Geçerli sayfanýn dizideki dizini
    public Button ileriButton; // Ýleri tuþunu temsil eden buton
    public Button geriButton; // Geri tuþunu temsil eden buton

    void Start()
    {
        // Baþlangýçta sadece ilk sayfayý aktif et
        for (int i = 0; i < sayfalar.Length; i++)
        {
            if (i == currentPageIndex)
                sayfalar[i].SetActive(true);
            else
                sayfalar[i].SetActive(false);
        }

        // Ýleri ve geri butonlarýný dinleyici fonksiyonlara baðla
        ileriButton.onClick.AddListener(NextPage);
        geriButton.onClick.AddListener(PreviousPage);

        // Ýlk sayfadaysak geri butonunu devre dýþý býrak
        geriButton.interactable = false;
    }

    // Ýleri gitme iþlevi
    public void NextPage()
    {
        if (currentPageIndex < sayfalar.Length - 1)
        {
            // Mevcut sayfayý pasifleþtir ve bir sonraki sayfayý etkinleþtir
            sayfalar[currentPageIndex].SetActive(false);
            currentPageIndex++;
            sayfalar[currentPageIndex].SetActive(true);

            // Eðer son sayfadaysak ileri butonunu devre dýþý býrak
            if (currentPageIndex == sayfalar.Length - 1)
                ileriButton.interactable = false;

            // Geri butonunu etkinleþtir
            geriButton.interactable = true;
        }
    }

    // Geri gitme iþlevi
    public void PreviousPage()
    {
        if (currentPageIndex > 0)
        {
            // Mevcut sayfayý pasifleþtir ve bir önceki sayfayý etkinleþtir
            sayfalar[currentPageIndex].SetActive(false);
            currentPageIndex--;
            sayfalar[currentPageIndex].SetActive(true);

            // Eðer ilk sayfadaysak geri butonunu devre dýþý býrak
            if (currentPageIndex == 0)
                geriButton.interactable = false;

            // Ýleri butonunu etkinleþtir
            ileriButton.interactable = true;
        }
    }
}
