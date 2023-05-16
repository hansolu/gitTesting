using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    #region �̱���
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
    bool IsLoadingScene = false; //���� �ε�������

    //public void SetLoadingScene(NextScene _scene)
    //{
    //    this._scene = _scene;
    //}
    //���ҽ� ���� 
    //�ε��� �θ��� ������ ���� / �ε��� ����... 
    //public void LoadScene(CTEnum.SceneKind _kind)
    //{
    //    SceneManager.LoadScene((int)_kind);
    //}

    public void GoNextScene(CTEnum.SceneKind _kind) //�ۿ��� ���������� ���� �θ��� �Լ�.
    {
        StartCoroutine(NextSceneLoading(_kind));
    }
    
    //�����̵��� Ȥ�� �ε����� ��ġ�� �ε��� ������ ����....
    IEnumerator NextSceneLoading(CTEnum.SceneKind _kind) //�� �ε� ����. �ε��� ���� �ҷ����� ������ �����ϰ� ������ �ҷ����� �ε��� ����.
    {
        int scenenum = (int)_kind;
        IsLoadingScene = true;
        Scene oldscene = SceneManager.GetActiveScene(); //���� ����ϰ� �ִ� ��. ���� �õ���� ��...

        //�ϴ� �ε��� ����
        op = SceneManager.LoadSceneAsync((int)CTEnum.SceneKind.Loading, LoadSceneMode.Additive);//���� ���� ���ؼ� �θ�

        while (op.isDone==false)
        {
            //�����̴� �� ����.. �ϰ������ �ϱ�
            yield return null;
        }
        
        yield return null; //���� �� �ʿ�� ������ �׷��� �������� ��� ������ �����̳��� ì���
        op = SceneManager.UnloadSceneAsync(oldscene);
        while (op.isDone ==false)
        {
            //�����̴� �� ����.. �ϰ������ �ϱ�
            yield return null;
        }

        //�ε����� ����...

        yield return null;//���� �� �ʿ�� ������ �׷��� �������� ��� ������ �����̳��� ì���
        op = SceneManager.LoadSceneAsync(scenenum, LoadSceneMode.Additive); //�������� �� �� �θ���
        op.allowSceneActivation = false; //�ϴ� ��Ȱ��ȭ
        yield return null;//���� �� �ʿ�� ������ �׷��� �������� ��� ������ �����̳��� ì���
        while (true) //�������� ũ�� op.IsDone�� Ʈ�簡 �ȵ� ���ɼ��� �ֱ⶧��
        {
            //���ҽ� �ε� ���� �ؾ��Ұ��ִٸ��ϰ�
            //��ư �������� �ʿ��� ���� ����....

            //�����̴� �� ����.. �ϰ������ �ϱ�
            yield return null;
            if (op.progress > 0.98f) //95�� �̻� �Ϸ� �Ǿ�����
            {                
                //�����̴� �ٸ� 1�� ����.

                op.allowSceneActivation = true; //
                break;   
            }
        }

        //�ε� �Ϸ����
         SceneManager.SetActiveScene(SceneManager.GetSceneAt( scenenum)); // ���� �ִ� ���� Active���̵Ǿ
        //���� �ҷ��� ģ���� ��Ƽ������� ��������.
        op = SceneManager.UnloadSceneAsync((int)CTEnum.SceneKind.Loading);
        while (op.isDone ==false)
        {
            yield return null;
        }
        IsLoadingScene = false; //�ε��Ϸ�
    }


    //[System.Obsolete("�� ���ؼ� �񵿱�� �� ����")]
    //public void LoadSceneAsync(CTEnum.SceneKind _kind)
    //{
    //    if (coroutine !=null) //
    //    {
    //        //�ڷ�ƾ�� ��������ʴٸ� ���� ���� �θ������ε� �� �θ��ٰų� ������ ���� Ȯ���� ŭ
    //        Debug.LogError("�ڷ�ƾ�� ����������� ��");
    //    }
    //    coroutine = StartCoroutine(LoadCoroutine(_kind));
    //}

    //IEnumerator LoadCoroutine(CTEnum.SceneKind _kind)
    //{
    //    float time = 0;
    //    AsyncOperation op = SceneManager.LoadSceneAsync((int)_kind); //�񵿱�� �ҷ���        
    //    //yield return op; //����ȭ �ҷ��ö����� ��ٸ�����..
    //    op.allowSceneActivation = false; //���� �ٷ� Ȱ��ȭ ���� ���� 

    //    while (op.isDone==false) 
    //    {
    //        yield return null;
    //        time += Time.deltaTime; //�ð� ����
    //        if (_scene !=null)
    //        {
    //            _scene.SetSlider(Mathf.Lerp(op.progress, 1f, time));
    //        }
            
    //        if (op.progress > 0.95f) //95�� �̻��̸� �׳� �Ϸ��ߴٰ� ����.
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
 