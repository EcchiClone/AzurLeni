using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ObjectPoolItem
{
    public int id;
    public string _name;

    public GameObject _gameObject;
    public GameObject _parent;
    public int _capacity;
}

public class PoolBase : MonoBehaviour
{

    public ObjectPoolItem[] objectPoolItems;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}