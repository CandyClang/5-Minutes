using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is a part of the map generation script : part 2
//This block will become a biome chunk

public class MapBlock : MonoBehaviour {
    public const float BLOCKSIZE = 5;           //This is mannually given value. if the size of the map block changes, change this value

    [SerializeField] GameObject[] MapTiles;     //This is an array of prefabs to spawn. (eg. ground blocks of grass, desert and snow)
    [SerializeField] GameObject[] Decoratives;  //This is an array of prefabs to spawn, decoration spawners to be placed on the map blocks
                                                //Order of the decoratives must match the order of the tiles
    [SerializeField] GameObject[] Traps;        //This is an array of prefabs to spawn, especially the traps
                                                //Order is irrelevant
    [Range(1, 3)]
    [SerializeField] private int length = 2;    //number of tiles to be placed in one biome chunck (per row & col)
    [Range(0, 100)]
    [SerializeField] private float decoSpawnRate = 50;  //possibility of decoration to be spawned on one block (not biome chunk).
    [Range(0, 20)]
    [SerializeField] private float trapSpawnRate = 5;   //possibility of trap to be spawned on one block (not biome chunk)

    Quaternion[] rotations = {                          //rotation to be used for spawn
        Quaternion.Euler(0, 0, 0),
        Quaternion.Euler(0, 90, 0),
        Quaternion.Euler(0, 180, 0),
        Quaternion.Euler(0, 270, 0)
    };

    private void OnEnable() {
        SpawnMapTiles();
    }

    void SpawnMapTiles() {
        GameObject temp;                                //temporary storage for each block
        int selfIndex;                                  //index in MapTiles[]
        bool decoWasSpawned = false;

        selfIndex = Random.Range(0, MapTiles.Length);   //assign first value randomly

        //Spawn MapTile
        if (MapTiles.Length != 0 && MapTiles != null) { //exception handling (for case of MapTiles[] being empty or null)
            for (int x = 0; x < length; ++x) {              //loop for 1~3 times (check the description of field 'length')
                for (int y = 0; y < length; ++y) {              //loop for 1~3 times (check the description of field 'length')

                    decoWasSpawned = false;                         //initialize variable to default value (=false)

                    temp = Instantiate(                             //instantiate new block and store it temporarily in 'temp'
                        MapTiles[selfIndex],                            //pick prefab from MapTiles[] using 'selfIndex'
                        new Vector3(                                    //position of the new block should be 
                            x * BLOCKSIZE,                                  //block size * current iteration count
                            0,                                              //0
                            y * BLOCKSIZE                                   //same as x but in z direction
                        ) + transform.position,                                 //based on its parent's position, which is biome chunk
                        Quaternion.identity,                            //dont rotate
                        transform);                                     //make biome chunk a parent of this new block

                    //Decoration Spawner
                    //try spawn the decoration spawner
                        //sorry for messy implementation ;P
                    GameObject decorationSpawner = SpawnThisByRate(Decoratives, selfIndex, decoSpawnRate, temp.transform, Quaternion.identity);
                    
                    //spawn actual decoration
                    //if spawner was spawned
                    if (decorationSpawner != null) {
                        //spawn actual decoration at given position (center of the map tile)
                        decorationSpawner.GetComponent<MapDeco>().Spawn(new Vector3(0, .5f, 0) + temp.transform.position);
                        //and say the decoration was spawned in this iteration
                        decoWasSpawned = true;
                    }

                    //Spawn Trap
                    //if decoration was spawned in this iteration, skip this
                    if (!decoWasSpawned) {
                        //if decoration was not spawned in this iteration (which means this tile is currently empty), try spawn the trap
                        SpawnThisByRate(
                            Traps,                                              //from traps array
                            Random.Range(0, Traps.Length),                      //pick one random index valid for traps array
                            trapSpawnRate,                                      //apply spawn rate (check description in the field)
                            temp.transform,                                     //give currently spawned map tile as a parent of the trap
                            rotations[Random.Range(0, rotations.Length)],       //pick random rotation from array
                            new Vector3(0, 1, 0) + temp.transform.position);    //give coordinate of the center of the new map tile
                    }
                }
            }
        }
    }

    //spanwing script
    GameObject SpawnThisByRate(
        GameObject[] SpawnFrom,                                 //array to base ons
        int toSpawnIndex,                                       //index to apply
        float rate,                                             //spawn rate
        Transform parent,                                       //object to treat as a parent
        Quaternion rotation,                                    //rotation to apply
        Vector3 position = new Vector3())                       //position to apply
    {
        if (Random.Range(0, 100) < rate) {                      //trigger by possibility
            if (SpawnFrom.Length != 0 && SpawnFrom != null) {       //exception handling (for case of given array being empty or null)
                return Instantiate(                                     //create target using all the parameter
                    SpawnFrom[toSpawnIndex],
                    position,
                    rotation,
                    parent
                );
            }
        }
        //if the leading case didnt return and exit, which means something was invalid, return null
        return null;
    }

    //return the size of the biome chunk
    public float GetSize() {
        if (MapTiles.Length != 0 && MapTiles != null) return 5 * length;
        return 0;
    }

    //return the size of each map tile
    public float GetIndivSize() {
        return BLOCKSIZE;
    }
}
