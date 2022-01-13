using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    private List<GameObject> _objects;
    private int _size;
    private GameObject _object;
    private Transform _poolParent;

    public Pool PoolSetUp(int size, GameObject prefab, Transform parent)
    {
        _size = size;
        _object = prefab;
        _poolParent = parent;
        _objects = new List<GameObject>();
        GrowPool(size);
        return this;
    }

    public void AddObject(GameObject prefab, float delay = 0)
    {
        _objects.Add(prefab);
        if (delay > 0)
            StartCoroutine(DisableDelay(prefab, delay));
        else
            prefab.SetActive(false);
        
    }

    public void GrowPool(int count)
    {
        for(int i = 0; i < count; i++)
        {
            GameObject prefab = Instantiate(_object);
            prefab.transform.SetParent(_poolParent);
            AddObject(prefab);
        }
    }

    public GameObject GetObject()
    {
         if (_objects.Count <= 5)
            GrowPool(1);
        GameObject _returnObj = _objects[0];
        _objects.RemoveAt(0);
        _returnObj.SetActive(true);
        
        return _returnObj;
    }

    private IEnumerator DisableDelay(GameObject prefab, float delay)
    {
        yield return new WaitForSeconds(delay);
        prefab.SetActive(false);
    }

    
}
