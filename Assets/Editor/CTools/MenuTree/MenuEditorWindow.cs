using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;


namespace CTEditor
{
    public class MenuEditorWindow : AMenuEditorWindow<MenuEditorWindow>
    {
        /// <summary>
        /// 在编辑器菜单栏生成工具索引
        /// </summary>
        [MenuItem("CTools/ToolBar")]
        public static void ShowFrame()
        {
            OpenEditorWindow("ToolBar");
        }

        /// <summary>
        /// 创建自定义菜单
        /// </summary>
        /// <returns></returns>
        protected override OdinMenuTree BuildMenuTree()
        {
            OdinMenuTree.Add("通用工具",new InterfaceDescription("通用工具界面","集合项目中的一些常用工具。"));
            OdinMenuTree.Add("常用工具脚本",new InterfaceDescription("常用脚本集合","收集的一些项目常用的脚本代码。"));
            AddAEditorWindowBase<ProjectAddressEditorWindowBase>();
            AddAEditorWindowBase<ConfigFloderViewEditorWindowBase>();
            return OdinMenuTree;
        }
    }
    
    /// <summary>
    /// 绘制UI
    /// </summary>
    public class InterfaceDescription
    {
        private string TitleName = "NoTitle";
            
        private string Description = "NoContent";

        private InterfaceDescription(){ }

        public InterfaceDescription(string name,string description)
        {
            this.TitleName = name;
            this.Description = description;
        }
            
        [OnInspectorGUI]
        public void Draw()
        {
            GUILayout.Label(TitleName, SirenixGUIStyles.SectionHeader, new GUILayoutOption[0]);
            SirenixEditorGUI.DrawThickHorizontalSeparator(4f, 10f);
            if (!string.IsNullOrEmpty(Description))
            {
                GUILayout.Label(Description, SirenixGUIStyles.MultiLineLabel, new GUILayoutOption[0]);
                SirenixEditorGUI.DrawThickHorizontalSeparator(10f, 10f);
            }
        }
    }
}