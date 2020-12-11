using System.Collections;
using UnityEngine;

public class HumanPatrolPoints : MonoBehaviour
{
    [SerializeField] protected float debugDrawRadius = 1.0f;

    //PATROL POINT MARKER
    public virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, debugDrawRadius);
    }
}
