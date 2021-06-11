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

