using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager
{

    public const string TowerPath = "Model/Role/"; //不可变变量定义敌人地址

    public static GameObject CreatTower(string model_name) //根据名字创建敌人方法
    {
        GameObject obj = Resources.Load<GameObject>(TowerPath + model_name);//获取到该目录下的物体
        GameObject tower = GameObject.Instantiate(obj);//实例化该物体
        return tower; //返回该物体

    }
}
