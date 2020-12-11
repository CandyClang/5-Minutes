using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerNameInput : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_InputField nameInput = null;
    [SerializeField] private Button continueButton = null;

    public static string DisplayName { get; private set; }

    private const string PlayerPrefsNameKey = "PlayerName";

    private void Start()
    {
        SetUpInputField();
    }

    private void SetUpInputField()
    {
        if (PlayerPrefs.HasKey(PlayerPrefsNameKey))
        {
            string defaultName = PlayerPrefs.GetString(PlayerPrefsNameKey);
            nameInput.text = defaultName;
            SetPlayerName(defaultName);
        }
    }

    public void SetPlayerName(string name)
    {
        name = nameInput.text;
        continueButton.interactable = !string.IsNullOrEmpty(name); 
    }

    public void SavePlayerName()
    {
        DisplayName = nameInput.text;
        PlayerPrefs.SetString(PlayerPrefsNameKey, DisplayName);
    }
}
