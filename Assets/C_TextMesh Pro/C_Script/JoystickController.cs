using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class JoystickController : MonoBehaviour
{
    public GameObject Player;
    public GameObject Joystick;//宣告搖桿平台
    public GameObject HoldOn;//宣告手上拿著的東西


    public GameObject Move_Platform;//宣告移動平台

    public Sprite changeJoystick;//插上零件的搖桿圖片
    public Sprite Change_Left;//搖桿左圖片
    public Sprite Change_Right;//搖桿右圖片

    float change_plat_y = (float)-2.82;
    float plat_y = (float)-1.7;//平台原來高度


    void Start()
    {

    }

    // Update is called once per fram()
    public void CAllTRigger()
    {
        if (isCombine)
        {
            if (Player.transform.position.x < -8.6f)
            {
                Left_Tigger();
            }
            else
            {
                Right_Tigger();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D JoystickPlatform_show)
    {
        if (Joystick.activeSelf)
        {
            if (JoystickPlatform_show.gameObject.tag == "Player")
            {
                Debug.Log("show_JoystickPlatform");
                Joystick.GetComponent<SpriteRenderer>().sprite = changeJoystick;
                HoldOn.SetActive(false);
                isCombine = true;
            }
        }
    }
    public bool isCombine = false;

    public void Left_Tigger()
    {
        if (Joystick.activeSelf)
        {
            Debug.Log("Left");
            Joystick.GetComponent<SpriteRenderer>().sprite = Change_Left;
            Move_Platform.transform.DOMoveY(change_plat_y, 1f);
        }
    }
    public void Right_Tigger()
    {
        if (Joystick.activeSelf)
        {
            Debug.Log("Right");
            Joystick.GetComponent<SpriteRenderer>().sprite = Change_Right;
            Move_Platform.transform.DOMoveY(plat_y, 1f);
        }
    }
}

