using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace CTEditor.Core.ExampleFactory
{
    public static class ExampleFactoryUtilities
    {
        /// <summary>
        /// 模板文件所在目录
        /// </summary>
        private const string c_TemplatePath =
            "Assets/Editor/CTools/ExampleScriptTemplate/ExampleScriptTemplate.txt";

        /// <summary>
        /// 输出目录
        /// </summary>
        private const string c_DesPath = "Assets/Editor/CTools/ScriptExamples";
        
        private static Dictionary<string, string> ReplaceMap = new Dictionary<string, string>(); //需要替换的位置

        public static void CreateScriptExampleFromTemplate(ExampleTemplate exampleTemplate)
        {
            if (exampleTemplate!=null)
            {
                string templateContent = AssetDatabase.LoadAssetAtPath<TextAsset>(c_TemplatePath).text; //读取代码模板
                
                string temp = templateContent;
                string finalFileName = $"{c_DesPath}/Example_{exampleTemplate.Name}.cs";

                exampleTemplate.Code = CodeEscape(exampleTemplate.Code);

                Dictionary<string, string> ReplaceMap = GetReplaceMap(exampleTemplate);

                foreach (var kParam in ReplaceMap)
                {
                    temp = temp.Replace(kParam.Key, kParam.Value); //将对应的值都替换到对应变量
                }

                SystemFileFolderHandle.DetermineTargetFolderExists(c_DesPath,true); //在目标目录创建代码文件夹
                
                while (File.Exists(finalFileName))
                {
                    finalFileName = finalFileName.Replace(".cs", $"_1.cs");
                }
                
                //将文件信息读入流中
                //初始化System.IO.FileStream类的新实例与指定路径和创建模式
                using (var fs = new FileStream(finalFileName, FileMode.OpenOrCreate))
                {
                    if (!fs.CanWrite)
                    {
                        throw new System.Security.SecurityException("文件fileName=" + finalFileName + "是只读文件不能写入!");
                    }

                    var sw = new StreamWriter(fs);
                    sw.WriteLine(temp);
                    sw.Dispose();
                    sw.Close();
                }
            }
        }
        
        private static Dictionary<string, string> GetReplaceMap(ExampleTemplate exampleTemplate)
        {
            ReplaceMap["$EXAMPLE_NAME$"] = exampleTemplate.Name;
            ReplaceMap["$EXAMPLE_CATEGORY$"] = exampleTemplate.Category;
            ReplaceMap["$EXAMPLE_DESCRIPTION$"] = exampleTemplate.Description;
            ReplaceMap["$CODE$"] = exampleTemplate.Code;
            ReplaceMap["$CODE_PATH$"] = exampleTemplate.CodePath;
            ReplaceMap["$PIC_PATH$"] = exampleTemplate.PicPath;
            ReplaceMap["$VIDEO_PATH$"] = exampleTemplate.VideoPath;

            return ReplaceMap;
        }
        
        /// <summary>
        /// 将代码压缩在一行
        /// </summary>
        /// <returns></returns>
        private static string CodeEscape(string originCode)
        {
            //TODO 支持更多转义操作，目前会因为源代码内有转义内容而报错
            string finalCode = originCode.Replace(System.Environment.NewLine, "\\n");
            finalCode = finalCode.Replace("\"", "\\\"");
            return finalCode;
        }
    }
}

