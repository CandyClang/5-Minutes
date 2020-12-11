using System.Collections;
using System.Collections.Generic;
//using UnityEditorInternal;
using UnityEngine;

public class HumanStunScript : MonoBehaviour
{
    public bool isStunned;
    public GameObject stunIcon;

    private GameObject player;
    
    PlayerController controller;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        controller = player.GetComponent<PlayerController>();
        stunIcon.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            StartCoroutine(StunWait());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }

    IEnumerator StunWait()
    {
        controller = player.GetComponent<PlayerController>();
        controller.enabled = false;
        isStunned = true;
        stunIcon.SetActive(true);
        player.GetComponent<PlayerController>().animator.SetBool("Stunned", true);



        yield return new WaitForSeconds(5f);
        controller.enabled = true;
        isStunned = false;
        stunIcon.SetActive(false);
        player.GetComponent<PlayerController>().animator.SetBool("Stunned", false);
    }
}
