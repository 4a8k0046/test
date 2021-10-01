using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backpack : MonoBehaviour
{

    public static GameObject[] BackpackSpace;  // 物品欄們


    private void Awake()
    {
        BackpackSpace = new GameObject[6];  // TODO 應該用不到這麼多 先訂六格
    }


    void Start()
    {
        //  自動抓取所有物品欄
        for(int i= 0; i<BackpackSpace.Length; i++)
        {
            BackpackSpace[i] = transform.Find(gameObject.name + i).gameObject;
        }
    }
    

    void Update()
    {
        // 物品欄裡有東西才會顯示
        for (int i =0; i<BackpackSpace.Length; i++)
        {
            if(BackpackSpace[i].transform.childCount != 0)
            {
                BackpackSpace[i].SetActive(true);
            }
            else
            {
                BackpackSpace[i].SetActive(false);

                if (i + 1 < BackpackSpace.Length && BackpackSpace[i + 1].transform.childCount != 0)
                {
                    BackpackSpace[i + 1].transform.GetChild(0).transform.position = BackpackSpace[i].transform.position;
                    BackpackSpace[i + 1].transform.GetChild(0).SetParent(BackpackSpace[i].transform);
                }
            }
        }
    }
}
