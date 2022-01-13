using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    private Dictionary<string, Pool> _pools = new Dictionary<string, Pool>();

    public Pool AddPool(GameObject prefab, string poolType, int size = 0, Transform parent = null)
    {
        if (!_pools.ContainsKey(poolType + "(Clone)"))
        {
            GameObject poolGo = GameObject.Find("[POOLS]") ?? new GameObject("[POOLS]");
            Pool _pool = new GameObject("Pool: " + prefab.name).AddComponent<Pool>();
            _pool.transform.SetParent(poolGo.transform);
            if (!parent)
                parent = _pool.transform;
            _pool.PoolSetUp(size, prefab, parent);
            _pools.Add(poolType + "(Clone)", _pool);
            return _pool;
        }
        else
            return _pools[poolType + "(Clone)"];
    }

    public Pool GetPool(string poolType)
    {
        if (_pools.ContainsKey(poolType))
        {
            Pool _pool = _pools[poolType];
            return _pool;
        }
        else
            return null;
    }

}
