using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveChair : MonoBehaviour
{
    RaycastHit2D hit;
    Vector3 worldPosition;
    public bool FloorMove = false;
    public float posX, posY; // 用來接收滑鼠點擊座標的X、Y值
    public GameObject Twin; // 雙胞胎積木 (另一個一樣的物件 
    Ray ray;

    float W; // 積木的一半寬度 (用來判斷積木有沒有超出可視範圍、設定新積木生成位置
    string ChangeName; //沒有編號的積木名稱 (沒有編號 = 雙生積木中較早生成的


    void Start()
    {
        W = GetComponent<PolygonCollider2D>().bounds.extents.x;
        ChangeName = string.Join("", gameObject.name.Split('1'));
    }

    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        posX = worldPosition.x;
        posY = worldPosition.y;


        //將兩個一樣形狀的積木 設成彼此的Twin
        if (gameObject.tag == "Blocks")
        {
            Twin = GameObject.Find(gameObject.name + "1");
        }
        else if (gameObject.tag == "Clone")
        {
            Twin = GameObject.Find(ChangeName);
        }


        if(Twin == null)
        {
            //積木從左邊出界，在右邊生成新積木
            if (transform.position.x - W <= -9.6f)
            {
                Twin = Instantiate(gameObject, new Vector3(/*gameObject.transform.position.x +*/ 9.6f+W, transform.position.y, transform.position.z), transform.rotation, transform.parent);
                Twin.name = gameObject.name + "1";
                Twin.tag = "Clone";
                Twin.GetComponent<MoveChair>().Twin = gameObject;
            }
            //積木從右邊出界，在左邊生成新積木
            else if (transform.position.x + W > 9.6f)
            {
                Twin = Instantiate(gameObject, new Vector3(/*gameObject.transform.position.x*/ - 9.6f-W, transform.position.y, transform.position.z), transform.rotation, transform.parent);
                Twin.name = gameObject.name + "1";
                Twin.tag = "Clone";
                Twin.GetComponent<MoveChair>().Twin = gameObject;
            }
        }
        

        //積木超出可視範圍就刪除  刪除前先確保Twin的tag為Blocks，且名字沒有編號 (因為它將會是兩個同形狀的積木中，較早生成的那一個
        if ((transform.position.x + W < -9.6f || transform.position.x - W > 9.6) )
        {
            Twin.name = ChangeName;
            Twin.tag = "Blocks";
            Destroy(gameObject);
        }



        ////移動室內牆面、地板
        //if (Input.GetMouseButtonDown(1) && !GameObject.Find("ViewUI"))
        //{
        //    hit = Physics2D.Raycast(ray.origin, ray.direction, 10, 1 << LayerMask.NameToLayer("Floor"));
        //    FloorMove = true;
        //    if (hit.collider.GetComponentInParent<MoveChair>().gameObject == gameObject)
        //    {
        //        //for (int i = 0; i < FindObjectsOfType<MoveChair>().Length; i++)
        //        //{
        //        //    FindObjectsOfType<MoveChair>()[i].transform.position =
        //        //        FindObjectsOfType<MoveChair>()[i].transform.position + new Vector3(Input.GetAxis("Mouse X") * BlockSpeed, 0, 0);
        //        //}

        //        FloorMove = true;
        //    }
        //    else if (hit.collider.GetComponent<MoveWall>().gameObject == GameObject.Find("Floor_Three") || hit.collider.GetComponent<MoveWall>().gameObject == GameObject.Find("Floor_Three1"))
        //    {
        //        //gameObject.transform.position = transform.position + new Vector3(Input.GetAxis("Mouse X") * BlockSpeed, 0, 0);

        //        FloorMove = true;
        //    }
        //}

        if (Input.GetMouseButtonDown(1) && !GameObject.Find("ViewUI"))
        {
            hit = Physics2D.Raycast(ray.origin, ray.direction, 10, 1 << LayerMask.NameToLayer("Floor"));
            

            if (hit.collider != null && hit.collider.GetComponent<MoveChair>() )
            {
                Debug.Log(hit.collider.name);
                //gameObject.transform.position = transform.position + new Vector3(Input.GetAxis("Mouse X") * BlockSpeed, 0, 0);

                 FloorMove = true;
            }
            else if (hit.collider.name == "Floor_Three" || hit.collider.name == "Floor_Three1")
            {
                Debug.Log("LLLL");
                //gameObject.transform.position = transform.position + new Vector3(Input.GetAxis("Mouse X") * BlockSpeed, 0, 0);

                FloorMove = true;
            }
        }


        if (Input.GetMouseButton(1) && FloorMove == true)
        {
            Debug.Log("555");
            FloorMoving();
        }

        if (Input.GetMouseButtonUp(1))
        {
            Debug.Log("XXX");

            FloorMove = false;
        }
    }

    float BlockSpeed = 0.15f;
    public void FloorMoving()
    {
        Debug.Log("???");

        if (hit.collider.name == "Floor_Three"|| hit.collider.name == "Floor_Three1")
        {
            Debug.Log("!!!");
            for (int i = 0; i < FindObjectsOfType<MoveChair>().Length; i++)
            {
                FindObjectsOfType<MoveChair>()[i].transform.position =
                    FindObjectsOfType<MoveChair>()[i].transform.position + new Vector3(Input.GetAxis("Mouse X") * BlockSpeed, 0, 0);
            }
        }

        //FindObjectsOfType<MoveChair>()[0].transform.position =
        //                FindObjectsOfType<MoveChair>()[0].transform.position + new Vector3(Input.GetAxis("Mouse X") * BlockSpeed, 0, 0);
        //if (Twin != null)
        //{
        //    FindObjectsOfType<MoveChair>()[1].transform.position =
        //            FindObjectsOfType<MoveChair>()[1].transform.position + new Vector3(Input.GetAxis("Mouse X") * BlockSpeed, 0, 0);
        //}

        else
        {
            hit.transform.position = transform.position + new Vector3(Input.GetAxis("Mouse X") * BlockSpeed, 0, 0);
            Twin.transform.position = Twin.transform.position + new Vector3(Input.GetAxis("Mouse X") * BlockSpeed, 0, 0);
        }

        //for (int i = 0; i < FindObjectsOfType<MoveChair>().Length; i++)
        //{
        //    FindObjectsOfType<MoveChair>()[i].transform.position =
        //        FindObjectsOfType<MoveChair>()[i].transform.position + new Vector3(Input.GetAxis("Mouse X") * BlockSpeed, 0, 0);
        //}

        // FindObjectsOfType<MoveChair>()[0].transform.position = FindObjectOfType<MoveChair>().gameObject.transform.position + new Vector3(Input.GetAxis("Mouse X") * BlockSpeed, 0, 0);

        //if (hit.collider.GetComponentInParent<MoveWall>().gameObject == GameObject.Find("Floor_Three"))
        //{
        //    gameObject.transform.position = transform.position + new Vector3(Input.GetAxis("Mouse X") * BlockSpeed, 0, 0);
        //}

        //else if (hit.collider != null && (hit.collider.GetComponentInParent<MoveWall>().gameObject == gameObject))
        //{
        //    gameObject.transform.position = transform.position + new Vector3(Input.GetAxis("Mouse X") * BlockSpeed, 0, 0);
        //    Twin.transform.position = Twin.transform.position + new Vector3(Input.GetAxis("Mouse X") * BlockSpeed, 0, 0);
        //}


        //if (Twin != null)
        //{
        //    if ((!Twin.GetComponent<MoveWall>().KidIsHere) || (transform.position.y + GetComponent<BoxCollider2D>().size.y > GameObject.Find("Player").transform.position.y && Twin.transform.position.y + GetComponent<BoxCollider2D>().size.y > GameObject.Find("Player").transform.position.y))
        //    {
        //        hit.transform.position = transform.position + new Vector3(Input.GetAxis("Mouse X") * BlockSpeed, 0, 0);
        //        Twin.transform.position = Twin.transform.position + new Vector3(Input.GetAxis("Mouse X") * BlockSpeed, 0, 0);
        //    }
        //}

        //else if (Twin == null)
        //{
        //    if (transform.position.y + GetComponent<BoxCollider2D>().size.y > GameObject.Find("Player").transform.position.y)
        //    {
        //        hit.transform.position = transform.position + new Vector3(Input.GetAxis("Mouse X") * BlockSpeed, 0, 0);
        //    }
        //}
    }

}
