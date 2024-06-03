using UnityEngine;
using UnityEngine.UI; // UI elemanlarý için gerekli kütüphane

public class HealthSystem : MonoBehaviour
{
    public int maxHealth = 300;
    public int currentHealth;

    private float healthDecreaseRate = 1f; // Her saniyede azalma miktarý

    public Slider healthSlider; // UI'daki saðlýk çubuðu

    private void Start()
    {
        currentHealth = maxHealth; // Baþlangýçta saðlýk maksimum deðerde baþlar
        InvokeRepeating("DecreaseHealth", 1f, 1f); // Her saniyede DecreaseHealth fonksiyonunu çaðýr

        // UI'daki saðlýk çubuðunu baþlangýçta güncelle
        UpdateHealthUI();
    }

    private void Update()
    {
        // Saðlýk deðeri ekrana yazdýr
        Debug.Log("Current Health: " + currentHealth);
    }

    private void DecreaseHealth()
    {
        // Her saniye saðlýk deðerini azalt
        currentHealth -= (int)(healthDecreaseRate);

        // Saðlýk sýfýr veya daha azsa ölüm iþlemlerini gerçekleþtir
        if (currentHealth <= 0)
        {
            Die();
        }

        // Saðlýk çubuðunu güncelle
        UpdateHealthUI();
    }

    private void Die()
    {
        // Karakterin ölümüyle ilgili iþlemler burada yapýlabilir
        Debug.Log("Character has died.");
        // Örneðin, karakteri etkisiz hale getir
        gameObject.SetActive(false);
    }

    private void UpdateHealthUI()
    {
        // Saðlýk çubuðunu güncelle
        healthSlider.value = (float)currentHealth;
    }
}