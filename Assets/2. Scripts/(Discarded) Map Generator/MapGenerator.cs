using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is a part of the map generation script : part 1
//This block will become a map container (you can assign a new container from this script if necessary)

public class MapGenerator : MonoBehaviour {
    [SerializeField] private GameObject MapBlock;                   //List of map block prefabs (biome chunk)

    public List<GameObject> MapBlockArray = new List<GameObject>(); //list of all map block

    [SerializeField] private TeleportController TeleportController; //specific initializer object for teleport trap
    [SerializeField] private MapRemover MapShrinker;                //map shrinker (think of PUBG's blue circle)
    
    [SerializeField] private Transform blocks;                      //storage of the map (self as default) (check Awake())
    [SerializeField] private int width;                             //width of the map (number of biome chunk per row)
    [SerializeField] private int height;                            //height of the map (number of biome chunk per col)

    [SerializeField] private GameObject[] Obtainables;              //array of buffs & debuffs
    [Range(0,10)]
    [SerializeField] private int noOfObtainables;                   //number of buffs & debuffs per game

    [SerializeField] private Transform RelativeTo1;                 //first nodes being used for spawning obtainable
    [SerializeField] private Transform RelativeTo2;                 //second nodes being used for spawning obtainable

    float blockSize;                                                //size of biome tile (check MapBlock.cs)
    
  //  [Server]
    void Awake() {                                                  //Start is called before the first frame update
        if (blocks == null) blocks = transform;                         //if 'blocks' was not given from inspector, set it to self

        SpawnMapBlocks();                                               //create map, deco and trap
        TeleportController.StartRegister();                             //start assigning teleport nodes to each other
        MapShrinker.StartRemover(this);                                 //start map shrinker
        SpawnBuffsAndDebuffs();                                         //spawn buffs and debuffs
    }

  //  [Server]
    public void  SpawnMapBlocks() {                                 //spawner for map
        for (int x = 0; x < width; ++x) {                               //iterate for several times until given value (x direction)
            for (int z = 0; z < height; ++z) {                              //iterate for several times until given value (z direction)
                blockSize = MapBlock.GetComponent<MapBlock>().GetSize();        //measured to give distance between blocks (prevent overlap)

                GameObject newBlock = Instantiate(                              //create biome chunk and save
                    MapBlock,
                    new Vector3(
                        x * blockSize,
                        -0.5f,
                        z * blockSize),
                    Quaternion.identity,
                    blocks);

                MapBlockArray.Add(newBlock);                                    //save biome chunk to MapBlockArray
            }
        }
    }

   // [Server]
    void SpawnBuffsAndDebuffs() {                                   //spawner for buffs and debuffs
        for(int i = 0; i < noOfObtainables; ++i)                        //iterate for designated times by field
            if(Obtainables != null && Obtainables.Length > 0)               //exception handling (for case of Obtainables[] being empty or null)
                Instantiate(                                                    //create buffs or debuffs randomly
                    Obtainables[Random.Range(0, Obtainables.Length)], 
                    GetRandPosFromNodes(), 
                    Quaternion.identity, 
                    transform);
    }

  //  [Server]
    public Vector3 GetCentre() {                                    //check GameStateManager.cs for usage of this method (DodgeBallMode)
        return new Vector3(blockSize * width / 2, transform.position.y, blockSize * height / 2);
    }

 //   [Server]
    public Vector2 GetSize() {
        return new Vector2(blockSize * width, blockSize * height);
    }

   // [Server]
    public float GetIndivSize() {
        return MapBlock.GetComponent<MapBlock>().GetIndivSize();
    }

   // [Server]
    Vector3 GetRandPosFromNodes() {                                 //return planear random position between two nodes (Check field)
        if(RelativeTo1 != null && RelativeTo2 != null)
            return new Vector3(
                Random.Range(RelativeTo1.position.x, RelativeTo2.position.x), 
                Random.Range(RelativeTo1.position.y, RelativeTo2.position.y), 
                Random.Range(RelativeTo1.position.z, RelativeTo2.position.z));
        return new Vector3();                                       //if leading case didnt return and exit, return null
    }

  //  [Server]
    public void destroyMap() {                                      //check GameStateManager.cs for usage of this method (DropTileMode)
        foreach (GameObject tile in MapBlockArray) {
            Destroy(tile);
        }
    }
}
