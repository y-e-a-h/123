using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo
{

    public string ModelName;//敌人名称
    public int HP;//敌人血量
    public float MoveSpd;//敌人移动速度
    public int Count;//敌人数量
    public int Wave;//波数
}


public class TowerInfo
{

    public string BtnName;
    public string ModelName;
    public int Attack;
    public float AttackCD;
    public float AttackRange;
    public string AttackAnim;
    public string BulletModelName;
    public float BulletMoveSpd;
}