using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager
{
    public static List<Bullet> mBulletList = new List<Bullet>();

    public static Bullet CreatBullet(string model_name)
    {
        if (mBulletList.Count > 0)
        {
            for (int i = 0; i < mBulletList.Count; i++)
            {
                if (mBulletList[i].gameObject.activeSelf == false && mBulletList[i].name == model_name)
                {
                    mBulletList[i].gameObject.SetActive(true);
                    return mBulletList[i];
                }
            }

        }
        GameObject obj;
        if (string.IsNullOrEmpty(model_name))
        {
            obj = new GameObject();
        }
        else
        {
            obj = Resources.Load<GameObject>("Model/" + model_name);
            obj = GameObject.Instantiate(obj);
        }
        obj.name = model_name;
        Bullet buttle = obj.AddComponent<Bullet>();
        mBulletList.Add(buttle);
        return buttle;
    }
}
