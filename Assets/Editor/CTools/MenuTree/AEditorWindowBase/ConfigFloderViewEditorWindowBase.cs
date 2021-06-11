using Sirenix.OdinInspector;
using UnityEditor;

namespace CTEditor
{
    public class ConfigFloderViewEditorWindowBase : AEditorWindowBase
    {
        public static EditorWindowInfo ThisEditorWindowInfo = new EditorWindowInfo("项目配置文件夹", "通用工具", "常用的文件夹（PersistentDataPath,StreamingAssetsPath）。");
        
        public override EditorWindowInfo GetEditorWindowInfo()
        {
            return ThisEditorWindowInfo;
        }
        
        public ConfigFloderViewEditorWindowBase()
        {
        }

        public ConfigFloderViewEditorWindowBase(EditorWindow editorWindow) : base(editorWindow)
        {
            
        }

        public override void DrawUI()
        {
            InterfaceDescription interfaceDescription = new InterfaceDescription(ThisEditorWindowInfo.Name, ThisEditorWindowInfo.Description);
            interfaceDescription.Draw();
        }
        
        [PropertySpace(10)]
        [LabelText("DataPathFolder"), Button]
        public void OpenDataPathFolder()
        {
            SystemFileFolderHandle.OpenTargetFolder(SystemStaticFile.SystemDataPathFolder,true);
        }
        
        [PropertySpace(10)]
        [LabelText("StreamingAssetsPathFolder"), Button]
        public void OpenStreamingAssetsPathFolder()
        {
            SystemFileFolderHandle.OpenTargetFolder(SystemStaticFile.SystemStreamingAssetsFolder,true);
        }
        
        [PropertySpace(10)]
        [LabelText("PersistentDataPathFolder"), Button]
        public void OpenPersistenDataPathFolder()
        {
            SystemFileFolderHandle.OpenTargetFolder(SystemStaticFile.SystemPersistenDataFolder);
        }
    }
}