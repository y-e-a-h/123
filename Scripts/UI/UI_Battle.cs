using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Battle : UI_Layer
{
    public Text mTextLife;//新建text变量
    public GameObject mObjTowerContent;//防御塔容器  //command + option + f 替换  
    public GameObject mObjEnemyHPInfo;//新建血量
    public Text mTextWave;
    public Text mTwaveTime;

    public List<TowerInfo> mTowerList = new List<TowerInfo>();//防御塔数组
    public List<GameObject> mButtonList = new List<GameObject>();

    public GameObject mBtnBaseTower;//防御塔按钮

    private float mPreRang = -1;
    private Vector3 mTouchClickPos;
    private bool _is_drag = false;
    private bool _is_click_btn = false;

    public void Update()
    {
        if (Input.touchCount >= 2)
        {
            if (mTouchClickPos != Vector3.zero)
            {
                mTouchClickPos = Vector3.zero;
            }
            Touch touch_1 = Input.GetTouch(0);
            Touch touch_2 = Input.GetTouch(1);
            if (mPreRang < 0)
            {
                mPreRang = Vector2.Distance(touch_1.position, touch_2.position);
            }
            else
            {
                float _rang = Vector2.Distance(touch_1.position, touch_2.position);
                Camera.main.fieldOfView *= _rang / mPreRang;
            }
        }
        else if (Input.touchCount > 0 || Input.GetMouseButton(0))
        {
            if (mPreRang >= 0)
            {
                mPreRang = -1;
            }
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.name == "map_show")
                {
                    if (mTouchClickPos == Vector3.zero)
                    {
                        mTouchClickPos = hit.point;
                    }
                    else if (mTouchClickPos != hit.point)
                    {
                        _is_drag = true;
                        Camera.main.transform.localPosition += mTouchClickPos - hit.point;
                        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        if (Physics.Raycast(ray, out hit))
                        {
                            if (hit.collider.name == "map_show")
                            {
                                mTouchClickPos = hit.point;
                            }
                            else
                            {
                                mTouchClickPos = Vector3.zero;
                            }
                        }
                        else
                        {
                            mTouchClickPos = Vector3.zero;
                        }

                    }

                }

            }
        }
        else
        {
            if (mPreRang >= 0)
            {
                mPreRang = -1;
            }
            if (mTouchClickPos != Vector3.zero)
            {
                mTouchClickPos = Vector3.zero;
            }
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {//Camera视野变小
                Camera.main.fieldOfView *= 0.9f;
            }

            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {//Camera视野变大
                Camera.main.fieldOfView *= 1.1f;
            }

            if (Input.GetKey(KeyCode.A))
            {//往左
                Vector3 pos = Camera.main.transform.localPosition;
                pos.x -= 0.2f;
                Camera.main.transform.localPosition = pos;
            }
            if (Input.GetKey(KeyCode.D))
            {//往右
                Vector3 pos = Camera.main.transform.localPosition;
                pos.x += 0.2f;
                Camera.main.transform.localPosition = pos;
            }
            if (Input.GetKey(KeyCode.W))
            {//往上
                Vector3 pos = Camera.main.transform.localPosition;
                pos.z += 0.2f;
                Camera.main.transform.localPosition = pos;
            }
            if (Input.GetKey(KeyCode.S))
            {//往下
                Vector3 pos = Camera.main.transform.localPosition;
                pos.z -= 0.2f;
                Camera.main.transform.localPosition = pos;
            }
        }

        //移动地图



        BattleManager.BattleUpdate();


        if (_is_click_btn == false && BattleManager.mBattleStop == false && ((Input.touchCount>0 && Input.GetTouch(0).phase == TouchPhase.Canceled)||Input.GetMouseButtonUp(0)))
        {
            if (_is_drag)
            {
                _is_drag = false;
            }
            else
            {
                if (mObjTowerContent.activeSelf)
                {
                    if (EventSystem.current.IsPointerOverGameObject() == false)
                    {
                        mObjTowerContent.SetActive(false);
                    }
                }
                else
                {
                    Vector3 pos = Input.mousePosition;
                    pos.x -= Screen.width >> 1;
                    pos.y -= Screen.height >> 1;

                    float mScale = Screen.height / 640f;
                    pos.x /= mScale;
                    pos.y /= mScale;

                    Vector3 btn_pos = pos;

                    RectTransform rect = GetComponent<RectTransform>();
                    pos.x /= rect.rect.width;
                    pos.y /= rect.rect.height;
                    pos.x += 0.5f;
                    pos.y += 0.5f;
                    Ray ray = Camera.main.ViewportPointToRay(pos);
                    RaycastHit[] hits = Physics.RaycastAll(ray);
                    for (int i = 0; i < hits.Length; i++)
                    {
                        if (hits[i].collider.name == "map_show")
                        {
                            pos = hits[i].point;
                            pos.x = Mathf.RoundToInt(pos.x);
                            pos.z = Mathf.RoundToInt(pos.z);

                            if (BattleManager.IsClickTower(pos) == false)
                            {
                                mObjTowerContent.SetActive(true);
                                mObjTowerContent.transform.localPosition = btn_pos;
                                return;
                            }
                        }
                    }
                    Debug.Log("点击到防御塔");
                }
            }
        }
        _is_click_btn = false;
    }


    public void BattleStop()
    {
        mObjTowerContent.SetActive(false);

    }


    public void BindEnemy(Enemy enemy)//绑定敌人
    {
        GameObject obj = Instantiate(mObjEnemyHPInfo);
        obj.transform.SetParent(transform);
        obj.SetActive(true);
        Item_Enemy_HP hp = obj.AddComponent<Item_Enemy_HP>();
        hp.InitData(enemy);
    }

    public void RefreshLife()
    {
        mTextLife.text = string.Format("剩余生命值：{0}", BattleManager.HomeLife);
    }

    public void RefreshWave(int wave)//刷新回合
    {
        mTextWave.text = string.Format("Round {0}",wave);
    }

    public void RefreshTimeDownCount(int time)//刷新回合倒计时
    {
        if (time<=0)
        {
            mTwaveTime.gameObject.SetActive(false);
        }
        else
        {
            mTwaveTime.gameObject.SetActive(true);
            mTwaveTime.text = time.ToString();
        }
    }

    public void InitData(string map_name,List<TowerInfo> tower_list)
    {
        mTowerList = tower_list;
        AudioMananger.PlayMusic("music_fuben");
        Camera.main.transform.localPosition = new Vector3(3, 25, 3.5f);//还原镜头位置 因为镜头可以改变
        Camera.main.fieldOfView = 34;//还原镜头视野
        MapManager.mMapObj.transform.localPosition = Vector3.zero;
        BattleManager.InitData(this, MapManager.GetMapPath(map_name));
        RefreshLife();
        mObjTowerContent.SetActive(false);
        RefreshTimeDownCount(0);
        mTextWave.text = "";
        mTwaveTime.text = "";

        for (int i = 0; i < tower_list.Count; i++)
        {
            GameObject obj = Instantiate(mBtnBaseTower);
            obj.SetActive(true);
            obj.GetComponentInChildren<Text>().text = tower_list[i].BtnName;
            obj.GetComponentInChildren<Button>().onClick.AddListener(() =>
            {
                OnButtonClick("btn_Tower", obj);
            });
            obj.transform.SetParent(mObjTowerContent.transform);
            mButtonList.Add(obj);
        }
    }

    public override void OnNodeLoad()
    {

    }
    public override void OnEnter()
    {
        base.OnEnter();
    }
    public override void OnExit()
    {
        base.OnExit();
    }

    public void CreatTower(Vector3 pos,GameObject obj)
    {
        int index = mButtonList.IndexOf(obj);
        BattleManager.CreateTower(pos,mTowerList[index]);
     
    }

    public override void OnButtonClick(string name, GameObject obj)//重写鼠标点击接口
    {
        switch (name)
        {
            case "btn_Tower"://近战
                {
                    Vector3 pos = mObjTowerContent.transform.localPosition;
                    RectTransform rect = GetComponent<RectTransform>();
                    pos.x /= rect.rect.width;
                    pos.y /= rect.rect.height;
                    pos.x += 0.5f;
                    pos.y += 0.5f;
                    Ray ray = Camera.main.ViewportPointToRay(pos);
                    RaycastHit[] hits = Physics.RaycastAll(ray);
                    for (int i = 0; i < hits.Length; i++)
                    {
                        if (hits[i].collider.name == "map_show")
                        {
                            pos = hits[i].point;
                            pos.x = Mathf.RoundToInt(pos.x);
                            pos.z = Mathf.RoundToInt(pos.z);
                            CreatTower(pos, obj);
                            _is_click_btn = true;
                            mObjTowerContent.SetActive(false);
                        }
                    }
                    break;
                }
        }

    }
    public override void OnNodeAsset(string name, GameObject obj)//根据名字获取各组件
    {
        switch (name)
        {
            case "Text_Life"://生命值
                mTextLife = obj.GetComponent<Text>();
                break;
            case "Tower_Info"://创建防御塔按钮
                mObjTowerContent = obj;
                break;
            case "enemy_hp_info"://血条
                mObjEnemyHPInfo = obj;//获取该血条
                obj.SetActive(false);//将其设定为隐藏状态
                break;
            case "Text_Wave"://波数
                mTextWave = obj.GetComponent<Text>();
                break;
            case "Text_Wave_Time"://回合倒计时
                mTwaveTime = obj.GetComponent<Text>();
                break;
            case "btn_Tower":
                obj.SetActive(false);
                mBtnBaseTower = obj;
                break;
        }
    }
}
