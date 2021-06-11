using System;
using System.Collections.Generic;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace CTEditor
{
    public abstract class AMenuEditorWindow<T> : OdinMenuEditorWindow where T : EditorWindow
    {
        public AEditorWindowBase currentWindow;
        public List<AEditorWindowBase> aEditorWindowBaseList = new List<AEditorWindowBase>();

        private OdinMenuTree odinMenuTree;

        public OdinMenuTree OdinMenuTree
        {
            get
            {
                if (odinMenuTree == null)
                {
                    odinMenuTree = new OdinMenuTree();
                    odinMenuTree.Config.DrawSearchToolbar = true; //打开搜索栏
                    odinMenuTree.Selection.SelectionChanged += OnSelectionChange;
                }

                return odinMenuTree;
            }
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            return OdinMenuTree;
        }

        public static void OpenEditorWindow(string title)
        {
            EditorWindow window = GetWindow<T>(title);
            window.position = new Rect(Screen.currentResolution.width / 2 - 500, Screen.currentResolution.height / 2 - 250, 1000, 500);
            window.Show();
        }

        public void OnSelectionChange(SelectionChangedType type)
        {
            switch (type)
            {
                case SelectionChangedType.ItemRemoved:
                    break;
                case SelectionChangedType.ItemAdded:
                    if (OdinMenuTree.Selection.SelectedValue != null)
                    {
                        if (OdinMenuTree.Selection.SelectedValue.GetType() != typeof(AEditorWindowBase)) return;
                        currentWindow = (AEditorWindowBase) OdinMenuTree.Selection.SelectedValue;
                        currentWindow.OnEnable();
                    }

                    break;
                case SelectionChangedType.SelectionCleared:
                    if (currentWindow != null)
                    {
                        currentWindow.OnDisable();
                        currentWindow = null;
                    }

                    break;
            }
        }

        public void AddAEditorWindowBase<EditorWindowBase>(EditorIcon icon = null) where EditorWindowBase : AEditorWindowBase
        {
            AEditorWindowBase aEditorWindowBase = (AEditorWindowBase) Activator.CreateInstance(typeof(EditorWindowBase), new object[] {this});
            
            //TODO:给菜单添加Icon
            OdinMenuTree.AddMenuItemAtPath(aEditorWindowBase.GetEditorWindowInfo().Category, new OdinMenuItem(OdinMenuTree, aEditorWindowBase
                .GetEditorWindowInfo().Name, aEditorWindowBase));

            aEditorWindowBaseList.Add(aEditorWindowBase);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            foreach (AEditorWindowBase item in aEditorWindowBaseList)
            {
                item.OnDisable();
            }
            aEditorWindowBaseList.Clear();
        }
    }
}