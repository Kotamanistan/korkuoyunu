using UnityEngine;
using UnityEngine.UI; // UI elemanlar� i�in gerekli k�t�phane

public class HealthSystem : MonoBehaviour
{
    public int maxHealth = 300;
    public int currentHealth;

    private float healthDecreaseRate = 1f; // Her saniyede azalma miktar�

    public Slider healthSlider; // UI'daki sa�l�k �ubu�u

    private void Start()
    {
        currentHealth = maxHealth; // Ba�lang��ta sa�l�k maksimum de�erde ba�lar
        InvokeRepeating("DecreaseHealth", 1f, 1f); // Her saniyede DecreaseHealth fonksiyonunu �a��r

        // UI'daki sa�l�k �ubu�unu ba�lang��ta g�ncelle
        UpdateHealthUI();
    }

    private void Update()
    {
        // Sa�l�k de�eri ekrana yazd�r
        Debug.Log("Current Health: " + currentHealth);
    }

    private void DecreaseHealth()
    {
        // Her saniye sa�l�k de�erini azalt
        currentHealth -= (int)(healthDecreaseRate);

        // Sa�l�k s�f�r veya daha azsa �l�m i�lemlerini ger�ekle�tir
        if (currentHealth <= 0)
        {
            Die();
        }

        // Sa�l�k �ubu�unu g�ncelle
        UpdateHealthUI();
    }

    private void Die()
    {
        // Karakterin �l�m�yle ilgili i�lemler burada yap�labilir
        Debug.Log("Character has died.");
        // �rne�in, karakteri etkisiz hale getir
        gameObject.SetActive(false);
    }

    private void UpdateHealthUI()
    {
        // Sa�l�k �ubu�unu g�ncelle
        healthSlider.value = (float)currentHealth;
    }
}