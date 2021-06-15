using UnityEngine;
using System.IO;
using UnityEditor;

public static class SystemFileFolderHandle
{
    /// <summary>
    /// 打开目标文件夹
    /// </summary>
    public static void OpenTargetFolder(string targetFolderPath, bool creat = false)
    {
        DetermineTargetFolderExists(targetFolderPath,creat);

        EditorUtility.OpenWithDefaultApp(targetFolderPath);
    }


    /// <summary>
    /// 判断时候包含目标文件夹
    /// </summary>
    /// <param name="targetFolderPath"></param>
    /// <returns></returns>
    public static void DetermineTargetFolderExists(string targetFolderPath, bool creat = false)
    {
        if (!Directory.Exists(targetFolderPath))
        {
            Debug.Log("系统不包含:" + targetFolderPath + "文件夹");
            if (!creat) return;
            Directory.CreateDirectory(targetFolderPath);
            Debug.Log("已创建目标文件夹:" + targetFolderPath);
        }
    }
}