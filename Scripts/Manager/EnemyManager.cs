using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager :MonoBehaviour
{
    public const string enemyPath = "Model/Role/"; //不可变变量定义敌人地址

    public static Enemy CreatEnemy(EnemyInfo info,List<Vector3> path_list) //根据名字创建敌人方法
    {
        GameObject obj = Resources.Load<GameObject>(enemyPath + info.ModelName);//获取到该目录下的物体
        GameObject enemy = Instantiate(obj);//实例化该物体
        Enemy En = enemy.AddComponent<Enemy>();//给该物体添加Enemy组件 
        En.InitData(path_list,info.HP,info.MoveSpd);
        return En; //返回该物体

    }

}
