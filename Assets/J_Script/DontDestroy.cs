using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public GameObject Backpack; // 物品欄
    //public GameObject ClueUI; // 檢視視窗

    void Start()
    {
        if (FindObjectOfType<Backpack>() == null)
        {
            Instantiate(Backpack).name = "Backpack";
        }

        //if (GameObject.Find("ClueUI") == null)
        //{
        //    Instantiate(ClueUI).name = "ClueUI";
        //}

        //Backpack = FindObjectOfType<Backpack>().gameObject;

        //DontDestroyOnLoad(ClueUI);
        //Backpack = FindObjectOfType<Backpack>().gameObject;

        DontDestroyOnLoad(Backpack);
    }
}
