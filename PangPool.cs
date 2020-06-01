using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Object Pool for Pang
/// </summary>
public class PangPool : SingleTon<PangPool>
{
    private Queue<Pang> pool = new Queue<Pang>();
    void Awake()
    {
        var _pool = GetComponentsInChildren<Pang>();
        foreach(var pang in _pool)
            pool.Enqueue(pang);
    }
    
    public Pang Generate(Vector2 pos)
    {
        var ret = pool.Dequeue();
        ret.randomizeColor();
        ret.drawColor();
        ret.transform.position = pos;
        return ret;
    }

    public void Push(Pang pang)
    {
        pang.transform.position = new Vector2(4000, 4000);
        pool.Enqueue(pang);
    }

}
