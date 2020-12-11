using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HearingScript : MonoBehaviour
{
	public bool isHeard = false;
	private GameObject player;
	private GameObject human;
	private Vector3 soundLocation;
	private float speed = 1.0f;

	void Start()
	{
		soundLocation = new Vector3(0.0f, 0.0f, 0.0f);
		human = GameObject.FindGameObjectWithTag("Human");
		player = GameObject.FindGameObjectWithTag("Human");
	}
	private void OnTriggerEnter(Collider other)
	{
        HumanStunScript stunScript = human.GetComponentInChildren<HumanStunScript>();

        if (other.gameObject.CompareTag("Player"))
		{
			Debug.Log("Heard");
			isHeard = true;

            if (isHeard)
			    soundLocation = player.transform.position;

            if (stunScript.isStunned == true)
            {
                isHeard = false;
            }
		}
	}

	private void OnTriggerExit(Collider other)
	{
		isHeard = false;
	}

	public bool GetIsHeard() 
	{
		return isHeard;
	}
}

