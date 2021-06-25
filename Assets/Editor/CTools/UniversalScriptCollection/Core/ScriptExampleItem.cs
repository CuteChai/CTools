using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;
using UnityEngine;

namespace CTEditor.Core
{
    public class ScriptExampleItem
    {
        public bool DrawCodeExample { get; set; }

        private static GUIStyle headerGroupStyle;

        private static GUIStyle tabGroupStyle;

        private static Color backgroundColor = new Color(192, 195, 195, byte.MaxValue);

        private GUITabGroup tabGroup;

        private ScriptExamplePreview scriptExamplePreviewDrawwer;

        private UniversalScriptExampleBase myExample;

        public UniversalScriptExampleBase GetExample()
        {
            return this.myExample;
        }

        public ScriptExampleItem(UniversalScriptExampleBase scriptExample)
        {
            this.DrawCodeExample = true;
            if (scriptExample == null)
            {
                Debug.LogError("AExampleBase数据为空，请检查类型");
                return;
            }
            myExample = scriptExample;
            scriptExamplePreviewDrawwer = new ScriptExamplePreview(scriptExample);
            this.tabGroup = new GUITabGroup()
            {
                ToolbarHeight = 30f
            };

            this.tabGroup.RegisterTab(scriptExample.GetExamplScriptInfo().Name);
        }

        [OnInspectorGUI]
        public void Draw()
        {
            GUIStyle guiStyle;
            if ((guiStyle = ScriptExampleItem.headerGroupStyle) == null)
            {
                (guiStyle = new GUIStyle()).padding = new RectOffset(4, 6, 10, 4);
            }

            ScriptExampleItem.headerGroupStyle = guiStyle;

            GUIStyle guiStyle2;
            if ((guiStyle2 = ScriptExampleItem.tabGroupStyle) == null)
            {
                (guiStyle2 = new GUIStyle(SirenixGUIStyles.BoxContainer)).padding = new RectOffset(0, 0, 0, 0);
            }

            ScriptExampleItem.tabGroupStyle = guiStyle2;

            GUILayout.BeginVertical(ScriptExampleItem.headerGroupStyle, new GUILayoutOption[0]);
            GUILayout.Label(this.myExample.GetExamplScriptInfo().Name, SirenixGUIStyles.SectionHeader, new GUILayoutOption[0]);

            SirenixEditorGUI.DrawThickHorizontalSeparator(4f, 10f); //绘画加粗的分隔线

            if (!string.IsNullOrEmpty(this.myExample.GetExamplScriptInfo().Description))
            {
                GUILayout.Label(this.myExample.GetExamplScriptInfo().Description, SirenixGUIStyles.MultiLineLabel, new GUILayoutOption[0]);
                SirenixEditorGUI.DrawThickHorizontalSeparator(10f, 10f);
            }

            if (this.scriptExamplePreviewDrawwer != null)
            {
                Color color = GUI.backgroundColor;
               // GUI.backgroundColor = ScriptExampleItem.backgroundColor;
                this.tabGroup.BeginGroup(true, ScriptExampleItem.tabGroupStyle);
                GUI.backgroundColor = color;

                GUITabPage guiTabPage = this.tabGroup.RegisterTab(this.myExample.GetExamplScriptInfo().Name);
                if (guiTabPage.BeginPage())
                {
                    scriptExamplePreviewDrawwer.Draw(this.DrawCodeExample);
                }
                guiTabPage.EndPage();
                
                this.tabGroup.EndGroup();
            }
            else
            {
                GUILayout.Label("No examples available.", new GUILayoutOption[0]);
            }

            GUILayout.EndVertical();
        }
    }
}