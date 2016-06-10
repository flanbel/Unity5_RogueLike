using UnityEngine;
using System.Collections;

/// <summary>
/// すべてのキャラクター(エネミー、プレイヤーetc...)の基底クラス
/// </summary>
[RequireComponent(typeof(Status))]
public class Character : MonoBehaviour {

    //マップ
    //Characterを継承するオブジェクトが一つのマップを使いまわす
    static protected DungeonForm m_Dungeon;

    //キャラクターの位置,情報を管理
    static protected Character[,] m_CharaMap;

    //マップ上でのポジション
    protected int PosX = 0, PosZ = 0;

    //向きの移動量
    protected int DirX, DirZ;

    //移動先のポジション　未実装
    protected Vector3 m_destinationPos;

    //ステータス
    protected Status m_State;

    //角度が少ない順0～315
    protected enum DIR { DOWN = 0, LEFTDOWN, LEFT, LEFTUP, UP, RIGHTUP, RIGHT, RIGHTDOWN, DIR_NUM };

    //キャラクターの向き
    protected DIR m_Dir = DIR.UP;

    //一つでいい
    static Log log;

    public Character() 
    {
        
    }

	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    /// 死亡判定
    /// </summary>
    protected bool isDeath() 
    {
        if (this.m_State.hp <= 0)
        {
            return true;
        }
        return false;
    }

    /// <summarZ>
    /// マップオブジェクトから二次元配列の情報を取得
    /// </summarZ>
    protected void SetMap()
    {
        if (m_Dungeon == null)
        {
            //マップ取得
            m_Dungeon = GameObject.Find("Dungeon").GetComponent<DungeonForm>();
            //キャラマップ生成
            m_CharaMap = new Character[m_Dungeon.MAX_MAP_X_SIZE,m_Dungeon.MAX_MAP_Z_SIZE];
            for(short x = 0;x < m_Dungeon.MAX_MAP_X_SIZE;x++)
            {
                for (short z = 0; z < m_Dungeon.MAX_MAP_Z_SIZE; z++)
                {
                    //マップの各要素をnullで初期化
                    m_CharaMap[x,z] = null;
                }
            }
        }
    }

    /// <summarZ>
    /// 移動できるかチェック
    /// </summarZ>
    protected bool CheckMove()
    {
        int NextX = PosX + DirX;
        int NextZ = PosZ + DirZ;

        //移動チェック
        //移動先が壁 && 移動先に何のキャラもいない
        if (m_Dungeon.m_map[NextX,NextZ] != (int)DungeonForm.MapID.MAP_WALL &&
            m_CharaMap[NextX,NextZ] == null) 
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// 移動先設定
    /// </summary>
    protected void Move() 
    {
        //アドレスを消す
        m_CharaMap[PosX, PosZ] = null;

        PosX += DirX;
        PosZ += DirZ;

        //移動後のポジションにアドレスを入れる
        m_CharaMap[PosX, PosZ] = this;

        //移動先設定
        this.transform.position = new Vector3(PosX * 1.0f, this.transform.position.y, PosZ * 1.0f);
    }

    /// <summary>
    /// 攻撃(仮)
    /// </summary>
    public void Attack() 
    {
        int X = 0,Z = 0;

        //向きからポジションを割り出す
        switch (m_Dir) 
        {
            case DIR.UP:
                Z = 1;
                break;
            case DIR.RIGHT:
                X = 1;
                break;
            case DIR.LEFT:
                X = -1;
                break;
            case DIR.DOWN:
                Z = -1;
                break;
            case DIR.RIGHTUP:
                Z = 1;
                X = 1;
                break;
            case DIR.LEFTUP:
                Z = 1;
                X = -1;
                break;
            case DIR.RIGHTDOWN:
                Z = -1;
                X = 1;
                break;
            case DIR.LEFTDOWN:
                Z = -1;
                X = -1;
                break;
        }

        int NextX = PosX + X;
        int NextZ = PosZ + Z;

        //向いている方向に方向にキャラクターが居る
        if (m_CharaMap[NextX, NextZ] != null)
        {
            Character chara = m_CharaMap[NextX, NextZ];
            int Damege = (int)this.m_State.PATK;
            chara.m_State.hp -= Damege;

            //ログに出力
            OutPutLog(m_CharaMap[NextX, NextZ].name + "に攻撃！" + Damege + "のダメージ！");

            //殺したかチェック
            if (chara.m_State.hp <= 0) 
            {
                OutPutLog(m_CharaMap[NextX, NextZ].name + "を倒した！");
            }
        }
    }

    /// <summary>
    /// かわりにログに出力してくれる関数
    /// </summary>
    /// <param name="text"></param>
    protected void OutPutLog(string text) 
    {
        //ないなら取得する
        if (log == null)
        {
            log = GameObject.Find("Log").GetComponent<Log>();
        }

        log.AddLogText(text);
    }
}
