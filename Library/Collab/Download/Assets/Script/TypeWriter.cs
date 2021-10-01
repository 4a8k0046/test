using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class TypeWriter : MonoBehaviour
{
    [Header("ui組件")]
    public Text textLabel;
    public GameObject Talk_toSomeOne;

    //public Image faceImage;

    [Header("文字檔")]
    public TextAsset textFile;
    public int index;//編號

    public Button button;
    public Button Tigger;


    //讓文檔變為字元型，讓字元型每一行存儲在這個清單裡  
    List<string> textList = new List<string>();

    void Awake()
    {

        GetTextFormFile(textFile);
    }

    private void Start()
    {
        Tigger.onClick.AddListener(btn_Tigger);
        TypeText();
    }

    public void btn_Tigger()
    {

        if (!Talk_toSomeOne.activeInHierarchy)
        {
            Talk_toSomeOne.SetActive(true);
            Btn_Onclick();
          
        }
        else
        { Talk_toSomeOne.SetActive(false); }
    }

    public void Btn_Onclick()
    {

        button.onClick.AddListener(TypeText);

    }

    public void TypeText()
    {
        if (index == 0)
        {
            //textLabel.text = "";
            textLabel.text = textList[index];
            index++;
            return;
        }
        textLabel.text = "";

        string str = textList[index];
        strLength = str.Length;

        duration = strLength * 0.3f;
        time = Time.time + duration;

        print(strLength);
        textLabel.DOText(str, duration);

        index++;

        if (index == textList.Count)
        {
            if (endCloseCoroutine != null)
            {
                StopCoroutine(endCloseCoroutine);
            }
            endCloseCoroutine = StartCoroutine(CloseAtEnd(duration));
        }
    }
    int strLength = 0;
    float duration = 0f, time = 0f;


    void GetTextFormFile(TextAsset flie)
    {
        textList.Clear();
        index = 0;

        //逐行讀取文字檔
        var lineDate = flie.text.Split('\n');
        foreach (var Line in lineDate)
        {
            textList.Add(Line);
        }
    }
    Coroutine endCloseCoroutine;
    IEnumerator CloseAtEnd(float duration)
    {
        yield return new WaitForSecondsRealtime(duration);

        Talk_toSomeOne.SetActive(false);
       
    }
}
