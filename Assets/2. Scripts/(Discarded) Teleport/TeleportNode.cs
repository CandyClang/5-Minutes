using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportNode : MonoBehaviour
{
    [SerializeField] TeleportNode target;               //other side of this teleport node directing
    [SerializeField] bool flip = false;                 //(DEBUG) whether to flip the rigid transformation direction

    bool suppress = false;                              //whether to invalidate the teleport node (without this, there's visual glitch)
    bool flipBasedOnColor = false;                      //(DEBUG) for coloring (flip)
    int flipValue = 1;                                  //(DEBUG) for reversing vector direction (flip)

    private void Start() {
        SetColor();
    }

    private void Update() {
        if (flip != flipBasedOnColor) {                     //to detect flip boolean being changed 
            SetColor();                                         //reset color DEBUG ONLY
        }
    }

    void SetColor() {                                   //for setting up color. DEBUG ONLY
        if (flip) {
            GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
        } else {
            GetComponent<MeshRenderer>().material.SetColor("_Color", Color.green);
        }
        flipBasedOnColor = flip;
    }

    private void OnTriggerEnter(Collider other) {       //actual teleport part
        if (!other.gameObject.CompareTag("Player")) return; //if the target object was not Player, exit script
        if (!suppress) {                                    //if the node is currently suppressed, dont teleport the collision target
            Vector3 relativePosition                            //find collision target's position relative to the teleport node
                = other.gameObject.transform.position- transform.position;
            target.SuppressCollision();                         //suppress the other side of the teleport node
            if (target.IsFlip()) {                              //to flip DEBUG ONLY 
                flipValue = -1;                                     //check field
                other.gameObject.GetComponent<Rigidbody>().velocity *= -1;  //invert rigid velocity
            }
            other.gameObject.transform.position                 //send collision target to the other side.
                = target.gameObject.transform.position - relativePosition * flipValue;
        }
    }

    private void OnTriggerExit(Collider other) {        //to re-enable teleport
        if (!other.gameObject.CompareTag("Player")) return; //if the target object was not Player, exit script
        suppress = false;                                   //release suppression
    }

    public void SuppressCollision() {                   //to give the other side an ability to suppress this node
        suppress = true;
    }

    public bool IsFlip() {                              //to flip DEBUG ONLY
        return flip;
    }

    public TeleportNode GetTarget() {                   //getter for TeleportController.cs
        if (target != null) return target;
        else return null;
    }

    public void SetTarget(TeleportNode node) {          //setter for TeleportController.cs
        target = node;
    }
}
