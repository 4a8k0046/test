using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropInBackpack : MonoBehaviour
{

    public static GameObject WhichIsUseing; // 讓場景物件取得 現在選取的是哪個物品欄物件
    public Material OutLine;  // 外框 
    public Material Original; // 沒外框
    bool PlayerIsUseing = false;  //是否為選取狀態


    // 使用物品
    void Update()
    {
        if(PlayerIsUseing == true)
        {
            WhichIsUseing = gameObject;
        }
    }


    private void OnMouseDown()
    {
        //若選中的物品本來就在發光 讓它不發光
        if (PlayerIsUseing)
        {
            PlayerIsUseing = false;

            gameObject.GetComponent<CanvasRenderer>().SetMaterial(Original, 0);

            WhichIsUseing = null;
        }

        // 只讓選中的物品發光 
        else
        {
            for (int i = 0; i < Backpack.BackpackSpace.Length; i++)
            {
                // 先關掉所有物品欄內物件的光
                if (Backpack.BackpackSpace[i].transform.childCount != 0)
                {
                    GameObject BackpackSpace = Backpack.BackpackSpace[i].transform.GetChild(0).gameObject;

                    BackpackSpace.GetComponentInChildren<PropInBackpack>().PlayerIsUseing = false;
                    
                    BackpackSpace.GetComponentInChildren<CanvasRenderer>().SetMaterial(Original, 0);
                }
                //如果物品欄沒有子物件 表示後面都空了 不用繼續迴圈
                else
                {
                    break;
                }
            }

            PlayerIsUseing = true;

            gameObject.GetComponent<CanvasRenderer>().SetMaterial(OutLine, 0);
        }
    }
}
