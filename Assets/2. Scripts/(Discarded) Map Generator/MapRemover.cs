using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRemover : MonoBehaviour {
    Vector2 dimension;
    float eachBlockSize;

    [Range(0, 1)]
    [SerializeField] float rate;

    [SerializeField] GameObject ForCollision;
    [SerializeField] GameObject MapHolder;

    public void StartRemover(MapGenerator MapGen) {
        MapGenerator mg = MapGen;
        dimension = mg.GetSize();
        eachBlockSize = mg.GetIndivSize();

        RelocateSelf();
        ForCollision.transform.localScale = new Vector3(dimension.x+eachBlockSize, .5f, dimension.y+eachBlockSize);
        StartCoroutine(StartShrinking());
    }

    void RelocateSelf() {
        transform.position = new Vector3((dimension.x-eachBlockSize)/2, transform.position.y, (dimension.y - eachBlockSize) / 2);
    }

    IEnumerator StartShrinking() {
        float started = Time.time;
        while(true) {
            yield return null;
            float valx = ForCollision.transform.localScale.x - rate * (Time.time - started) * Time.deltaTime;
            float valz = ForCollision.transform.localScale.z - rate * (Time.time - started) * Time.deltaTime;
            if (!(valx <= 0)) {
                ForCollision.transform.localScale = new Vector3(valx, 20, valz);
            } else break;
        }
    }
}