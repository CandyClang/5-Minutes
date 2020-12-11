using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegoBlock : MonoBehaviour
{
    public void selfTurnOff() {
        gameObject.SetActive(false);
    }
}
