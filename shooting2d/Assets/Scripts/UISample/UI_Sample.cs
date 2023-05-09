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
    //Dropdown.OptionData dropOptions; //����ٿ��� �ɼ�..

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
        RadioToggles = new Toggle[RadioGroup.childCount]; //3��¥�� ��� �迭����
        //RadioToggles[0] = RadioGroup.GetChild(0).GetComponent<Toggle>();
        //RadioToggles[1] = RadioGroup.GetChild(1).GetComponent<Toggle>();
        //RadioToggles[2] = RadioGroup.GetChild(2).GetComponent<Toggle>();
        for (int i = 0; i < RadioGroup.childCount; i++)
        {
            RadioToggles[i] = RadioGroup.GetChild(i).GetComponent<Toggle>();
        }

        ToggleGroup = GameObject.Find("ToggleGroup").transform;
        Toggles = new Toggle[ToggleGroup.childCount]; //3��¥�� ��� �迭����        
        for (int i = 0; i < ToggleGroup.childCount; i++)
        {
            Toggles[i] = ToggleGroup.GetChild(i).GetComponent<Toggle>();
        }

        RadioToggles[0].transform.GetChild(1).GetComponent<Text>().text = "ĸ����";
        RadioToggles[1].transform.GetChild(1).GetComponent<Text>().text = "����";
        RadioToggles[2].transform.GetChild(1).GetComponent<Text>().text = "�簢��";

        Toggles[1].transform.GetChild(1).GetComponent<Text>().text = "�ʷ�";
        Toggles[2].transform.GetChild(1).GetComponent<Text>().text = "�Ķ�";

        RadioToggles[0].isOn = true; //ĸ���� ����Ʈ

        for (int i = 0; i < Toggles.Length; i++)
        {
            Toggles[i].isOn = false; //�ƹ��͵� ���������������� ����Ʈ
        }
        

        slider = GameObject.Find("Slider").GetComponent<Slider>();
        slider.value = 1;

        dropdown = GameObject.Find("Dropdown").GetComponent<Dropdown>();
        //dropdown.options.Clear(); //������ �ִ� �ɼǵ��� �ϴ� �����
        Dropdown.OptionData dropoptions = new Dropdown.OptionData();
        dropoptions.text = "����";
        //dropoptions.image //��Ī�� image���ѵ� �������� ���� sprite���� �׳� �ֱ� �ٶ�� �׸� �־��ָ� ��.
        dropdown.options.Add(dropoptions);

        Background = GameObject.Find("Background").transform;

        Circle = Resources.Load<GameObject>("Shapes\\Circle");
        Capsule = Resources.Load<GameObject>("Shapes\\Capsule");
        Square = Resources.Load<GameObject>("Shapes\\Square");

        camera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //����Ŭ��
        {
            if (EventSystem.current.IsPointerOverGameObject() ==false)
                //���� UI�� ������ �ƴ϶��
            {
                CreateShape(); //���� ���� ��ư���� �����س��� ������ �����.
            }
        }
    }

    void CreateShape()
    {
        if (dropdown.value ==0) //
        {
            float red = Toggles[0].isOn ? 1 : 0; //���׿�����
            float blue = Toggles[2].isOn ? 1 : 0; //���׿�����
            float green = Toggles[1].isOn ? 1 : 0; //���׿�����            
            GameObject _obj;
            if (RadioToggles[0].isOn ==true) //ĸ�� �������̶�� ��
            {
                _obj = Instantiate(Capsule, Background);
            }
            else if (RadioToggles[1].isOn == true) //�� ������
            {
                _obj = Instantiate(Circle, Background);
            }
            else //Ȥ�� �׸� ������.
            {
                _obj = Instantiate(Square, Background);
            }
            _obj.GetComponent<SpriteRenderer>().color = new Color(red, green, blue, slider.value);
            //Input.mousePosition //���콺 ��ġ
            Vector3 vec = camera.ScreenToWorldPoint(Input.mousePosition);
            vec.z = 0;
            _obj.transform.position = vec;
        }
    }
}
