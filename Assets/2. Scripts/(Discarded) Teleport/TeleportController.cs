using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportController : MonoBehaviour
{
    //array to store all teleport nodes
    [SerializeField] GameObject[] Nodes;

    public void StartRegister() {
        Nodes = GameObject.FindGameObjectsWithTag("Teleport");      //store all teleport nodes to array

        List<int> usedIndexes = new List<int>();                    //temporary storage to save all the used indexes while iteration

        // I know this is wasty implementation. I will revice it later

        while (true) {                                              //loop until invalid

            //Part 1 - find if there is unused index
            int index = 0;                                          //intialize temporary integer

            while (true) {                                          //lopp until invalid
                if (usedIndexes.Count == Nodes.Length) return;          //if all the index number are used, exit this script
                index = Random.Range(0, Nodes.Length);                  //give random index valid for Nodes[]
                if (!usedIndexes.Contains(index)) {                     //if this index was not used before
                    usedIndexes.Add(index);                                 //add this index to used indexes array
                    break;                                                  //and make inner loop invalid
                }
            }

            //Part 2 - Set target
            TeleportNode nodeThis                                   //grab Teleport Node of this index
                = Nodes[index].GetComponent<TeleportNode>();
            TeleportNode nodeTarget = nodeThis.GetTarget();         //grab this Teleport Node target (supposedly null)
            if (nodeTarget == null) {                               //if the target was null
                int indexToApproach = Random.Range(0, Nodes.Length);    //get random other index for target Teleport Node
                TeleportNode nodeToRegister                             //grab random other Teleport Node
                    = Nodes[indexToApproach].GetComponent<TeleportNode>();
                nodeThis.SetTarget(nodeToRegister);                     //assign random other Teleport Node
            }
        }
    }
}
