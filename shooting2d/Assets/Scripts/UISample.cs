using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UISample : MonoBehaviour
{
    public Text _text;


    public TMP_Text text1; //���~
    public TextMeshProUGUI text2; //Ȯ����...

    void Start()
    {
        text1 = GetComponent<TMP_Text>();
        text1.text = "aaaaaa";
        //text2 = GetComponent<TextMeshProUGUI>();
        //text2.text = "�������";
    }
}
