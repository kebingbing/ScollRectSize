using UnityEngine;
using System.Collections;

public class ItemSize : MonoBehaviour {

    public ScrollRectSize ss;
    UnityEngine.UI.GridLayoutGroup glg;
    RectTransform rt;
    RectTransform node;
    // Use this for initialization
    void Start () {
        ss = GetComponentInParent<ScrollRectSize>();
        if(ss != null)
        {
            ss.AddItem(this);
        }

        node = transform.Find("Image").GetComponent<RectTransform>();
        rt = transform.GetComponent<RectTransform>();
        glg = ss.content.GetComponent<UnityEngine.UI.GridLayoutGroup>();
	}

    public float ItemX
    {
        get
        {
            return 2*(glg.cellSize.x + glg.spacing.x);
        }
    }

    public void NotifyChange()
    {
        float delt = Mathf.Abs(ss.content.anchoredPosition.x + rt.anchoredPosition.x - ItemX);
        float scale = 1 - delt / ItemX;

        if (scale < -0.5f)
        {
            scale = 0.6f;
        }
        else if(scale > 0.95f)
        {

            scale = 1;
        }
        else
        {
            scale = Mathf.Max(scale, 0.6f);
        }
        
        node.localScale = new Vector3(1.1f,1.1f,1)*scale;

    }

    public Vector2 ContentCenterPos()
    {
        Vector2 pos = new Vector2(0,0);

        return node.sizeDelta;
    }


}
