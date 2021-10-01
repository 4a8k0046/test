using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(MoveFloor);
    }

    private Vector3 lastMousePosition = Vector3.zero;
    private float xMin = -7.3f, xMax = 7.3f;
    private float y;
    public static bool MoveFloor = false;
    public Button TalkToMom;

    public void OnMouseDown( Button A)
    {
        if (A == TalkToMom)
        {
            Debug.Log("00");
        }

        else if (A.name == "TalkToDad")
        {

        }

        else if (A.tag == "floor")
        {
            MoveFloor = true;

            //Vector3 offset = Camera.main.ScreenToWorldPoint(Input.mousePosition) - lastMousePosition;
            //y = GetComponent<Transform>().position.y;
            //transform.position = new Vector3(Mathf.Clamp(transform.position.x, xMin, xMax), transform.position.y, 0);
            //offset.y = 0; //y軸 不變(水平拖曳)
            //transform.position += offset;
        }

        else if (A.tag != "floor")
        {
            MoveFloor = false;
        }

    }


}
