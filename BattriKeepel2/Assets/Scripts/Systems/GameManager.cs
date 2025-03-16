using System.Collections.Generic;

public class GameManagerLogger : Logger
{
}

[System.Serializable]
public class GameManagerData : DynamicScriptableObject
{
    public int test;
    public string bonjour = "10454";
    public float nombre = 3.4f;

    public override string ToString()
    {
        return test + " | " + bonjour + " | " + nombre;
    }
}

public class GameManager : GameEntityMonoBehaviour
{
    GameManagerData data = new();
    void Awake() {
        List<GameManagerData> savedData;
        savedData = SaveSystem.Load<GameManagerData>(this);
        if(savedData != null)
        {
            Log.Info<GameManagerLogger>(savedData[0]);
            Log.Info<GameManagerLogger>(savedData[1]);
            /*Log.Info<GameManagerLogger>(savedData[1].bonjour);*/
            /*Log.Info<GameManagerLogger>(savedData[1].nombre);*/
        }
        else
        {
            data.test = 101;
            data.bonjour = "dddd";
            data.nombre = 11111.25f;
            SaveSystem.Save<GameManagerData>(data, this, 0);
            data.test = 120;
            data.bonjour = "ddhhdd";
            data.nombre = 11131.25f;
            SaveSystem.Save<GameManagerData>(data, this, 1);
            /*Log.Info<GameManagerLogger>("Save");*/
        }
    }
}
