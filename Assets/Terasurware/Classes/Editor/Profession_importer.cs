using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Xml.Serialization;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

public class Profession_importer : AssetPostprocessor
{
    private static readonly string filePath = "Assets/Resources/ExcelData/Profession.xls";
    private static readonly string[] sheetNames = { "Profession", };
    
    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string asset in importedAssets)
        {
            if (!filePath.Equals(asset))
                continue;

            using (FileStream stream = File.Open (filePath, FileMode.Open, FileAccess.Read))
            {
                var book = new HSSFWorkbook(stream);

                foreach (string sheetName in sheetNames)
                {
                    var exportPath = "Assets/Resources/ExcelData/" + sheetName + ".asset";
                    
                    // check scriptable object
                    var data = (Entity_Profession)AssetDatabase.LoadAssetAtPath(exportPath, typeof(Entity_Profession));
                    if (data == null)
                    {
                        data = ScriptableObject.CreateInstance<Entity_Profession>();
                        AssetDatabase.CreateAsset((ScriptableObject)data, exportPath);
                        data.hideFlags = HideFlags.NotEditable;
                    }
                    data.param.Clear();

					// check sheet
                    var sheet = book.GetSheet(sheetName);
                    if (sheet == null)
                    {
                        Debug.LogError("[QuestData] sheet not found:" + sheetName);
                        continue;
                    }

                	// add infomation
                    for (int i=1; i<= sheet.LastRowNum; i++)
                    {
                        IRow row = sheet.GetRow(i);
                        ICell cell = null;
                        
                        var p = new Entity_Profession.Param();
			
					cell = row.GetCell(0); p.ProfessionID = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(1); p.ActiveFlg = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(2); p.Name = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(3); p.Level = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(4); p.HP = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(5); p.Speed = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(6); p.PhysicsATK = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(7); p.PhysiceDEF = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(8); p.MagicATK = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(9); p.MagicDEF = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(10); p.HPExt = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(11); p.SpeedExt = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(12); p.PhysicsATKExt = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(13); p.PhysiceDEFExt = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(14); p.MagicATKExt = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(15); p.MagicDEFExt = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(16); p.Luck = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(17); p.CriticalRate = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(18); p.CriticalPower = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(19); p.FireATK = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(20); p.NatureATK = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(21); p.IceATK = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(22); p.EarthATK = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(23); p.ThunderATK = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(24); p.WaterATK = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(25); p.LifeATK = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(26); p.FireDEF = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(27); p.NatureDEF = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(28); p.IceDEF = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(29); p.EarthDEF = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(30); p.ThunderDEF = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(31); p.WaterDEF = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(32); p.LifeDEF = (cell == null ? 0.0 : cell.NumericCellValue);

                        data.param.Add(p);
                    }
                    
                    // save scriptable object
                    ScriptableObject obj = AssetDatabase.LoadAssetAtPath(exportPath, typeof(ScriptableObject)) as ScriptableObject;
                    EditorUtility.SetDirty(obj);
                }
            }

        }
    }
}
