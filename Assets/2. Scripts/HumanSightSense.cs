using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HumanSightSense : MonoBehaviour
{
    [SerializeField] private float fieldOfViewAngle = 110f;
    [SerializeField] private bool playerSighted;

    private NavMeshAgent human;
    private SphereCollider sightCollider;
    private GameObject player;

    void Awake()
    {
        human = GetComponent<NavMeshAgent>();
        sightCollider = GetComponent<SphereCollider>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            playerSighted = false;
            Debug.Log("Human can't see player");

            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);

            if (angle < fieldOfViewAngle * 0.5f)
            {
                RaycastHit hit;

                if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, sightCollider.radius))
                {
                    if (hit.collider.gameObject == player)
                    {
                        playerSighted = true;
                        Debug.Log("Human can see player");
                    }
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerSighted = false;
            Debug.Log("Human can't see player");
        }
    }
}
