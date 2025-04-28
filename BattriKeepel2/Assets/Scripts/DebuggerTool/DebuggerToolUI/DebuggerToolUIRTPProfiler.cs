#if DEVELOPMENT_BUILD || UNITY_EDITOR
using Unity.Profiling;

[DebuggerToolAccess]
public class ProfilerStats
{
    [DebuggerToolAccess] public double FrameTimeMs{set; get;}
    [DebuggerToolAccess] public long GCMemoryMB{set; get;}
    [DebuggerToolAccess] public long SysMemoryMB{set; get;}
}

public class DebuggertoolUIRTProfiler : DebuggerToolUIBase
{
    ProfilerRecorder m_systemMemoryRecorder = new();
    ProfilerRecorder m_gcMemoryRecorder = new();
    ProfilerRecorder m_mainThreadTimeRecorder = new();

    ProfilerStats m_stats;

    bool hasGeneratedFields = false;

    public override void OnCreate()
    {
        Log.Info<DebuggerLogger>("RT Profiler created");
        m_stats = (ProfilerStats)script;
        m_systemMemoryRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "System Used Memory");
        m_gcMemoryRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "GC Reserved Memory");
        m_mainThreadTimeRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "Main Thread", 15);
        hasGeneratedFields = true;
    }

    public override void Destroy()
    {
        Log.Info<DebuggerLogger>("RT Profiler destroyed");
        m_systemMemoryRecorder.Dispose();
        m_gcMemoryRecorder.Dispose();
        m_mainThreadTimeRecorder.Dispose();
    }

    public override void Update()
    {
        if(!hasGeneratedFields) return;

        m_stats.FrameTimeMs = GetRecordedFrameAverage(m_mainThreadTimeRecorder) * (1e-6f);
        m_stats.GCMemoryMB = m_gcMemoryRecorder.LastValue / (1024 * 1024);
        m_stats.SysMemoryMB = m_systemMemoryRecorder.LastValue / (1024 * 1024);
    }

    public ProfilerStats GetData()
    {
        return m_stats;
    }

    double GetRecordedFrameAverage(ProfilerRecorder recorder)
    {
        int samplesCount = recorder.Capacity;
        if(samplesCount == 0)
        {
            return 0;
        }

        double r = 0;
        unsafe
        {
            var sample = stackalloc ProfilerRecorderSample[samplesCount];
            recorder.CopyTo(sample, samplesCount);
            for (int i = 0; i < samplesCount; i++)
            {
                r += sample[i].Value;
            }
            r /= samplesCount;
        }

        return r;
    }

}
#endif
