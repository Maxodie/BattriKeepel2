public class PurFunTwoThousand : DebuggerToolBase
{
    public void CrashHardware()
    {
        unsafe
        {
            bool activeBomb = true;
            Log.Trace("RUSH B, bomb planted");
#if UNITY_EDITOR
            Log.Success("Computer successfully crashed");
            activeBomb = false;
#endif
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
#if UNITY_EDITOR
        Log.Success("Game successfully crashed");
        activeBomb = false;
#endif
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
