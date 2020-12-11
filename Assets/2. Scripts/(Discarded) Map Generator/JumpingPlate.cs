using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingPlate : MonoBehaviour
{
    //Jumping plate is only activatable after this interval from last activation
    [SerializeField] float cooldown = 2f;
    //Jumping plate's casting direction + strenght
    [SerializeField] Vector3 jumpStrength = new Vector3(0, 10, 0);

    //For Jumping plate's casting motion
    Animator animator;

    //For activation control
    bool validity = true;

    private void Start() {
        //initialize animator
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision) {
        //when collided, check validity first
        if (validity) {
            //and if the plate was activatable, first, make the plate inactive
            validity = false;
            //cast the collision target to the jump direction + strenght
            collision.gameObject.GetComponent<Rigidbody>().AddForce(jumpStrength, ForceMode.Impulse);
            //activate the plate's casting animation
            animator.SetTrigger("Stepped");
            //and start coroutine to wait for designated seconds before next activation
            StartCoroutine(CooldownTimer(cooldown));
        }
    }

    IEnumerator CooldownTimer(float duration) {
        yield return new WaitForSeconds(duration);      //wait for designated seconds by parameter
        validity = true;                                //and activate the plate back
    }
}