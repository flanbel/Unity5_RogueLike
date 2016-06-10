using UnityEngine;
using System.Collections;

/// <summary>
/// プレイヤーのクラス
/// </summary>
public class Player : Character {

    //アクション(移動、攻撃,技etc...)ターン数が進む行動
    bool m_Action;
    public bool isAction { get { return m_Action; } }

    //その場回転とかstay
    bool m_stay;

    //体力バー
    public GameObject Hpbar;
    //体力テキスト
    public GameObject Hptext;

    //設定している職業の配列 最大数は3
    //define定義したい
    public Profession[] m_JobArray = new Profession[3];

    //現在選択しているジョブ
    [SerializeField]
    Profession m_SelectJob;
    public Profession SelectJob { get { return m_SelectJob; } }

    //設定されているジョブの数
    short m_jobcnt;
    public short jobcnt { get { return m_jobcnt; } }

    //今選択されているjobの添え字
    short m_NowJobIdx = 0;
    
    //スキル
    //public Skill[] m_Skill;
 
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
        this.transform.position = new Vector3(PosX * 1.0f, 0.0f, PosZ * 1.0f);

        m_destinationPos = this.transform.position;

        //ステータス取得
        m_State = this.GetComponent<Status>();
        DecisionState();
	}

    void Update()
    {
        //キーの押下判定
        //上矢印
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveUp();
        }
        //下矢印
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveDonw();
        }

        //右矢印
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
        }
        //左矢印
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
        }

        Vector3 rot = this.transform.localEulerAngles;


        Debug.Log(m_Dir);
        //回転させる(+180.0fはモデルが反対向いてるからその調整)
        this.transform.localEulerAngles = new Vector3(0.0f, ((int)m_Dir * 45.0f) - 180.0f, 0.0f);

        //移動可能なら移動
        if (CheckMove())
        {
            Move();
            //体力を減らすテスト
            m_State.hp--;
        }

        //攻撃
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Attack();
        }
        //レベルアップ
        if (Input.GetKeyDown(KeyCode.C)) 
        {
            m_SelectJob.JobLevelUp();
            m_State.maxHp += (int)m_SelectJob.ProfessionParam.HPExt;
            //体力の最大値と同じに
            m_State.hp += m_State.maxHp / 10;
            OutPutLog(m_SelectJob.ProfessionParam.Name + "のLevelが上がった！");
            GameObject.Find("levelup").GetComponent<levelup>().feedout();
        }

        //プレイヤーの仕事ではない？
        {
            //体力テキストの更新
            Hptext.GetComponent<UnityEngine.UI.Text>().text = m_State.hp.ToString() + '/' + m_State.maxHp.ToString();

            //体力の比率
            float Hpratio = (float)m_State.hp / (float)m_State.maxHp;
            if (Hpbar.transform.localScale.x > 0)
            {
                Hpbar.transform.localScale = new Vector3(Hpratio, Hpbar.transform.localScale.y, Hpbar.transform.localScale.z);
            }
            //0以下
            else
            {
                Hpbar.transform.localScale = new Vector3(0.0f, Hpbar.transform.localScale.y, Hpbar.transform.localScale.z);
            }
        }

        if (isDeath())
        {
            //ログ出力
            OutPutLog("あなたは死んでしまった！");
            //マップ上から消す
            m_CharaMap[this.PosX, this.PosZ] = null;
            //アクティブじゃなくなる
            this.gameObject.SetActive(false);
        };

        //向きの移動量を初期化
        DirX = DirZ = 0;
    }

    //ジョブからステータス算出
    void DecisionState()
    {
        //ジョブの数
        m_jobcnt = 0;
        //ジョブのサイズ分繰り返す
        for(short j = 0;j < m_JobArray.Length;j++)
        {
            //その要素にジョブが格納されている。
            if (m_JobArray[j] != null) 
            {
                m_jobcnt++;
                //Hpは色業の合計でいいかな。
                m_State.maxHp += m_JobArray[j].ProfessionParam.HP;
                //案1：設定している職業のステータスを足すパターン
                //m_State.PATK += m_JobArray[j].ProfessionParam.PhysicsATK;
                //m_State.PDEF += m_JobArray[j].ProfessionParam.PhysiceDEF;
                //m_State.MATK += m_JobArray[j].ProfessionParam.MagicATK;
                //m_State.MDEF += m_JobArray[j].ProfessionParam.MagicDEF;
                //m_State.SPD += m_JobArray[j].ProfessionParam.Speed;
            }
        }
        //体力の最大値と同じに
        m_State.hp = m_State.maxHp;
        JobChenge();
    }

    /// <summary>
    /// 次のジョブに変更する関数
    /// </summary>
    public void JobChenge() 
    {
        //添え字のジョブに設定
        m_SelectJob = m_JobArray[(m_NowJobIdx++ % m_jobcnt)];
        //案2：選択されている職業のステータスのみ反映させるパターン
        m_State.PATK = m_SelectJob.ProfessionParam.PhysicsATK;
        m_State.PDEF = m_SelectJob.ProfessionParam.PhysiceDEF;
        m_State.MATK = m_SelectJob.ProfessionParam.MagicATK;
        m_State.MDEF = m_SelectJob.ProfessionParam.MagicDEF;
        m_State.SPD = m_SelectJob.ProfessionParam.Speed;
    }

    //↓移動用にとりあえずつくったゴミ関数
    
    public void MoveUp() 
    {
        DirZ = 1;
        m_Dir = DIR.UP;
    }

    public void MoveDonw()
    {
        DirZ = -1;
        m_Dir = DIR.DOWN;
    }

    public void MoveRight()
    {
        DirX = 1;
        m_Dir = DIR.RIGHT;
    }

    public void MoveLeft()
    {
        DirX = -1;
        m_Dir = DIR.LEFT;
    }


    public void MoveLeftUp()
    {
        DirX = -1;
        DirZ = 1;
        m_Dir = DIR.LEFTUP;
    }

    public void MoveRightUp()
    {
        DirX = 1;
        DirZ = 1;
        m_Dir = DIR.RIGHTUP;
    }

    public void MoveLeftDown()
    {
        DirX = -1;
        DirZ = -1;
        m_Dir = DIR.LEFTDOWN;
    }

    public void MoveRightDown()
    {
        DirX = 1;
        DirZ = -1;
        m_Dir = DIR.RIGHTDOWN;
    }
}