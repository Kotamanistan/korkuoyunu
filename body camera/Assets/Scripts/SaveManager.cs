using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{
    public InputField nameInputField;
    public Button createButton;

    private void Start()
    {
        // Butona týklama olayýný dinle
        createButton.onClick.AddListener(CreateSave);
    }

    private void CreateSave()
    {
        // InputField'dan ismi al
        string playerName = nameInputField.text;

        if (string.IsNullOrEmpty(playerName))
        {
            Debug.Log("Ýsim girmediniz!");
            return;
        }

        // Save iþlemini gerçekleþtirin (örneðin, PlayerPrefs kullanarak)
        SaveGame(playerName);

        Debug.Log("Save oluþturuldu: " + playerName);
    }

    private void SaveGame(string playerName)
    {
        // Save verisini oluþtur
        PlayerPrefs.SetString("saveName", playerName);
        PlayerPrefs.Save();

        // Alternatif olarak, dosya sistemi veya diðer save yöntemlerini kullanabilirsiniz
        // Örnek olarak, bir dosya kaydetme:
        // System.IO.File.WriteAllText(Application.persistentDataPath + "/" + playerName + ".txt", "Save Data");
    }
}

