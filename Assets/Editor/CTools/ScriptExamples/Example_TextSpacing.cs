using System.Collections;
using System.Collections.Generic;
using CTEditor.Core;
using UnityEngine;

namespace CTEditor.Example
{
    public class Example_TextSpacing : UniversalScriptExampleBase
    {
        public static ExamplScriptInfo ScriptInfo =
            new ExamplScriptInfo("TextSpacing",
                "UI",
                "调整Text字体间距。",
                "",
                "Assets/Editor/CTools/UniversalScriptCollection/ScritpExample/Example_TextSpacing",
                picPath:"",
                videoPath:"",
                typeof(Example_TextSpacing));

        public override ExamplScriptInfo GetExamplScriptInfo()
        {
            return ScriptInfo;
        }
    }

}
