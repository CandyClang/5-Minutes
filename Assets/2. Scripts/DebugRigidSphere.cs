using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is for JinScene's Debug Rigid Sphere control

public class DebugRigidSphere : MonoBehaviour
{
    public float strenght;

    public void GoFront() {
        GetComponent<Rigidbody>().AddForce(transform.forward * strenght, ForceMode.Impulse);
    }
    public void GoBack() {
        GetComponent<Rigidbody>().AddForce(transform.forward * -strenght, ForceMode.Impulse);
    }
    public void GoLeft() {
        GetComponent<Rigidbody>().AddForce(transform.right * -strenght, ForceMode.Impulse);
    }
    public void GoRight() {
        GetComponent<Rigidbody>().AddForce(transform.right * strenght, ForceMode.Impulse);
    }
    public void ResetToIdentity() {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.angularDrag = 0;
        rb.angularVelocity = new Vector3();
        rb.velocity = new Vector3();
        transform.rotation = Quaternion.identity;
    }
}
