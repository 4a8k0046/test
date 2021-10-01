using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SecurityBoxButtn : MonoBehaviour
{
    int Num = 0;

    private void Update()
    {
        Num = int.Parse(transform.parent.Find("Num").GetComponent<TMP_Text>().text);
    }

    private void OnMouseDown()
    {
        if (gameObject.name == "Up")
        {
            Num = (Num + 1) % 10;
            transform.parent.Find("Num").GetComponent<TMP_Text>().text = Num + "";
        }
        else if (gameObject.name == "Down")
        {
            Num = (Num - 1);
            if(Num == -1) { Num = 9; }
            transform.parent.Find("Num").GetComponent<TMP_Text>().text = Num + "";
        }
    }
}
