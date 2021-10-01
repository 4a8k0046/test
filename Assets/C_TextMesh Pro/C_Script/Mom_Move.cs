using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mom_Move : MonoBehaviour
{
    public GameObject Dad;//宣告 公螳螂物件
    public GameObject Mom;//宣告 母螳螂物件
    float m_Speed = 0.3f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D MomMove) 
    {
        Vector3 position = Dad.GetComponent<Transform>().position;
        Debug.LogError("Dad_position");
        Vector3 position2 = Mom.GetComponent<Transform>().position;
        Debug.LogError("Mom_position");

        if (MomMove.gameObject.tag == "Floor_2" && position2.x < position.x-1.5f)
        {
            Debug.LogError("move_mom");
            Mom.transform.Translate(Vector3.right* m_Speed,  Space.World);
            Debug.LogError("0");

        }
        else
        {
            Debug.LogError(gameObject.name);
            Debug.LogError(gameObject.tag);
        }

    }

}
