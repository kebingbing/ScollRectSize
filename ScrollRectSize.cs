using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScrollRectSize : UnityEngine.UI.ScrollRect
{


    public List<ItemSize> ItemSizeList = new List<ItemSize>();
    public void AddItem(ItemSize item)
    {
        if(!ItemSizeList.Contains(item))
        {
            ItemSizeList.Add(item);
        }
    }

    public void NotifyContentMoveIng()
    {
        var iter = ItemSizeList.GetEnumerator();
        while(iter.MoveNext())
        {
            iter.Current.NotifyChange();
        }
    }

    protected override void SetContentAnchoredPosition(Vector2 position)
    {
        base.SetContentAnchoredPosition(position);
        NotifyContentMoveIng();
    }

    public override void LayoutComplete()
    {
        base.LayoutComplete();
        NotifyContentMoveIng();
    }
    //public override void OnBeginDrag(PointerEventData eventData)
    //{
    //    base.OnBeginDrag(eventData);
    //}

    //public override void OnDrag(PointerEventData eventData)
    //{
    //    base.OnDrag(eventData);
    //    NotifyContentMoveIng();
    //    //Debug.Log(eventData.ToString());
    //}

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);

    }



}

