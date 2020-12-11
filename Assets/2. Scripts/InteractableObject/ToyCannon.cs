using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyCannon : MonoBehaviour
{
    [SerializeField] GameObject cannonball;
    [SerializeField] Transform cannonballSpawnPosition;
    [SerializeField] Vector3 strengthDistribution;
    [SerializeField] int strength;
    [SerializeField] Vector3 shootCalcRes = new Vector3();
    bool bActivated = false;

    private void Start() {
        CalculateForce();

        cannonball = Instantiate(cannonball, cannonballSpawnPosition.position, Quaternion.identity);
        cannonball.SetActive(false);
    }

    public void Trigger() {
        if (!bActivated) {
            cannonball.SetActive(true);
            cannonball.GetComponent<Rigidbody>().AddForce(shootCalcRes);
            bActivated = true;
        }
    }

    public void ResetToIdentity() {
        cannonball.GetComponent<Rigidbody>().velocity           = new Vector3();
        cannonball.GetComponent<Rigidbody>().angularDrag        = 0;
        cannonball.GetComponent<Rigidbody>().angularVelocity    = new Vector3();
        cannonball.transform.localRotation = Quaternion.identity;
        cannonball.transform.position = cannonballSpawnPosition.position;
        cannonball.SetActive(false);
        bActivated = false;
    }

    public void CalculateForce() {
        shootCalcRes = 
            (transform.forward  * strengthDistribution.z    * strength) + 
            (transform.up       * strengthDistribution.y    * strength) + 
            (transform.right    * strengthDistribution.x    * strength);
    }
}
