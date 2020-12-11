using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameDisplay : NetworkBehaviour
{
    [SerializeField] private TMP_Text playerNameText = null;

    [SyncVar(hook = nameof(HandlePlayerNameChanged))]
    public string playerName = "Player Name";

    //Server
    public void SetPlayerName(string playerName)
    {
        this.playerName = playerName;
    }


    //Client
    private void HandlePlayerNameChanged(string oldValue, string newValue)
    {
        playerNameText.text = PlayerNameInput.DisplayName;
    }
}
