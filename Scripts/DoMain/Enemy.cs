using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public List<Vector3> mPathList = new List<Vector3>();//新创建一个新的存放坐标的list
    public float mMoveSpeed = 2f;//定义移动速度为2
    public int mHP = 10;
    public int MaxHP = 10;//最大  血量
    public Item_Enemy_HP item_hp;
    public Transform mHP_Pos;


    public void InitData(List<Vector3> path, int hp,float move_spd)
    {
        mPathList.AddRange(path);//将文件导入的坐标放入创建的list
        if (mPathList.Count > 0) //删除list中第一个点
        {
            transform.localPosition = mPathList[0];
            mPathList.RemoveAt(0);
        }
        mHP = MaxHP = hp;
        mMoveSpeed = move_spd;

        Animation anim = GetComponent<Animation>();
        anim.PlayQueued("RunFront",QueueMode.PlayNow);
        mHP_Pos = transform.FindChild("hp_pos");

    }

    public void AddHP(int hp)
    {
        mHP += hp;
        if (mHP<=0)
        {
            DeleteObj();

        }
        item_hp.ReFershHP();
    }



    public void SetDirection() { }



    public void BattleUpdate()
    {
        if(mPathList.Count > 0)
        {
            Vector3 pos = mPathList[0];//创建临时坐标pos为第一个要移动到的坐标
            float rang; //当前坐标到下一个坐标的距离
            if ( transform.localPosition.x == pos.x) //如果x轴相同的情况下 
            {
                //y轴移动
                rang = Mathf.Abs(transform.localPosition.z - pos.z);//取绝对值
                if (transform.localPosition.z>pos.z)
                {
                    transform.localEulerAngles = new Vector3(0, 180, 0);
                }
                else
                {
                    transform.localEulerAngles = new Vector3(0, 0, 0);

                }
            }
            else
            {
                //x轴移动
                pos.z = transform.localPosition.z;
                rang = Mathf.Abs(transform.localPosition.x - pos.x);
                if(transform.localPosition.x > pos.x)
                {
                    transform.localEulerAngles = new Vector3(0, 270, 0);
                }
                else
                {
                    transform.localEulerAngles = new Vector3(0, 90, 0);

                }
            }
            float tutal_Time = rang / mMoveSpeed;  //距离除以移动速度

            if (Time.deltaTime >= tutal_Time)//根据updata刷新时间间隔 如果大于剩余时间 即可以马上到达
            {
                transform.localPosition = pos; //移动到下一个目标点
                if (transform.localPosition == mPathList[0])//如果到达第一个目标点
                {
                    mPathList.RemoveAt(0);//删除该目标点
                }
            }
            else
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, pos, Time.deltaTime / tutal_Time);

            }
        }

        else
        {
            mHP = 0;
            DeleteObj(); //到达最后点 删除该物体
            BattleManager.EnemyAttack();
        }
    }



    public void DeleteObj() //删除物体方法
    {
        Destroy(gameObject);
    }
}
