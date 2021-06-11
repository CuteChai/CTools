using UnityEngine;
using System.IO;
using UnityEditor;

public static class SystemFileFolderHandle
{
    /// <summary>
    /// 打开目标文件夹
    /// </summary>
    public static void OpenTargetFolder(string targetFolderPath, bool creat=false)
    {
        var TargetFolderExists = DetermineTargetFolderExists(targetFolderPath);
        if (TargetFolderExists)
        {
            EditorUtility.OpenWithDefaultApp(targetFolderPath);
        }
        else if (creat)
        {
            Debug.Log("已创建目标文件夹:"+targetFolderPath);
            Directory.CreateDirectory(targetFolderPath);
        }
    }
    
    
    /// <summary>
    /// 判断时候包含目标文件夹
    /// </summary>
    /// <param name="targetFolderPath"></param>
    /// <returns></returns>
    private static bool DetermineTargetFolderExists(string targetFolderPath)
    {
        if (Directory.Exists(targetFolderPath))
        {
            return true;
        }
        else
        {
            Debug.Log("系统不包含:"+targetFolderPath+"文件夹");
            return false;
        }
    }
}