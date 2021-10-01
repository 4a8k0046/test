using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour
{
    public Button button;
    public GameObject Talk_toSomeOne;

    void Start()
    {
        button.onClick.AddListener(btn_Tigger);
    }

    void btn_Tigger()

    {
        if (!gameObject.activeInHierarchy)
        { gameObject.SetActive(true); }
        else
        { gameObject.SetActive(false); } 
    }
} 
