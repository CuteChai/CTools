using System.Collections.Generic;
using System.Linq;
using CTEditor.Core;
using CTEditor.Core.ExampleFactory;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;


namespace CTEditor
{
    public class MenuEditorWindow : AMenuEditorWindow<MenuEditorWindow>
    {
        private PropertyTree m_ExamplePropertyTree;

        private ExampleTemplate m_ExampleTemplate = new ExampleTemplate();

        private Vector2 scrollPosition;
        
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
            OdinMenuTree.Add("通用工具",new InterfaceDescription("通用工具","<color=yellow>集合项目中的一些常用工具，方面在项目中能快速查询及使用相应的功能。</color>"));
            OdinMenuTree.Add("常用脚本集合",new InterfaceDescription("常用脚本集合","<color=yellow>收集的一些项目常用的脚本代码。</color>"));
            OdinMenuTree.Add("项目设置", Resources.FindObjectsOfTypeAll<PlayerSettings>().FirstOrDefault());
            AddAEditorWindowBase<ProjectAddressEditorWindowBase>();
            AddAEditorWindowBase<ConfigFloderViewEditorWindowBase>();
            CToolsEditorWindowUtilities.BuildMenuTree(OdinMenuTree);
            return OdinMenuTree;
        }
        
        /// <summary>
        /// 绘制用于Example创建的UI
        /// </summary>
        private void DrawExampleCreatorEditor()
        {
            if (m_ExamplePropertyTree == null)
            {
                if (m_ExampleTemplate == null)
                {
                    m_ExampleTemplate = new ExampleTemplate();
                }

                m_ExamplePropertyTree = PropertyTree.Create(m_ExampleTemplate);
            }

            GUILayout.Label("自动生成ScriptExample代码", SirenixGUIStyles.SectionHeader, new GUILayoutOption[0]);

            SirenixEditorGUI.DrawThickHorizontalSeparator(4f, 10f);
            m_ExamplePropertyTree.Draw(false);
            
        }
        
        protected override void DrawEditors()
        {
            GUILayout.BeginArea(new Rect(4f, 0f, Mathf.Max(300f, base.position.width - this.MenuWidth - 4f),
                base.position.height));
            this.scrollPosition = GUILayout.BeginScrollView(this.scrollPosition, GUILayoutOptions.ExpandWidth(false));
            GUILayout.Space(4f);
            if (m_ShouldDrawExampleCreatorUI)
            {
                DrawExampleCreatorEditor();
            }
            else
            {
                base.DrawEditors();
                if (scriptExampleItem!=null)
                {
                    scriptExampleItem.Draw();
                }
            }

            GUILayout.EndScrollView();
            GUILayout.EndArea();
        }
        
        protected override void OnGUI()
        {
            GUILayout.BeginHorizontal();

            GUI.color = Color.HSVToRGB(
                Mathf.Cos((float) EditorApplication.timeSinceStartup + 1f) * 0.125f + 0.325f, 1, 1);//绿蓝渐变色
            if (GUILayout.Button(
                new GUIContent("Create a Example OverView", EditorGUIUtility.FindTexture("Toolbar Plus")),
                "toolbarbutton", GUILayout.Width(200)))
            {
                m_ShouldDrawExampleCreatorUI = true;
            }

            GUI.color = Color.white;
            GUILayout.EndHorizontal();
            base.OnGUI();
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