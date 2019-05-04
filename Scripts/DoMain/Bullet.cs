using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public Enemy mTarget;
    public float mMoveSpeed;//移动速度
    public int mAttack;//攻击力
    private Vector3 mTargetPos;//子弹位置

    //定义两个接口
    public void InitData(Enemy enemy,float move_spd,int atk)//敌人还存在的时候
    {
        mTarget = enemy;
        mMoveSpeed = move_spd;
        mAttack = atk;
        mTargetPos = enemy.transform.localPosition;//子弹位置为敌人位置

    }
    public void InitData(Vector3 pos , float move_spd, int atk)//敌人不存在的时候
    {
        mTarget = null;
        mMoveSpeed = move_spd;
        mAttack = atk;
        mTargetPos = pos;

    }

    private float rang;
    private float tutal_time;
	
	void Update () 
    {
        if (mTarget == null) //如果敌人为空
        {
            if (mMoveSpeed >0)
            {
                rang = Vector3.Distance(transform.localPosition, mTargetPos);//子弹和敌人距离
                tutal_time = rang / mMoveSpeed;//运行总时间
            }
            else
            {
                tutal_time = 0;
            }
            if (Time.deltaTime >= tutal_time)//如果每帧运行时间大于总时间
            {

                gameObject.SetActive(false);//将子弹显示关闭
            }
            else
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, mTargetPos, Time.deltaTime / tutal_time);//从当前位置运行到敌人位置，时间：暂时不懂

            }
        }
        else//如果有敌人
        {
            mTargetPos = mTarget.transform.localPosition;
            if (mMoveSpeed>0)
            {
                rang = Vector3.Distance(transform.localPosition, mTargetPos);
                tutal_time = rang / mMoveSpeed;
            }
            else
            {
                tutal_time = 0; 
            }

            if (Time.deltaTime >= tutal_time)//如果每帧运行时间大于总时间，子弹到达敌人位置
            {
                AudioMananger.PlaySound("hit");//播放击中音效
                mTarget.AddHP(-mAttack);//敌人生命值减少
                gameObject.SetActive(false);//将子弹隐藏

            }
            else
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, mTargetPos, Time.deltaTime / tutal_time);//同上

            }
        }
    }
}
