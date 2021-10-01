using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkToGetProps : MonoBehaviour //Todo 應該寫成 到哪句對話後 顯示道具
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public GameObject Z;

    // Update is called once per frame
    void Update()
    {
        //if (/*到哪個對話*/)
        //{
        Instantiate(Z, GameObject.Find("Block_One").transform.position, transform.rotation, GameObject.Find("Block_One").transform);
        //}
    }
}
