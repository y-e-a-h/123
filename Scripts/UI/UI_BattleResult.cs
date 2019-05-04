using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_BattleResult : UI_Layer 
{
    public Text mTextResult;

    public void InitData(bool is_win)
    {
        if (is_win)
        {
            mTextResult.text = "Win!";
            AudioMananger.PlaySound("battle_success");
        }
        else
        {
            mTextResult.text = "False!";
            AudioMananger.PlaySound("battle_fail");

        }
    }

    public override void OnNodeLoad()
    {
    }

    public override void OnEnter()
    {
    }

    public override void OnExit()
    {
    }

    public override void OnButtonClick(string name, GameObject obj)
    {
        switch (name)
        {
            case "btn_return":
                BattleManager.Clear();
                UIManager.ExitUI(this);
                UIManager.ExitALLUI();//关闭所有界面
                AudioMananger.PlayMusic("music_login");
                UIManager.EnterUI<UI_Main>();//打开开始界面
                break;
        }
    }

    public override void OnNodeAsset(string name, GameObject obj)
    {
        switch (name)
        {
            case "Text_result":
                mTextResult = obj.GetComponent<Text>();
                break;
        }
    }
}
