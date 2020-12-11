using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource[] SFX;
    [SerializeField] AudioSource[] Music;

    List<float> originalValuesSFX;
    List<float> originalValuesMusic;

    private void Start() {
        originalValuesSFX = new List<float>();
        originalValuesMusic = new List<float>();

        foreach (AudioSource audio in SFX) {
            originalValuesSFX.Add(audio.volume);
        }
        foreach (AudioSource audio in Music) {
            originalValuesMusic.Add(audio.volume);
        }
    }

    // Start is called before the first frame update
    public void OnValueChange() {
        ApplySFX();
        ApplyMusic();
    }

    void ApplySFX() {
        for(int x = 0; x < SFX.Length; ++x) {
            SFX[x].volume = PlayerPrefs.GetFloat("MasterVolume") * PlayerPrefs.GetFloat("SFXVolume") * originalValuesSFX[x];
        }
    }

    void ApplyMusic() {
        for (int x = 0; x < Music.Length; ++x) {
            Music[x].volume = PlayerPrefs.GetFloat("MasterVolume") * PlayerPrefs.GetFloat("MusicVolume") * originalValuesMusic[x];
        }
    }
}
