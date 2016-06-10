using UnityEngine;
using UnityEngine.UI;
using System.Collections;
/// <summary>
/// 職業アイコン
/// </summary>
public class JobIcon : MonoBehaviour {
    ////プレイヤー
    //Player player;
    ////画像情報
    //Image iconimage;
    ////テキスト(0:名前,1:level)
    //Text[] icontext = new Text[2];

    //// Use this for initialization
    //void Start()
    //{
    //    player = GameObject.Find("Player").GetComponent<Player>();
        
    //    //Icon作成
    //    {
    //        GameObject jobIcon = new GameObject();
    //        jobIcon.name = "jobIcon";
    //        jobIcon.transform.SetParent(this.transform);
    //        jobIcon.transform.localPosition = Vector3.zero;
    //        //イメージ作成
    //        {
    //            iconimage = jobIcon.AddComponent<Image>();
    //            //テクスチャ取得
    //            Texture2D tex = Resources.Load("Texture/Job") as Texture2D;
    //            //テクスチャからSprite作成し設定
    //            iconimage.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
    //            //こいつのwidthとheight変更
    //            jobIcon.GetComponent<RectTransform>().sizeDelta = new Vector2(140, 140);
    //        }

    //        //Button機能追加
    //        {
    //            Button button = jobIcon.AddComponent<Button>();
    //            //押された時に実行する関数追加。
    //            button.onClick.AddListener(ChengeJob);
    //        }

    //        //テキスト追加
    //        {
    //            GameObject nametext = (GameObject)Instantiate(Resources.Load("Prefab/UIText"));
    //            nametext.name = "jobname";
    //            nametext.transform.SetParent(jobIcon.transform);
    //            nametext.transform.localPosition = new Vector3(0.0f, 15.0f, 0.0f);

    //            GameObject leveltext = (GameObject)Instantiate(Resources.Load("Prefab/UIText"));
    //            leveltext.name = "joblevel";
    //            leveltext.transform.SetParent(jobIcon.transform);
    //            leveltext.transform.localPosition = new Vector3(0.0f, -5.0f, 0.0f);

    //            //テキスト情報
    //            {
    //                //名前textのフォーマット
    //                icontext[0] = nametext.GetComponent<Text>();
    //                icontext[0].text = player.SelectJob.ProfessionParam.Name;
    //                icontext[0].color = Color.black;
    //                icontext[0].alignment = TextAnchor.MiddleCenter;
    //                //Leveltextのフォーマット
    //                icontext[1] = leveltext.GetComponent<Text>();
    //                icontext[1].text = player.SelectJob.ProfessionParam.Level.ToString();
    //                icontext[1].color = Color.black;
    //                icontext[1].alignment = TextAnchor.MiddleCenter;
    //                icontext[1].fontSize = 20;
    //            }
    //        }
    //    }
    //}

    //プレイヤー
    Player player;
    //テキスト(0:名前,1:level)
    Text[] icontext = new Text[2];

    void Start() 
    {
        icontext[0] = this.transform.FindChild("JobName").gameObject.GetComponent<Text>();
        icontext[1] = this.transform.FindChild("JobLevel").gameObject.GetComponent<Text>();
        player = GameObject.Find("Player").GetComponent<Player>();

        //テキスト変更
        icontext[0].text = player.SelectJob.ProfessionParam.Name;
        icontext[1].text = player.SelectJob.ProfessionParam.Level.ToString();
    }
	
	// Update is called once per frame
	void Update () {
        //切り替え
        if (Input.GetKeyDown(KeyCode.X))
        {
            ChengeJob();
        }
        UpdateIcon();
	}

    public void ChengeJob() 
    {
        //選択しているジョブを次のジョブに
        //添え字をjubnumで割ったあまりにしてループさせる。
        player.JobChenge();
    }

    public void UpdateIcon() 
    {
        //テキスト変更
        icontext[0].text = player.SelectJob.ProfessionParam.Name;
        icontext[1].text = player.SelectJob.ProfessionParam.Level.ToString();
    } 
}
