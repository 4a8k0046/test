using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class LeftSwitch : MonoBehaviour
{
   
    public GameObject Joystick;//宣告搖桿平台
    public GameObject HoldOn;//宣告手上拿著的東西

    public GameObject Move_Platform;//宣告移動平台
    public Sprite Change_Left;//搖桿左圖片

    float change_plat_y = (float)-6.34;
    float plat_y = (float)-1.7;//平台原來高度

    int count_left,control_left;

    public TypeWriter typeWriter;

    private void OnTriggerEnter2D(Collider2D Left_switch)
    {
        if (typeWriter.showjoyplat = true && Left_switch.tag == "Player")
        {
            Debug.LogError("L");
            HoldOn.SetActive(false);
            Joystick.GetComponent<SpriteRenderer>().sprite = Change_Left;
            count_left ++;
            control_left = count_left %2;
            if (control_left == 1)
            {
                Move_Platform.transform.DOMoveY(change_plat_y, 1f);
            }
            else 
            {
                Move_Platform.transform.DOMoveY(plat_y, 1f);
            }
            
        }
    }
}
