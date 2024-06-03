using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI bileþenlerine eriþmek için gerekli

public class CharacterHealth : MonoBehaviour
{
    public int maxCan = 100; // Maksimum can deðeri
    public int canArtisMiktari = 5; // Saniyede artacak can miktarý
    public float zamanAraligi = 1f; // Canýn artacaðý zaman aralýðý (saniye)
    public float hasarAlmaSuresi = 45f; // Hasar almama süresi (saniye)

    private float gecenSure = 0f; // Hasar alýnmayan süreyi hesaplamak için kullanýlacak sayaç
    private int currentCan; // Mevcut can miktarý

    public Slider healthSlider; // UI Slider referansý

    void Start()
    {
        currentCan = maxCan; // Baþlangýçta caný maksimuma ayarla
        healthSlider.maxValue = maxCan; // Slider'ýn maksimum deðerini ayarla
        healthSlider.value = currentCan; // Slider'ýn baþlangýç deðerini ayarla
    }

    void Update()
    {
        // Eðer karakter hasar almadýysa, geçen süreyi arttýr
        if (currentCan == maxCan)
        {
            gecenSure += Time.deltaTime;
            // Geçen süre, hasar alma süresini aþtýysa caný arttýr
            if (gecenSure >= hasarAlmaSuresi)
            {
                currentCan = Mathf.Min(maxCan, currentCan + (int)(canArtisMiktari * Time.deltaTime));
                healthSlider.value = currentCan; // Slider'ýn deðerini güncelle
            }
        }
        else
        {
            gecenSure = 0f; // Hasar alýndýysa geçen süreyi sýfýrla
        }
    }

    // Hasar alýndýðýnda bu fonksiyonu çaðýrarak caný azalt
    public void HasarAl(int hasarMiktari)
    {
        currentCan = Mathf.Max(0, currentCan - hasarMiktari);
        gecenSure = 0f; // Hasar alýndýðýnda geçen süreyi sýfýrla
        healthSlider.value = currentCan; // Slider'ýn deðerini güncelle
    }
}