using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exam : MonoBehaviour
{
    //1��) �ﰢ�Լ��� �̿��Ͽ� x�� ���̸� ���ϴ� ���� ���ּ���  //float  x = a * Mathf.Cos(b) ;
    //2��) ���� ������ ���� ���� Vec�� ������
    //  ������Ʈ���� �Ʒ� �ΰ��� ��� ��� ��
    //  this.Transform.position = Vec ��  //this�� ��ġ���� ��Ȯ�� Vec ��.
    //  this.Transform.Translate(Vec) ��    �� ���̸� �������ּ���  //this.transform.position += vec; //�� ��ġ���� vec�� ���� ����.

    //3��) 5���� ��ü�� ��ĥ�� �����ȿ� �������� �����Ͽ�
    //     �����̽��� ������ ������ �÷��̾ ���Ͽ� �����̴� �ڵ���� ¥�ּ���
    //                  (���⺤�͸� ���ϴ� ����� �ƴ��� üũ�ϱ� ����.)
    //     �÷��̾�� ���� ��ü�� 2���� �ٽ� ��ĥ�� �����ȿ� �������� ��������ϴ�

    //     1) ��, ��ü�� �����ϴ� ���� ��ũ��Ʈ�� �̱������� ¥���մϴ�
    //     2) ��ü���� ������Ʈ Ǯ�� �����ؾ��մϴ�
    //     3) �����̽� �����⸦ �õ��� Ƚ����ŭ �����̽��� ������ �ִ� �ð����� ���� ��Ƶ־��մϴ�. 
    //     4) �÷���(��ųʸ�, ����Ʈ, �迭, ť, ���� ��) �� 3���� �̻� ������ּ���.

    //          -�÷��� ���� �����ϴٸ� �ƹ� �÷��� ���� �ϰ� 3���� ��Ҹ� �׳� �־��ٰ� �����ϴ� ����̶� �������ϴ�.

    //      3���� ��� �Ϻ��� �ڵ带 ��¥���� �� �� ��ҿ� �´� �����̶� ���صθ� �������ϴ�

    //int / float  / string / double / bool / short
    void solution3_4() //�÷��� ����
    {
        //�÷��� - �迭�� ���
        string[] a = new string[3] { "1", "2", "3" }; //����¥�� �迭����
        for (int i = 0; i < a.Length; i++) //
        {
            a[i] = "a" + i;
        }

        //�÷��� ����Ʈ�� ���
        List<int> list = new List<int>(); //����
        for (int i = 0; i < 3; i++)
        {
            list.Add(i * 5);
        }

        list.Remove(3); //����. ��Ȯ�� 3�� �� ã�Ƽ� ����
        list.RemoveAt(0); //0��° ��� ����.

        //�÷��� ��ųʸ��� ���
        Dictionary<int, float> dic = new Dictionary<int, float>();
        for (int i = 0; i < 3; i++)
        {
            dic.Add( i+1, (i+1) * 0.7f); //1,2,3
        }

        if (dic.ContainsKey(2)) //��ųʸ��� Ű�� 2�� Ű�Ͱ����� �����Ѵٸ�, 
        {
            dic.Remove(2);//Ű�� 2�� ���׵��� �����޶�
        }

        //�÷��� ť�� ���
        Queue<float> que = new Queue<float>();
        que.Enqueue(1.2f); 
        que.Enqueue(4.5f);
        que.Enqueue(2.8f);

        que.Dequeue(); //������ ���� ���� ���� �ָ� ����.

        //�÷��� ������ ���
        Stack<string> stack = new Stack<string>();
        stack.Push("����1");
        stack.Push("����2");
        stack.Push("����3");

        stack.Pop(); //���� �������� ��, ����3�� ���ͼ� ����1,�̶� ����2 �� �������� ����.

        //���⺤�� ���ϱ�
        //Ÿ����ġ - ���� ��ġ 
        Transform Target = null; //������ Ÿ�� ���� ����
        Vector3 LookDir = Vector3.zero; //���⺤�� ������ ���� ����
        LookDir = Target.position - this.transform.position; //�̰Ŵ� ���� ������ ���� ���⺤�� �̰�
        //�긦 Ȱ���ؼ� ������
        LookDir = (Target.position - this.transform.position).normalized; //�̰Ŵ� ũ�Ⱑ 1¥���� Ÿ���� �ٶ󺸴� ������ ���� ���⺤��
    }
}
