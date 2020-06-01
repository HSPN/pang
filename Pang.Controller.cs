using UnityEngine;
using System.Collections;

public partial class Pang : MonoBehaviour
{
    IEnumerator TryFalling(Tile to)
    {
        var slice = (to.transform.position - transform.position) * 0.04f;

        for (int i=0; i<25; i++)
        {
            transform.position += slice;
            yield return null;
        }
        transform.position = to.transform.position;
        to.pang = this;
    }
    IEnumerator TrySwap(Tile to)
    {
        var slice = (to.transform.position - transform.position) * 0.04f;
        var tile_origin = tile;
        var to_origin = to;
        for (int i = 0; i < 25; i++)
        {
            transform.position += slice;
            to.pang.transform.position -= slice;
            yield return null;
        }
        transform.position = to.transform.position;
        to.pang.transform.position = tile.transform.position;
        tile.pang = to.pang;
        to.pang = this;

        Tile.UpdateRoutine();
        if (tile_origin.state == Tile.State.Pang || to_origin.state == Tile.State.Pang)
            yield break;
        for (int j=0; j < 25; j++)
        {
            to_origin.pang.transform.position -= slice;
            tile_origin.pang.transform.position += slice;
            yield return null;
        }
        to_origin.pang.transform.position = tile_origin.transform.position;
        tile_origin.pang.transform.position = to_origin.transform.position;
        to_origin.pang = tile_origin.pang;
        tile_origin.pang = this;
    }
}
