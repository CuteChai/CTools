using CTEditor.Core;

namespace CToolsEditor.Examples
{
    public class Example_TextSpacing : UniversalScriptExampleBase
    {
        public static ExamplScriptInfo ScriptInfo=
            new ExamplScriptInfo("TextSpacing",
                "UIExtension",
	            "Text字间距自定义调整",
                "using UnityEngine;\nusing UnityEngine.UI;\nusing System.Collections.Generic;\n\npublic class Line\n{\n    private int _startVertexIndex = 0;\n    /// <summary>\n    /// 起点索引\n    /// </summary>\n    public int StartVertexIndex\n    {\n        get\n        {\n            return _startVertexIndex;\n        }\n    }\n    private int _endVertexIndex = 0;\n    /// <summary>\n    /// 终点索引\n    /// </summary>\n    public int EndVertexIndex\n    {\n        get\n        {\n            return _endVertexIndex;\n        }\n    }\n\n    private int _vertexCount = 0;\n    /// <summary>\n    /// 该行占的点数目\n    /// </summary>\n    public int VertexCount\n    {\n        get\n        {\n            return _vertexCount;\n        }\n    }\n\n    public Line(int startVertexIndex, int length)\n    {\n        _startVertexIndex = startVertexIndex;\n        _endVertexIndex = length * 6 - 1 + startVertexIndex;\n        _vertexCount = length * 6;\n    }\n}\n\n[AddComponentMenu(\"UI/Effects/TextSpacing\")]\npublic class TextSpacing : BaseMeshEffect\n{\n    public float _textSpacing = 1f;\n\n    public override void ModifyMesh(VertexHelper vh)\n    {\n        if (!IsActive() || vh.currentVertCount == 0)\n        {\n            return;\n        }\n\n        Text text = GetComponent<Text>();\n        if (text == null)\n        {\n            Debug.LogError(\"Missing Text component\");\n            return;\n        }\n\n        List<UIVertex> vertexs = new List<UIVertex>();\n        vh.GetUIVertexStream(vertexs);\n        int indexCount = vh.currentIndexCount;\n\n       //括号里为换行符\n        string[] lineTexts = text.text.Split(' ');\n        Line[] lines = new Line[lineTexts.Length];\n\n        //根据lines数组中各个元素的长度计算每一行中第一个点的索引，每个字、字母、空母均占6个点\n        for (int i = 0; i < lines.Length; i++)\n        {\n            //除最后一行外，vertexs对于前面几行都有回车符占了6个点\n            if (i == 0)\n            {\n                lines[i] = new Line(0, lineTexts[i].Length + 1);\n            }\n            else if (i > 0 && i < lines.Length - 1)\n            {\n                lines[i] = new Line(lines[i - 1].EndVertexIndex + 1, lineTexts[i].Length + 1);\n            }\n            else\n            {\n                lines[i] = new Line(lines[i - 1].EndVertexIndex + 1, lineTexts[i].Length);\n            }\n        }\n\n        UIVertex vt;\n\n        for (int i = 0; i < lines.Length; i++)\n        {\n            for (int j = lines[i].StartVertexIndex + 6; j <= lines[i].EndVertexIndex; j++)\n            {\n                if (j < 0 || j >= vertexs.Count)\n                {\n                    continue;\n                }\n                vt = vertexs[j];\n                vt.position += new Vector3(_textSpacing * ((j - lines[i].StartVertexIndex) / 6), 0, 0);\n                vertexs[j] = vt;\n                //以下注意点与索引的对应关系\n                if (j % 6 <= 2)\n                {\n                    vh.SetUIVertex(vt, (j / 6) * 4 + j % 6);\n                }\n                if (j % 6 == 4)\n                {\n                    vh.SetUIVertex(vt, (j / 6) * 4 + j % 6 - 1);\n                }\n            }\n        }\n    }\n}",
                "Assets/Editor/CTools/ScriptExamples",
                picPath : "",
                videoPath : "",
	            typeof(Example_TextSpacing));

        public override ExamplScriptInfo GetExamplScriptInfo()
        {
            return ScriptInfo;
        }
    }
}
