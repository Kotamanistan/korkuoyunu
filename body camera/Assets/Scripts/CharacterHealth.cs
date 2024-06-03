using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI bile�enlerine eri�mek i�in gerekli

public class CharacterHealth : MonoBehaviour
{
    public int maxCan = 100; // Maksimum can de�eri
    public int canArtisMiktari = 5; // Saniyede artacak can miktar�
    public float zamanAraligi = 1f; // Can�n artaca�� zaman aral��� (saniye)
    public float hasarAlmaSuresi = 45f; // Hasar almama s�resi (saniye)

    private float gecenSure = 0f; // Hasar al�nmayan s�reyi hesaplamak i�in kullan�lacak saya�
    private int currentCan; // Mevcut can miktar�

    public Slider healthSlider; // UI Slider referans�

    void Start()
    {
        currentCan = maxCan; // Ba�lang��ta can� maksimuma ayarla
        healthSlider.maxValue = maxCan; // Slider'�n maksimum de�erini ayarla
        healthSlider.value = currentCan; // Slider'�n ba�lang�� de�erini ayarla
    }

    void Update()
    {
        // E�er karakter hasar almad�ysa, ge�en s�reyi artt�r
        if (currentCan == maxCan)
        {
            gecenSure += Time.deltaTime;
            // Ge�en s�re, hasar alma s�resini a�t�ysa can� artt�r
            if (gecenSure >= hasarAlmaSuresi)
            {
                currentCan = Mathf.Min(maxCan, currentCan + (int)(canArtisMiktari * Time.deltaTime));
                healthSlider.value = currentCan; // Slider'�n de�erini g�ncelle
            }
        }
        else
        {
            gecenSure = 0f; // Hasar al�nd�ysa ge�en s�reyi s�f�rla
        }
    }

    // Hasar al�nd���nda bu fonksiyonu �a��rarak can� azalt
    public void HasarAl(int hasarMiktari)
    {
        currentCan = Mathf.Max(0, currentCan - hasarMiktari);
        gecenSure = 0f; // Hasar al�nd���nda ge�en s�reyi s�f�rla
        healthSlider.value = currentCan; // Slider'�n de�erini g�ncelle
    }
}