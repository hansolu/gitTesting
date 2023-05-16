using UnityEngine;
using UnityEngine.UI;

public class NextScene : MonoBehaviour
{
    public CTEnum.SceneKind _nextScene;
    //public Slider slider;

    void Start()
    {
        //if (slider !=null)
        //{
        //    slider.value = 0; //혹시 모르니 UI 로딩바 초기화 해두는것
        //    LoadSceneManager.Instance.SetLoadingScene(this);
        //}

        //Invoke("GoToLoadingScene", 1f);
        //Invoke("GoToLoadingScene_Async", 1f);
    }
        
    //public void SetSlider(float val)
    //{
    //    if (slider == null)
    //    {
    //        return;
    //    }
    //    slider.value = val;
    //}
    void GoNextScene()
    {
        LoadSceneManager.Instance.GoNextScene(_nextScene);
    }
    //void GoToLoadingScene_Async() //씬이동 비동기
    //{
    //    LoadSceneManager.Instance.LoadSceneAsync(_nextScene);
    //}

    void GoToLoadingScene() //단순 씬이동
    {
        LoadSceneManager.Instance.LoadScene(_nextScene);
    }    
}
