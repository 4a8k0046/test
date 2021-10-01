using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ElevatorMove : MonoBehaviour
{
    int UpOrDown = 1;  //改變此變數的正負 控制自動升降機要上升還是下降
    
    void FixedUpdate()
    {
        if (transform.position.y <= GameObject.Find("Floor_Two").transform.position.y && transform.position.y >= GameObject.Find("Floor_One").transform.position.y+0.2f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + UpOrDown * Time.deltaTime, transform.position.z);
        }
        else
        {
            UpOrDown *= -1;
            transform.position = new Vector3(transform.position.x, transform.position.y + UpOrDown * Time.deltaTime, transform.position.z);
        }
    }
}
