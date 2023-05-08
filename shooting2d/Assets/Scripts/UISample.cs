using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UISample : MonoBehaviour
{
    public Text _text;


    public TMP_Text text1; //요거~
    public TextMeshProUGUI text2; //확장판...

    void Start()
    {
        text1 = GetComponent<TMP_Text>();
        text1.text = "aaaaaa";
        //text2 = GetComponent<TextMeshProUGUI>();
        //text2.text = "어어어어어어";
    }
}
