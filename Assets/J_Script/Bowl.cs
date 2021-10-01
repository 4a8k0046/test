using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bowl : MonoBehaviour
{
    bool Touch = false;  // 角色靠近物品才能讓物品消失 (完成拿取動作
    Vector3 PlayerVt3;


    void Start()
    {

    }


    void Update()
    {
        PlayerVt3 = GameObject.Find("Player").transform.position;
        Touch = Mathf.Abs(transform.position.x - PlayerVt3.x) < 1f && Mathf.Abs(transform.position.y - PlayerVt3.y) < 1f;
    }


    private void OnMouseDown()
    {
        Debug.Log("touch");
        if (Touch && PropInBackpack.WhichIsUseing)
        {
            Debug.Log("close ");
            if (PropInBackpack.WhichIsUseing.name == "FodderInBackpack")
            {
                Debug.Log("use prop");
                transform.parent.Find("BowlWithFodder").gameObject.SetActive(true);
                Destroy(PropInBackpack.WhichIsUseing);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameObject.Find("BowlWithFodder") && collision.name == "Birdcage")
        {
            //todo 餵鳥對話 & 動畫....設定?
            Debug.Log("eat");
            Destroy(GameObject.Find("BowlWithFodder"));
        }
    }
}
