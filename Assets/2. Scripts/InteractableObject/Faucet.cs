using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faucet : MonoBehaviour
{
    [SerializeField] AudioPlayer audioPlayer;
    [SerializeField] HumanController humanController;
    [SerializeField] bool isFaucetOn = false;
    [SerializeField] bool isPlayedAlready = false;
    [SerializeField] FaucetType whatAmI;

    enum FaucetType {
        LOO,
        TOILET,
        KITCHEN
    }

    private void Start() {
        InvokeRepeating("CheckByRate", 0.0f, 0.2f);
    }

    void CheckByRate() {
        switch (whatAmI) {
            case FaucetType.LOO:
                Trigger(humanController.isFaucetOn);
                break;
            case FaucetType.TOILET:
                Trigger(humanController.isToiletFaucetOn);
                break;
            case FaucetType.KITCHEN:
                Trigger(humanController.isKitchenFaucetOn);
                break;
        }

        if (isFaucetOn) {
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
        isFaucetOn = state;
    }
}
