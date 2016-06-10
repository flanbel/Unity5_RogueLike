using UnityEngine;
using System.Collections;
/// <summary>
/// 設定したターゲットと一定の距離を保つ
/// </summary>
public class ComplianceTarget : MonoBehaviour {

    //距離を保つ対象
    public GameObject Target;
    //ターゲットとの距離
    public Vector3 Dist = Vector3.zero;

	// Use this for initialization
	void Start () {
	
	}
	
    //アップデート後に実行
    void LateUpdate()
    {
        Vector3 pos = Target.transform.position;
        pos += Dist;
        this.transform.position = pos;
	}
}
