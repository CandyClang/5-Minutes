using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroUI : MonoBehaviour {
    public GameObject introUI;
    public GameObject secondIntroUI;
    public static bool isIntro = true;
    bool bPaged;

    void Start() {
        bPaged = false;
        Intro();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Return)) {
            if (!bPaged) {
                bPaged = true;
                SecondIntro();
            } else {
                Skipped();
            }
        }
    }

    void Skipped() {
        introUI.SetActive(false);
        secondIntroUI.SetActive(false);
        Time.timeScale = 1f;
        isIntro = false;
    }

    void Intro() {
        introUI.SetActive(true);
        secondIntroUI.SetActive(false);
        Time.timeScale = 0.0f;
        isIntro = true;
    }

    void SecondIntro() {
        introUI.SetActive(false);
        secondIntroUI.SetActive(true);
        Time.timeScale = 0.0f;
        isIntro = true;
    }
}