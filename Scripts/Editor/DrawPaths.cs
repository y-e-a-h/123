using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using System;
using System.IO;

public class DrawPaths : MonoBehaviour
{
    public List<Vector3> mMapList = new List<Vector3>();

    // Use this for initialization
    void Start()
    {
        mMapList.Add(Vector3.zero);
        //mMapList.Add(new Vector3(10, 0, 0));
        mMapList.Add(new Vector3(10, 0, 10));
        mMapList.Add(new Vector3(5, 0, 2));
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetMouseButtonDown(0)) {
        //    Debug.Log("左键按下");
        //}
        //if(Input.GetMouseButton(0)) {
        //    Debug.Log("鼠标左键按下移动");
        //}
        //if(Input.GetMouseButtonUp(0)) {
        //    Debug.Log("左键松开");
        //}

        //if(Input.GetAxis("Mouse ScrollWheel") > 0) {
        //    Debug.Log("大于0：" + Input.GetAxis("Mouse ScrollWheel"));
        //}

        //if(Input.GetAxis("Mouse ScrollWheel") < 0) {
        //    Debug.Log("小于0："+Input.GetAxis("Mouse ScrollWheel"));
        //}

        //if(Input.GetKey(KeyCode.W)) {
        //    Debug.Log("W按键");
        //}
        //if(Input.GetKeyDown(KeyCode.W)) {
        //    Debug.Log("W按键被按下");
        //}
        //if(Input.GetKeyUp(KeyCode.W)) {
        //    Debug.Log("W按键被松开");
        //}
        if (Input.GetMouseButtonDown(0))
        {//添加路径点
            Vector3 pos = Input.mousePosition;
            pos.z = 10;
            pos = Camera.main.ScreenToWorldPoint(pos);
            Debug.Log(pos);

            pos.x = Mathf.RoundToInt(pos.x);
            pos.z = Mathf.RoundToInt(pos.z);

            this.mMapList.Add(pos);
        }
        if (Input.GetMouseButtonDown(1))
        {//取消路径点
            if (this.mMapList.Count > 0)
            {
                this.mMapList.RemoveAt(this.mMapList.Count - 1);
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {//Camera视野变小
            Camera.main.orthographicSize *= 0.9f;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {//Camera视野变大
            Camera.main.orthographicSize *= 1.1f;
        }

        if (Input.GetKey(KeyCode.A))
        {//往左
            Vector3 pos = Camera.main.transform.localPosition;
            pos.x -= 1;
            Camera.main.transform.localPosition = pos;
        }
        if (Input.GetKey(KeyCode.D))
        {//往右
            Vector3 pos = Camera.main.transform.localPosition;
            pos.x += 1;
            Camera.main.transform.localPosition = pos;
        }
        if (Input.GetKey(KeyCode.W))
        {//往上
            Vector3 pos = Camera.main.transform.localPosition;
            pos.z += 1;
            Camera.main.transform.localPosition = pos;
        }
        if (Input.GetKey(KeyCode.S))
        {//往下
            Vector3 pos = Camera.main.transform.localPosition;
            pos.z -= 1;
            Camera.main.transform.localPosition = pos;
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < this.mMapList.Count; i++)
            {
                sb.AppendLine(string.Format("{0},{1}", this.mMapList[i].x, this.mMapList[i].z));
            }
            string filepath = EditorUtility.SaveFilePanel("保存地图文件", ".", DateTime.Now.ToString("yyyyMMddHHmm"), "txt");
            File.WriteAllText(filepath, sb.ToString());
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (mMapList.Count > 0)
        {
            Gizmos.DrawCube(mMapList[0], new Vector3(0.98f, 0.98f, 0.98f));
        }
        Gizmos.color = Color.red;
        int _x, _z;
        for (int i = 1; i < mMapList.Count; i++)
        {
            _x = (int)mMapList[i - 1].x;
            _z = (int)mMapList[i - 1].z;
            while (_x > mMapList[i].x)
            {
                _x--;
                Gizmos.DrawCube(new Vector3(_x, 0, _z), new Vector3(0.98f, 0.98f, 0.98f));
            }
            while (_x < mMapList[i].x)
            {
                _x++;
                Gizmos.DrawCube(new Vector3(_x, 0, _z), new Vector3(0.98f, 0.98f, 0.98f));
            }
            while (_z > mMapList[i].z)
            {
                _z--;
                Gizmos.DrawCube(new Vector3(_x, 0, _z), new Vector3(0.98f, 0.98f, 0.98f));
            }
            while (_z < mMapList[i].z)
            {
                _z++;
                Gizmos.DrawCube(new Vector3(_x, 0, _z), new Vector3(0.98f, 0.98f, 0.98f));
            }
        }
        Gizmos.color = Color.yellow;
        if (mMapList.Count > 0)
        {
            Gizmos.DrawCube(mMapList[mMapList.Count - 1], new Vector3(0.98f, 0.98f, 0.98f));
        }
    }


}
