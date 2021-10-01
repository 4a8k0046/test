using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Props : MonoBehaviour
{
    public static bool PropsCanTouch = false; // 遮擋到物品的名單是否是空的 (是空的才能點擊並拿取物品
    bool Touch = false;  // 角色靠近物品才能讓物品消失 (完成拿取動作
    Vector3 PlayerVt3;
    public GameObject PropsInBackpack;  // 道具在物品欄中的樣子 (預製物件


    void Update()
    {
        PlayerVt3 = GameObject.Find("Player").transform.position;
        
        Touch = Mathf.Abs(transform.position.x - PlayerVt3.x) < 1f && Mathf.Abs(transform.position.y - PlayerVt3.y) < 1f; 
    }
    

    private void OnMouseDown()
    {
        // 成功點擊場景上的道具後 道具消失、生成預製物件到物品欄
        if(gameObject.tag == "CannotBeObscured")
        {
            if (PropsCanTouch && Touch)
            {
                for (int i = 0; i < 6; i++)
                {
                    if (GameObject.Find("Backpack").transform.Find("Backpack" + i).childCount == 0)
                    {
                        Instantiate(PropsInBackpack, GameObject.Find("Backpack").transform.Find("Backpack" + i).transform).name = gameObject.name + "InBackpack";

                        break;
                    }
                }
                //TODO 測試完記得取消註解 
                gameObject.SetActive(false);
            }
        }

        // 可無視遮擋 只要靠近可直接點擊
        else if(gameObject.tag == "PropInScence")
        {
            if (Touch)
            {
                for (int i = 0; i < 6; i++)
                {
                    if (GameObject.Find("Backpack").transform.Find("Backpack" + i).childCount == 0)
                    {
                        Instantiate(PropsInBackpack, GameObject.Find("Backpack").transform.Find("Backpack" + i).transform).name = gameObject.name + "InBackpack";

                        break;
                    }
                }
                //TODO 測試完記得取消註解 
                gameObject.SetActive(false);
            }
        }

        else // 物件若不在場景中 可直接點擊拿取 
        {
            for (int i = 0; i < 6; i++)
            {
                if (GameObject.Find("Backpack").transform.Find("Backpack" + i).childCount == 0)
                {
                    Instantiate(PropsInBackpack, GameObject.Find("Backpack").transform.Find("Backpack" + i).transform).name = gameObject.name + "InBackpack";

                    break;
                }
            }
            //TODO 測試完記得取消註解 
            gameObject.SetActive(false);
        }
    }
}
