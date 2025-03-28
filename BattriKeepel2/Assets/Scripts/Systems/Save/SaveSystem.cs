using System.IO;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class SaveData<T>
{
    public SerializableDictionary<string, SerializableDictionary<int, T>> loadedDynamicScriptableObject = new();
}

[System.Serializable]
public class SerializableDictionaryElement<TKey, TValue>
{
    public TKey m_key;
    public TValue m_value;

    public SerializableDictionaryElement(TKey key, TValue value)
    {
        m_key = key;
        m_value = value;
    }
}

[System.Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    [SerializeField]
    public List<SerializableDictionaryElement<TKey, TValue>> elements = new();

    public void OnAfterDeserialize()
    {
        Clear();

        foreach(var element in elements)
        {
            this[element.m_key] = element.m_value;
        }
    }

    public void OnBeforeSerialize()
    {
        elements.Clear();

        foreach(var pair in this)
        {
            elements.Add(new SerializableDictionaryElement<TKey, TValue>(pair.Key, pair.Value));
        }
    }
}

public static class SaveSystem
{
    static string saveFolderPath = Application.persistentDataPath + "/Save";

    public static void Save<T>(DynamicScriptableObject dynamicData, object objectId, int id) where T : DynamicScriptableObject
    {
        string savePath = saveFolderPath + $"/{typeof(T)}.qt";

        if(!Directory.Exists(saveFolderPath))
        {
            Directory.CreateDirectory(saveFolderPath);
        }

        SaveData<T> resolvedData;
        string stringData = File.ReadAllText(savePath);
        resolvedData = JsonUtility.FromJson(stringData, typeof(SaveData<T>)) as SaveData<T>;

        if(resolvedData == null)
        {
            resolvedData = new();
        }

        if(!resolvedData.loadedDynamicScriptableObject.ContainsKey(objectId.GetType().FullName))
        {
            resolvedData.loadedDynamicScriptableObject.Add(objectId.GetType().FullName, new SerializableDictionary<int, T>());
        }

        resolvedData.loadedDynamicScriptableObject[objectId.GetType().FullName].Add(id, (T)dynamicData);

        string data = JsonUtility.ToJson(resolvedData);
        FileStream file = new FileStream(savePath, FileMode.Create);
        file.Close();
        File.WriteAllText(savePath, data);
    }

    public static List<T> Load<T>(IGameEntity objectId) where T : DynamicScriptableObject
    {
        string savePath = saveFolderPath + $"/{typeof(T)}.qt";

        if(!Directory.Exists(saveFolderPath))
        {
            Directory.CreateDirectory(saveFolderPath);
        }

        if(!File.Exists(savePath))
        {
            FileStream file = new FileStream(savePath, FileMode.Create);
            file.Close();
            return null;
        }

        string data = File.ReadAllText(savePath);

        SaveData<T> resolvedData;
        try
        {
            resolvedData = JsonUtility.FromJson(data, typeof(SaveData<T>)) as SaveData<T>;
            List<T> result = new();
            foreach(T item in resolvedData.loadedDynamicScriptableObject[objectId.GetType().FullName].Values)
            {
                result.Add(item);
            }
            return result;
        }
        catch
        {
            resolvedData = null;
        }

        return null;
    }
}
