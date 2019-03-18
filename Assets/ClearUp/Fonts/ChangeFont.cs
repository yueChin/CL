/************************************************************
  FileName: CanvasLogic.cs
  Author:       Version :1.0          Date: 2018-9-14
  Description:批量换字体(鼠标选中Canvas，拖入要替换的字体)
************************************************************/
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class ChangeFont
{
    private Font getFont;
    private static Font targetFont;

#if UNITY_EDITOR_WIN
    [MenuItem("Tools/批量换字体")]
    public static void Open()
    {
        EditorWindow.GetWindow(typeof(ChangeFont));
    }
  

    void OnGUI()
    {
        getFont = (Font)EditorGUILayout.ObjectField(getFont, typeof(Font), true, GUILayout.MinWidth(100f));
        targetFont = getFont;
        if (GUILayout.Button("更换"))
        {
            ChangeFonts();
        }
    }

    public static void ChangeFonts()
    {
        UnityEngine.Object[] texts = Selection.GetFiltered(typeof(Text), SelectionMode.Deep);
        foreach (object item in texts)
        {
            Text tmptext = (Text)item;
            Undo.RecordObject(tmptext, tmptext.gameObject.name);
            tmptext.font = targetFont;
            EditorUtility.SetDirty(tmptext);
        }
        Debug.Log("替换完毕");
    }
#endif
}
