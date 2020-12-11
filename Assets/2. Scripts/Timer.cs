using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] GameObject leafBlock;
    [SerializeField] GameObject rootBlock;

    [SerializeField] TMPro.TextMeshProUGUI timeLabel;

    private float maxTime = 300.0f;
    private float currentTime;

    [SerializeField] int numberOfLego = 34;
    float secPerBlock;
    float lastBlockSecond;
    bool bUp = true;

    // Start is called before the first frame update
    void Start() {
        currentTime = maxTime;
        secPerBlock = maxTime / numberOfLego;
        lastBlockSecond = maxTime - secPerBlock;
        StartCoroutine(TickTock());
    }

    IEnumerator TickTock() {
        while (true) {
            yield return null;
            if (currentTime >= 0) {
                currentTime -= Time.deltaTime;

                if(currentTime < lastBlockSecond) {
                    //leafBlock.SetActive(false);
                    if (bUp)
                        leafBlock.GetComponent<Animator>().SetTrigger("Up");
                    else
                        leafBlock.GetComponent<Animator>().SetTrigger("Down");
                    bUp = !bUp;
                    leafBlock = leafBlock.transform.parent.gameObject;
                    lastBlockSecond -= secPerBlock;
                }

                var ts = TimeSpan.FromSeconds(currentTime);
                timeLabel.text = string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);
            } else {
                break;
            }
        }
    }

    public float GetCurrentTime() {
        return currentTime;
    }
}
