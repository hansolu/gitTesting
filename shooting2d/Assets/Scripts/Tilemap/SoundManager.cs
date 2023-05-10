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
    public AudioSource audioSource; //���� ��� ������Ʈ..�Ҹ��� ���� ��ü
    AudioClip[] audioClip_bgm; //������� ��Ƶ� �迭
    AudioClip[] audioClip_sfx; //�Ҹ� ���� ��ü//ȿ���� ��Ƶ� �迭

    void Init() 
    {
        audioClip_bgm = Resources.LoadAll<AudioClip>("Audio\\BGM"); //

    }

    public void PlaySFX(AudioSource _sorce, SoundKind _kind)
    {
        switch (_kind)
        {
            case SoundKind.Hit:
                _sorce.PlayOneShot(audioClip_sfx[(int)_kind], 1f);//�ѹ��� ���
                //_sorce.play//���� Ʋ���~
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

//audioSource.Play(); //��� 
//audioSource.Stop(); //���� 
//audioSource.Pause(); //�Ͻ����� 
//audioSource.UnPause(); //�Ͻ����� ���� 
//audioSource.playOnAwake = true; //�� ���۽� �ٷ� ��� 
//audioSource.loop = true; //�ݺ� ��� 
//audioSource.mute = true; //���Ұ� 
//audioSource.volume = 1.0f; //���� (0.0 ~ 1.0f) 
//audioSource.PlayOneShot(audioClip, 1.0f); //Ư�� Ŭ�� �ѹ� �� ��� //ȿ���� 
//audioSource.clip = audioClip; //����� Ŭ�� ��ü 
//audioSource.isPlaying : ��� ���� üũ

