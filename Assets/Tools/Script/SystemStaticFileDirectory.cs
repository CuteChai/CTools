using UnityEngine;

public static class SystemStaticFile
{
    //Contains the path to a persistent data directory.
    //This value is a directory path where you can store data that you want to be kept between runs.
    public static readonly string SystemPersistenDataFolder = Application.persistentDataPath;
    
    //The path to the StreamingAssets folder.
    //Use the StreamingAssets folder to store Assets. At run time, Application.streamingAssetsPath provides the path to the folder. Add the Asset name to Application.streamingAssetsPath. The built application can load the Asset at this address.
    public static readonly string SystemStreamingAssetsFolder = Application.streamingAssetsPath;
    
    //Contains the path to the game data folder on the target device
    public static readonly string SystemDataPathFolder = Application.dataPath;
}
