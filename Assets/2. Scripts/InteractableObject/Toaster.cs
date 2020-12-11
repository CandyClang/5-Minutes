using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toaster : MonoBehaviour
{
    [SerializeField] ParticleSystem Smoke;
    [SerializeField] HumanController humanController;
    [SerializeField] AudioPlayer audioPlayer;
    [SerializeField] bool isToasterOn = false;
    [SerializeField] bool isPlayedAlready = false;

    private void Start() {
        InvokeRepeating("CheckByRate", 0.0f, 0.2f);
    }

    void CheckByRate() {
        Trigger(humanController.isToasterOn);

        if (isToasterOn) {
            if (!isPlayedAlready) {
                Smoke.Play();
                audioPlayer.Play();
                isPlayedAlready = true;
            }
        } else {
            Smoke.Stop();
            audioPlayer.Stop();
            isPlayedAlready = false;
        }
    }

    public void Trigger(bool state) {
        isToasterOn = state;
    }
}
