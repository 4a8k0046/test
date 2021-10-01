using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TypeWriter : MonoBehaviour
{
    [Header("ui組件")]
    public Text textLabel;
    public GameObject TalkFrist;
    public GameObject Talk_toSomeOne;
    public GameObject Joystick;
    public GameObject HoldOn;

    public List<Talk_To_MomData> Mom_talk;//母螳螂對話 List


    public Button button;//換下句劇情的 按鈕

    public bool showjoyplat;//宣告一個平台跟搖桿是否出現的布林

    void Awake()
    {
        showjoyplat = false;

    }
    private void Start()
    {
        DataPool.DataInit();
        Mom_talk = DataPool.talk_To_Mom.dataList;

        TypeText();
        Btn_Onclick();

    }

    public Talk_To_MomData FindTargetDataByIndex(string p_Index)
    {
        foreach (Talk_To_MomData talk_To_MomData in Mom_talk)
        {
            if (talk_To_MomData.Index == p_Index)
            {
                return talk_To_MomData;
            };
        }
        return null;
    }

    private void Update()
    {

        if (TalkFrist.activeSelf && Input.GetKeyDown(KeyCode.Z))
        {
            TalkFrist.SetActive(false);
            Talk_toSomeOne.SetActive(true);
        }

    }



    public void Btn_Onclick()
    {

        button.onClick.AddListener(TypeText);

    }

    int index = 0;
    public void TypeText()
    {

        Talk_To_MomData talk_To_MomData = FindTargetDataByIndex(index.ToString());
        if (talk_To_MomData != null)
        {
            textLabel.text = (talk_To_MomData.Dialogue);
            index++;
        }
        else
        {
            if (endCloseCoroutine != null)
            {
                StopCoroutine(endCloseCoroutine);
            }
            endCloseCoroutine = StartCoroutine(CloseAtEnd(0.3f));
        }


    }

    Coroutine endCloseCoroutine;
    IEnumerator CloseAtEnd(float duration)
    {
        yield return new WaitForSecondsRealtime(duration);

        Talk_toSomeOne.SetActive(false);
        Joystick.SetActive(true);
        HoldOn.SetActive(true);
        showjoyplat = true;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("talk_mom_frist");
        TalkFrist.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        TalkFrist.SetActive(false);
    }


}
