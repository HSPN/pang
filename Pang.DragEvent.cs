using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;

/// <summary>
/// Drag Event for Pang
/// </summary>
public partial class Pang : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    [SerializeField] private GameObject _collider = null;
    bool dragEvent = false;
    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        dragEvent = true;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        if (!dragEvent) return;

        var pang = eventData.pointerCurrentRaycast.gameObject.GetComponent<Pang>();
        if (pang && pang.gameObject != gameObject) movePos(pang);
    }

    void movePos(Pang pang)
    {
        dragEvent = false;
        if (!tile || !pang.tile) return;

        if (pang.tile.state != Tile.State.Filled || tile.state != Tile.State.Filled) return;
        if (!tile.isNear(pang.tile)) return;

        pang.StartCoroutine("TrySwap", tile);
        //pang.StartCoroutine("TryFalling",tile);
        //StartCoroutine("TryFalling", pang.tile);
    }
}
