using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWall : MonoBehaviour
{
    RaycastHit2D hit;
    Vector3 worldPosition;
    public bool FloorMove = false;
    public float posX, posY; // 用來接收滑鼠點擊座標的X、Y值
    public GameObject Twin; // 雙胞胎積木 (另一個一樣的物件 

    public bool KidIsHere = false;

    float W; // 積木的一半寬度 (用來判斷積木有沒有超出可視範圍、設定新積木生成位置
    string ChangeName; //沒有編號的積木名稱 (沒有編號 = 雙生積木中較早生成的

    void Start()
    {
        W = GetComponent<PolygonCollider2D>().bounds.extents.x;
        ChangeName = string.Join("", gameObject.name.Split('1'));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            KidIsHere = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            KidIsHere = false;
        }
    }


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


        if (gameObject.tag != "Books")
        {
            if (gameObject.transform.position.x >= 0 && Twin == null)
            {
                Twin = Instantiate(gameObject, new Vector3(transform.position.x - W * 2, transform.position.y, transform.position.z), transform.rotation, transform.parent);
                Twin.name = gameObject.name + "1";
                Twin.tag = "Clone";
            }

            if (gameObject.transform.position.x < 0 && Twin == null)
            {
                Twin = Instantiate(gameObject, new Vector3(transform.position.x + W * 2, transform.position.y, transform.position.z), transform.rotation, transform.parent);
                Twin.tag = "Clone";
                Twin.name = gameObject.name + "1";
            }

            //積木超出可視範圍就刪除  刪除前先確保Twin的tag為Blocks，且名字沒有編號 (因為它將會是兩個同形狀的積木中，較早生成的那一個
            if (transform.position.x + W < -W || transform.position.x - W > W)
            {
                Twin.name = ChangeName;
                Twin.tag = "Blocks";

                Destroy(gameObject);
            }
        }


        //移動室內牆面、地板
        if (Input.GetMouseButtonDown(1) && !GameObject.Find("ViewUI"))
        {
            PlayerMove.Climbable = false;  //防止移動積木時觸發玩家攀爬

            hit = Physics2D.Raycast(ray.origin, ray.direction, 10, 1 << LayerMask.NameToLayer("Floor"));

            if (hit.collider != null && hit.collider.GetComponentInParent<MoveWall>().gameObject == gameObject)
            {
                FloorMove = true;
            }
        }

        //移動書
        else if (Input.GetMouseButtonDown(1) && GameObject.Find("ViewUI"))
        {
            hit = Physics2D.Raycast(ray.origin, ray.direction, 10, 1 << LayerMask.NameToLayer("Books"));

            if (hit.collider != null && hit.collider.GetComponentInParent<MoveWall>().gameObject == gameObject)
            {
                FloorMove = true;
            }
        }

        if (gameObject == Physics2D.Raycast(ray.origin, ray.direction, 10, 1 << LayerMask.NameToLayer("Books")) && Mathf.Abs(hit.transform.position.x - hit.transform.parent.position.x) > 5.8)
        {
            if (hit.transform.tag == "Books")
            {
                hit.transform.position = new Vector3(transform.parent.transform.position.x, 0, 0) * Time.deltaTime;
                Debug.Log(hit.transform.gameObject.name);
            }
        }


        if (Input.GetMouseButton(1) && FloorMove == true)
        {
            FloorMoving();
        }

        if (Input.GetMouseButtonUp(1))
        {
            FloorMove = false;
        }
    }

    float BlockSpeed = 0.15f;
    public void FloorMoving()
    {
        if (Twin != null)
        {
            if ((!KidIsHere && !Twin.GetComponent<MoveWall>().KidIsHere) || (transform.position.y + GetComponent<BoxCollider2D>().size.y > GameObject.Find("Player").transform.position.y && Twin.transform.position.y + GetComponent<BoxCollider2D>().size.y > GameObject.Find("Player").transform.position.y))
            {
                hit.transform.position = transform.position + new Vector3(Input.GetAxis("Mouse X") * BlockSpeed, 0, 0);
                Twin.transform.position = Twin.transform.position + new Vector3(Input.GetAxis("Mouse X") * BlockSpeed, 0, 0);
            }
        }

        else if (Twin == null)
        {
            if (!KidIsHere || transform.position.y + GetComponent<BoxCollider2D>().size.y > GameObject.Find("Player").transform.position.y)
            {
                hit.transform.position = transform.position + new Vector3(Input.GetAxis("Mouse X") * BlockSpeed, 0, 0);
            }
        }
    }
}


