using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Xml.Serialization;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

public class Monster_importer : AssetPostprocessor
{
    //�G�N�Z���t�@�C���p�X
    private static readonly string filePath = "Assets/Resources/ExcelData/Monster.xls";
    //�V�[�g(���̃^�u�݂����Ȃ��)��
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
                //�t�@�C���t�H�[�}�b�g�����񂽂炩�񂽂�...�B
                var book = new HSSFWorkbook(stream);

                foreach (string sheetName in sheetNames)
                {
                    //�o�͐�p�X
                    var exportPath = "Assets/Resources/ExcelData/" + sheetName + ".asset";
                    
                    // �p�X�ƌ^�Ɉ�v����Asset����
                    var data = (Entity_Monster)AssetDatabase.LoadAssetAtPath(exportPath, typeof(Entity_Monster));
                    //Asset��������Ȃ�����
                    if (data == null)
                    {
                        //Asset�쐬
                        data = ScriptableObject.CreateInstance<Entity_Monster>();
                        AssetDatabase.CreateAsset((ScriptableObject)data, exportPath);
                        data.hideFlags = HideFlags.NotEditable;
                    }
                    //���X�g�̃N���A
                    data.param.Clear();

					// sheet������V�[�g�擾
                    var sheet = book.GetSheet(sheetName);
                    if (sheet == null)
                    {
                        //�V�[�g���Ȃ��������O�ɕ\��
                        Debug.LogError("[QuestData] �V�[�g�����݂��܂���B:" + sheetName);
                        //���̃V�[�g��
                        continue;
                    }

                	//���ǉ�
                    for (int i=1; i<= sheet.LastRowNum; i++)
                    {
                        //�s�擾�H
                        IRow row = sheet.GetRow(i);
                        ICell cell = null;
                        //���̏����i�[����̂ɓK�����N���X�ō쐬
                        var p = new Entity_Monster.Param();
                        //  i�s0��ڂ̃Z�����擾
                        cell = row.GetCell(0);
                        //�Z�����擾�ł����Ȃ�X�g�����O�^�ŃZ���̒l�����
                        p.name = (cell == null ? "" : cell.StringCellValue);

                        //���X�g�ɒǉ�
                        data.param.Add(p);
                    }
                    
                    // scriptable�ۑ�
                    ScriptableObject obj = AssetDatabase.LoadAssetAtPath(exportPath, typeof(ScriptableObject)) as ScriptableObject;
                    //�_�[�e�B�Ƃ��ă}�[�N
                    //����: Unity 5.3 ���O�́ASetDirty �̓I�u�W�F�N�g�Ƀ_�[�e�B�ƃ}�[�N�t�������v�ȕ��@�ł����B
                    //5.3 �ȍ~�}���`�V�[���ҏW�̓����ɔ����A�V�[���̃I�u�W�F�N�g��ύX����Ƃ��ɂ��̊֐��͎g�p����Ȃ��Ȃ�܂����B
                    //���̊֐��͍���A�V�����o�[�W������ Unity �Ŕp�~�����\��������܂��B. 
                    EditorUtility.SetDirty(obj);
                }
            }

        }
    }
}
