using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityQuickSheet;
using System.Linq;

///
/// !!! Machine generated code !!!
///
[CustomEditor(typeof(Talk_To_Mom))]
public class Talk_To_MomEditor : BaseExcelEditor<Talk_To_Mom>
{	    
    public override bool Load()
    {
        Talk_To_Mom targetData = target as Talk_To_Mom;

        string path = targetData.SheetName;
        if (!File.Exists(path))
            return false;

        string sheet = targetData.WorksheetName;

        ExcelQuery query = new ExcelQuery(path, sheet);
        if (query != null && query.IsValid())
        {
            targetData.dataArray = query.Deserialize<Talk_To_MomData>().ToArray();
            targetData.dataList = query.Deserialize<Talk_To_MomData>();
            EditorUtility.SetDirty(targetData);
            AssetDatabase.SaveAssets();
            return true;
        }
        else
            return false;
    }
}
