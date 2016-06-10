using UnityEngine;
using System.Collections;

/// <summary>
/// エネミーのクラス
/// </summary>
public class Enemy : Character {

    Enemy() 
    {
        
    }

	// Use this for initialization
	void Start () {
        SetMap();
        do
        {
            //マップ内のルーム上にランダムに生成。
            PosX = Random.Range(0, m_Dungeon.MAX_MAP_X_SIZE);
            PosZ = Random.Range(0, m_Dungeon.MAX_MAP_Z_SIZE);
        } while (m_Dungeon.m_map[PosX, PosZ] != (int)DungeonForm.MapID.MAP_ROOM ||
            m_CharaMap[PosX, PosZ] != null);

        //方向もランダムに
        m_Dir = (DIR)Random.Range((int)DIR.UP, (int)DIR.DIR_NUM);

        //自分のアドレスを入れる
        m_CharaMap[PosX, PosZ] = this;

        //移動
        this.transform.position = new Vector3(PosX * 1.0f, 0.5f, PosZ * 1.0f);

        //ステータス取得
        m_State = this.GetComponent<Status>();
	}
	
	// Update is called once per frame
	void Update () {

        if (isDeath())
        {
            m_CharaMap[this.PosX, this.PosZ] = null;
            this.gameObject.SetActive(false);
        };
	}
}
