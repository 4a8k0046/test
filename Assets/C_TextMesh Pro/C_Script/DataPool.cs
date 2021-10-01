using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPool : MonoBehaviour
{
    public static Talk_To_Mom talk_To_Mom;

    public static void DataInit()
    {
        talk_To_Mom = Resources.Load<Talk_To_Mom>("Talk_To_Mom");
    }
}
