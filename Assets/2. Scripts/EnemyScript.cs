using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyScript : MonoBehaviour
{
    public Transform target;
    private float dist;
    public float speed;
    public bool isAggro;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

	// Update is called once per frame
	void Update()
    {
        float dist = Vector3.Distance(target.position, transform.position);
        if (dist < 20)
            isAggro = true;
        if (dist > 20)
            isAggro = false;
        if (isAggro)
            chasePlayer();
    }

	private void FixedUpdate()
	{
        
    }

	private void chasePlayer() 
    {
        Vector3 position = Vector3.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);
        rb.MovePosition(position);
        transform.LookAt(target);
    }

	private void OnTriggerEnter (Collider other)
	{
        if (other.gameObject.tag == "Player") 
        {
           /* What should happen here?  
           PlayerController.DOSOMETHING
           */
        }
	}
}
