using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clue : MonoBehaviour
{
    bool Touch = false;  // 角色靠近物品才能開啟檢視視窗

    bool PropsCanTouch = false; // 遮擋到物品的名單是否是空的 (是空的才能開啟檢視視窗
    int A = 0;  // 遮擋名單上的數量
    public string[] TriggerGameObject;  // 用來記錄 有誰遮擋到 程式掛載的這個物件 

    public GameObject ClueInUI;  // 物件在檢視視窗的樣子

    Vector3 PlayerVt3;


    void Start()
    {
        TriggerGameObject = new string[6];  //TODO 應該用不到這麼多 以防萬一 先設六格 比較保險
    }

    
    #region 遮擋名單增加及刪除
    // 增加名單
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<MoveFloor>())
        {
            TriggerGameObject[A] = collision.gameObject.name;
            A++;
            PropsCanTouch = false;
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
    #endregion
    
    
    void Update()
    {
        PlayerVt3 = GameObject.Find("Player").transform.position;

        //遮擋名單是空的 可以點擊物品        
        PropsCanTouch = A == 0;

        // 物品在一定距離及高度範圍內才能拿
        Touch = Mathf.Abs(transform.position.x - PlayerVt3.x) < 1f && Mathf.Abs(transform.position.y - PlayerVt3.y) < 1.3f;
    }
    
    
    private void OnMouseDown()
    {
        // 顯示檢視視窗 (用於場景物件 未被遮擋且在一定距離內
        if (gameObject.tag == "CannotBeObscured")
        {
            if (PropsCanTouch && Touch)
            {
                ClueInUI.transform.parent = GameObject.Find("ClueUI").transform.GetChild(0).transform;
                ClueInUI.SetActive(true);
            }
        }
        // 顯示檢視視窗 (用於檢視視窗中的物件 沒有遮擋、距離限制
        else if (gameObject.tag == "ClueInUI")
        {
                ClueInUI.transform.parent = GameObject.Find("ClueUI").transform.GetChild(0).transform;
                ClueInUI.SetActive(true);
        }
        // 顯示檢視視窗 (其他例外狀況 在距離內都能點擊
        else
        {
            if (Touch)
            {
                ClueInUI.transform.parent = GameObject.Find("ClueUI").transform.GetChild(0).transform;
                ClueInUI.SetActive(true);
            }
        }
    }
}
