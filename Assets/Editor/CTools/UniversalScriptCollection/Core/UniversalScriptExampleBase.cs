using UnityEngine;
using UnityEngine.Video;

namespace CTEditor.Core
{
    [OriginTrackerAttribute]
    public abstract class UniversalScriptExampleBase
    {
        private static ExamplScriptInfo examplScriptInfo = new ExamplScriptInfo();

        public virtual ExamplScriptInfo GetExamplScriptInfo()
        {
            return examplScriptInfo;
        }
        
        private VideoClip videoSource; //当前案例的视频演示
        private Texture2D pictureSource; //当前案例的图片样式
        private Texture playIconSource;  //播放按钮图标
        
        public virtual void Init()
        {
                    
        }
    }
}
