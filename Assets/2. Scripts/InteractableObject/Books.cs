using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Books : MonoBehaviour
{
    [SerializeField] Vector3 Force;
    [SerializeField] bool bMessUp = false;
    Quaternion OriginalRotation;
    Vector3 OriginalPosition;
    Rigidbody rgby;
    bool bForced = false;
    bool bOrganized = false;

    private void Start() {
        OriginalRotation = transform.rotation;
        OriginalPosition = transform.position;
        rgby = GetComponent<Rigidbody>();
        InvokeRepeating("CheckByRate", 0.0f, 0.2f);
    }

    void CheckByRate() {
        if (bMessUp) {
            if (!bForced)
                MessUp();
        }
        else {
            if (!bOrganized)
                Organize();
        }
    }

    void MessUp() {
        rgby.AddForce(Force);
        bForced = true;
        bOrganized = false;
    }

    void Organize() {
        rgby.angularDrag = 0;
        rgby.angularVelocity = Vector3.zero;
        rgby.velocity = Vector3.zero;
        transform.position = OriginalPosition;
        transform.rotation = OriginalRotation;
        bForced = false;
        bOrganized = true;
    }

    public void Trigger(bool toState) {
        bMessUp = toState;
    }
}
