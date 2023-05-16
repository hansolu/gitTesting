using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    #region 싱글톤
    static LoadSceneManager instance = null;
    public static LoadSceneManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new LoadSceneManager();
            }
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            if (instance != this)
            {
                Destroy(this);
            }
        }
    }
    #endregion 
    
    //[SerializeField]
    //NextScene _scene=null; //

    Coroutine coroutine = null;
    AsyncOperation op; 
    bool IsLoadingScene = false; //현재 로딩중인지

    //public void SetLoadingScene(NextScene _scene)
    //{
    //    this._scene = _scene;
    //}
    //리소스 세팅 
    //로딩씬 부르고 다음씬 세팅 / 로딩씬 해제... 
    //public void LoadScene(CTEnum.SceneKind _kind)
    //{
    //    SceneManager.LoadScene((int)_kind);
    //}

    public void GoNextScene(CTEnum.SceneKind _kind) //밖에서 다음씬으로 가기 부르는 함수.
    {
        StartCoroutine(NextSceneLoading(_kind));
    }
    
    //슬라이딩바 혹은 로딩바의 위치나 로딩바 세팅은 취향....
    IEnumerator NextSceneLoading(CTEnum.SceneKind _kind) //씬 로드 정석. 로딩씬 먼저 불러놓고 이전씬 해제하고 다음씬 불러놓고 로딩씬 해제.
    {
        int scenenum = (int)_kind;
        IsLoadingScene = true;
        Scene oldscene = SceneManager.GetActiveScene(); //현재 사용하고 있는 씬. 이제 올드씬이 될...

        //일단 로딩씬 세팅
        op = SceneManager.LoadSceneAsync((int)CTEnum.SceneKind.Loading, LoadSceneMode.Additive);//다음 씬을 더해서 부름

        while (op.isDone==false)
        {
            //슬라이더 바 세팅.. 하고싶으면 하기
            yield return null;
        }
        
        yield return null; //굳이 할 필요는 없지만 그래도 한프레임 쉬어서 안정성 조금이나마 챙기기
        op = SceneManager.UnloadSceneAsync(oldscene);
        while (op.isDone ==false)
        {
            //슬라이더 바 세팅.. 하고싶으면 하기
            yield return null;
        }

        //로딩씬만 존재...

        yield return null;//굳이 할 필요는 없지만 그래도 한프레임 쉬어서 안정성 조금이나마 챙기기
        op = SceneManager.LoadSceneAsync(scenenum, LoadSceneMode.Additive); //다음으로 갈 씬 부르기
        op.allowSceneActivation = false; //일단 비활성화
        yield return null;//굳이 할 필요는 없지만 그래도 한프레임 쉬어서 안정성 조금이나마 챙기기
        while (true) //다음씬이 크면 op.IsDone이 트루가 안될 가능성이 있기때문
        {
            //리소스 로드 따로 해야할것있다면하고
            //여튼 다음씬에 필요한 뭔가 세팅....

            //슬라이더 바 세팅.. 하고싶으면 하기
            yield return null;
            if (op.progress > 0.98f) //95퍼 이상 완료 되었을때
            {                
                //슬라이더 바를 1로 세팅.

                op.allowSceneActivation = true; //
                break;   
            }
        }

        //로딩 완료됐음
         SceneManager.SetActiveScene(SceneManager.GetSceneAt( scenenum)); // 원래 있던 씬이 Active씬이되어서
        //새로 불러온 친구를 액티브씬으로 수정해줌.
        op = SceneManager.UnloadSceneAsync((int)CTEnum.SceneKind.Loading);
        while (op.isDone ==false)
        {
            yield return null;
        }
        IsLoadingScene = false; //로딩완료
    }


    //[System.Obsolete("씬 더해서 비동기로 할 것임")]
    //public void LoadSceneAsync(CTEnum.SceneKind _kind)
    //{
    //    if (coroutine !=null) //
    //    {
    //        //코루틴이 비어있지않다면 뭔가 씬을 부르는중인데 또 부른다거나 문제가 있을 확률이 큼
    //        Debug.LogError("코루틴이 비어있지않음 ㅜ");
    //    }
    //    coroutine = StartCoroutine(LoadCoroutine(_kind));
    //}

    //IEnumerator LoadCoroutine(CTEnum.SceneKind _kind)
    //{
    //    float time = 0;
    //    AsyncOperation op = SceneManager.LoadSceneAsync((int)_kind); //비동기로 불러서        
    //    //yield return op; //동기화 불러올때까지 기다리겠음..
    //    op.allowSceneActivation = false; //씬을 바로 활성화 하진 말고 

    //    while (op.isDone==false) 
    //    {
    //        yield return null;
    //        time += Time.deltaTime; //시간 누적
    //        if (_scene !=null)
    //        {
    //            _scene.SetSlider(Mathf.Lerp(op.progress, 1f, time));
    //        }
            
    //        if (op.progress > 0.95f) //95퍼 이상이면 그냥 완료했다고 보자.
    //        {
    //            time = 0;                
    //            op.allowSceneActivation = true;
    //            coroutine = null;
    //            _scene = null;
    //            yield break;
    //        }            
    //    }
    //}
}
 