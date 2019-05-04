using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class MapManager
{
    public static GameObject mMapObj;//创建一个gameobject
    public const string MapPath = "Maps/";//地图路径
    public static List<GameObject> mMapRunBox = new List<GameObject>();//砖块的数组

    public static List<Vector3> GetMapPath(string map_name)
    {
        TextAsset ta = Resources.Load<TextAsset>(MapPath + map_name);//ta为地图坐标
        string text = ta.text;//获取坐标文本
        string[] pos_str = text.TrimEnd('\n').Split('\n');//把坐标切割保存为字符串数组
        List<Vector3> list = new List<Vector3>();//创建一个保存vector3的数组
        string[] pos_xz;//创建一个空的字符串数组
        for (int i = 0; i < pos_str.Length; i++)//循环遍历初次切割的数组pos_str
        {
            pos_xz = pos_str[i].Split(',');//将其根据逗号切割
            if (pos_xz.Length == 2)//作保护处理，若长度为2
            {
                list.Add(new Vector3(int.Parse(pos_xz[0]), 0, int.Parse(pos_xz[1])));//int.parse将类型转化为int型，并将其保存为一个坐标放入创建的坐标数组
            }
        }
        for (int i = 0; i < mMapRunBox.Count; i++)//如果之前有地图，则将其删除该数组中数据
        {
            Object.Destroy(mMapRunBox[i]);
        }
        if (list.Count > 0)//作保护，如果坐标数组不为空
        {
            GameObject box_obj = Resources.Load<GameObject>("Model/map_box");//box_obj为导入的砖块
            Vector3 pos = list[0];//pos为第一个坐标的位置
            box_obj = GameObject.Instantiate(box_obj);//实例化砖块
            box_obj.transform.localPosition = pos;//砖块的位置为初始位置pos
            mMapRunBox.Add(box_obj);//将砖块添加至地图数组中
            for (int i = 1; i < list.Count; i++)//循环遍历切割后坐标数组
            {
                while (pos.x > list[i].x)//如果下个坐标的x坐标小于当前x坐标
                {
                    pos.x--;//将当前x坐标减一
                    box_obj = GameObject.Instantiate(box_obj);//实例化砖块
                    box_obj.transform.localPosition = pos;//刷新当前位置
                    mMapRunBox.Add(box_obj);//添加至地图数组中
                }
                while (pos.x < list[i].x)
                {
                    pos.x++;
                    box_obj = GameObject.Instantiate(box_obj);
                    box_obj.transform.localPosition = pos;
                    mMapRunBox.Add(box_obj);
                }
                while (pos.z < list[i].z)
                {
                    pos.z++;
                    box_obj = GameObject.Instantiate(box_obj);
                    box_obj.transform.localPosition = pos;
                    mMapRunBox.Add(box_obj);
                }
                while (pos.z > list[i].z)
                {
                    pos.z--;
                    box_obj = GameObject.Instantiate(box_obj);
                    box_obj.transform.localPosition = pos;
                    mMapRunBox.Add(box_obj);
                }
            }
        }
        return list;//返回一个地图坐标
    }

    public static void ClearMapBox()//清除砖块数组
    {
        for (int i = 0; i < mMapRunBox.Count; i++)
        {
            Object.Destroy(mMapRunBox[i]);//删除砖块
        }
        mMapRunBox.Clear();//将数组清空
    }
}
