using UnityEngine;
using System.Collections;

public class SingleTon<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T _sInstance = null;

    public static T sInstance
    {
        get
        {
            if (_sInstance == null) allocateT();
            return _sInstance;
        }
    }
    private static void allocateT()
    {
        _sInstance = (T)FindObjectOfType(typeof(T));
        if (_sInstance == null)
        {
            GameObject go = GameObject.Find("GameManager");
            if (go == null) go = new GameObject("GameManager");
            _sInstance = go.AddComponent<T>();
        }
    }
}
