using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System;


public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rb2d ;
    Animator am;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        am = GetComponent<Animator>();

        transform.position = new Vector3(GameObject.Find("House_Door").transform.position.x, transform.position.y, transform.position.z); //確保角色初始位置
    }

    public static bool Climbable = false;
    public float posX, posY;  //用來接收滑鼠點擊座標的X、Y值
    Vector3 worldPosition;
    RaycastHit2D hit;
    Ray ray;

    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0) && am.GetCurrentAnimatorStateInfo(0).IsName("BoyIdle") && !GameObject.Find("ViewUI")　/*hit == Physics2D.Raycast(ray.origin, ray.direction, 10, 1 << LayerMask.NameToLayer("UI"))*/)
        {
            //todo 宥諭改的 好像沒用到??C:\Users\4a8k0\OneDrive\桌面\家鎖\Assets\J_Script\ElevatorMove.cs
            JoystickController joystickController = FindObjectOfType<JoystickController>();
            if (joystickController != null) { joystickController.CAllTRigger(); }


            gameObject.GetComponent<Rigidbody2D>().constraints = ~RigidbodyConstraints2D.FreezePositionY;
            //gameObject.GetComponent<Rigidbody2D>().gravityScale = 5;
            Climbable = true;

            worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);  //取得滑鼠點擊的座標
            worldPosition.z = 0;

            posX = worldPosition.x;
            posY = worldPosition.y;

            //if (/*am.GetCurrentAnimatorStateInfo(0).IsName("BoyIdle") */ 　/*!am.GetCurrentAnimatorStateInfo(0).IsName("BoyClimb_1") && !am.GetCurrentAnimatorStateInfo(0).IsName("BoyFall")*/)
            {
                // 角色轉向
                if (transform.position.x > posX)
                {
                    gameObject.GetComponent<SpriteRenderer>().flipX = false;
                }
                else
                {
                    gameObject.GetComponent<SpriteRenderer>().flipX = true;
                }


                if (TouchesRightWall == true) //若碰到牆
                {
                    if (ClosestPoint.x < posX)
                    {
                        Move();

                    }
                }
                else if (TouchesLeftWall == true) //若碰到牆
                {
                    if (ClosestPoint.x > posX)
                    {
                        Move();
                    }
                }
                else
                {
                    Move();
                }

                //if (/*Climbable == false && !am.GetCurrentAnimatorStateInfo(0).IsName("BoyClimb")*/)
                //{
                //    Move();
                //}
            }
        }

        //移動到目的地之後 跑步動畫停止
        if (Math.Abs(transform.position.x - posX) <= 0.3 || am.GetCurrentAnimatorStateInfo(0).IsName("BoyClimb_1") || am.GetCurrentAnimatorStateInfo(0).IsName("BoyFall_1"))
        {
            am.SetBool("Run", false);
        }



        //if (GetComponent<Rigidbody2D>().velocity.y != 0 && !am.GetCurrentAnimatorStateInfo(0).IsName("BoyClimb"))
        //{
        //    am.SetTrigger("Fall");
        //    GetComponent<Rigidbody2D>().constraints.FreezeRotationY
        //}


        //  起跳動作結束 開始下墜
        if (am.GetCurrentAnimatorStateInfo(0).IsName("BoyFall_2") /* && gameObject.GetComponent<Rigidbody2D>().constraints != RigidbodyConstraints2D.FreezeRotation*/)
        {
            gameObject.GetComponent<Rigidbody2D>().constraints = ~RigidbodyConstraints2D.FreezePositionY;

            //gameObject.GetComponent<Rigidbody2D>().gravityScale = 5;

            //Invoke("Falling", 0.5f);
            //Falling();
        }

        // 跳起 碰到地面 準備上爬
        if (am.GetCurrentAnimatorStateInfo(0).IsName("BoyClimb_2") && IfTouchdown)
        {
            IfTouchdown = false;

            //gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            Sequence ClimbAnimation = DOTween.Sequence()
                .Insert(0.2f, transform.DOMoveY(transform.position.y + 0.5f, 0.25f))
                .Insert(0.2f, transform.DOMoveX(transform.position.x + 0.2f * Math.Sign(contact.x - transform.position.x), 0.1f))
                .Insert(0.45f, transform.DOMoveY(contact.y + transform.GetComponent<BoxCollider2D>().size.y / 2, 0.3f))
                /*.Insert(1.1f, transform.DOMoveY((contact.y + transform.GetComponent<BoxCollider2D>().size.y / 2), 0.7f))*/;
            Debug.Log(transform.GetComponent<BoxCollider2D>().size.y / 3);
        }


    }

    bool IfTouchdown = false;  // 是否接觸地面
    bool TouchesRightWall = false;  //是否接觸積木 右邊 垂直面
    bool TouchesLeftWall = false;  //是否接觸積木 左邊 垂直面



    private void OnCollisionStay2D(Collision2D collision)
    {
        //  判斷是否落地
        if (am.GetCurrentAnimatorStateInfo(0).IsName("BoyFall_2") && GetComponent<Rigidbody2D>().velocity.y == 0)
        {
            IfTouchdown = true;
            Falling();
        }
    }

    // 落地動畫
    void Falling()
    {
        if (GetComponent<Rigidbody2D>().velocity.y == 0 && IfTouchdown == true/*&& gameObject.GetComponent<Rigidbody2D>().constraints == RigidbodyConstraints2D.FreezeRotation*/)
        {
            am.SetTrigger("Touchdown");
            IfTouchdown = false;
        }
        //gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        //gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        //gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
    }

    // 跳下動畫 以及 向上、向前位移
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == ("Block_Floor") && GetComponent<Rigidbody2D>().velocity.y != 0 && !am.GetCurrentAnimatorStateInfo(0).IsName("BoyClimb_1") && !am.GetCurrentAnimatorStateInfo(0).IsName("BoyFall_2"))
        {
            //transform.position = new Vector3(transform.position.x + 0.5f * Math.Sign(transform.position.x - posX), transform.position.y, transform.position.z);
            MoveAnimation.Pause();
            am.SetTrigger("Fall");
            Climbable = false;
            

            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            //gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;

            Sequence FallAnimation = DOTween.Sequence()
               .Append(transform.DOMoveX(transform.position.x + 0.2f * Math.Sign(transform.position.x - posX), 0.01f))
               .Insert(0.2f, transform.DOMoveY(transform.position.y + 0.3f, 0.1f))
               .Insert(0.2f, transform.DOMoveX(transform.position.x + 0.8f * Math.Sign(posX - transform.position.x), 0.5f));
            
            //gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        }
    }


    Tweener MoveAnimation; // = DOTween.Sequence();

    private void Move()  //角色跟著滑鼠點擊座標移動
    {
        if(!Physics2D.Raycast(ray.origin, ray.direction, 10, 1 << LayerMask.NameToLayer("UI")))
        {
            double L = Math.Abs(transform.position.x - posX);  //計算點擊位置與Player現在位置 (用來計算所要的速度 

            posX = worldPosition.x;

            am.SetBool("Run", true);
            MoveAnimation = (transform.DOMoveX(posX, (float)L / 3));
        }
    }


    public static float ColliderHigth;
    public static Vector2 contact;

    Vector3 ClosestPoint;  //碰撞點
    Vector3 PlayerClosestPoint;  //碰撞時玩家位置

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //  攀爬 (起跳
        if (collision.tag == "floor" && Climbable)
        {
            contact = collision.gameObject.transform.position;  //被碰撞物件座標

            if (transform.position.y + (GetComponent<BoxCollider2D>().size.y / 2) >= collision.gameObject.transform.position.y && posY >= contact.y)
            {
                Climbable = false;
                IfTouchdown = true;

                am.SetTrigger("Climb");

                MoveAnimation.Pause();
                gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                //gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;

                Sequence ClimbAnimation = DOTween.Sequence()
                    .Insert(0.5f, transform.DOMoveX(collision.transform.GetComponent<BoxCollider2D>().bounds.center.x, 0.2f))
                    .Insert(0.5f, transform.DOMoveY(contact.y, 0.2f))
                    //.Insert(1.1f, transform.DOMoveY((contact.y + transform.GetComponent<BoxCollider2D>().size.y / 2), 0.7f))
                    ;

                //KeepRuning();
            }
        }


        //  爬梯子
        if (collision.tag == "Ladder")
        {
            MoveAnimation.Pause();
            am.SetBool("ClimbLadder", true);
            am.SetBool("Run", false);
            
            float Floor3 = GameObject.Find("Floor_Three").transform.position.y;
            float Floor2 = GameObject.Find("Floor_Two").transform.position.y;

            float Direction = Floor3 - transform.position.y; // 判斷 要上樓還是下樓
            float WaitForStart = (Direction > 0) ? 1.3f : 1f; // 延遲向上移動 (等待攀爬動畫撥放
            

            Sequence ClimbLadderMoveAnimation = DOTween.Sequence()
                    .Append(transform.DOMoveX(collision.transform.position.x, 1))
                    .Insert(WaitForStart, transform.DOMoveY(transform.position.y + Math.Sign(Direction) * Math.Abs(Floor2 - Floor3), 2));

            Invoke("StopClimbLadder", 2.5f);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // 判斷是否撞牆
        if (collision.gameObject.tag == "Left_Wall" && transform.position.x < posX)
        {
            MoveAnimation.Pause();
            am.SetBool("Run", false);
            ClosestPoint = collision.ClosestPoint(transform.position);
            //PlayerClosestPoint = transform.position;
            transform.position = new Vector3(ClosestPoint.x - 0.1f, transform.position.y, transform.position.z);
            TouchesLeftWall = true;
        }
        if (collision.gameObject.tag == "Right_Wall" && transform.position.x > posX)
        {
            MoveAnimation.Pause();
            am.SetBool("Run", false);
            ClosestPoint = collision.ClosestPoint(transform.position);
            //PlayerClosestPoint = transform.position;
            transform.position = new Vector3(ClosestPoint.x + 0.1f, transform.position.y, transform.position.z);
            TouchesRightWall = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 離開牆壁
        if (collision.gameObject.tag == "Left_Wall")
        {
            ClosestPoint = new Vector3();
            TouchesLeftWall = false;
        }
        if (collision.gameObject.tag == "Right_Wall")
        {
            ClosestPoint = new Vector3();
            TouchesRightWall = false;
        }
    }

    void StopClimbLadder()  // TODO 爬梯子動畫結束
    {
        am.SetBool("ClimbLadder", false);
    }
}