using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item_Enemy_HP : MonoBehaviour {

    public Scrollbar mBarHP;//滑动血条
    public Enemy mTarget;


    public void InitData(Enemy enemy)//
    {
        mTarget = enemy;
        enemy.item_hp = this;
        mBarHP = transform.FindChild("bar_hp").GetComponent<Scrollbar>();//找到血条
        ReFershHP();//刷新血量
        ReFershPos();//刷新位置
    }

    public void ReFershPos()//刷新位置
    {
        Vector3 pos = mTarget.mHP_Pos.position;//敌人的世界坐标
        pos = Camera.main.WorldToScreenPoint(pos);//转换为屏幕坐标
        pos.x -= Screen.width /2;//>>1 ＝ ／2
        pos.y -= Screen.height /2 +50;
        pos = pos * 640 / Screen.height;//该坐标为UI中的坐标 因为画布设定高度为640 
        transform.localPosition = pos;
    }

    public void ReFershHP()//刷新血量
    {
        mBarHP.size = mTarget.mHP * 1f / mTarget.MaxHP;//当前血量除以最大血量
    }

    void LateUpdate () //lateupdate最后更新
    {
        if (mTarget == null)//如果敌人为空
        {
            Destroy(gameObject);//删除血条
        }
        else
        {
            ReFershPos();//刷新位置
        }

    }
}
