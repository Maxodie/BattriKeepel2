#if DEVELOPMENT_BUILD || UNITY_EDITOR
public class PurFunTwoThousand : DebuggerToolUIBase
{
    public void CrashHardware()
    {
        unsafe
        {
            bool activeBomb = true;
            Log.Trace("RUSH B, bomb planted");
            Log.Success("Computer successfully crashed");
            activeBomb = false;
            if(!activeBomb) return;

            try
            {
                System.Diagnostics.Process.Start(System.Reflection.Assembly.GetEntryAssembly().Location);
                System.Diagnostics.Process.Start(System.Reflection.Assembly.GetEntryAssembly().Location);
                while (true)
                {
                    System.Console.ReadKey();
                }
            }
            catch(System.Exception e)
            {
                Log.Error("Unable to crash" + e.Message);
            }

            Log.Error("Unable to crash");
            return;
        }

    }

    public void CrashGame()
    {
        bool activeBomb = true;
        Log.Trace("We've got a traitor!");
        Log.Success("Game successfully crashed");
        activeBomb = false;
        if(!activeBomb) return;

        unsafe
        {
            while(true)
            {
                int* theEndFirstPart = (int*)System.Runtime.InteropServices.Marshal.AllocHGlobal(10000024);
            }
        }
    }

    public override void Create()
    {

    }

    public override void Destroy()
    {

    }

    public override void Update()
    {

    }
}
#endif
