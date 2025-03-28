# BattriKeepel2
This is the BattriKeepel repo

## doc
### Logger
There are 5 logging levels : `Info` `Trace` `Success` `Warn` `Error`
```c#
//use default Logger : Log.<LogLevel>(msg);

Log.Info("Hello");
Log.Trace("Hello");
Log.Success("Hello");
Log.Warn("Hello");
Log.Error("Hello");

//create Logger : create a logger class and use it as template

public class CustomLogger : Logger
{
    //Creation callback function (falcultatif)
    public override void OnCreated()
    {
    }

    //Log start callback function (falcultatif)
    public override void OnLogStart()
    {
    }
}

//use custom logger

Log.Info<CustomLogger>("Hello");
Log.Trace<CustomLogger>("Hello");
Log.Success<CustomLogger>("Hello");
Log.Warn<CustomLogger>("Hello");
Log.Error<CustomLogger>("Hello");
```

### Save System
You can save and load dynamic data
```c#
//Data must inherite from DynamicScriptableObject

public class MyData : DynamicScriptableObject
{
    public int test;
}

//Save data exemple with a simple container but that works with every c# object

public class DataContainerExemple
{
    MyData dataToSave;

    DataContainerExemple()
    {
        dataToSave.test = 5;
        int myDataId = 0;
        SaveSystem.Save<Mydata>(dataToSave, this, myDataId);
    }
}



//Load data with the same container exemple

public class DataContainerExemple
{
    MyData dataToSave;

    DataContainerExemple()
    {
        int myDataId = 0;
        List<MyData> loadedData = SaveSystem.Load<MyData>(this);
        dataToSave.test = loadedDate[myDataId].test;
    }
}
```
