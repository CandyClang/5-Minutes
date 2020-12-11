using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is a part of the map generation script : part 3
//This block will become a decoration container

public class MapDeco : MonoBehaviour
{
    [SerializeField] GameObject[] Decoratives;      //array of decorations (not spawner)

    Quaternion[] rotations = {                      //rotation pool
        Quaternion.Euler(0,0,0),
        Quaternion.Euler(0, 90, 0),
        Quaternion.Euler(0, 180, 0),
        Quaternion.Euler(0, 270, 0)
    };

    public void Spawn(Vector3 position)             //if triggered from MapBlock.cs
    {
        if(Decoratives != null && Decoratives.Length != 0) {    //exception handling (for case of Decoratives[] being empty or null)
            Instantiate(                                            //spawn decoration randomply to given position
                Decoratives[Random.Range(0, Decoratives.Length)],
                position,
                rotations[Random.Range(0, rotations.Length)],
                transform
                );
        }
    }
}
