using UnityEngine;
using System.IO;

public static class PhotoSaveSystem
{
    public static void SavePhoto(byte[] pixels, string name)
    {
        string folderPath = Application.persistentDataPath + "/SavedPhoto/";
        string path = folderPath + name + ".png";

        if(!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        FileStream fs = new FileStream(path, FileMode.Create);
        fs.Close();
        File.WriteAllBytes(path, pixels);
    }
}
