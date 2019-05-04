using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float AttackRange = 3;//攻击距离
    public Enemy mAttackTarget;//敌人
    public float AttackCD = 0.8f;//攻击cd
    private float __time = 0;//
    public Animation mAnim;//攻击动画
    public int mAttack;//攻击力
    public string mBulletModelName;
    public float BulletMoveSpd;
    public string mAttackAnim;

    public void InitData(TowerInfo info)
    {
        mAttack = info.Attack;
        AttackCD = info.AttackCD;
        AttackRange = info.AttackRange;
        mBulletModelName = info.BulletModelName;
        BulletMoveSpd = info.BulletMoveSpd;
        mAttackAnim = info.AttackAnim;

    }

    public void Start()
    {
        mAnim = GetComponent<Animation>();//获取animation组件
        mAnim["Idle"].layer = -1;//将该动画优先级调高，使得在无其他动画时默认该动画

    }



    public void Update()
    {
        __time += Time.deltaTime;//时间加上每帧时间
        if (__time >= AttackCD)//如果时间大于攻击cd
        {
            __time = 0;//将时间置零
            if (mAttackTarget == null)
            {
                mAttackTarget = GetAttackTarget();//获取在攻击范围内的敌人
                if (mAttackTarget != null)
                {
                    transform.LookAt(mAttackTarget.transform);//将防御塔朝向敌人
                    Attack(mAttackTarget);//攻击敌人
                }
            }
            else if (IsAttactEnemy(mAttackTarget))//如果存在敌人且能攻击
            {
                Attack(mAttackTarget);//攻击

            }
            else//这段可以优化
            {
                mAttackTarget = GetAttackTarget();
                if (mAttackTarget != null)
                {
                    transform.LookAt(mAttackTarget.transform);
                    Attack(mAttackTarget);
                }
            }
        }

    }

    public bool IsAttactEnemy(Enemy enemy)//判断是否可以攻击
    {
        if (Vector3.Distance(enemy.transform.localPosition, transform.localPosition) <= AttackRange)//如果防御塔和敌人距离小于攻击距离
        {
            return true;//返回true
        }
        return false;//返回fasle
    }

    public Enemy GetAttackTarget()
    {
        for (int i = 0; i < BattleManager.mEnemyList.Count; i++)//循环遍历敌人数组
        {
            if (Vector3.Distance(transform.localPosition, BattleManager.mEnemyList[i].transform.localPosition) <= AttackRange)//如果防御塔距离小于攻击距离
            {
                return BattleManager.mEnemyList[i];//返回敌人

            }
        }
        return null;
    }


    public IEnumerator AttackButtle(Enemy enemy, Vector3 pos, float time)
    {
        AudioMananger.PlaySound("attack");
        yield return new WaitForSeconds(time);

        Bullet bullet = BulletManager.CreatBullet(mBulletModelName);
        if (enemy != null)
        {
            bullet.InitData(enemy, BulletMoveSpd, mAttack);
        }
        else
        {
            bullet.InitData(pos, BulletMoveSpd, mAttack);
        }
        bullet.transform.localPosition = transform.localPosition;

    }



    public void Attack(Enemy enemy)
    {
        AnimationState __as = mAnim.PlayQueued(mAttackAnim, QueueMode.PlayNow, PlayMode.StopSameLayer);//播放攻击动画
        if (AttackCD > 1)//调节动画速度
        {
            __as.speed = 1 / AttackCD;

        }
        else
        {
            __as.speed = 1;
        }
        StartCoroutine(AttackButtle(enemy, enemy.transform.localPosition, __as.speed * 0.8f));
    }
}