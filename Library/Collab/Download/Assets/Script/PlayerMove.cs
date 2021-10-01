using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System;


public class PlayerMove : MonoBehaviour
{
    private void Start()
    {
    }

    public static bool Climbable = true;
    public float posX, posY;//用來接收滑鼠點擊座標的X、Y值
    Vector3 worldPosition;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))  //取得滑鼠點擊的座標
        {
            worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPosition.z = 0;

            posX = worldPosition.x;
            posY = worldPosition.y;

            if (Climbable == true)
            {
                Move();
            }

            Debug.Log(MoveFloor.FloorMove);
        }
    }

    Sequence MoveAnimation = DOTween.Sequence();
    private void Move()  //角色跟著滑鼠點擊座標移動
    {
        double L = Math.Abs(transform.position.x - posX);  //計算點擊位置與Player現在位置 (用來計算所要的速度 

        if (MoveFloor.FloorMove == false)
        {
            MoveAnimation.Append(transform.DOMoveX(posX, (float)L / 3));
        }
    }


    Sequence ClimbAnimation = DOTween.Sequence();
    public static float ColliderHigth;
    public static Vector2 contact;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "floor")
        {
            contact = collision.gameObject.transform.position;  //被碰撞物件座標
            ColliderHigth = collision.GetComponent<BoxCollider2D>().size.y + 0.1f;  //取得被碰撞體高度

            if (transform.position.y + (GetComponent<BoxCollider2D>().size.y / 2) >= collision.gameObject.transform.position.y /*collision.GetComponent<BoxCollider2D>().bounds.center.y*/ && posY >= contact.y && Climbable == true)
            {
                MoveAnimation.Pause();
                //Climbable = false;
                ClimbAnimation.Append(transform.DOMoveY((PlayerMove.contact.y + transform.GetComponent<BoxCollider2D>().size.y / 2), 0.2f))/*.Append(transform.DOMoveX(contact.x , 1f))*/;

                Debug.Log(collision.gameObject.name + "Yes");
            }

            else
            {
                Debug.Log(collision.gameObject.name + "No");
            }
        }


        // Climbable = true;
    }

    //private void OnTriggerExit2D(Collider2D collision)
    //{

    //    //transform.DOPlay();
    //    //GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
    //}

    private void OnCollisionStay2D(Collision2D collision)
    {
        Climbable = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        MoveAnimation.Pause();
        Climbable = false;
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //  //GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

    //    ContactPoint2D contact = collision.contacts[0];  //取得碰撞點

    //    float ColliderHigth = collision.collider.GetComponent<BoxCollider2D>().size.y;  //取得碰撞體高度

    //    if (transform.position.y + GetComponent<BoxCollider2D>().bounds.extents.y >= collision.collider.GetComponent<BoxCollider2D>().bounds.center.y && posY >= contact.point.y)
    //    {
    //        ClimbAnimation.Append(transform.DOMoveY(transform.position.y + ColliderHigth, 1f))/*.Append(transform.DOMoveX((transform.position.x + contact.point.x)/2, 1f))*/;


    //        Debug.Log(collision.gameObject.name + "Yes" + contact.point.y);
    //        Climbable = true;
    //    }
    //    else
    //    {
    //        Debug.Log(collision.gameObject.name + "No");
    //        Climbable = true;
    //    }

    //    //if (contact - GetComponent<Collider2D>() = hight;)

    //    //Debug.Log("C" + contact); Debug.Log("R" + rot); Debug.Log("P" + pos);

    //}


    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
    //}
}