using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SeletLevel : UI_Layer
{


    public override void OnNodeLoad()
    {
        base.OnNodeLoad();
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public void EnterBattle(int index)
    {
        Close();
        List<TowerInfo> tower_list = new List<TowerInfo>();
        if (index % 3 == 1||index %3 == 0)
        {
            tower_list.Add(new TowerInfo
            {
                Attack = 8,
                AttackAnim  = "AttackMelee2",
                AttackCD  = 1f,
                AttackRange = 2,
                BtnName = "Melle",
                BulletModelName = "",
                BulletMoveSpd = 0,
                ModelName = "Warrior_example1"
            });
        }
        if (index % 3 == 2 || index % 3 == 0)
        {
            tower_list.Add(new TowerInfo
            {
                Attack = 3,
                AttackAnim = "AttackRange1",
                AttackCD = 0.5f,
                AttackRange = 6,
                BtnName = "Remotely",
                BulletModelName = "bullet",
                BulletMoveSpd = 15,
                ModelName = "Archer_example2"
            });
        }
        if (index == 9)
        {
            tower_list.Add(new TowerInfo
            {
                Attack = 1,
                AttackAnim = "AttackRange1",
                AttackCD = 0.3f,
                AttackRange = 10,
                BtnName = "Super Remotely",
                BulletModelName = "bullet",
                BulletMoveSpd = 20,
                ModelName = "Archer_example2"
            });
        }
        UIManager.EnterUI<UI_Battle>().InitData(index.ToString(),tower_list);
    }

    public override void OnButtonClick(string name, GameObject obj)
    {
        switch (name)
        {
            case "btn_back":
                Close();
                UIManager.EnterUI<UI_Main>();//打开开始界面
                break;
            case "btn_lv_1":
                EnterBattle(1);
                break;
            case "btn_lv_2":
                EnterBattle(2);
                break;
            case "btn_lv_3":
                EnterBattle(3);
                break;
            case "btn_lv_4":
                EnterBattle(4);
                break;
            case "btn_lv_5":
                EnterBattle(5);
                break;
            case "btn_lv_6":
                EnterBattle(6);
                break;
            case "btn_lv_7":
                EnterBattle(7);
                break;
            case "btn_lv_8":
                EnterBattle(8);
                break;
            case "btn_lv_9":
                EnterBattle(9);
                break;

        }
    }

    public override void OnNodeAsset(string name, GameObject obj)
    {
        base.OnNodeAsset(name, obj);
    }
}
