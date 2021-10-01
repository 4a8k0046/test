using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueUI : MonoBehaviour
{
    public static GameObject ViewUI;  // 檢視視窗
    
    void Start()
    {
        ViewUI = gameObject.transform.Find("ViewUI").gameObject;
    }

    void Update()
    {
        // 檢視視窗有東西才會顯示
        if (ViewUI.transform.childCount !=0 )
        {
            ViewUI.SetActive(true);
        }
        else
        {
            ViewUI.SetActive(false);
        }
    }
}

