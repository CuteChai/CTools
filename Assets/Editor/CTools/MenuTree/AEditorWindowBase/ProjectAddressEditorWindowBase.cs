using System;
using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace CTEditor
{
    public class ProjectAddressEditorWindowBase : AEditorWindowBase
    {
        private static EditorWindowInfo ThisEditorWindowInfo = new EditorWindowInfo("项目地址", "通用工具", "该工程的GitHub地址链接。（ https://github.com/CuteChai/CTools.git ）");

        public override EditorWindowInfo GetEditorWindowInfo()
        {
            return ThisEditorWindowInfo;
        }

        public ProjectAddressEditorWindowBase()
        {
        }

        public ProjectAddressEditorWindowBase(EditorWindow editorWindow) : base(editorWindow)
        {
        }

        public override void DrawUI()
        {
            InterfaceDescription interfaceDescription = new InterfaceDescription(ThisEditorWindowInfo.Name, ThisEditorWindowInfo.Description);
            interfaceDescription.Draw();
        }

        [PropertySpace(10)]
        [LabelText("Github"), Button]
        public void OpenGithub()
        {
            Application.OpenURL("https://github.com/CuteChai/CTools.git");
            
        }
        
        //TODO Current Time Move TO MainInterface Bottom
        [PropertySpace(330)]
        [Button("@\"Current Time: \" + DateTime.Now.ToString(\"HH:mm:ss\")")]
        public void WorldTime()
        {
            Debug.Log("Current Time:" + DateTime.Now.ToString("HH:mm:ss"));
        }
        
        public override void OnEnable()
        {
            DrawUI();
        }
    }
}