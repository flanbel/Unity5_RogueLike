using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// アイテムリストを表示？
/// </summary>
public class ItemList : MonoBehaviour
{
    //ボタン押下フラグ
    bool pushbutton = false;
    //rect
    RectTransform rectT = null;

	void Start () {
        //ないなら取得
        if (!rectT)
        {
            rectT = this.gameObject.GetComponent<RectTransform>();
            rectT.sizeDelta = new Vector2(rectT.sizeDelta.x, 0);
        }
	}
	
	// Update is called once per frame
    void Update()
    {
        if(this.gameObject)
        {
            //伸びる
            if (pushbutton && rectT.sizeDelta.y < 200)
            {
                rectT.sizeDelta = new Vector2(rectT.sizeDelta.x, rectT.sizeDelta.y + 40);
            }
            //縮む
            else if (!pushbutton && rectT.sizeDelta.y > 0)
            {
                rectT.sizeDelta = new Vector2(rectT.sizeDelta.x, rectT.sizeDelta.y - 40);
            }
        }
    }
    /// <summary>
    /// アイテムリスト表示関数
    /// </summary>
    public void ShowItem() 
    {
        //リスト表示
        if (!pushbutton)
        {
            pushbutton = true;
        }
        else 
        {
            pushbutton = false;
        }
    }
}
