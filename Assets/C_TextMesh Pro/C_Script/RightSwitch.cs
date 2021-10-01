using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RightSwitch : MonoBehaviour
{
    public GameObject Joystick;//宣告搖桿平台
    public GameObject HoldOn;//宣告手上拿著的東西

    public GameObject Move_Platform;//宣告移動平台
    public Sprite Change_Right;//搖桿右圖片


    //public Show_JoystickPlatform show_JoystickPlatform;

    private void OnTriggerEnter2D(Collider2D Rirgt_switch)
    {
        //ebug.LogError(show_JoystickPlatform.isCombine);

        if (/*show_JoystickPlatform.isCombine == true &&*/ Rirgt_switch.tag == "Player")
        {
            Debug.LogError("R");
            HoldOn.SetActive(false);
            Joystick.GetComponent<SpriteRenderer>().sprite = Change_Right;

        }
    }
}
