using UnityEngine;
using System.Collections;
/// <summary>
/// ビルボード(3Dの板ポリを常にカメラの方向に向かせる) 未完
/// </summary>
public class Billboard : MonoBehaviour
{
    public Camera LookAtCam;

    Transform this_t_;

    void Awake()
    {
        this_t_ = this.transform;
    }

    void Update()
    {
        if (LookAtCam == null) return;
        Transform cam_t = LookAtCam.transform;

        Vector3 vec = cam_t.position - this_t_.position;
        vec.x = vec.z = 0.0f;
        this_t_.LookAt(cam_t.position - vec);

        
        //ビルボード処理を行う。
		//カメラの回転行列取得
        Quaternion q = LookAtCam.transform.rotation;

		//クォータニオン生成
		float s;
		float halfAngle =this.transform.rotation.z * 0.5f;
		s = Mathf.Sin(halfAngle);
        q.w = Mathf.Cos(halfAngle);
        q.x = q.x * s;
        q.y = q.y * s;
        q.z = q.z * s;

    }
}