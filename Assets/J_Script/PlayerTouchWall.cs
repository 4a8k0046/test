using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchWall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && gameObject.tag == "Right_Wall")
        {
            GetComponentInParent<MoveFloor>().isKid_At_Right = true;
            //GetComponentInParent<MoveFloor>().isKid_At_Left = false;
        }
        if (collision.gameObject.tag == "Player" && gameObject.tag == "Left_Wall")
        {
            GetComponentInParent<MoveFloor>().isKid_At_Left = true;
            //GetComponentInParent<MoveFloor>().isKid_At_Right = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GetComponentInParent<MoveFloor>().isKid_At_Left = false;
            GetComponentInParent<MoveFloor>().isKid_At_Right = false;
        }
    }
}
