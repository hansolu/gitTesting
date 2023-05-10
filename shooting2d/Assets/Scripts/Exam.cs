using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exam : MonoBehaviour
{
    //1번) 삼각함수를 이용하여 x의 길이를 구하는 식을 써주세요  //float  x = a * Mathf.Cos(b) ;
    //2번) 힘과 방향을 가진 벡터 Vec이 있을때
    //  업데이트에서 아래 두가지 경우 사용 시
    //  this.Transform.position = Vec 과  //this의 위치값이 정확히 Vec 임.
    //  this.Transform.Translate(Vec) 의    두 차이를 서술해주세요  //this.transform.position += vec; //내 위치값에 vec를 누적 더함.

    //3번) 5개의 객체를 색칠된 공간안에 랜덤으로 생성하여
    //     스페이스를 누르고 있으면 플레이어를 향하여 움직이는 코드들을 짜주세요
    //                  (방향벡터를 구하는 방법을 아는지 체크하기 위함.)
    //     플레이어에게 닿은 객체는 2초후 다시 색칠된 공간안에 랜덤으로 만들어집니다

    //     1) 단, 객체를 관리하는 메인 스크립트는 싱글톤으로 짜야합니다
    //     2) 객체들은 오브젝트 풀로 관리해야합니다
    //     3) 스페이스 누르기를 시도한 횟수만큼 스페이스를 누르고 있던 시간들을 따로 담아둬야합니다. 
    //     4) 컬렉션(딕셔너리, 리스트, 배열, 큐, 스택 등) 은 3종류 이상 사용해주세요.

    //          -컬렉션 수가 부족하다면 아무 컬렉션 선언 하고 3개의 요소를 그냥 넣었다가 삭제하는 방식이라도 괜찮습니다.

    //      3번의 경우 완벽한 코드를 못짜더라도 각 평가 요소에 맞는 설명이라도 잘해두면 괜찮습니다

    //int / float  / string / double / bool / short
    void solution3_4() //컬렉션 구현
    {
        //컬렉션 - 배열의 경우
        string[] a = new string[3] { "1", "2", "3" }; //세개짜리 배열선언
        for (int i = 0; i < a.Length; i++) //
        {
            a[i] = "a" + i;
        }

        //컬렉션 리스트의 경우
        List<int> list = new List<int>(); //선언
        for (int i = 0; i < 3; i++)
        {
            list.Add(i * 5);
        }

        list.Remove(3); //삭제. 정확히 3인 값 찾아서 삭제
        list.RemoveAt(0); //0번째 요소 삭제.

        //컬렉션 딕셔너리의 경우
        Dictionary<int, float> dic = new Dictionary<int, float>();
        for (int i = 0; i < 3; i++)
        {
            dic.Add( i+1, (i+1) * 0.7f); //1,2,3
        }

        if (dic.ContainsKey(2)) //딕셔너리에 키가 2인 키와값쌍이 존재한다면, 
        {
            dic.Remove(2);//키가 2인 사항들을 지워달라
        }

        //컬렉션 큐의 경우
        Queue<float> que = new Queue<float>();
        que.Enqueue(1.2f); 
        que.Enqueue(4.5f);
        que.Enqueue(2.8f);

        que.Dequeue(); //무조건 제일 먼저 넣은 애를 빼줌.

        //컬렉션 스택의 경우
        Stack<string> stack = new Stack<string>();
        stack.Push("더함1");
        stack.Push("더함2");
        stack.Push("더함3");

        stack.Pop(); //가장 마지막에 들어간, 더함3이 나와서 더함1,이랑 더함2 만 남아있을 것임.

        //방향벡터 구하기
        //타겟위치 - 나의 위치 
        Transform Target = null; //임의의 타겟 변수 설정
        Vector3 LookDir = Vector3.zero; //방향벡터 저장할 변수 선언
        LookDir = Target.position - this.transform.position; //이거는 힘과 뱡향을 가진 방향벡터 이고
        //얘를 활용해서 쓰려면
        LookDir = (Target.position - this.transform.position).normalized; //이거는 크기가 1짜리인 타겟을 바라보는 뱡향을 가진 방향벡터
    }
}
