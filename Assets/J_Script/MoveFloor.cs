using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveFloor : MonoBehaviour
{
    RaycastHit2D hit;
    Vector3 worldPosition;
    public bool FloorMove = false;
    public float posX, posY; // 用來接收滑鼠點擊座標的X、Y值
    public GameObject Twin; // 雙胞胎積木 (另一個一樣的物件 

    public bool KidIsHere = false,
        isKid_At_Left = false, isKid_At_Right = false; // 有在積木裡面，並且看位置，才會判斷是true。積木外面一樣是false


    float W; // 積木的一半寬度 (用來判斷積木有沒有超出可視範圍、設定新積木生成位置
    string ChangeName; //沒有編號的積木名稱 (沒有編號 = 雙生積木中較早生成的
    void Start()
    {
        W = GetComponent<PolygonCollider2D>().bounds.extents.x;
        ChangeName = string.Join("", gameObject.name.Split('1'));
    }


    #region 改別方式了
    //Todo 玩家撞牆在這邊
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        if (collision.transform.position.x > GetComponent<BoxCollider2D>().bounds.center.x)
    //        {
    //            isKid_At_Right = true;
    //            isKid_At_Left = false;
    //        }
    //        else
    //        {
    //            isKid_At_Right = false;
    //            isKid_At_Left = true;
    //        }
    //    }
    //}

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        isKid_At_Left = false;
    //        isKid_At_Right = false;
    //    }
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Legs")
    //    {
    //        KidIsHere = true;
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Legs")
    //    {
    //        KidIsHere = false;
    //    }
    //}
    #endregion


    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        posX = worldPosition.x;
        posY = worldPosition.y;


        // 將兩個一樣形狀的積木 設成彼此的Twin
        if (gameObject.tag == "Blocks")
        {
            Twin = GameObject.Find(gameObject.name + "1");
        }
        else if (gameObject.tag == "Clone")
        {
            Twin = GameObject.Find(ChangeName);
        }


        //積木超出可視範圍就刪除  刪除前先確保Twin的tag為Blocks，且名字沒有編號 (因為它將會是兩個同形狀的積木中，較早生成的那一個
        if (transform.position.x + W <= -9.6 || transform.position.x - W >= 9.6)
        {
            Twin.name = ChangeName;
            Twin.tag = "Blocks";

            Destroy(gameObject);
        }


        //積木從右邊出界，在左邊生成新積木
        if (transform.position.x + W >= 9.6f && Twin == null)
        {
            Twin = Instantiate(gameObject, new Vector3(-9.6f - W, transform.position.y, transform.position.z), transform.rotation, transform.parent);

            Twin.name = gameObject.name + "1";
            Twin.tag = "Clone";
        }
        //積木從左邊出界，在右邊生成新積木
        if (transform.position.x - W <= -9.6f && Twin == null)
        {
            Twin = Instantiate(gameObject, new Vector3(9.6f + W, transform.position.y, transform.position.z), transform.rotation, transform.parent);

            Twin.tag = "Clone";
            Twin.name = gameObject.name + "1";
        }


        //if (Input.GetMouseButtonDown(1) && !GameObject.Find("ViewUI"))
        //{
        //    PlayerMove.Climbable = false;  //防止移動積木時觸發玩家攀爬

        //    hit = Physics2D.Raycast(ray.origin, ray.direction, 10, 1 << LayerMask.NameToLayer("Floor"));

        //    if (hit.collider != null && hit.collider.GetComponentInParent<MoveFloor>().gameObject == gameObject)
        //    {
        //        MousePositionX = posX;
        //        FloorMove = true;
        //    }
        //}

        //if (Input.GetMouseButton(1) && FloorMove == true && !hit.collider.GetComponent<MoveFloor>().KidIsHere)
        //{
        //    FloorMoving();
        //}
        //else if (Input.GetMouseButtonUp(1))
        //{
        //    FloorMove = false;
        //}


        if (Input.GetMouseButtonDown(1) && !GameObject.Find("ViewUI"))
        {
            PlayerMove.Climbable = false;  //防止移動積木時觸發玩家攀爬

            hit = Physics2D.Raycast(ray.origin, ray.direction, 10, 1 << LayerMask.NameToLayer("Floor"));

            if (hit.collider != null && hit.collider.GetComponentInParent<MoveFloor>().gameObject == gameObject && !KidIsHere)
            {
                FloorMove = true;
            }
        }

        Debug.Log(hit.collider.GetComponent<MoveFloor>().KidIsHere);

        if (Input.GetMouseButton(1) && FloorMove == true /*&& !KidIsHere*/)
        {
            if (KidIsHere || isKid_At_Left || isKid_At_Right)
            {
                FloorMove = false;
            }
            else
            {
                MousePositionX = worldPosition.x;
            }

            FloorMoving();
        }
    }

    float MousePositionX;
    float BlockSpeed = 2f;

    public void FloorMoving()
    {
        //if (hit.collider != null && hit.collider.GetComponentInParent<MoveFloor>().gameObject == gameObject && !KidIsHere)
        //{
        //    Sequence ClimbAnimation = DOTween.Sequence()
        //   .Append(transform.DOMoveX(MousePositionX, Mathf.Abs(transform.position.x - MousePositionX) / BlockSpeed));
        //}
        //if(hit.collider.GetComponent<MoveFloor>().KidIsHere|| KidIsHere || isKid_At_Left || isKid_At_Right)
        //{
        // transform.DOPause();
        //}

        //transform.position = transform.position +  new Vector3(MousePositionX - transform.position.x, 0,0) / BlockSpeed * Time.deltaTime;


        if (Twin != null && !Twin.GetComponent<MoveFloor>().KidIsHere && !KidIsHere)
        {
            if ((isKid_At_Left || Twin.GetComponent<MoveFloor>().isKid_At_Left) && Input.GetAxis("Mouse X") > 0) // 小孩在積木左邊，滑鼠只能往右拖曳
            {
                FloorMove = true;

                hit.transform.position = transform.position + new Vector3(MousePositionX - transform.position.x, 0, 0) / BlockSpeed * Time.deltaTime;
                Twin.transform.position = Twin.transform.position + new Vector3(MousePositionX - transform.position.x, 0, 0) / BlockSpeed * Time.deltaTime;
            }
            else if ((isKid_At_Right || Twin.GetComponent<MoveFloor>().isKid_At_Right) && Input.GetAxis("Mouse X") < 0) // 小孩在積木右邊，滑鼠只能往左拖曳
            {
                FloorMove = true;

                hit.transform.position = transform.position + new Vector3(MousePositionX - transform.position.x, 0, 0) / BlockSpeed * Time.deltaTime;
                Twin.transform.position = Twin.transform.position + new Vector3(MousePositionX - transform.position.x, 0, 0) / BlockSpeed * Time.deltaTime;
            }
            else if (!isKid_At_Left && !isKid_At_Right && !Twin.GetComponent<MoveFloor>().isKid_At_Right && !Twin.GetComponent<MoveFloor>().isKid_At_Left)
            {
                hit.transform.position = transform.position + new Vector3(MousePositionX - transform.position.x, 0, 0) / BlockSpeed * Time.deltaTime;
                Twin.transform.position = Twin.transform.position + new Vector3(MousePositionX - transform.position.x, 0, 0) / BlockSpeed * Time.deltaTime;
            }
        }

        else if (Twin == null && !KidIsHere)
        {
            if (isKid_At_Left && Input.GetAxis("Mouse X") > 0 && !KidIsHere) // 小孩在積木左邊，滑鼠只能往右拖曳
            {
                FloorMove = true;

                hit.transform.position = transform.position + new Vector3(MousePositionX - transform.position.x, 0, 0) / BlockSpeed * Time.deltaTime;
            }

            else if (isKid_At_Right && Input.GetAxis("Mouse X") < 0 && !KidIsHere) // 小孩在積木右邊，滑鼠只能往左拖曳
            {
                FloorMove = true;

                hit.transform.position = transform.position + new Vector3(MousePositionX - transform.position.x, 0, 0) / BlockSpeed * Time.deltaTime;
            }

            else if (!isKid_At_Left && !isKid_At_Right)
            {
                hit.transform.position = transform.position + new Vector3(MousePositionX - transform.position.x, 0, 0) / BlockSpeed * Time.deltaTime;
            }
        }
        else
        {
            FloorMove = false;
        }


        //if (Twin != null && !Twin.GetComponent<MoveFloor>().KidIsHere && !KidIsHere)
        //{
        //    if ((isKid_At_Left || Twin.GetComponent<MoveFloor>().isKid_At_Left) && Input.GetAxis("Mouse X") > 0) // 小孩在積木左邊，滑鼠只能往右拖曳
        //    {
        //        hit.transform.position = transform.position + new Vector3(Input.GetAxis("Mouse X") * BlockSpeed, 0, 0);
        //        Twin.transform.position = Twin.transform.position + new Vector3(Input.GetAxis("Mouse X") * BlockSpeed, 0, 0);

        //        hit.transform.position = transform.position + new Vector3(Input.GetAxis("Mouse X") * BlockSpeed, 0, 0);
        //        Twin.transform.position = Twin.transform.position + new Vector3(Input.GetAxis("Mouse X") * BlockSpeed, 0, 0);
        //    }

        //    else if ((isKid_At_Right || Twin.GetComponent<MoveFloor>().isKid_At_Right) && Input.GetAxis("Mouse X") < 0) // 小孩在積木右邊，滑鼠只能往左拖曳
        //    {
        //        hit.transform.position = transform.position + new Vector3(Input.GetAxis("Mouse X") * BlockSpeed, 0, 0);
        //        Twin.transform.position = Twin.transform.position + new Vector3(Input.GetAxis("Mouse X") * BlockSpeed, 0, 0);
        //    }

        //    else if (!isKid_At_Left && !isKid_At_Right && !Twin.GetComponent<MoveFloor>().isKid_At_Right && !Twin.GetComponent<MoveFloor>().isKid_At_Left)
        //    {
        //        hit.transform.position = transform.position + new Vector3(Input.GetAxis("Mouse X") * BlockSpeed, 0, 0);
        //        Twin.transform.position = Twin.transform.position + new Vector3(Input.GetAxis("Mouse X") * BlockSpeed, 0, 0);
        //    }
        //}

        //else if (Twin == null && !KidIsHere)
        //{
        //    if (isKid_At_Left && Input.GetAxis("Mouse X") > 0 && !KidIsHere) // 小孩在積木左邊，滑鼠只能往右拖曳
        //    {
        //        hit.transform.position = transform.position + new Vector3(Input.GetAxis("Mouse X") * BlockSpeed, 0, 0);
        //    }

        //    else if (isKid_At_Right && Input.GetAxis("Mouse X") < 0 && !KidIsHere) // 小孩在積木右邊，滑鼠只能往左拖曳
        //    {
        //        hit.transform.position = transform.position + new Vector3(Input.GetAxis("Mouse X") * BlockSpeed, 0, 0);
        //    }

        //    else if (!isKid_At_Left && !isKid_At_Right)
        //    {
        //        hit.transform.position = transform.position + new Vector3(Input.GetAxis("Mouse X") * BlockSpeed, 0, 0);
        //    }
        //}
        //else { }
    }





    /*
    第一種作法是剛剛網路上看到的。積木開始移動的位置跟後來的位置，中間用設限偵測是否有角色，有的話則設定積木的位置回去。
    第二種是限制積木的移動速度，有一個最高速的限制。 
    第三種是積木能夠知道玩家的位置，並用它自身的位置跟玩家的位置，判斷出他能移動的範圍，如果超過那個範圍，積木就只能停在邊緣、不能再被移動。

    剛剛有點想取巧的試給積木加上Rigidbody、改成在FixedUpdate判斷沒有用，所以我想得要用上面三種中的一種了。
    */





    #region 筆記
    //bool若寫成靜態變數
    //就算把程式掛到不同物件上
    //還是會用到同樣的、唯一一個的bool值 
    #endregion


}


