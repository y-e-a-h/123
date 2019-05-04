using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class UI_Layer : MonoBehaviour {

    public void __init_node(Transform tf) //循环遍历子物体查找button组件监听调用
    {
        OnNodeAsset(tf.name, tf.gameObject); 
        Button btm = tf.GetComponent<Button>();  //获取button组件
        if (btm != null) //如果该ui存在button组件
        {
            btm.onClick.AddListener(() => //当按键按下后监听
            {
                OnButtonClick(btm.name, btm.gameObject); //调用OnButtonClick方法  
            });
        }

        for (int i = 0; i < tf.childCount; i++)
        {
            __init_node(tf.GetChild(i));
        }

    }

    private void Start()
    {
        OnEnter();
    }

    private void OnDestroy()
    {
        OnExit();
    }

    public virtual void OnNodeLoad()
    {

    }

    public virtual void OnEnter()
    {

    }

    public virtual void OnExit()
    {

    }

    public void Close()
    {
        UIManager.ExitUI(this);
    }

    public virtual void OnButtonClick(string name,GameObject obj)
    {

    }

    public virtual void OnNodeAsset(string name,GameObject obj)
    {

    }
}
