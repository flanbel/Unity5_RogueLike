using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class levelup : MonoBehaviour {

    Vector3 pos;

    bool flg = false;

    Text txt;

	// Use this for initialization
	void Start () {
        pos = this.transform.localPosition;
        txt = this.GetComponent<Text>();
        //表示しない
        txt.color = Color.clear;
	}
	
	// Update is called once per frame
	void Update () {
        //上昇
        if (flg) 
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + 1.0f, transform.localPosition.z);
            txt.color = new Color(0.0f, 0.0f, 0.0f, txt.color.a - 0.05f);

            if (txt.color.a <= 0) 
            {
                flg = false;
            }
        }
	}

    public void feedout() 
    {
        //黒に
        txt.color = Color.black;
        transform.localPosition = pos;
        flg = true;
    }
}
