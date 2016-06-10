using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;                              //!< UnityEditorを入れてね！

/**
 * Inspector拡張クラス
 */
[CustomEditor(typeof(Status))]               //!< 拡張するときのお決まりとして書いてね
public class CharacterEditor : Editor           //!< Editorを継承するよ！
{
    bool folding = false;

    public override void OnInspectorGUI()
    {
        // target は処理コードのインスタンスだよ！ 処理コードの型でキャストして使ってね！
        Status state = target as Status;

        /* -- カスタム表示 -- */

        // -- 体力 --
        EditorGUILayout.LabelField("体力(現在/最大)");
        //これで挟むと同じ行に書かれる
        EditorGUILayout.BeginHorizontal();
        state.hp = EditorGUILayout.IntField(state.hp, GUILayout.Width(48));
        state.maxHp = EditorGUILayout.IntField(state.maxHp, GUILayout.Width(48));
        EditorGUILayout.EndHorizontal();

        // 現在HPが最大HPを上回らないように補正
        if (state.hp > state.maxHp)
        {
            //上回れば最大値で上書きする
            state.hp = state.maxHp;
        }

        // -- 攻撃力 --
        state.PATK = EditorGUILayout.FloatField("攻撃力", state.PATK);

        // -- 防御力 --
        state.PDEF = EditorGUILayout.FloatField("防御力", state.PDEF);

        // -- 魔法攻撃力 --
        state.MATK = EditorGUILayout.FloatField("魔法攻撃力", state.MATK);

        // -- 魔法防御 --
        state.MDEF = EditorGUILayout.FloatField("魔法防御", state.MDEF);

        // -- 速度 --
        state.SPD = EditorGUILayout.FloatField("速度", state.SPD);

        //スペース
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("各属性補正倍率");

        // -- 火属性 --
        state.fire = EditorGUILayout.FloatField("火属性", state.fire);

        // -- 水属性 --
        state.water = EditorGUILayout.FloatField("水属性", state.water);

        // -- 自然属性 --
        state.nature = EditorGUILayout.FloatField("自然属性", state.nature);

        // -- 氷属性 --
        state.ice = EditorGUILayout.FloatField("氷属性", state.ice);

        // -- 雷属性 --
        state.thunder = EditorGUILayout.FloatField("雷属性", state.thunder);

        // -- 土属性 --
        state.earth = EditorGUILayout.FloatField("土属性", state.earth);

        // -- 生命属性 --
        state.life = EditorGUILayout.FloatField("生命属性", state.life);

        //スペース
        EditorGUILayout.Space();

        // -- 種族名 --
        state.charaName = EditorGUILayout.TextField("名前", state.charaName);

        // -- 友達 --
        List<GameObject> list = state.friends;
        int i, len = list.Count;

        // 折りたたみ表示
        if (folding = EditorGUILayout.Foldout(folding, "友達"))
        {
            // リスト表示
            for (i = 0; i < len; ++i)
            {
                list[i] = EditorGUILayout.ObjectField(list[i], typeof(GameObject), true) as GameObject;
            }

            GameObject go = EditorGUILayout.ObjectField("追加", null, typeof(GameObject), true) as GameObject;
            if (go != null)
                list.Add(go);
        }

        if (GUILayout.Button("押すな"))
        {
            // 押下時に実行したい処理
        }
    }
}