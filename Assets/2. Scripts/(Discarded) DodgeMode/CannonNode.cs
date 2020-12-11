using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonNode : MonoBehaviour
{
    GameObject[] Relatives;
    [SerializeField] GameObject Cannon;
    List<GameObject> CannonsPlaced = new List<GameObject>();
    bool isInstantiated = false;

    private void OnDestroy() {
        foreach (GameObject Cannon in CannonsPlaced) {
            Destroy(Cannon);
        }
        CannonsPlaced.Clear();
    }

    public void SetRelatives(GameObject[] Relatives) {
        this.Relatives = Relatives;
    }

    private void Start() {
        CannonsPlaced.Add(Instantiate(
            Cannon,
            transform.position,
            Quaternion.identity,
            transform.parent
            ));
        CannonsPlaced.Add(Instantiate(
            Cannon,
            transform.position,
            Quaternion.identity,
            transform.parent
            ));

        int count = 0;
        foreach (GameObject SpawnedCannon in CannonsPlaced) {
            SpawnedCannon.GetComponent<Cannon>().SetShootDirection((Relatives[Mathf.Abs(count - 1)].transform.position - transform.position).normalized);
            SpawnedCannon.GetComponent<Cannon>().SetDirection((Relatives[count].transform.position - transform.position).normalized);
            ++count;
        }
    }
}
