using UnityEngine;
using System.Collections;
/// <summary>
/// エネミーを生成するクラス
/// </summary>
public class EnemyGenerate : MonoBehaviour {
	// Use this for initialization
	void Start () {
        //エネミー生成
        Entity_Monster entityMonster = Resources.Load("ExcelData/Monster") as Entity_Monster;
        for (short i = 0; i < entityMonster.param.Count; i++) 
        {
            GameObject enemy;
            if (entityMonster.param[i].name != "スライム")
            {
                //cube生成
                enemy = GameObject.CreatePrimitive(PrimitiveType.Cube);
                //色変更
                enemy.GetComponent<Renderer>().material.color = Color.red;
            }
            else
            {
                enemy = (GameObject)Instantiate(Resources.Load("Prefab/slime") as GameObject,Vector3.zero,Quaternion.identity);
            }
            //Enemyコンポーネント追加
            enemy.AddComponent<Enemy>();
            //名称変更
            enemy.name = entityMonster.param[i].name;
            //親設定
            enemy.transform.SetParent(this.transform);
        }

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
