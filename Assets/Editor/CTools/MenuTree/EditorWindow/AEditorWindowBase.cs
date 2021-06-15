using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace CTEditor
{
    public abstract class AEditorWindowBase
    {
        [HideInInspector]
        public EditorWindow editorWindow;

        public static EditorWindowInfo editorWindowInfo = new EditorWindowInfo();

        public virtual EditorWindowInfo GetEditorWindowInfo()
        {
            return editorWindowInfo;
        }
        
        public AEditorWindowBase(){}

        public AEditorWindowBase(EditorWindow editorWindow)
        {
            this.editorWindow = editorWindow;
        }

        public virtual void OnEnable(){ }
        public virtual void OnDisable(){ }
        
        [OnInspectorGUI]
        public virtual void DrawUI(){ }
    }
}

