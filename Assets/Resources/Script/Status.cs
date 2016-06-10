using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// ステータスクラス
/// </summary>
public class Status : MonoBehaviour
{
    //! 体力
    [SerializeField]
    int m_hp_now = 100;                          //!< 現在値
    [SerializeField]
    int m_hp_max = 100;                          //!< 最大値

    [SerializeField]
    float m_patk = 0.0f;                        //!< 物理攻撃力
    [SerializeField]
    float m_pdef = 0.0f;                        //!< 物理防御力
    [SerializeField]
    float m_matk = 0.0f;                        //!< 魔法攻撃
    [SerializeField]
    float m_mdef = 0.0f;                        //!< 魔法防御
    [SerializeField]
    float m_spd = 0.0f;                         //!< スピード

    /// <summary>
    /// 各属性補正値
    /// </summary>
    [SerializeField]
    float m_fire = 1.00f;                         //!< 火属性

    [SerializeField]
    float m_water = 1.00f;                        //!< 水属性

    [SerializeField]
    float m_ice = 1.00f;                          //!< 氷属性

    [SerializeField]
    float m_nature = 1.00f;                       //!< 自然属性

    [SerializeField]
    float m_thunder = 1.00f;                      //!< 雷属性

    [SerializeField]
    float m_earth = 1.00f;                        //!< 土属性

    [SerializeField]
    float m_life = 1.00f;                         //!< 生命属性

    [SerializeField]
    string m_name = "Character Name";            //!< キャラ名

    [SerializeField]
    List<GameObject> m_friends = new List<GameObject>();       //!< 友達リスト


    //! 拡張クラスからアクセスするために getter/setter を用意してね！ (他でも使うと思うけど)
    public int hp 
    {
        get 
        {
            return m_hp_now;
        }
        set 
        {
            //最大値を超えるなら最大値をセット
            if (value > maxHp)
            {
                m_hp_now = maxHp;
            }
            //負の値なら0にする
            else if(value < 0)
            {
                m_hp_now = 0;
            }
            //それ以外なら代入
            else 
            {
                m_hp_now = value;
            }
        }
    }
    public int maxHp 
    {
        get { return m_hp_max; }
        set 
        {
            m_hp_max = value;
            //最大値以上なら変更
           if(m_hp_now > m_hp_max)
           {
               m_hp_now = m_hp_max;
           }
        }
    }

    public float PATK { get { return m_patk; } set { m_patk = value; } }
    public float PDEF { get { return m_pdef; } set { m_pdef = value; } }
    public float MATK { get { return m_matk; } set { m_matk = value; } }
    public float MDEF { get { return m_mdef; } set { m_mdef = value; } }
    public float SPD { get { return m_spd; } set { m_spd = value; } }

    public float fire { get { return m_fire; } set { m_fire = value; } }
    public float water { get { return m_water; } set { m_water = value; } }
    public float nature { get { return m_nature; } set { m_nature = value; } }
    public float ice { get { return m_ice; } set { m_ice = value; } }
    public float thunder { get { return m_thunder; } set { m_thunder = value; } }
    public float earth { get { return m_earth; } set { m_earth = value; } }
    public float life { get { return m_life; } set { m_life = value; } }

    public string charaName { get { return m_name; } set { m_name = value; } }
    public List<GameObject> friends { get { return m_friends; } set { m_friends = value; } }

    //3DText定義
    GameObject HpText;

    void Start()
    {
        //プレハブから読み込み
        HpText = (GameObject)Instantiate(Resources.Load("Prefab/3DText"));
        //親に設定
        HpText.transform.SetParent(this.transform);
        //名前
        HpText.name = "HpText";
        //色
        HpText.GetComponent<TextMesh>().color = Color.black;
        //font size
        HpText.GetComponent<TextMesh>().fontSize = 50;
        //中央ぞろえに設定
        HpText.GetComponent<TextMesh>().anchor = TextAnchor.MiddleCenter;

        //移動
        HpText.transform.localPosition = new Vector3(0.0f, 1.0f, 0.0f);
        //スケール値を小さく(10分の1)
        HpText.transform.localScale = new Vector3(0.1f, 0.1f, 1.0f);
    }

    void Update() 
    {
        //HP表示
        HpText.GetComponent<TextMesh>().text = hp.ToString() + '/' + maxHp.ToString();
        //回転を0に
        HpText.transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
    }
}