using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// ログを表示,管理するクラス
/// </summary>
public class Log : MonoBehaviour {
    //ログを格納するコンテナ
    GameObject m_Logcontena;
    //ログの数
    int m_lognum = 0;
    //ログのスクロールバー
    GameObject m_bar;

	void Start () {
        //LogContena取得
        m_Logcontena = GameObject.Find("LogContena");
        //スクロールバー取得
        m_bar = this.gameObject.transform.FindChild("Scrollbar").gameObject;
	}
	
	void Update () {

	}

    /// <summary>
    /// 引数として受け取ったstringをLogに表示する
    /// </summary>
    /// <param name="logstring"></param>
    public void AddLogText(string logstring) 
    {
        //Purefabからログテキストのインスタンス生成
        GameObject logtext = (GameObject)Instantiate(Resources.Load("Prefab/LogText"));
        logtext.name = "logtext" + m_lognum++;
        //フォント変更
        //logtext.GetComponent<Text>().font = Resources.Load("Font/PixelMplus10-Regular")as Font;
        //文字列格納
        logtext.GetComponent<Text>().text = logstring;
        //コンテナの中に格納
        logtext.transform.SetParent(m_Logcontena.transform);
        logtext.transform.localScale = Vector3.one;

        //ログが 30 以上なら古いログ削除
        if (m_Logcontena.transform.childCount >= 30) 
        {
            GameObject child = m_Logcontena.transform.Find("logtext" + (m_lognum - 30)).gameObject;
            Destroy(child);
        }

        //スクロールバーを常に下に配置。(自動スクロール)
        m_bar.GetComponent<Scrollbar>().value = -1;
    }
}
