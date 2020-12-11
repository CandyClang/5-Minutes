using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Demo_control : MonoBehaviour {

    [Header("animator")]
    public Animator animator;

    //event
    #region
    public void on_btn_click(string triger_name)
    {
        this.animator.SetTrigger(triger_name);
    } 
    #endregion
}
