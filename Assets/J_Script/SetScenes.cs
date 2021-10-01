using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetScenes: MonoBehaviour  //控制室內外場景顯示、關閉
{
    public static GameObject Outside ,Indside ;
    
    void Start()
    {
        Outside = transform.Find("1").gameObject;
        Indside = transform.Find("2").gameObject;
    }
}
