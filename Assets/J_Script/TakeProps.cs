using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeProps : MonoBehaviour  // 判斷道具是否被遮擋
{

    public string[] TriggerGameObject;  // 用來記錄 有誰遮擋到 程式掛載的這個物件 
    int A = 0;


    void Start()
    {
        TriggerGameObject = new string[6];  //TODO 應該用不到這麼多 以防萬一 先設六格 比較保險
    }
    

    // 增加名單
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<MoveFloor>())
        {
            TriggerGameObject[A] = collision.gameObject.name;
            A++;
            Props.PropsCanTouch = false;
        }
    }
    // 從名單刪除
    private void OnTriggerExit2D(Collider2D collision)
    {
        for (int i = 0; i < TriggerGameObject.Length; i++)
        {
            if (TriggerGameObject[i] == collision.name)
            {
                for (int j = i + 1; j < TriggerGameObject.Length; j++)
                {
                    TriggerGameObject[i] = TriggerGameObject[j];
                    i++;
                }
                A--;
            }
        }
    }

    private void Update()
    {
        if (A == 0)  // 遮擋名單是空的 可以點擊物品
        {
            Props.PropsCanTouch = true;
        }
    }
}
