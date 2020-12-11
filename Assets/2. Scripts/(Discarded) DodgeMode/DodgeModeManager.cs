using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeModeManager : MonoBehaviour
{
    [SerializeField] private Transform RelativeTo1;                 //first nodes being used for spawning cannon
    [SerializeField] private Transform RelativeTo2;                 //second nodes being used for spawning cannon
    [SerializeField] private GameObject CannonNode;                 //grab prefab
    List<GameObject> CannonNodesPlaced = new List<GameObject>();    //cannon nodes placed in scene
    private float cannonPosHeight = 1.801537f;                      //change if necessary

    private void OnEnable() {
        if (RelativeTo1 == null) GameObject.Find("RelativeTo1");
        if (RelativeTo2 == null) GameObject.Find("RelativeTo2");
        PlaceCannonNodeAtPoint();
        AssignRelative();
    }

    private void OnDestroy() {
        foreach(GameObject Node in CannonNodesPlaced) {
            Destroy(Node);
        }
        CannonNodesPlaced.Clear();
    }

    void PlaceCannonNodeAtPoint() {                                 //return planear random position between two nodes (Check field)
        CannonNodesPlaced.Add(Instantiate(
            CannonNode,
            new Vector3(RelativeTo1.position.x, cannonPosHeight, RelativeTo1.position.z),
            Quaternion.identity,
            transform.parent
            )
        );
        CannonNodesPlaced.Add(Instantiate(
            CannonNode,
            new Vector3(RelativeTo1.position.x, cannonPosHeight, RelativeTo2.position.z),
            Quaternion.identity,
            transform.parent
            )
        );
        CannonNodesPlaced.Add(Instantiate(
            CannonNode,
            new Vector3(RelativeTo2.position.x, cannonPosHeight, RelativeTo2.position.z),
            Quaternion.identity,
            transform.parent
            )
        );
        CannonNodesPlaced.Add(Instantiate(
            CannonNode,
            new Vector3(RelativeTo2.position.x, cannonPosHeight, RelativeTo1.position.z),
            Quaternion.identity,
            transform.parent
            )
        );
    }

    void AssignRelative() {
        GameObject[] ToModify = CannonNodesPlaced.ToArray();
        List<GameObject> temp = new List<GameObject>();

        temp.Add(ToModify[1]);
        temp.Add(ToModify[3]);
        ToModify[0].GetComponent<CannonNode>().SetRelatives(temp.ToArray());

        temp.Clear();
        temp.Add(ToModify[0]);
        temp.Add(ToModify[2]);
        ToModify[1].GetComponent<CannonNode>().SetRelatives(temp.ToArray());

        temp.Clear();
        temp.Add(ToModify[1]);
        temp.Add(ToModify[3]);
        ToModify[2].GetComponent<CannonNode>().SetRelatives(temp.ToArray());

        temp.Clear();
        temp.Add(ToModify[0]);
        temp.Add(ToModify[2]);
        ToModify[3].GetComponent<CannonNode>().SetRelatives(temp.ToArray());

        temp.Clear();
    }
}
