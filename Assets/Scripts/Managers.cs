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
    public FieldManager Field;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        Instance.Data = Instance.AddComponent<DataManager>();
        Instance.Network = Instance.AddComponent<NetworkManager>();
        Instance.ObjectPool = Instance.AddComponent<ObjectPoolManager>();
        Instance.Field = Instance.AddComponent<FieldManager>();
    }
}
