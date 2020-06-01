using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// tile1       tile2
///       up down
/// up down          down
/// pang2       tile1
/// </summary>
public partial class Tile : MonoBehaviour
{
    private static List<List<Tile>> map = null;
    private const int rowLen = 12;
    private const int colLen = 7;
    private int col;
    private int row;
    [SerializeField] private Pang _pang;
    public Pang pang
    {
        get { return _pang; }
        set
        {
            _pang = value;
            if(_pang) _pang.tile = this;
            state =
                 _pang == null ?
                 State.Empty :
                 TryFall();  //is pang more falling? T : Falling, F : Filled
            if (state == State.Empty) TryFill();
            else if (state == State.Filled) Filled();
        }
    }
    public enum State
    {
        Filled, //pang exist
        Empty,  //no pang
        Falling, //pang is Falling
        Pang    //pang!
    };
     public State state;
    //Initialize Class
    public static void MapInit()
    {
        if (map != null) return;    //prevent init twice
        map = new List<List<Tile>>(colLen);
        for (int i = 0; i < colLen; i++)
        {
            map.Add(new List<Tile>(rowLen));
            for (int j=0; j< rowLen; j++)
            {
                map[i].Add(null);
            }
        }
    }

    private static void AddToMap(Tile bucket)
    {
        var pos = bucket.transform.localPosition;
        var x = (int)(pos.x * 0.01);
        var y = (int)(-pos.y * 0.01);

        bucket.col = y;
        bucket.row = x;

        map[y][x] = bucket;
    }

    private void Awake()
    {
        MapInit();
        AddToMap(this);
        _pang = null;
        state = State.Empty;
        tryPangList = new Queue<Tile>();
    }

    private void Start()
    {
        TryFill();
    }

    private bool IsOutOfBound(int _col, int _row)
    {
        var ret = false;
        ret |= _col < 0 || _col >= colLen;
        ret |= _row < 0 || _row >= rowLen;
        return ret;
    }

    public bool isNear(Tile tile)
    {
        if (col - tile.col == 0 && Mathf.Abs(row - tile.row) == 1) return true;
        else if (row - tile.row == 0 && Mathf.Abs(col - tile.col) == 1) return true;
        else return false;
    }
}
