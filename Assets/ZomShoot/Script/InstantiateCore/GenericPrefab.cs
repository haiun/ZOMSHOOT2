using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class GenericPrefab
{
    public static T Instantiate<T>()
    {
        var prefabPathAttr = typeof(T).GetCustomAttribute<PrefabPath>();
        var prefab = Resources.Load(prefabPathAttr.Path) as GameObject;
        var go = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity);
        return go.GetComponent<T>();
    }

    public static T Instantiate<T>(Transform parent)
    {
        var prefabPathAttr = typeof(T).GetCustomAttribute<PrefabPath>();
        var prefab = Resources.Load(prefabPathAttr.Path) as GameObject;
        var go = GameObject.Instantiate(prefab, parent.position, parent.rotation, parent);
        return go.GetComponent<T>();
    }

    public static List<T> Instantiate<T>(Transform parent, int count)
    {
        var prefabPathAttr = typeof(T).GetCustomAttribute<PrefabPath>();
        var prefab = Resources.Load(prefabPathAttr.Path) as GameObject;
        List<T> ret = new List<T>();
        for (int i = 0; i < count; ++i)
        {
            var go = GameObject.Instantiate(prefab, parent);
            ret.Add(go.GetComponent<T>());
        }
        return ret;
    }

    public static T InstantiatePathFormat<T>(string a0)
    {
        var prefabPathAttr = typeof(T).GetCustomAttribute<PrefabPath>();
        var path = string.Format(prefabPathAttr.Path, a0);
        var prefab = Resources.Load(path) as GameObject;
        var go = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity);
        return go.GetComponent<T>();
    }
}
