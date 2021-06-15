using System;
using System.Collections.Generic;
using System.Reflection;
using Sirenix.OdinInspector.Editor;

namespace CTEditor.Core
{
    public class CToolsEditorWindowUtilities
    {
        private static readonly Dictionary<Type, UniversalScriptExampleBase> allScriptExampleDic = new Dictionary<Type, UniversalScriptExampleBase>();

        static CToolsEditorWindowUtilities()
        {
            Assembly assembly=Assembly.GetAssembly(typeof(CToolsEditorWindowUtilities));
            Type[] types = assembly.GetTypes();

            foreach (var type in types)
            {
                object[] objects = type.GetCustomAttributes(typeof(OriginTrackerAttribute), true);
                if (objects.Length==0||type.IsAbstract)
                {
                    continue;
                }
                UniversalScriptExampleBase temp = Activator.CreateInstance(type) as UniversalScriptExampleBase;
                allScriptExampleDic.Add(type,temp);
            }
        }
        
        /// <summary>
        /// 创建脚本案例的菜单树
        /// </summary>
        /// <param name="tree"></param>
        public static void BuildMenuTree(OdinMenuTree tree)
        {
            foreach (var scriptExample in allScriptExampleDic)
            {
                ExamplScriptInfo trickOverViewInfo = (scriptExample.Value).GetExamplScriptInfo();
                OdinMenuItem menuItem =
                    new OdinMenuItem(tree, trickOverViewInfo.Name, scriptExample.Key)
                    {
                        Value = scriptExample.Key,
                        SearchString = trickOverViewInfo.Name + trickOverViewInfo.Description
                    };
                tree.AddMenuItemAtPath("常用脚本集合/"+trickOverViewInfo.Category, menuItem);
            }
            tree.MarkDirty();
        }
        
        /// <summary>
        /// 根据脚本类型获取脚本实例
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static UniversalScriptExampleBase GetExampleByType(Type type)
        {
            if (allScriptExampleDic.TryGetValue(type, out var aExampleBase))
            {
                return aExampleBase;
            }
            return null;
        }
    }
}


