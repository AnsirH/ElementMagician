using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name; // 곡의 이름

    public AudioClip clip; // 곡
}

public class SoundManager : MonoBehaviour
{
    static public SoundManager instance;


    public AudioSource[] audioSourceEffect;
    public AudioSource audioSourceBgm;

    public string[] playSoundName;

    public Sound[] effectSounds;
    public Sound[] bgmSounds;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    public void PlaySoundEffect(string _name)
    {
        for(int i = 0; i < effectSounds.Length; i++)
        {
            if(_name == effectSounds[i].name)
            {
                for(int j = 0; j < audioSourceEffect.Length; j++)
                {
                    if(!audioSourceEffect[j].isPlaying)
                    {
                        playSoundName[j] = effectSounds[i].name;
                        audioSourceEffect[j].clip = effectSounds[i].clip;
                        audioSourceEffect[j].Play();
                        return;
                    }
                }
                Debug.Log("모든 가용 AudioSource가 사용중입니다.");
                return;
            }
        }
        Debug.Log(_name + "사운드가 SoundManager에 등록되지 않았습니다.");

    }

    public void StopAllSE()
    {
        for(int i = 0; i < audioSourceEffect.Length; i++)
        {
            audioSourceEffect[i].Stop();
        }
    }

    public void StopSE(string _name)
    {
        for(int i = 0; i < audioSourceEffect.Length; i++)
        {
            if(playSoundName[i] == _name)
            {
                audioSourceEffect[i].Stop();
                return;
            }
        }

        Debug.Log("재생중인" + _name + "사운드가 없습니다.");
    }
}
