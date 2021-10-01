using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Doors : MonoBehaviour
{
    Vector3 PlayerVt3; // 玩家座標
    public static bool DoorCanUse = false;  //是否有積木遮擋門口
    bool TryToOpen = false;  //玩家是否點擊門 (嘗試開門 (大門用
    bool Touch = false;  // 角色是否靠近門
    bool Disappear = false;  // 物件是否要漸漸消失 (慢慢變透明後刪除
    Vector3 BlueStartingPoint; // 藍門初始位置
    int BlueDoorDirection = -1; // 藍色門移動方向
    //int SceneNumber;  // House_Door專用 (切換室內外場景


    private void Start()
    {
        //SceneNumber = SceneManager.GetActiveScene().buildIndex;
        PlayerVt3 = new Vector3(transform.position.x, PlayerVt3.y, PlayerVt3.z); //確保角色初始位置
        BlueStartingPoint = GameObject.Find("Blue_Door").transform.position;
    }


    private void OnMouseDown()
    {
        TryToOpen = true;

        // 執行解鎖動作
        if (Touch && PropInBackpack.WhichIsUseing)
        {
            if (gameObject.name == "Red_Door" && PropInBackpack.WhichIsUseing.name == "RoomKeyInBackpack")
            {
                gameObject.transform.Find("Lock").gameObject.SetActive(false);
                Destroy(PropInBackpack.WhichIsUseing);
            }

            if (gameObject.name == "Double_Door" && PropInBackpack.WhichIsUseing.name == "DoubleKeyInBackpack")
            {
                gameObject.transform.Find("Lock").gameObject.SetActive(false);
                Destroy(PropInBackpack.WhichIsUseing);
            }

            if (gameObject.name == "House_Door" && PropInBackpack.WhichIsUseing.name == "HouseKeyInBackpack")
            {
                gameObject.transform.Find("Lock").gameObject.SetActive(false);
                Destroy(PropInBackpack.WhichIsUseing);
            }

            if (gameObject.name == "Cabinet" && PropInBackpack.WhichIsUseing.name == "CabinetKeyInBackpack")
            {
                gameObject.transform.parent.transform.Find("Cabinet_Open").gameObject.SetActive(true);
                gameObject.SetActive(false);
                Destroy(PropInBackpack.WhichIsUseing);
            }

            if (gameObject.name == "Birdcage" && PropInBackpack.WhichIsUseing.name == "BirdcageKeyInBackpack")
            {
                GameObject.Find("UI").transform.Find("WindowInUI").transform.Find("RedHandle").gameObject.SetActive(true);
                transform.parent.Find("EmptyBirdcage").gameObject.SetActive(true);
                transform.parent.Find("Bird").gameObject.SetActive(true);
                Destroy(PropInBackpack.WhichIsUseing);
                Destroy(gameObject);
            }
        }

        if (gameObject.name == "BoxOpen") // 保險箱半開
        {
            transform.parent.Find("BoxNull").gameObject.SetActive(true);
            Destroy(gameObject);
        }

        else if (Touch && gameObject.name == "Drawer") // 櫃子下方蓋子 
        {
            Disappear = true;
            transform.parent.Find("SecurityBox").gameObject.SetActive(true);
        }
    }
    private void OnMouseExit()
    {
        TryToOpen = false;
    }


    private void Update()
    {
        PlayerVt3 = GameObject.Find("Player").transform.position;
        Touch = Mathf.Abs(transform.position.x - PlayerVt3.x) < 1f && Mathf.Abs(transform.position.y - PlayerVt3.y) < 1f;

        if (gameObject.name == "BoxClose") // 未解鎖保險箱
        {
            if (transform.Find("Button").Find("Green").Find("Num").GetComponent<TMP_Text>().text == "1" &&
               transform.Find("Button").Find("Blue").Find("Num").GetComponent<TMP_Text>().text == "1" &&
               transform.Find("Button").Find("Red").Find("Num").GetComponent<TMP_Text>().text == "3")
            {
                transform.parent.Find("BoxOpen").gameObject.SetActive(true);
                Destroy(gameObject);
            }
        }

        if (Disappear)
        {
            float Alhpa = GetComponent<SpriteRenderer>().color.a;

            if (Alhpa <= 0)
            {
                Destroy(gameObject);
                Disappear = false;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, Alhpa - 0.3f * Time.deltaTime);
            }
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        // 執行 "大門" 開門後的動作 (室內外場景切換
        if (collision.gameObject.name == "Player" && DoorCanUse && TryToOpen)
        {
            Debug.Log("1");
            if (gameObject.name == "House_Door")
            {
                Debug.Log("2");

                if (SetScenes.Outside.activeSelf == true)
                {
                    Debug.Log("3");

                    SetScenes.Indside.SetActive(true);
                    SetScenes.Outside.SetActive(false);
                    TryToOpen = false;
                }
                else
                {
                    Debug.Log("4");

                    SetScenes.Indside.SetActive(false);
                    SetScenes.Outside.SetActive(true);
                    TryToOpen = false;
                }
                //SceneManager.LoadScene(Mathf.Abs(SceneNumber - 1));
                PlayerVt3 = new Vector3(transform.position.x, PlayerVt3.y, PlayerVt3.z); //確保角色初始位置
            }
        }

        // 紅藍門接觸並等高 => 變合併門
        if (gameObject.name == "Red_Door" && collision.name == "Blue_Door" && transform.position.y >= collision.transform.position.y)
        {
            transform.parent.Find("Double_Door").gameObject.SetActive(true);
            //GameObject.Find("Chair").layer = 8;
            //GameObject.Find("ClueUI").gameObject.GetComponent<BoxCollider2D>().enabled = false;
            GameObject.Find("UI").transform.Find("DoubleBedInUI").Find("Mom_Diary").gameObject.SetActive(true);
            GameObject.Find("UI").transform.Find("DoubleBedInUI").Find("PaperBall").gameObject.SetActive(false);
            Destroy(gameObject);
            Destroy(collision.gameObject);
            GameObject.Find("DoorButton").gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }

        // 藍門升降按鈕
        if (gameObject.name == "DoorButton" && (collision.name == "Chair" || collision.name == "Player"))
        {
            GameObject BlueDoor = GameObject.Find("Blue_Door");
            GameObject RedDoor = GameObject.Find("Red_Door");

            // 門升降中不可移動椅子或角色 防止門升降到一半
            GameObject.Find("Chair").layer = 0;
            GameObject.Find("ClueUI").gameObject.GetComponent<BoxCollider2D>().enabled = true;

            BlueDoor.transform.position = BlueDoor.transform.position + new Vector3(0, 1f * BlueDoorDirection, 0) * Time.deltaTime;

            if (BlueDoor.transform.position.y <= RedDoor.transform.position.y)
            {
                BlueDoorDirection = 1;
            }

            if (BlueDoor.transform.position.y > BlueStartingPoint.y)
            {
                BlueDoorDirection = -1;
                BlueDoor.transform.position = BlueDoor.transform.position + new Vector3(0, -BlueDoor.transform.position.y + BlueStartingPoint.y, 0);
                collision.transform.position = new Vector3(gameObject.transform.position.x + gameObject.GetComponent<BoxCollider2D>().size.x+ collision.gameObject.GetComponent<BoxCollider2D>().size.x, collision.transform.position.y , collision.transform.position.z);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (gameObject.name == "DoorButton" && (collision.name == "Chair" || collision.name == "Player"))
        {
            GameObject.Find("Chair").layer = 8;
            GameObject.Find("ClueUI").gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
