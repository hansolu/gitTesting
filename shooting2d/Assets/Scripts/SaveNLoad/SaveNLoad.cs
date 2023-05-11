using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using Newtonsoft.Json; //Json��������
using System.Xml; //XML��������

public class SaveNLoad : MonoBehaviour
{
    string filepath = "";    
    void Start()
    {
        #region playerprefs�� �̿��� ���� �� �ε�
        //���� ���� ���ŭ �и��� ���� PlayerPrefs

        //PlayerPrefs.SetString("�̸�1", ����);
        //PlayerPrefs.SetInt("�̸�2", ����);
        //PlayerPrefs.SetFloat("�̸�3", ����);

        //if (PlayerPrefs.HasKey("�̸�1"))
        //{
        //    string �̸�1�Ǻ��� = PlayerPrefs.GetString("�̸�1");
        //}
        //if (PlayerPrefs.HasKey("�̸�2"))
        //{            
        //    int �̸�2�Ǻ��� = PlayerPrefs.GetInt("�̸�2");
        //}
        //if (PlayerPrefs.HasKey("�̸�3"))
        //{            
        //    float �̸�3�Ǻ��� = PlayerPrefs.GetFloat("�̸�3");
        //}

        ////�����
        //PlayerPrefs.DeleteAll(); //������
        //PlayerPrefs.DeleteKey("�̸�"); //"�̸�"Ű ã�Ƽ� ����.

        //=> ������ġ�� ������Ʈ��
        //������Ʈ�� ������ �Ѽ�
        //HKEY_CURRENT_USER \ SOFTWARE \ ȸ���̸� �ȿ� ���� ������
        //������ ������Ʈ�� �������ָ� ��.
        #endregion
        //=====================

        filepath = Path.Combine(Application.streamingAssetsPath, "test.json");
        //Application.dataPath //������Ʈ ���� ����(Assets)
        //Application.persistentDataPath // ������̸�/Appdata/LocalLow/ȸ���̸�/���δ�Ʈ�̸�
        //Application.streamingAssetsPath //Assets/StreamingAssets ����.
        //(StreamingAssets���� ��Ȯ�� ���� ���� �����س�����)

        //using (FileStream fs = new FileStream("���/�����̸�.��������", ���ϸ��))
        //{
        //    //����� �������� ���� �ȿ� ����
        //}//����� �ȿ������� �ȿ� �ȵ��� �ۿ�����...

        //WriteText(filepath, "������ �� �Է�");
        ////WriteTextBinary(filepath, "������ �� �Է�");
        //Debug.Log(ReadText(filepath));
        ////======================================

        PersonInfo _personinfo = new PersonInfo();
        _personinfo.ID = "firstID";
        _personinfo.age = 55;
        _personinfo.attendance = 0.89f;
        _personinfo.arr = new string[4] {"�ϰų�","string�迭","ä���","��"};
        _personinfo.list.Add('a');
        _personinfo.list.Add('b');
        _personinfo.list.Add('c');
        _personinfo.list.Add('d');
        
        _personinfo.dic.Add(1, 1f);
        _personinfo.dic.Add(2, 2f);
        _personinfo.dic.Add(3, 2f);

        _personinfo.vec = new Vector2Int(11,22); //struct 



        ///////////////////////////
        //JSon���Ϸ� ����
        string writingdata = JsonConvert.SerializeObject(_personinfo, Newtonsoft.Json.Formatting.Indented); //Json���·� �ٲٴ� �۾�

        //WriteText(filepath, writingdata);
        Debug.Log(writingdata);

        //\====================
        //Json���� �ҷ�����
        //string readdata = ReadText(filepath);

        //PersonInfo _personinfo2 = JsonConvert.DeserializeObject<PersonInfo>(readdata); //JSon���¸� <������� ���𰡷� �ٲٴ� �۾�>
        //Debug.Log(_personinfo2.ToString());



        /////==============XML��..
        //
        //filepath = Path.Combine(Application.streamingAssetsPath, "test.xml");
        //CreateXML(filepath);

        //readdata = ReadText(filepath);

        //LoadXML(readdata);
    }

    #region �ؽ�Ʈ�� ���
    #region ���̺� ��� ���� 1
    //    StringBuilder stringbuilder = new StringBuilder();

    //        for (int i = 0; i<������ƮǮ�� ����; i++)
    //        {
    //            stringbuilder.Append(������ƮǮ[i].ToString());            
    //        }

    //stringbuilder.ToString(); //stringbuilder���⿡  ����� ������� string���� ���� �� ����.

    //public class AA
    //    {
    //        //���̺� �Ŵ����� ���� �־ ������ �˾Ƽ� �� ������ �ٵ������ ��������.
    //        public override string ToString()
    //        {
    //            return ���� �����ϰ���� �� Ŭ������ ������ �ִ��� string�ϳ��� ���ļ� ������;
    //        }
    //    }

    /*���̺�Ŵ������� ���� �����͵��� �����ؼ� string �ϳ��� �ִ��� ���ļ� ������ �� �� stinrg ���ϱ� ������ �̷��� �������� Stringbuilder����*/
    #endregion

    void WriteText(string _filepath, string message) //����
    {
        DirectoryInfo directoryinfo = new DirectoryInfo(Application.streamingAssetsPath); //streamingasset������ �ִ��� Ȯ�κ���..
        if (directoryinfo.Exists == false)//������Ƽ�� �Լ��� �ƴ�.
        {
            directoryinfo.Create();
        }

        using (FileStream fs = new FileStream(filepath, FileMode.Append)) 
        {
            StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8); //�ѱ۵� �����ϵ��� encoding �Ӽ��� �ذ�..                        
            sw.Write(message);
            sw.Flush();
            //fs.Close();
        }
    }

    void WriteTextBinary(string _filepath, string message)
    {
        //���ǻ� �ϴ� ����
        DirectoryInfo directoryinfo = new DirectoryInfo(Application.streamingAssetsPath); //streamingasset������ �ִ��� Ȯ�κ���..
        if (directoryinfo.Exists == false)//������Ƽ�� �Լ��� �ƴ�.
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
    string ReadText(string _filepath)//�б�
    {               
        string str = "";
        FileInfo fileinfo = new FileInfo(_filepath);
        if (fileinfo.Exists)
        {
            using (StreamReader sr = new StreamReader(_filepath))
            {
                str = sr.ReadToEnd(); //������ �� �о����                
                //string[] bb = str.Split("\\"); //����ū �����ڷ� ���� �����ؼ� ��Ʈ���迭 ������
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

    class PersonInfo //�븮�� ����. ������ ��Ƶδ� �װ�... 
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

        // Xml�� �����Ѵ�(xml�� ������ ���ڵ� ����� �����ش�.)
        xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "yes"));

        // ��Ʈ ��� ����
        XmlNode root = xmlDoc.CreateNode(XmlNodeType.Element, "CharacterInfo", string.Empty);
        xmlDoc.AppendChild(root);

        // �ڽ� ��� ����
        XmlNode child = xmlDoc.CreateNode(XmlNodeType.Element, "Character", string.Empty);
        root.AppendChild(child);

        // �ڽ� ��忡 �� �Ӽ� ����        
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
