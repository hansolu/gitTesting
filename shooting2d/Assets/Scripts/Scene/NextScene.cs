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
        //    slider.value = 0; //Ȥ�� �𸣴� UI �ε��� �ʱ�ȭ �صδ°�
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
    //void GoToLoadingScene_Async() //���̵� �񵿱�
    //{
    //    LoadSceneManager.Instance.LoadSceneAsync(_nextScene);
    //}

    void GoToLoadingScene() //�ܼ� ���̵�
    {
        LoadSceneManager.Instance.LoadScene(_nextScene);
    }    
}
