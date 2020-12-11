using UnityEngine;

public class NetworkMainMenu : MonoBehaviour
{
    [SerializeField] private NetworkManagerLobby networkManager = null;

    [Header("UI")]
    [SerializeField] private GameObject pagePanel = null;

    public void HostLobby()
    {
        networkManager.StartHost();

        pagePanel.SetActive(false);
    }
}
