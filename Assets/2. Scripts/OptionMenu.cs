using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour {
    [SerializeField] SoundManager soundManager;

    public void OnValueChangeMaster(Slider slider) {
        PlayerPrefs.SetFloat("MasterVolume", slider.value);
        soundManager.OnValueChange();
    }

    public void OnValueChangeSFX(Slider slider) {
        PlayerPrefs.SetFloat("SFXVolume", slider.value);
        soundManager.OnValueChange();
    }

    public void OnValueChangeMusic(Slider slider) {
        PlayerPrefs.SetFloat("MusicVolume", slider.value);
        soundManager.OnValueChange();
    }
}