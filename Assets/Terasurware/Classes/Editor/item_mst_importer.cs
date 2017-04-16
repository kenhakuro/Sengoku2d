using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Xml.Serialization;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

public class item_mst_importer : AssetPostprocessor
{
    private static readonly string filePath = "Assets/Resources/Data/item_mst.xls";
    private static readonly string[] sheetNames = { "item_mst", };
    
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
                    var exportPath = "Assets/Resources/Data/" + sheetName + ".asset";
                    
                    // check scriptable object
                    var data = (Entity_item_mst)AssetDatabase.LoadAssetAtPath(exportPath, typeof(Entity_item_mst));
                    if (data == null)
                    {
                        data = ScriptableObject.CreateInstance<Entity_item_mst>();
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
                        
                        var p = new Entity_item_mst.Param();
			
					cell = row.GetCell(0); p.id = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(1); p.itemCode = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(2); p.itemName = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(3); p.itemExp = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(4); p.effect = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(5); p.canBuy = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(6); p.canSell = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(7); p.buy = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(8); p.sell = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(9); p.itemRatio = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(10); p.itemNameEng = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(11); p.itemExpEng = (cell == null ? "" : cell.StringCellValue);

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
