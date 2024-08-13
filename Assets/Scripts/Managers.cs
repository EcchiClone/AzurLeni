using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Managers : MonoBehaviour
{
    public static Managers Instance;
    public DataManager Data;
    public NetworkManager Network;
    public ObjectPoolManager ObjectPool;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        Instance.Data = Instance.AddComponent<DataManager>();
        Instance.Network = Instance.AddComponent<NetworkManager>();
        Instance.ObjectPool = Instance.AddComponent<ObjectPoolManager>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
