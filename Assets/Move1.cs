using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Move1 : MonoBehaviour
{
    public bool useRigidbody = false;
    public Rigidbody2D rb2D;

    Vector3 startpos;
    private void Start()
    {
        startpos = transform.position;
    }
    void Update()
    {
        if (useRigidbody)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                rb2D.velocity = new Vector2(1, 0);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                rb2D.velocity = new Vector2(-1, 0);
            }
            else
            {
                rb2D.velocity = new Vector2(0, 0);
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                //transform.Translate(new Vector2(1, 0) * Time.deltaTime);
                transform.DOMove(startpos+new Vector3 (6,0,0),1f).SetEase(Ease.Linear);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                //transform.Translate(new Vector2(-1, 0) * Time.deltaTime);
                transform.DOMove(startpos - new Vector3(6, 0, 0), 1f).SetEase(Ease.Linear);
            }
            else
            {

            }
        }
    }
}
