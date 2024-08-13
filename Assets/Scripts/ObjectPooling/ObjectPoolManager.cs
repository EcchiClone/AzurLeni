using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public Dictionary<string, PoolBase> ObjectPools = new Dictionary<string, PoolBase>();

    private void Update()
    {
    }
}
