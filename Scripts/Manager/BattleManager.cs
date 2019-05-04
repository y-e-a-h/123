using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BattleManager 
{
    public static List<Vector3> mPathList; //创建一个list
    public static List<EnemyInfo> CallEnemyList = new List<EnemyInfo>();//敌人种类的list
    public static int HomeLife;//家生命值
    public static UI_Battle ui_battle;
    public static bool mBattleStop;//判断战斗是否结束
    public static int mCurWave;//战斗波数
    public static int mWaveTime;//回合倒计时

    private static float _time;


    public static List<Enemy> mEnemyList = new List<Enemy>();//创建敌人数组
    public static List<Tower> mTowerList = new List<Tower>();//创建防御塔数组

    public static EnemyInfo enemyInfo(string _name,int _hp,float _spd,int _count,int _wave)//敌人种类接口
    {
        EnemyInfo info = new EnemyInfo
        {
            ModelName = _name,
            HP = _hp,
            MoveSpd = _spd,
            Count = _count,
            Wave = _wave
            
        };
        return info;
    }

    public static void InitData(UI_Battle ui,List<Vector3> path_list)
    {
        ui_battle = ui;
        mPathList = path_list;
        mCurWave = 0;//波数初始化为0
        HomeLife = 5;//家生命值初始化为5
        _time = 0;
        mEnemyList.Clear();
        CallEnemyList.Add(enemyInfo("Warrior", 10,1.5f, 5, 1));
        //CallEnemyList.Add(enemyInfo("Warrior_example1", 5, 4, 3));
        //CallEnemyList.Add(enemyInfo("Warrior_example2", 50, 1, 1));
        CallEnemyList.Add(enemyInfo("Warrior", 5, 1.5f, 1, 2));
        CallEnemyList.Add(enemyInfo("Warrior", 50, 0.5f, 1, 3));



        for (int i = 0; i < mEnemyList.Count; i++)
        {
            mEnemyList[i].DeleteObj();
        }
        mBattleStop = false;
    }


    public static void EnemyAttack() //敌人攻击
    {
        if (HomeLife > 1)
        {
            HomeLife--;
        }
        else
        {
            HomeLife = 0;
            mBattleStop = true; //战斗停止
            UIManager.EnterUI<UI_BattleResult>().InitData(false);
            ui_battle.BattleStop();//隐藏创建防御塔按钮
            Debug.Log("died");

        }
        ui_battle.RefreshLife();//刷新生命
    }

    public static void Clear()
    {
        MapManager.ClearMapBox();
        for (int i = 0; i < mEnemyList.Count; i++)
        {
            GameObject.Destroy(mEnemyList[i].gameObject);
        }
        mEnemyList.Clear();
        for (int i = 0; i < mTowerList.Count; i++)
        {
            GameObject.Destroy(mTowerList[i].gameObject);
        }
        mTowerList.Clear();

    }


    public static void BattleUpdate()//创建敌人
    {
        if (mBattleStop == false)//如果战斗还没有结束
        {
            if (CallEnemyList.Count > 0)//如果应该创建的敌人种类大于0
            {
                _time += Time.deltaTime;//_time根据update增加 
                if (_time > 0.5f)// 每一秒创建一个
                {
                    _time--;
                    if (mWaveTime > 0)
                    {
                        mWaveTime -= 1;
                        ui_battle.RefreshTimeDownCount(mWaveTime);
                    }
                    else if (mCurWave < CallEnemyList[0].Wave)
                    {
                        mCurWave++;
                        mWaveTime = 5;
                        ui_battle.RefreshTimeDownCount(mWaveTime);
                        ui_battle.RefreshWave(mCurWave);
                    }
                    else
                    {

                        Enemy enemy = EnemyManager.CreatEnemy(CallEnemyList[0], mPathList);//创建
                        mEnemyList.Add(enemy);//加入敌人的列表
                        ui_battle.BindEnemy(enemy);
                        _time -= 0.5f;
                        if (CallEnemyList[0].Count > 1)
                        {
                            CallEnemyList[0].Count--;
                        }
                        else
                        {
                            CallEnemyList.RemoveAt(0);
                        }
                    }

                }
            }
            
          

            for (int i = mEnemyList.Count - 1; i >= 0; i--)
            {

                if (mEnemyList[i] == null)//如果为空
                {
                    mEnemyList.RemoveAt(i);//列表中删除该敌人
                }
                else
                { 
                    mEnemyList[i].BattleUpdate();//每一个敌人进行创建
                    if (mEnemyList[i].mHP <= 0)
                    {
                        mEnemyList.RemoveAt(i);
                    }
                }
            }

            if (HomeLife > 0 && CallEnemyList.Count == 0 && mEnemyList.Count == 0)//敌人完全被消灭
            {
                mBattleStop = true;
                UIManager.EnterUI<UI_BattleResult>().InitData(true);
                ui_battle.BattleStop();//隐藏创建防御塔按钮
                Debug.Log("vectory!");
            }

        }
    }

    public static void CreateTower(Vector3 pos,TowerInfo info) //封装好的创建防御塔函数
    {
        GameObject tower = TowerManager.CreatTower(info.ModelName);//创建防御塔
        tower.transform.localPosition = pos;//该防御塔坐标为输入地址pos
        Tower tw = tower.AddComponent<Tower>();//新创建tw为tower加入tower组件
        tw.InitData(info);
        mTowerList.Add(tw);//将tw加入防御塔列表

    }

    public static bool IsClickTower(Vector3 pos)//判断是否在敌人行进路线以及已存在的防御塔位置
    {
        for (int i = 0; i < mTowerList.Count; i++)//循环遍历防御塔列表
        {
            if (pos == mTowerList[i].transform.localPosition)//
            {
                return true;
            }
        }
        for (int i = 0; i < MapManager.mMapRunBox.Count; i++)//循环遍历行进路线列表
        {
            if (Vector3.Distance(pos, MapManager.mMapRunBox[i].transform.localPosition) < 0.1f)//直接相等可能因为浮点数有误差，故采用判断点击位置和路线位置距离
            {
                return true;
            }
        }
    
        return false;
    }


}
