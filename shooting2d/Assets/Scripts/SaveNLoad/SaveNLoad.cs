using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using Newtonsoft.Json; //Json쓰기위함
using System.Xml; //XML쓰기위함

public class SaveNLoad : MonoBehaviour
{
    string filepath = "";    
    void Start()
    {
        #region playerprefs를 이용한 저장 및 로드
        //가장 쉽고 쉬운만큼 털리기 좋은 PlayerPrefs

        //PlayerPrefs.SetString("이름1", 내용);
        //PlayerPrefs.SetInt("이름2", 내용);
        //PlayerPrefs.SetFloat("이름3", 내용);

        //if (PlayerPrefs.HasKey("이름1"))
        //{
        //    string 이름1의변수 = PlayerPrefs.GetString("이름1");
        //}
        //if (PlayerPrefs.HasKey("이름2"))
        //{            
        //    int 이름2의변수 = PlayerPrefs.GetInt("이름2");
        //}
        //if (PlayerPrefs.HasKey("이름3"))
        //{            
        //    float 이름3의변수 = PlayerPrefs.GetFloat("이름3");
        //}

        ////지우기
        //PlayerPrefs.DeleteAll(); //다지움
        //PlayerPrefs.DeleteKey("이름"); //"이름"키 찾아서 지움.

        //=> 저장위치는 레지스트리
        //레지스트리 편집기 켜서
        //HKEY_CURRENT_USER \ SOFTWARE \ 회사이름 안에 보통 있을것
        //여기의 레지스트리 삭제해주면 됨.
        #endregion
        //=====================

        filepath = Path.Combine(Application.streamingAssetsPath, "test.json");
        //Application.dataPath //프로젝트 폴더 내부(Assets)
        //Application.persistentDataPath // 사용자이름/Appdata/LocalLow/회사이름/프로덕트이름
        //Application.streamingAssetsPath //Assets/StreamingAssets 폴더.
        //(StreamingAssets으로 정확히 폴더 내가 생성해놔야함)

        //using (FileStream fs = new FileStream("경로/파일이름.파일형식", 파일모드))
        //{
        //    //제대로 열렸으면 여기 안에 접근
        //}//제대로 안열렸으면 안에 안들어가고 밖에나옴...

        //WriteText(filepath, "마지막 줄 입력");
        ////WriteTextBinary(filepath, "마지막 줄 입력");
        //Debug.Log(ReadText(filepath));
        ////======================================

        PersonInfo _personinfo = new PersonInfo();
        _personinfo.ID = "firstID";
        _personinfo.age = 55;
        _personinfo.attendance = 0.89f;
        _personinfo.arr = new string[4] {"암거나","string배열","채우는","중"};
        _personinfo.list.Add('a');
        _personinfo.list.Add('b');
        _personinfo.list.Add('c');
        _personinfo.list.Add('d');
        
        _personinfo.dic.Add(1, 1f);
        _personinfo.dic.Add(2, 2f);
        _personinfo.dic.Add(3, 2f);

        _personinfo.vec = new Vector2Int(11,22); //struct 



        ///////////////////////////
        //JSon파일로 쓰기
        string writingdata = JsonConvert.SerializeObject(_personinfo, Newtonsoft.Json.Formatting.Indented); //Json형태로 바꾸는 작업

        //WriteText(filepath, writingdata);
        Debug.Log(writingdata);

        //\====================
        //Json파일 불러오기
        //string readdata = ReadText(filepath);

        //PersonInfo _personinfo2 = JsonConvert.DeserializeObject<PersonInfo>(readdata); //JSon형태를 <꺾쇠안의 무언가로 바꾸는 작업>
        //Debug.Log(_personinfo2.ToString());



        /////==============XML로..
        //
        //filepath = Path.Combine(Application.streamingAssetsPath, "test.xml");
        //CreateXML(filepath);

        //readdata = ReadText(filepath);

        //LoadXML(readdata);
    }

    #region 텍스트의 경우
    #region 세이브 방식 예시 1
    //    StringBuilder stringbuilder = new StringBuilder();

    //        for (int i = 0; i<오브젝트풀의 개수; i++)
    //        {
    //            stringbuilder.Append(오브젝트풀[i].ToString());            
    //        }

    //stringbuilder.ToString(); //stringbuilder여기에  저장된 내용들을 string으로 받을 수 있음.

    //public class AA
    //    {
    //        //세이브 매니저가 따로 있어서 걔한테 알아서 얘 내용좀 다듬으라고 보내놓고.
    //        public override string ToString()
    //        {
    //            return 내가 저장하고싶은 이 클래스의 내용을 최대한 string하나로 합쳐서 내보냄;
    //        }
    //    }

    /*세이브매니저에서 나의 데이터들을 취합해서 string 하나로 최대한 합쳐서 쓰려고 할 때 stinrg 더하기 연산자 이런거 쓰지말고 Stringbuilder쓸것*/
    #endregion

    void WriteText(string _filepath, string message) //쓰기
    {
        DirectoryInfo directoryinfo = new DirectoryInfo(Application.streamingAssetsPath); //streamingasset폴더가 있는지 확인부터..
        if (directoryinfo.Exists == false)//프로퍼티라서 함수가 아님.
        {
            directoryinfo.Create();
        }

        using (FileStream fs = new FileStream(filepath, FileMode.Append)) 
        {
            StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8); //한글도 가능하도록 encoding 속성을 준것..                        
            sw.Write(message);
            sw.Flush();
            //fs.Close();
        }
    }

    void WriteTextBinary(string _filepath, string message)
    {
        //도의상 일단 해줌
        DirectoryInfo directoryinfo = new DirectoryInfo(Application.streamingAssetsPath); //streamingasset폴더가 있는지 확인부터..
        if (directoryinfo.Exists == false)//프로퍼티라서 함수가 아님.
        {
            directoryinfo.Create();
        }

        using (FileStream fs = new FileStream(filepath, FileMode.Create))
        {
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(message);
            bw.Flush();
            //bw.Close();
            //fs.close();
        }
    }
    string ReadText(string _filepath)//읽기
    {               
        string str = "";
        FileInfo fileinfo = new FileInfo(_filepath);
        if (fileinfo.Exists)
        {
            using (StreamReader sr = new StreamReader(_filepath))
            {
                str = sr.ReadToEnd(); //끝까지 다 읽어내겠음                
                //string[] bb = str.Split("\\"); //가장큰 구분자로 먼저 구분해서 스트링배열 빼놓고
                //PersonInfo _person = new PersonInfo();
                //for (int i = 0; i < bb.Length; i++)
                //{
                //    _person.SetInfo(bb[i].Split(":")[0], bb[i].Split(":")[1]);
                //}
                //sr.Close();
            }
        }
        else
        {
            return "";
        }

        return str;
    }

    #endregion

    class PersonInfo //대리자 패턴. 변수만 모아두는 그거... 
    {
        public string ID;        
        public int age;
        public float attendance;
        public string[] arr;
        public List<char> list = new List<char>();
        public Dictionary<int, float> dic = new Dictionary<int, float>();
        public Vector2Int vec = Vector2Int.zero;

        //public void SetInfo(string _id, string val)
        //{
        //    switch (_id)
        //    {
        //        case "ID":
        //            ID = val;
        //            break;
        //        //case "PersonName":
        //        //    PersonName = val;
        //        //    break;
        //        case "age":
        //            int.TryParse(val, out age);
        //            break;
        //        case "attendance":
        //            float.TryParse(val, out attendance);
        //            break;
        //        default:
        //            break;
        //    }
        //}

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append($"ID : {ID} \n");
            builder.Append($"age : {age} \n");
            builder.Append($"attendance : {attendance} \n");
            for (int i = 0; i < arr.Length; i++)
            {
                builder.Append($"arr[{i}] : {arr[i]} \n");
            }
            for (int i = 0; i < list.Count; i++)
            {
                builder.Append($"list[{i}] : {list[i]} \n");
            }
            foreach (var item in dic)
            {
                builder.Append($"[{item.Key}] : {item.Value} \n");
            }
            builder.Append($"vec : {vec.x} / {vec.y} \n");
            return builder.ToString();
        }
    }

    public void CreateXML(string _filepath)
    {
        XmlDocument xmlDoc = new XmlDocument();

        // Xml을 선언한다(xml의 버전과 인코딩 방식을 정해준다.)
        xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "yes"));

        // 루트 노드 생성
        XmlNode root = xmlDoc.CreateNode(XmlNodeType.Element, "CharacterInfo", string.Empty);
        xmlDoc.AppendChild(root);

        // 자식 노드 생성
        XmlNode child = xmlDoc.CreateNode(XmlNodeType.Element, "Character", string.Empty);
        root.AppendChild(child);

        // 자식 노드에 들어갈 속성 생성        
        XmlElement name = xmlDoc.CreateElement("Name");
        name.InnerText = "wergia";
        child.AppendChild(name);

        XmlElement lv = xmlDoc.CreateElement("Level");
        lv.InnerText = "1";
        child.AppendChild(lv);

        XmlElement exp = xmlDoc.CreateElement("Experience");
        exp.InnerText = "45";
        child.AppendChild(exp);

        xmlDoc.Save(_filepath);
    }

    void LoadXML(string data)        
    {                
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(data);

        XmlNodeList nodes = xmlDoc.SelectNodes("CharacterInfo/Character");

        foreach (XmlNode node in nodes)
        {
            Debug.Log("Name :: " + node.SelectSingleNode("Name").InnerText);
            Debug.Log("Level :: " + node.SelectSingleNode("Level").InnerText);
            Debug.Log("Exp :: " + node.SelectSingleNode("Experience").InnerText);
        }
    }
}
