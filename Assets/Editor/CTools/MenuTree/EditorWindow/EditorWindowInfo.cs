using System;
using CTEditor.Core;
using UnityEngine;

namespace CTEditor
{
    public class EditorWindowInfo
    {
        /// <summary>
        /// 显示在TreeView的条目名称
        /// </summary>
        public string Name = "NoName";
        
        /// <summary>
        /// 分组
        /// </summary>
        public string Category = "Uncategorized";

        /// <summary>
        /// 界面功能的描述
        /// </summary>
        public string Description;

        public EditorWindowInfo(){ }

        public EditorWindowInfo(string name,string category,string description)
        {
            this.Name = name;
            this.Category = category;
            this.Description = description;
        }
    }
}

namespace CTEditor.Core
{
    public class ExamplScriptInfo
    {
        /// <summary>
        /// 显示在TreeView的条目名称
        /// </summary>
        public string Name = "NoName";

        /// <summary>
        /// 分组
        /// </summary>
        public string Category = "Uncategorized";

        /// <summary>
        /// 界面功能的描述
        /// </summary>
        public string Description;

        /// <summary>
        /// 代码模板
        /// </summary>
        public string Code;

        /// <summary>
        /// 代码模板的路径
        /// </summary>
        public string CodePath;

        /// <summary>
        /// 演示图片的路径
        /// </summary>
        public string PicPath;

        /// <summary>
        /// 演示视频的路径
        /// </summary>
        public string VideoPath;

        private UniversalScriptExampleBase exampleBase;

        public ExamplScriptInfo()
        {
        }

        public ExamplScriptInfo(string name, string category, string description, string code, string codePath, string picPath, string videoPath, Type type)
        {
            this.Name = name;
            this.Category = category;
            this.Description = description;

            this.Code = code;
            this.CodePath = codePath;

            this.PicPath = picPath;
            this.VideoPath = videoPath;

            this.exampleBase = CToolsEditorWindowUtilities.GetExampleByType(type);
        }
    }
}

