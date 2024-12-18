using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[Serializable]
public class ObjectPoolItemSettings
{
    public string name;

    public GameObject gameObject;
    public GameObject parent;
    public int capacity;
}

public class PoolBase : MonoBehaviour
{
    public string PoolName;
    public List<ObjectPoolItemSettings> objectPoolItemSettings;

    public Dictionary<string, List<GameObject>> Pool = new Dictionary<string, List<GameObject>>();

    protected virtual void Start()
    {
        Game.Pool[PoolName] = this;
        InitPool();
    }

    void InitPool()
    {
        foreach (var item in objectPoolItemSettings)
        {
            Pool[item.name] = Enumerable.Range(0, item.capacity).Select(_ =>
            {
                var obj = Instantiate(item.gameObject, item.parent ? item.parent.transform : transform);
                obj.SetActive(false);
                return obj;
            }).ToList();
        }
    }

    public GameObject Add(ObjectPoolItemSettings item) // 초과대여 시 실행
    {
        GameObject obj = Instantiate(item.gameObject, item.parent.transform);
        obj.SetActive(true);
        Pool[item.name].Add(obj);
        return obj;
    }

    public GameObject Get(string name)
    {
        Pool.TryGetValue(name, out var objList);
        if (objList == null) return null;

        GameObject obj;

        if (obj = objList.FirstOrDefault(x => x.activeSelf == false))
        {
            obj.SetActive(true);
        }
        else
        {
            obj = Add(objectPoolItemSettings.FirstOrDefault(x => x.name == name));
        }
        return obj;
    }

    public GameObject Release(GameObject obj)
    {
        // 비활성화
        obj.SetActive(false);

        // 초기화
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;
        obj.transform.localScale = Vector3.one;
        if (obj.TryGetComponent(out Rigidbody rb))
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.Sleep();
        }
        if (obj.TryGetComponent(out Rigidbody2D rb2d))
        {
            rb2d.linearVelocity = Vector2.zero;
            rb2d.angularVelocity = 0f;
            rb2d.Sleep();
        }
        if (obj.TryGetComponent(out TrailRenderer trail))
        {
            trail.Clear();
        }
        return obj;
    }
}
