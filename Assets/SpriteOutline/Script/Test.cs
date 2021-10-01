using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       
        //ClimbAnimation.Append(transform.DOMoveY((PlayerMove.contact.y + transform.position.y), 0.2f))/*.Append(transform.DOMoveX(contact.x , 1f))*/;
    }
    Sequence ClimbAnimation = DOTween.Sequence();
    // Update is called once per frame
    void Update()
    {

        ClimbAnimation.Append(transform.DOMoveY((PlayerMove.contact.y + transform.GetComponent<BoxCollider2D>().size.y/2), 0.2f))/*.Append(transform.DOMoveX(contact.x , 1f))*/;
    }
}

