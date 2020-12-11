using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//speeding powerup (or down)

public class PowerUp_Speed : MonoBehaviour {
    protected PlayerController pc;                  //apply target
    public float timer;                             //interval

    //add new properties if necessary

    public float modif_value;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {                        //filter by tag
            transform.SetParent(other.transform);               //attach this to the target
            pc = GetComponentInParent<PlayerController>();      //get player controller from parent
            GetComponent<MeshRenderer>().enabled = false;       //make this invisible
            StartCoroutine("Disappear");                        //start coroutine to apply effect for given time only
            
            DoThisWhenActivated();                              //actual task part

            //Invoke("DebugPrint", .1f);                          //use this if needed (this script does something every .1f second)
        }
    }

    IEnumerator Disappear() {
        while (timer >= 0) {                                    //until timer value = 0, loop
            yield return null;                                      //dont return anything
            timer -= Time.deltaTime;                                //decrease value
        }
        DoThisBeforeDestroy();                                  //actual task part
        Destroy(gameObject);                                    //distroy self (not coroutine but effect)
    }

    /*
    virtual protected void DebugPrint() {                   //do something every .1f second
        Invoke("DebugPrint", .1f);                              //self recall
    }
    */

    virtual protected void DoThisWhenActivated() {
        pc.moveSpeed *= modif_value;                        //apply effect
    }

    virtual protected void DoThisBeforeDestroy() {
        pc.moveSpeed /= modif_value;                        //remove effect
    }
}