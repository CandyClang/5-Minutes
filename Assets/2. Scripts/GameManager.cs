using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject LegoTimerBar;
    private Timer timer;
    private float currentTime;

    public GameObject gameOverScreen;
    public GameObject gameWonScreen;

    [SerializeField] private HumanController human;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        timer = LegoTimerBar.GetComponent<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        HumanIncreaseSpeed();
        currentTime = timer.GetCurrentTime();

        if (currentTime <= 0 && !human.hasToyFriend) //WIN CONDITION
        {
            SceneManager.LoadScene("Win Cutscene");
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
        else if (currentTime <= 0 && human.hasToyFriend) //LOSE CONDITION
        {
            SceneManager.LoadScene("Lose Cutscene");

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    private void HumanIncreaseSpeed()
    {
        if (currentTime < 180) //Speed up 1.5x after 2 mins
            human.humanAgent.speed = 7.5f;
        else if (currentTime < 60) //Speed up 2x after 5 mins
            human.humanAgent.speed = 10f;
    }
}
