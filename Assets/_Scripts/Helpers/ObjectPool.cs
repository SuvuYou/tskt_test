using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Component
{
    private readonly T _prefab;
    private readonly Transform _parent;
    private readonly Queue<T> _objects = new();

    public ObjectPool(T prefab, int initialSize, Transform parent = null)
    {
        _prefab = prefab;
        _parent = parent;

        for (int i = 0; i < initialSize; i++)
        {
            T newObj = Object.Instantiate(_prefab, _parent);
            newObj.gameObject.SetActive(false);
            _objects.Enqueue(newObj);
        }    
    }

    public T Get()
    {
        if (_objects.Count == 0)
        {
            AddObject();
        }

        T obj = _objects.Dequeue();
        obj.gameObject.SetActive(true);
        return obj;
    }

    public void ReturnToPool(T obj)
    {
        obj.gameObject.SetActive(false);
        _objects.Enqueue(obj);
    }

    private void AddObject()
    {
        T newObj = Object.Instantiate(_prefab, _parent);
        newObj.gameObject.SetActive(false);
        _objects.Enqueue(newObj);
    }
}