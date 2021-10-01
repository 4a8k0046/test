using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenDoor : MonoBehaviour  //用於判斷門是否被遮擋
{

    public string[] TriggerGameObject;
    int A = 0;


    void Start()
    {
        TriggerGameObject = new string[6];
    }
    
    void Update()
    {
        if (A == 0)
        {
            Doors.DoorCanUse = true;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<MoveFloor>())
        {
            TriggerGameObject[A] = collision.gameObject.name;
            A++;
            Doors.DoorCanUse = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        for(int i=0; i<TriggerGameObject.Length; i++)
        {
            if(TriggerGameObject[i] == collision.name)
            {
                for(int j=i+1; j<TriggerGameObject.Length; j++)
                {
                    TriggerGameObject[i] = TriggerGameObject[j];
                    i++;
                }
                A--;
            }
        }
    }
}
