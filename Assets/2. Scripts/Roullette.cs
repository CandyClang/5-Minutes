using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roullette : MonoBehaviour {
    private Rigidbody2D rb;

    [SerializeField] private float randBetweenMin = 400;
    [SerializeField] private float randBetweenMax = 700;

    [Range(4, 8)]
    [SerializeField] private int sectorDivisionCount = 5;
    [Range(1, 5)]
    [SerializeField] private int maxRollAtATime = 2;

    private float sectorSize;

    private bool wasSpinned;
    private int counter = 0;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        sectorSize = 360.0f / sectorDivisionCount;
    }

    public void OnClick() {
        //To check if the roullette was ever triggered
        wasSpinned = true;

        //if player used all the given chance to spin
        if (wasSpinned && counter >= maxRollAtATime) print("No more chance left to roll");
        //if the chance is still left
        else {
            rb.angularVelocity += Random.Range(randBetweenMin, randBetweenMax);
            counter++;
        }
    }

    public void Reset() {
        //reset roullette status to initial status
        wasSpinned = false;
        counter = 0;
        rb.angularVelocity = 0;
        //transform.rotation = Quaternion.identity;
    }

    public void Update() {
        //prevent players to roll when the result is obvious
        if (wasSpinned && rb.angularVelocity < 100.0f) counter += 100;

        //When roullette is stopped
        if (wasSpinned && rb.angularVelocity < 0.01f) {
            //CeilToInt was used to make the result number be a humanly number (eg. not 0~n scale but 1~n scale)
            GetResult(Mathf.CeilToInt(transform.localRotation.eulerAngles.z / sectorSize));
        }
    }

    void GetResult(int division) {
        //Change this when you are implementing this script
        print("result was in " + division + " sector");
    }
}