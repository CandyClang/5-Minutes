using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TvRemote : MonoBehaviour
{
    [SerializeField] AudioPlayer audioPlayer;
    [SerializeField] GameObject TVScreenWhiteCube;
    [SerializeField] HumanController humanController;
    [SerializeField] bool isTVOn = false;
    [SerializeField] bool isPlayedAlready = false;
    [SerializeField] bool amIPC = false;

    private void Start() {
        InvokeRepeating("CheckByRate", 0.0f, 0.2f);
    }

    void CheckByRate() {
        if (amIPC)
            Trigger(humanController.isPCOn);
        else
            Trigger(humanController.isTVOn);

        TVScreenWhiteCube.SetActive(isTVOn);

        if (isTVOn) {
            if (!isPlayedAlready) {
                audioPlayer.Play();
                isPlayedAlready = true;
            }
        } else {
            audioPlayer.Stop();
            isPlayedAlready = false;
        }
    }

    public void Trigger(bool state) {
        isTVOn = state;
    }
}
