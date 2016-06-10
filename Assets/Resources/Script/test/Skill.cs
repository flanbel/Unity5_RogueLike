using UnityEngine;
using System.Collections;
/// <summary>
/// 技クラス
/// </summary>
public class Skill : MonoBehaviour {

    [SerializeField]
    //Attack magnificationの略
    float m_atkmag = 100;                           //!< 攻撃倍率
    public float atkmag { get { return m_atkmag; } set { m_atkmag = value; } }

    [SerializeField]
    bool[][] m_atkrange;                            //!< 攻撃範囲
    public bool[][] atkrng { get { return m_atkrange; } set { m_atkrange = value; } }

    public enum ELEMENT { FIRE = 0, WATER, ICE, NATURE, THUNDER, EARTH, LIFE, ELEMENTNUM };

    [SerializeField]
    ELEMENT m_element;                              //!< 属性
    public ELEMENT element { get { return m_element; } set { m_element = value; } }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
