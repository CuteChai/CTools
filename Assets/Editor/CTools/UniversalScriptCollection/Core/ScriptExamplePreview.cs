using System;
using CTEditor.toolkit;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace CTEditor.Core
{
    public class ScriptExamplePreview
    {
        private static GUIStyle exampleGroupStyle;

        private static GUIStyle previewStyle;

        private static GUIStyle codeTextStyle;

        private static Color previewBackgroundColorDrak = new Color(56, 56, 56, byte.MaxValue);

        private static Color previewBackgroundColorLight = new Color(194, 194, 194, byte.MaxValue);

        private PropertyTree tree;

        private string highlightedCode;

        private Vector2 scrollPosition;

        private Action<Rect> m_DrawCallbackAction;

        private bool showRaw;

        private UniversalScriptExampleBase myExample;

        public ScriptExamplePreview(UniversalScriptExampleBase scriptExample)
        {
            this.myExample = scriptExample;
            //this.m_DrawCallbackAction = scriptExample.;
            try
            {
                this.highlightedCode = SyntaxHighlighter.Parse(scriptExample.GetExamplScriptInfo().Code);
            }
            catch (Exception exception)
            {
                Debug.LogError($"条目{scriptExample.GetExamplScriptInfo().Name}的代码块高亮失败，原因是：{exception}");
                this.highlightedCode = scriptExample.GetExamplScriptInfo().Code;
                this.showRaw = true;
            }
        }

        public void Draw(bool drawCodeExample)
        {
            if (ScriptExamplePreview.exampleGroupStyle == null)
            {
                ScriptExamplePreview.exampleGroupStyle = new GUIStyle(GUIStyle.none)
                {
                    padding = new RectOffset(1, 1, 10, 0)
                };
            }

            if (ScriptExamplePreview.previewStyle == null)
            {
                ScriptExamplePreview.previewStyle = new GUIStyle(GUIStyle.none)
                {
                    padding = new RectOffset(0, 0, 0, 0)
                };
            }

            GUILayout.BeginVertical(ScriptExamplePreview.exampleGroupStyle, new GUILayoutOption[0]);
            // //画预览UI
            //GUILayout.Label("Preview", SirenixGUIStyles.BoldTitle, new GUILayoutOption[0]);
            GUILayout.BeginVertical();
             Rect rect = GUIHelper.GetCurrentLayoutRect().Expand(4, 0);
            SirenixEditorGUI.DrawSolidRect(rect,
                EditorGUIUtility.isProSkin ? ScriptExamplePreview.previewBackgroundColorDrak : ScriptExamplePreview.previewBackgroundColorLight, true);
            SirenixEditorGUI.DrawBorders(rect,1,true);
            //  GUILayout.Space(8f);
            // GUILayout.Space(8f);
            GUILayout.EndVertical();
            if (drawCodeExample && myExample.GetExamplScriptInfo().Code != null)
            {
                GUILayout.Space(12f);
                GUILayout.Label("Code", SirenixGUIStyles.BoldTitle, new GUILayoutOption[0]);
                Rect rect1 = SirenixEditorGUI.BeginToolbarBox(new GUILayoutOption[0]);  
                SirenixEditorGUI.DrawSolidRect(rect1.HorizontalPadding(1), SyntaxHighlighter.BackgroundColor, true);
                SirenixEditorGUI.BeginToolbarBoxHeader(22f);  //BeginToolbarBoxHeader
                if (SirenixEditorGUI.ToolbarButton(this.showRaw ? "Hightlighted" : "Raw", false))
                {
                    this.showRaw = !this.showRaw;
                }

                GUILayout.FlexibleSpace(); //排列到一整行
                EditorGUILayoutExtension.LinkFileLabelField("点击定位到脚本目录", this.myExample.GetExamplScriptInfo().CodePath);
                GUILayout.FlexibleSpace(); //排列到一整行
                if (SirenixEditorGUI.ToolbarButton("Copy", false))
                {
                    Clipboard.Copy<string>(this.myExample.GetExamplScriptInfo().Code); //复制到剪切板
                }

                SirenixEditorGUI.EndToolbarBoxHeader();  //86 
                if (ScriptExamplePreview.codeTextStyle == null)
                {
                    ScriptExamplePreview.codeTextStyle = new GUIStyle(SirenixGUIStyles.MultiLineLabel);
                    ScriptExamplePreview.codeTextStyle.normal.textColor = SyntaxHighlighter.TextColor;
                    ScriptExamplePreview.codeTextStyle.active.textColor = SyntaxHighlighter.TextColor;
                    ScriptExamplePreview.codeTextStyle.focused.textColor = SyntaxHighlighter.TextColor;
                    ScriptExamplePreview.codeTextStyle.wordWrap = false;
                }
                GUIContent content = GUIHelper.TempContent(this.showRaw
                    ? this.myExample.GetExamplScriptInfo().Code.TrimEnd(
                        new char[]
                        {
                            '\n',
                            '\r'
                        })
                    : this.highlightedCode);
                Vector2 vector = ScriptExamplePreview.codeTextStyle.CalcSize(content); //根据内容计算大小
                GUILayout.BeginHorizontal(new GUILayoutOption[0]);  
                GUILayout.Space(-3F);
                GUILayout.BeginVertical(new GUILayoutOption[0]);  
                GUIHelper.PushEventType((Event.current.type == EventType.ScrollWheel) ? EventType.Used : Event.current.type);
                this.scrollPosition = GUILayout.BeginScrollView(this.scrollPosition, true, false, GUI.skin.horizontalScrollbar, GUIStyle.none,
                    new GUILayoutOption[]
                    {
                        GUILayout.MinHeight(vector.y + 20)
                    });
                Rect rect2 = GUILayoutUtility.GetRect(vector.x + 50f, vector.y).AddXMin(4f).AddY(2f);
                if (this.showRaw)
                {
                    EditorGUI.SelectableLabel(rect2,this.myExample.GetExamplScriptInfo().Code,ScriptExamplePreview.codeTextStyle);
                    GUILayout.Space(-14f);
                }
                else
                {
                    GUI.Label(rect2,content,ScriptExamplePreview.codeTextStyle);
                }
                GUILayout.EndScrollView();  //122
                GUIHelper.PopEventType();
                GUILayout.EndVertical();  //120 
                GUILayout.Space(-3f);
                GUILayout.EndHorizontal();  //118 
                GUILayout.Space(-3f);
                SirenixEditorGUI.EndToolbarBox(); //84
            }
            GUILayout.EndVertical();
        }
    }
}