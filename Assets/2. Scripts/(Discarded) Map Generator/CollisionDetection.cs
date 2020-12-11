using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour {
    private void OnTriggerExit(Collider other) {
        print("was triggered");
        if (LayerMask.NameToLayer("Land") == other.gameObject.layer) {
            print("was land");
            other.gameObject.SetActive(false);
        }
    }
}
