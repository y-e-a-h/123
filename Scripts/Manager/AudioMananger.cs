using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMananger
{
    public const string MusicPath = "audio/bg/";//背景音乐地址
    public const string SoundPath = "audio/sound/";//战斗音效地址
    public static bool mIsMusicOn;
    public static bool mIsSoundOn;

    public static void InitData()
    {
        mIsMusicOn = PlayerPrefs.GetInt("is_music_on", 1) == 1;
        mIsSoundOn = PlayerPrefs.GetInt("is_sound_on", 1) == 1;
    }

    public static void SetMusicOn(bool is_on)
    {
        mIsMusicOn = is_on;
        if (is_on)
        {
            PlayerPrefs.GetInt("is_music_on", 1);
        }
        else
        {
            PlayerPrefs.GetInt("is_music_on", 0);
        }

        if (_as_music!=null)
        {
            if (_as_music.isPlaying)
            {
                //当前已经有背景音乐
                if (is_on == false)
                {
                    _as_music.Stop();
                }
            }
            else
            {
                //当前没有背景音乐
                if (is_on == true)
                {
                    _as_music.Play();
                }


            }
        }
    }

    public static void SetSoundOn(bool is_on)
    {
        mIsSoundOn = is_on;
        if (is_on)
        {
            PlayerPrefs.GetInt("is_sound_on", 1);
        }
        else
        {
            PlayerPrefs.GetInt("is_sound_on", 0);
        }
        for (int i = 0; i < ASList.Count; i++)
        {
            if (ASList[i].isPlaying)
            {
                if (is_on == false)
                {
                    ASList[i].Stop();
                }
            }
        }
    }



    public static AudioSource _as_music;//声音源

    public static void PlayMusic(string name)//播放背景音乐
    {
        if (_as_music == null)//如果声音源为空，即没有播放音乐
        {
            _as_music = Camera.main.gameObject.AddComponent<AudioSource>();//在maincamera中创建audiosource组件
            _as_music.clip = Resources.Load<AudioClip>(MusicPath + name);//音乐片段为从resource中load
            _as_music.loop = true;//将循环设为true
            if (mIsMusicOn)
            {
                _as_music.Play();//播放音乐
            }
        }
        else
        {
            _as_music.Stop();//如果有音乐播放，则停止该音乐
            _as_music.clip = Resources.Load<AudioClip>(MusicPath + name);//重新查找音乐
            _as_music.loop = true;
            if (mIsMusicOn)
            {
                _as_music.Play();//播放音乐
            }
        }
   
    }

    public static List<AudioSource> ASList = new List<AudioSource>(); //创建音源列表存放音效

    public static void PlaySound(string name)//播放音效
    {
        if (mIsSoundOn == false)
        {
            return;
        }
        AudioSource _as = null;//创建_as 作为音源
        for (int i = 0; i < ASList.Count; i++)//循环遍历音效列表
        {
            if (ASList[i].isPlaying == false)//如果
            {
                _as = ASList[i];
                break;
            }
        }
        if (_as == null)//
        {
            _as = Camera.main.gameObject.AddComponent<AudioSource>();
            ASList.Add(_as);
        }
        _as.clip = Resources.Load<AudioClip>(SoundPath + name);
        _as.loop = false;
        if (mIsSoundOn)
        {
            _as.Play();
        }
    }

}
