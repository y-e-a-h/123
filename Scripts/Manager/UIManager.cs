using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour 

{
    public const string UIPath = "UI_Perfabs/";//UI的地址
    public static GameObject mUIRoot;//创建gameobject
    public static List<MonoBehaviour> mUIList = new List<MonoBehaviour>(); //定义一个集合ui组件

    private static void InitData()
    {
        mUIRoot = GameObject.Find("Canvas");//找到画布
    }

    public static T EnterUI<T>() where T :UI_Layer
    {
        if(mUIRoot == null)//作保护，直到找到canvas画布
        {
            InitData(); 
        }
        GameObject obj = Resources.Load<GameObject>(UIPath + typeof(T));//typeof是获取UI名，加入地址找到resource下的ui，
        GameObject ui = Instantiate(obj);//实例化
        ui.transform.SetParent(mUIRoot.transform);//设置父子关系，设置到该画布下
        ui.transform.localPosition = Vector3.zero; //重初始化
        ui.transform.localScale = Vector3.one; //重初始化
        ui.transform.localRotation = Quaternion.identity;//重初始化

        RectTransform rect = ui.GetComponent<RectTransform>(); //设置RectTransform属性
        rect.offsetMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;

        T t = ui.AddComponent<T>();
        t.__init_node(t.transform);//因为继承UI_Layer，所以调用UI_Layer的__init_node的循环遍历组件调用按钮
        t.OnNodeLoad();//依旧觉得没用
        mUIList.Add(t);//将该UI添加进UI数组
        return t;//返回一个UI界面
    }

    public static void ExitUI(MonoBehaviour mono)//退出UI方法
    {
        mUIList.Remove(mono);//在数组中删除该UI
        Destroy(mono.gameObject);//删除UI界面
    }

    public static void ExitALLUI()//循环遍历去除UI数组中的UI
    {
        for (int i = 0; i < mUIList.Count; i++)
        {
            //Destroy(mUIList[i]);
            Destroy(mUIList[i].gameObject);//删除UI界面
            mUIList.Clear();//清空UI数组
        }
    }
}
