using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnrealThings : MonoBehaviour
{
    void Update()
    {
        if (PropInBackpack.WhichIsUseing && PropInBackpack.WhichIsUseing.name == "GlassesInBackpack")
        {
            gameObject.transform.Find("Reality").gameObject.SetActive(true);
            gameObject.transform.Find("Virtual").gameObject.SetActive(false);
        }
        else
        {
            gameObject.transform.Find("Virtual").gameObject.SetActive(true);
            gameObject.transform.Find("Reality").gameObject.SetActive(false);
        }
    }
}
