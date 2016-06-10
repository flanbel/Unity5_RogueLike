using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Xml.Serialization;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

public class Monster_importer : AssetPostprocessor
{
    //エクセルファイルパス
    private static readonly string filePath = "Assets/Resources/ExcelData/Monster.xls";
    //シート(下のタブみたいなやつ)名
    private static readonly string[] sheetNames = { "Monster", };
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="importedAssets"></param>
    /// <param name="deletedAssets"></param>
    /// <param name="movedAssets"></param>
    /// <param name="movedFromAssetPaths"></param>
    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string asset in importedAssets)
        {
            if (!filePath.Equals(asset))
                continue;

            using (FileStream stream = File.Open (filePath, FileMode.Open, FileAccess.Read))
            {
                //ファイルフォーマットがうんたらかんたら...。
                var book = new HSSFWorkbook(stream);

                foreach (string sheetName in sheetNames)
                {
                    //出力先パス
                    var exportPath = "Assets/Resources/ExcelData/" + sheetName + ".asset";
                    
                    // パスと型に一致するAsset検索
                    var data = (Entity_Monster)AssetDatabase.LoadAssetAtPath(exportPath, typeof(Entity_Monster));
                    //Assetが見つからなかった
                    if (data == null)
                    {
                        //Asset作成
                        data = ScriptableObject.CreateInstance<Entity_Monster>();
                        AssetDatabase.CreateAsset((ScriptableObject)data, exportPath);
                        data.hideFlags = HideFlags.NotEditable;
                    }
                    //リストのクリア
                    data.param.Clear();

					// sheet名からシート取得
                    var sheet = book.GetSheet(sheetName);
                    if (sheet == null)
                    {
                        //シートがない胸をログに表示
                        Debug.LogError("[QuestData] シートが存在しません。:" + sheetName);
                        //次のシートに
                        continue;
                    }

                	//情報追加
                    for (int i=1; i<= sheet.LastRowNum; i++)
                    {
                        //行取得？
                        IRow row = sheet.GetRow(i);
                        ICell cell = null;
                        //その情報を格納するのに適したクラスで作成
                        var p = new Entity_Monster.Param();
                        //  i行0列目のセルを取得
                        cell = row.GetCell(0);
                        //セルが取得できたならストリング型でセルの値を入手
                        p.name = (cell == null ? "" : cell.StringCellValue);

                        //リストに追加
                        data.param.Add(p);
                    }
                    
                    // scriptable保存
                    ScriptableObject obj = AssetDatabase.LoadAssetAtPath(exportPath, typeof(ScriptableObject)) as ScriptableObject;
                    //ダーティとしてマーク
                    //注意: Unity 5.3 より前は、SetDirty はオブジェクトにダーティとマーク付けする主要な方法でした。
                    //5.3 以降マルチシーン編集の導入に伴い、シーンのオブジェクトを変更するときにこの関数は使用されなくなりました。
                    //この関数は今後、新しいバージョンの Unity で廃止される可能性があります。. 
                    EditorUtility.SetDirty(obj);
                }
            }

        }
    }
}
