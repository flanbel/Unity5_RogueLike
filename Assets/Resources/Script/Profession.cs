using UnityEngine;
using System.Collections;
/// <summary>
/// 職業クラス
/// </summary>
public class Profession : MonoBehaviour
{
    //データベースへの参照
    static Entity_Profession ProfessionData = null;
    public enum JOBID { 無色 = 0, 召喚士, 盗賊, 混術師, 騎士, 暗殺者, 侍, 狂戦士, 戦士, 回復師, 付加魔術師, 魔術師, 弓兵, 狙撃手, 二丁拳銃, 罠師, 指示師, JOB_NUM };
    //ID
    public JOBID Id = 0;
    //自分の職業データ
    public Entity_Profession.Param ProfessionParam;

	// Use this for initialization
	void Start () {
        //もしデータがないなら取得
        if (ProfessionData == null)
        {
            //データベースへの参照取得
            ProfessionData = Resources.Load("ExcelData/Profession") as Entity_Profession;
        }
        //Id番目のデータ取得
        ProfessionParam = ProfessionData.param[(int)Id];
        this.name = ProfessionParam.Name;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    /// <summary>
    /// レベルアップ！
    /// </summary>
    public void JobLevelUp() 
    {
        Entity_Profession.Param pr = this.ProfessionParam;
        pr.Level++;
        pr.HP += (int)pr.HPExt;
        pr.Speed += (int)pr.SpeedExt;
        pr.PhysicsATK += (int)pr.PhysicsATKExt;
        pr.PhysiceDEF += (int)pr.PhysiceDEFExt;
        pr.MagicATK += (int)pr.MagicATKExt;
        pr.MagicDEF += (int)pr.MagicDEFExt;
        this.ProfessionParam = pr;
    }
}
