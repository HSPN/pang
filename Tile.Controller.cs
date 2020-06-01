using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class Tile : MonoBehaviour
{
    public static Queue<Tile> tryPangList;
    private void TryFill()
    {
        if (state == State.Filled) return;   //Assert Never twice
        var genPos = transform.position + new Vector3(0, 100, 0);
        var upperTile =
            col == 0 ?
            null :
            map[col - 1][row];

        var upperPang =
            upperTile == null ?
            PangPool.sInstance.Generate(genPos) :   //new Pang
            upperTile.pang;   //Select upper Pang

        if (upperTile && upperTile.state == State.Pang) return;
        if (upperPang != null)
        {
            upperPang.StartCoroutine("TryFalling", this); 
            if (upperTile != null) upperTile.pang = null;
            //if (upperTile != null ) return;
        }
    }

    private State TryFall()
    {
        var downerBucket =
            col == colLen - 1 ?
            null :
            map[col + 1][row];

        if (!downerBucket || downerBucket.state == State.Filled) return State.Filled;
        else if (downerBucket.state == State.Pang) return State.Filled;
        else
        {
            downerBucket.TryFill();
            return State.Falling;
        }
    }

    private void TryPang()
    {
        if (state == State.Pang) return;    //prevent pang twice

        var Result = CheckPang(pang, col, row);

        if (!Result) return;

        DoPang(col, row);
    }

    private bool CheckPang(Pang p, int _col, int _row)
    {
        return CheckPangRcsv(p, _col + 1, _row) + CheckPangRcsv(p, _col - 1, _row) >= 2 ||   //Check Col 3
        CheckPangRcsv(p ,_col, _row + 1) + CheckPangRcsv(p, _col, _row - 1) >= 2;     //Check Row 3
    }

    //check pang recursively
    //_col, _row is 'Absolute' Coordinate
    private int CheckPangRcsv(Pang p, int _col, int _row)
    {
        if (IsOutOfBound(_col, _row)) return 0;
        var t = map[_col][_row];

        if (_col > col) _col++;
        else if (_col < col) _col--;

        if (_row > row) _row++;
        else if (_row < row) _row--;
        if (t.state == State.Filled &&
            p.ColorCmp(t.pang))
            return 1 + CheckPangRcsv(p, _col, _row);

        else return 0;
    }

    //Pang Recursively
    //_col, _row is 'Absolute' Coordinate
    private void DoPang(int _col, int _row)
    {
        if (IsOutOfBound(_col, _row)) return;

        {
            var t = map[_col][_row];
            if (t.state != State.Filled) return;
            if (!pang.ColorCmp(t.pang)) return;
            t.state = State.Pang;
            t.pang.StartCoroutine("PangFX",t);
        }

        DoPang(_col + 1, _row);
        DoPang(_col - 1, _row);
        DoPang(_col, _row + 1);
        DoPang(_col, _row - 1);
    }

    private void Filled()
    {
        tryPangList.Enqueue(this);
    }

    public static void UpdateRoutine()
    {
        //it should be done in Update although update overhead.
        //Tile tile;
        while(tryPangList.Count > 0)
        {
            tryPangList.Dequeue().TryPang();
        }
    }
}
