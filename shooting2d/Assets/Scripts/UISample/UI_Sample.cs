using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Sample : MonoBehaviour
{
    public Transform RadioGroup;
    [SerializeField]
    Toggle[] RadioToggles;

    [SerializeField]
    Transform ToggleGroup;
    [SerializeField]
    Toggle[] Toggles;

    [SerializeField]
    Slider slider;

    [SerializeField]
    Dropdown dropdown;
    //Dropdown.OptionData dropOptions; //드랍다운의 옵션..

    [SerializeField]
    Transform Background;

    [SerializeField]
    GameObject Circle;
    [SerializeField]
    GameObject Capsule;
    [SerializeField]
    GameObject Square;

    Camera camera;

    void Start()
    {
        RadioGroup = GameObject.Find("RadioGroup").transform;
        RadioToggles = new Toggle[RadioGroup.childCount]; //3개짜리 토글 배열생김
        //RadioToggles[0] = RadioGroup.GetChild(0).GetComponent<Toggle>();
        //RadioToggles[1] = RadioGroup.GetChild(1).GetComponent<Toggle>();
        //RadioToggles[2] = RadioGroup.GetChild(2).GetComponent<Toggle>();
        for (int i = 0; i < RadioGroup.childCount; i++)
        {
            RadioToggles[i] = RadioGroup.GetChild(i).GetComponent<Toggle>();
        }

        ToggleGroup = GameObject.Find("ToggleGroup").transform;
        Toggles = new Toggle[ToggleGroup.childCount]; //3개짜리 토글 배열생김        
        for (int i = 0; i < ToggleGroup.childCount; i++)
        {
            Toggles[i] = ToggleGroup.GetChild(i).GetComponent<Toggle>();
        }

        RadioToggles[0].transform.GetChild(1).GetComponent<Text>().text = "캡슐형";
        RadioToggles[1].transform.GetChild(1).GetComponent<Text>().text = "원형";
        RadioToggles[2].transform.GetChild(1).GetComponent<Text>().text = "사각형";

        Toggles[1].transform.GetChild(1).GetComponent<Text>().text = "초록";
        Toggles[2].transform.GetChild(1).GetComponent<Text>().text = "파랑";

        RadioToggles[0].isOn = true; //캡슐형 디폴트

        for (int i = 0; i < Toggles.Length; i++)
        {
            Toggles[i].isOn = false; //아무것도 선택하지않은것이 디폴트
        }
        

        slider = GameObject.Find("Slider").GetComponent<Slider>();
        slider.value = 1;

        dropdown = GameObject.Find("Dropdown").GetComponent<Dropdown>();
        //dropdown.options.Clear(); //기존에 있던 옵션들을 일단 지우고
        Dropdown.OptionData dropoptions = new Dropdown.OptionData();
        dropoptions.text = "삭제";
        //dropoptions.image //명칭이 image긴한데 ㅇ내용을 보면 sprite여서 그냥 넣길 바라는 그림 넣어주면 됨.
        dropdown.options.Add(dropoptions);

        Background = GameObject.Find("Background").transform;

        Circle = Resources.Load<GameObject>("Shapes\\Circle");
        Capsule = Resources.Load<GameObject>("Shapes\\Capsule");
        Square = Resources.Load<GameObject>("Shapes\\Square");

        camera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //왼쪽클릭
        {
            if (EventSystem.current.IsPointerOverGameObject() ==false)
                //내가 UI를 누른게 아니라면
            {
                CreateShape(); //내가 라디오 버튼으로 선택해놨던 도형을 만들기.
            }
        }
    }

    void CreateShape()
    {
        if (dropdown.value ==0) //
        {
            float red = Toggles[0].isOn ? 1 : 0; //삼항연산자
            float blue = Toggles[2].isOn ? 1 : 0; //삼항연산자
            float green = Toggles[1].isOn ? 1 : 0; //삼항연산자            
            GameObject _obj;
            if (RadioToggles[0].isOn ==true) //캡슐 선택중이라는 것
            {
                _obj = Instantiate(Capsule, Background);
            }
            else if (RadioToggles[1].isOn == true) //원 선택중
            {
                _obj = Instantiate(Circle, Background);
            }
            else //혹은 네모 선택중.
            {
                _obj = Instantiate(Square, Background);
            }
            _obj.GetComponent<SpriteRenderer>().color = new Color(red, green, blue, slider.value);
            //Input.mousePosition //마우스 위치
            Vector3 vec = camera.ScreenToWorldPoint(Input.mousePosition);
            vec.z = 0;
            _obj.transform.position = vec;
        }
    }
}
