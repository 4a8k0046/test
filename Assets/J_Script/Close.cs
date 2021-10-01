using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Close : MonoBehaviour
{
    private void OnMouseDown()
    {
        //Destroy(GameObject.Find("ViewUI").transform.GetChild(0).gameObject);
        //GameObject.Find("ViewUI").transform.GetChild(0).gameObject.SetActive(false);
        //GameObject.Find("ViewUI").transform.GetChild(0).transform.parent = GameObject.Find("UI").transform;
        //gameObject.transform.parent.gameObject.SetActive(false);

        gameObject.transform.parent.transform.parent = GameObject.Find("UI").transform; 
        
        gameObject.transform.parent.gameObject.SetActive(false);
    }
}
