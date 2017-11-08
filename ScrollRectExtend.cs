using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using LuaInterface;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ScrollRectExtend : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, ICanvasElement
{
    readonly List<NumItem> _items = new List<NumItem>();
    private LuaFunction _notifyScrollNumChange = null;
    // Use this for initialization
    public GameObject itemgo;
    public RectTransform content;
    public GridLayoutGroup glg;
    public float itemx;
    public int _count = 0;
    public enum Direction
    {
        Left,
        Right,
        None
    }

    public Direction dir = Direction.None;
    [NoToLua]
    public void Awake()
    {
  
        itemx = glg.cellSize.x + glg.spacing.x;
        Refresh();
  
    }

    [NoToLua]
    public void Refresh()
    {
      
        //for (int i = 3 - 1; i >= 0; i--)
        //{
        //   var go = GameObject.Instantiate(itemgo);
        //    go.transform.SetParent(content);
        //    go.SetActive(true);
        //    go.transform.localPosition = Vector3.zero;
        //    go.transform.localScale = Vector3.one;
        //}

        for (int i = 0; i <= content.childCount-1 ; i++)
        {
            var item = content.GetChild(i).GetComponent<NumItem>();
            item.Init(i,this);
            item.OnEndDrag(content.anchoredPosition.x);
            _items.Add(item);

        }
    }




    public  void OnEndDrag(UnityEngine.EventSystems.PointerEventData eventData)
    {
        if (!IsDrag(eventData.delta.x))
        {
            return;
        }

        var _x = content.anchoredPosition.x % itemx;
        Vector2 deltVector2 = eventData.position - startpos;
        float deltx;
        if (_x != 0)
        {

            //右滑动
            if (deltVector2.x >= 0)
            {
                if(_x >0)
                {
                    deltx = itemx - _x;
                }
                else
                {
                    deltx = -_x;
                }
                
                dir = Direction.Right;
            }
            else
            {
                if (_x > 0)
                {
                    deltx = ( -_x);
                }
                else
                {
                    deltx = (-itemx - _x);
                }
                
    
                dir = Direction.Left;
            }
            

        }
        else
        {
          
            deltx = 0;
            if (deltVector2.x > 0)
            {

                dir = Direction.Right;
            }
            else
            {
              
                dir = Direction.Left;
            }

        }

        content.DOAnchorPosX(content.anchoredPosition.x + deltx, 0.3f);
        for (int i = 0; i < _items.Count; i++)
        {
            _items[i].OnEndDrag(content.anchoredPosition.x + deltx);
        }
  

    }

    public void OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
    {
        if (!IsDrag(eventData.delta.x))
        {
            return;
        }

        content.anchoredPosition = content.anchoredPosition + new Vector2(eventData.delta.x, 0);
        for (int i = 0; i < _items.Count; i++)
        {
            _items[i].OnDrag(eventData.delta.x);
        }

        
    }

  
    public bool IsDrag(float deltx)
    {
        float x = content.anchoredPosition.x + deltx;
        if (x >= 2f * itemx - content.rect.width && x <=  1*itemx + 1)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public Vector2 startpos = Vector2.zero;

    public void OnBeginDrag(PointerEventData eventData)
    {
        startpos = eventData.position;
    }

    public void ItemsMove(float x)
    {

        content.anchoredPosition = content.anchoredPosition + new Vector2(x, 0);
        for (int i = 0; i < _items.Count; i++)
        {
            _items[i].OnDrag(x);
        }
    }


    public void Rebuild(CanvasUpdate executing)
    {
    
    }

    public void GraphicUpdateComplete()
    {
        
    }


    virtual public void  LayoutComplete()
    {
        ItemsMove(0);
    }


    protected  void OnEnable()
    {
        CanvasUpdateRegistry.RegisterCanvasElementForLayoutRebuild(this);
    }

    protected  void OnDisable()
    {
        CanvasUpdateRegistry.UnRegisterCanvasElementForRebuild(this);
    }

    public bool IsDestroyed()
    {
        return this == null;
    }
}
