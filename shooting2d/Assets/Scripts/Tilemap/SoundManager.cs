using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundKind
{ 
    Hit,
    Walk,
    Talk,
    
}
public class SoundManager : MonoBehaviour
{    
    public AudioSource audioSource; //음악 재생 컴포넌트..소리를 내는 주체
    AudioClip[] audioClip_bgm; //배경음악 모아둔 배열
    AudioClip[] audioClip_sfx; //소리 파일 자체//효과음 모아둔 배열

    void Init() 
    {
        audioClip_bgm = Resources.LoadAll<AudioClip>("Audio\\BGM"); //

    }

    public void PlaySFX(AudioSource _sorce, SoundKind _kind)
    {
        switch (_kind)
        {
            case SoundKind.Hit:
                _sorce.PlayOneShot(audioClip_sfx[(int)_kind], 1f);//한번만 재생
                //_sorce.play//음악 틀어둠~
                break;
            case SoundKind.Walk:
                break;
            case SoundKind.Talk:
                break;
            default:
                break;
        }
    }
}

//audioSource.Play(); //재생 
//audioSource.Stop(); //정지 
//audioSource.Pause(); //일시정지 
//audioSource.UnPause(); //일시정지 해제 
//audioSource.playOnAwake = true; //씬 시작시 바로 재생 
//audioSource.loop = true; //반복 재생 
//audioSource.mute = true; //음소거 
//audioSource.volume = 1.0f; //볼륨 (0.0 ~ 1.0f) 
//audioSource.PlayOneShot(audioClip, 1.0f); //특정 클립 한번 만 재생 //효과음 
//audioSource.clip = audioClip; //오디오 클립 교체 
//audioSource.isPlaying : 재생 여부 체크

