using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class OutSwitch : MonoBehaviour
{
    //public Show_JoystickPlatform show_JoystickPlatform;

    public GameObject Switch;
    private void OnTriggerEnter2D(Collider2D outswitch) 
    {
        //if (show_JoystickPlatform.isCombine = true && outswitch.tag == "Player") 
        {
            Switch.SetActive(false);
        }
    }
}

