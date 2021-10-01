using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BirdMove : MonoBehaviour
{
    Vector3 Door;
    public GameObject Switch;
    Animator am;


    void Start()
    {
        Door = GameObject.Find("House_Door").transform.position;
        am = GetComponent<Animator>();

        if (transform.parent.name == "2")
        {
            am.SetBool("FlyOrNot", true);

            GameObject.Find("ClueUI").gameObject.GetComponent<BoxCollider2D>().enabled = true;

            Sequence ClimbAnimation = DOTween.Sequence()
            .Append(transform.DOMove(Door, 2.5f))
            .Append(transform.DOScale(0, 1));
        }
    }
    
    void Update()
    {
        if(transform.localScale.y == 0)
        {
            GameObject.Find("ClueUI").gameObject.GetComponent<BoxCollider2D>().enabled = false;

            am.SetBool("FlyOrNot", false);
            gameObject.transform.parent = Switch.transform;
            transform.position = Switch.transform.position;
            transform.localScale = new Vector3(1, 1, 0);
        }
    }
}
