using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UI_Main : UI_Layer
{
    public Text mTextTitle;
    public Button mBtnStart;
    public Button mBtnClose;
    public Image mImageMusic;
    public Image mImageSound;


    public void SetMusicOn(bool is_on)
    {
        if (is_on)
        {
            mImageMusic.rectTransform.anchoredPosition = new Vector3(-50, 1, 0);
            mImageMusic.color = new Color32(233, 0, 0, 255);
        }
        else
        {
            mImageMusic.rectTransform.anchoredPosition = new Vector3(0, 1, 0);
            mImageMusic.color = new Color32(0, 9, 215, 255);
        }
    }

    public void SetSoundOn(bool is_on)
    {
        if (is_on)
        {
            mImageSound.rectTransform.anchoredPosition = new Vector3(-50, 1, 0);
            mImageSound.color = new Color32(233, 0, 0, 255);
        }
        else
        {
            mImageSound.rectTransform.anchoredPosition = new Vector3(0, 1, 0);
            mImageSound.color = new Color32(0, 9, 215, 255);
        }
    }


    public void Start()
    {
       //__init_node(transform);
    }
    public override void OnEnter()
    {
        SetMusicOn(AudioMananger.mIsMusicOn);
        SetSoundOn(AudioMananger.mIsSoundOn);
    }

    public override void OnExit()
    {

    }


    public override void OnButtonClick(string name, GameObject obj) //封装按键后的事件
    {

        switch (name)
        {
            case "Button_Start":
                Debug.Log("开始游戏！");
                Close();
                UIManager.EnterUI<UI_SeletLevel>();
                break;
            case "Button_Close": 
                Close();
                break;
            case "Button_Music":
                if (AudioMananger.mIsMusicOn)
                {
                    SetMusicOn(false);
                    AudioMananger.SetMusicOn(false);
                }
                else
                {
                    SetMusicOn(true);
                    AudioMananger.SetMusicOn(true);
                }
                break;
            case "Button_Sound":
                if (AudioMananger.mIsSoundOn)
                {
                    SetSoundOn(false);
                    AudioMananger.SetSoundOn(false);
                }
                else
                {
                    SetSoundOn(true);
                    AudioMananger.SetSoundOn(true);
                }
                break;
        }
    }
                

    public override void OnNodeAsset(string name, GameObject obj)//根据循环遍历出的组件通过switch挑选处理
    {
        switch (name)
        {
            case "Text_Title":
                mTextTitle = obj.GetComponent<Text>();
                mTextTitle.text = "xxx";
                break;
            case "Image_music":
                mImageMusic = obj.GetComponent<Image>();
                break;
            case "Image_sound":
                mImageSound = obj.GetComponent<Image>();
                break;

        }

    }

}
