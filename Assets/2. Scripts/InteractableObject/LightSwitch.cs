using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    [SerializeField] AudioPlayer audioPlayer;
    [SerializeField] GameObject LampShadeOn;
    [SerializeField] GameObject LampShadeOff;
    [SerializeField] HumanController humanController;
    [SerializeField] Light pointlight;
    [SerializeField] bool isLightOn = false;
    [SerializeField] bool isPlayedAlready = false;
    [SerializeField] int lampNo = 0;

    private void Start() {
        InvokeRepeating("CheckByRate", 0.0f, 0.2f);
    }

    void CheckByRate() {
        switch(lampNo) {
            case 1:
                Trigger(humanController.isLightsOn);
                break;
            case 2:
                Trigger(humanController.isLights2On);
                break;
            case 3:
                Trigger(humanController.isLights3On);
                break;
        }

        LampShadeOn.SetActive(isLightOn);
        LampShadeOff.SetActive(!isLightOn);
        pointlight.enabled = isLightOn;

        if (isLightOn) {
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
        isLightOn = state;
    }
}
