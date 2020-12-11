using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] float intervalBetweenShots = 3.0f;
    [SerializeField] GameObject Ball;
    [SerializeField] Vector3 ShotTowards = new Vector3(0, 0, 1);
    [SerializeField] float shotStrengthMultiplier = 20.0f;

    bool canShoot = false;
    bool suppressed = true;

    Vector3 direction;
    [SerializeField] float selfMovementMultiplier = 10.0f;

    Quaternion[] rotations = {                          //rotation to be used for spawn
        Quaternion.Euler(0, 0, 0),
        Quaternion.Euler(0, 90, 0),
        Quaternion.Euler(0, 180, 0),
        Quaternion.Euler(0, 270, 0)
    };

    private void Start() {
        Ball = Instantiate(Ball);
    }

    private void Update() {
        transform.position += direction * selfMovementMultiplier * Time.deltaTime;
        
        if (canShoot) {
            Ball.transform.position += ShotTowards * shotStrengthMultiplier * Time.deltaTime;
        }
    }

    void Shot() {
        Ball.transform.position = transform.position;
    }

    public void SetDirection(Vector3 direction) {
        this.direction = direction;
    }

    public void SetShootDirection(Vector3 direction) {
        ShotTowards = direction;
        Ball.GetComponent<CannonBall>().headingTo = ShotTowards;
        transform.localRotation = rotations[GetIndexByDirection(ShotTowards)];
        canShoot = true;
        InvokeRepeating("Shot", 0, intervalBetweenShots);
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.CompareTag("Triggerer"))
            if (!suppressed)
                direction *= -1;
    }

    private void OnCollisionExit(Collision collision) {
        if (collision.collider.CompareTag("Triggerer"))
            suppressed = false;
    }

    int GetIndexByDirection(Vector3 direction) {
        if (direction.x > 0) return 1;
        else if (direction.x < 0) return 3;
        else if (direction.z > 0) return 0;
        else return 2;
    }
}
