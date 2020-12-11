using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStateManager : MonoBehaviour {

    // [SerializeField] MapBlock[] MapTiles;
    //Gamemode
    public GameState CurrGameState = GameState.Normal;
    private List<GameState> minigamesLeft = new List<GameState>();
    private float minigameInterval;

    //Drop Tile
    [SerializeField] private MapGenerator mapGenerator;
    public List<GameObject> mapBlocks = new List<GameObject>();
    private int maxMapSize;
    private int randomTileArrayIndex;
    private bool dropActive = true;
    float dropLockout = 0f;

    //Dodge
    [SerializeField] private GameObject DodgeModeManager;

    //GUI
    public GameObject EndScreen;
    public Image timerBarImage;
    public Text timeLabel;
    public Text gameStateLabel;
    public Text objectiveLabel;
    public Text Player1ScoreText;
    public Text Player2ScoreText;
    public Text Player3ScoreText;
    public Text Player4ScoreText;

    //Timer & Score
    public float score;
    private float maxTime = 300;
    private float currentTime;
    private float scoreTimer;

  
    public enum GameState {
        Normal,                                                             //normal gamemode
        Tiles,                                                              //falling tiles minigame
        King,                                                               //king of the hill minigame
        Dodge,                                                              //dodgeball minigame
        Crown,                                                              //keep the crown minigame
        Treasure                                                            //treasure trove minigame
    };

   // [Server]
    // Start is called before the first frame update
    void Start()
    {
        EndScreen.SetActive(false);

        //Timer
        currentTime = maxTime;
        minigameInterval = maxTime / 6;
        StartCoroutine(Timer());
        InvokeRepeating("ChangeMinigame", minigameInterval, minigameInterval);

        //Adds minigames to the list
        minigamesLeft.Add(GameState.Tiles);
        minigamesLeft.Add(GameState.King);
        minigamesLeft.Add(GameState.Dodge);
        minigamesLeft.Add(GameState.Crown);
        minigamesLeft.Add(GameState.Treasure);

        //Places map blocks into the array
        randomTileArrayIndex = Random.Range(0, mapBlocks.Count);
        mapBlocks = mapGenerator.MapBlockArray;
        maxMapSize = mapBlocks.Count;
    }

   // [Server]
    // Update is called once per frame
    void Update() {

        if (currentTime < minigameInterval)
            CancelInvoke("ChangeMinigame");

        switch (CurrGameState) {
            case GameState.Normal:
                objectiveLabel.text = "Score as many points as you can within the time limit and avoid traps/falling!";
                dropActive = false;
                break;

            case GameState.Tiles:
                DropTileMode();
                break;

            case GameState.King:
                objectiveLabel.text = "WIP";
                dropActive = false;
                break;

            case GameState.Dodge:
                objectiveLabel.text = "Dodge incoming projectiles!";
                DodgeBallMode();
                break;

            case GameState.Crown:
                objectiveLabel.text = "WIP";
                dropActive = false;
                break;

            case GameState.Treasure:
                objectiveLabel.text = "WIP";
                dropActive = false;
                break;
        }
    }

   // [Server]
    //Timer
    IEnumerator Timer() {
        while (true) {
            yield return null;
            if (currentTime >= 0) {
                currentTime -= Time.deltaTime;

                timerBarImage.fillAmount = currentTime / maxTime;
                timeLabel.text = "Time until next minigame: " + GetMinigameTimeLeft().ToString("F");
                gameStateLabel.text = "Current Minigame: " + CurrGameState.ToString();
                scoreTimer += Time.deltaTime;

                if (scoreTimer > .1f) {
                    score += 1;
                    Player1ScoreText.text = "Player 1 Score: " + score.ToString();
                    scoreTimer = 0;
                }
            }
            else
            {
                break;
            }
        }
        EndGame();
    }

  //  [Server]
    //end the game and display end screen
    private void EndGame() {
        Time.timeScale = 0;
        EndScreen.SetActive(true);

        GameObject.Find("FinalScoreText").GetComponent<Text>().text = "Final Score: " + score;
    }

   // [Server]
    //resets map array
    private void ResetMap() {
        mapGenerator.destroyMap();
        mapGenerator.SpawnMapBlocks();
        mapBlocks = mapGenerator.MapBlockArray;
    }

  //  [Server]
    //changes gamestate and removes it from the minigamesleft list
    private void ChangeMinigame() {
        int randomIndex = Random.Range(0, minigamesLeft.Count - 1);
        GameState nextGameState = minigamesLeft[randomIndex];
        minigamesLeft.RemoveAt(randomIndex);
        CurrGameState = nextGameState;

        dropActive = true;
    }

  //  [Server]
    //returns the amount of time left in the minigame
    private float GetMinigameTimeLeft() {
        float currMinigame = minigamesLeft.Count * minigameInterval;

        if (currMinigame - currentTime < minigameInterval)
            return currentTime - currMinigame;

        return 0;
    }

  //  [Server]
    private void DropTile() {
        if (dropActive)
            mapBlocks[randomTileArrayIndex].gameObject.transform.Translate(Vector3.down * Time.deltaTime * 10);
    }

//[Server]
    void DropTileMode() {
        objectiveLabel.text = "Avoid falling tiles and be the last one standing to win!";

        //if remaining players > 1 || remaining time > 0
        //if all players die then return to gamestate normal

        //reset map when minigame time is finished
        if (GetMinigameTimeLeft() < .2f)
        {
            dropActive = false;
            ResetMap();
            CurrGameState = GameState.Normal;
        }

        //drops tiles with a 1.5 second lockout in between
        if (mapBlocks.Count > 1)
        {

            if (dropActive)
                mapBlocks[randomTileArrayIndex].gameObject.transform.Translate(Vector3.down * Time.deltaTime * 10);

            if (mapBlocks[randomTileArrayIndex].transform.position.y < -5)
            {
                mapBlocks[randomTileArrayIndex].gameObject.SetActive(false);
                mapBlocks.RemoveAt(randomTileArrayIndex);
                randomTileArrayIndex = Random.Range(0, mapBlocks.Count);
                dropActive = false;
            }

            if (dropActive == false)
            {
                dropLockout += Time.deltaTime;
                if (dropLockout > 1.5f)
                {
                    dropActive = true;
                    dropLockout = 0f;
                }
            }
        }
        else //if all tiles drop, reset the map
        {
            ResetMap();
            CurrGameState = GameState.Normal;
        }  
    }

    //[Server]
    void DodgeBallMode() {
        dropActive = false;
        DodgeModeManager.SetActive(true);

        if (GetMinigameTimeLeft() < 2) {
            CurrGameState = GameState.Normal;
            Destroy(DodgeModeManager);
        }
    }
}
