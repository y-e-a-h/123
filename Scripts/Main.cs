using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
        AudioMananger.InitData();
        AudioMananger.PlayMusic("music_login");
        MapManager.mMapObj = GameObject.Find("map"); //初始化map
        UIManager.EnterUI<UI_Main>();//载入界面


    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
