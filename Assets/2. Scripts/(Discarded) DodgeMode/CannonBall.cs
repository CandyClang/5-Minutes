using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public Vector3 headingTo = new Vector3();
    [SerializeField] float strengthMultiplier = 5.0f;

    private void OnCollisionEnter(Collision collision) {
        print("Collided");
        print(collision.collider.name);
        if (collision.collider.CompareTag("Player")) {
            print("Was Player");
            collision.gameObject.GetComponent<Rigidbody>().AddForce(headingTo * strengthMultiplier, ForceMode.Impulse);
        }
    }
}
