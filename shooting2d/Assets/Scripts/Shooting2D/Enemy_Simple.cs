using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Simple : Enemy //단순히 움직이는 적이 될 것임. 총안쏘는애
{
    //얘는 Awake / Start같은것 부르기 어려울 것이고
    public AnimationCurve curve; //커브를 쓸줄은 알아야 나중에 디자이너가 그려둔 곡선을 그대로 쓸수있겠죠...
    float siny = 0;
    float xvalue = 0;
    bool isplus = true;
    public override void StartShoot() //만약 내가 매개변수를 뒀다면
    { //매개변수로 세팅도 해야하기때문에

        EnemyKind = CTEnum.EnemyKind.Enemy_A;
        power = 2;
        HP = 5;
        HPMax = 5; //나중에 UI같은거 그리게 될 때 맥스 필수죠.
        speed = 3;
        orgpos.x = Random.Range( - GameManager.Instance.Width +1, GameManager.Instance.Width-1);
        orgpos.y = GameManager.Instance.Height + 1; //화면 밖 1정도 위에서 태어남..
        transform.position = orgpos;
        //dir = orgpos;
        base.StartShoot();
    }
    void FixedUpdate()
    {
        if (IsActiving == false) 
        {
            return;
        }
        
        if (isplus)
        {
            xvalue += Time.deltaTime;
            if (xvalue >= 1) //내가 좀더 크게 지그재그 하고싶으면 1이 아니고 2, 3.. 등등 숫자를 달리주면 되겠죠...
            {
                isplus = false;
            }
        }
        else
        {
            xvalue -= Time.deltaTime;
            if (xvalue <= -1)
            {
                isplus = true;
            }
        }
        dir.x = xvalue; //x값은 증가했다 감소했다 ㅁ반복..
        dir.y -= Time.deltaTime *speed; //y는 계속 감소하고

        transform.position = orgpos + dir;

        #region 사인함수 사용한 곡선
        //siny += Time.deltaTime;
        //dir.y -= Time.deltaTime;
        //dir.x = Mathf.Sin(siny) * speed;

        //transform.position = orgpos + dir;
        #endregion
        #region 직선의 방법
        ////그냥 직선으로 내려오는 경우        
        //dir.y -= Time.deltaTime;

        //transform.position = dir;        
        //Translate== position+= (자체적으로 누적더하기) / position =  직접 값넣기 랑 차이를 이제 아신다면
        #endregion
        
        if (transform.position.y <= -GameManager.Instance.Height)
        {
            Die();
        }
    }
}
