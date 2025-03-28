using Unity.Profiling;

[DebuggerToolAccess]
public class ProfilerStats
{
    [DebuggerToolAccess] public double FrameTime{set; get;}
    [DebuggerToolAccess] public long GCMemoryMB{set; get;}
    [DebuggerToolAccess] public long SysMemoryMB{set; get;}
}

public class RTProfiler : DebuggerToolBase
{
    ProfilerStats m_stats;
    ProfilerRecorder m_systemMemoryRecorder;
    ProfilerRecorder m_gcMemoryRecorder;
    ProfilerRecorder m_mainThreadTimeRecorder;

    public override void Create()
    {
        m_systemMemoryRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "System Used Memory");
        m_gcMemoryRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "GC Reserved Memory");
        m_mainThreadTimeRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "Main Thread", 15);
    }

    public override void Destroy()
    {
        m_systemMemoryRecorder.Dispose();
        m_gcMemoryRecorder.Dispose();
        m_mainThreadTimeRecorder.Dispose();
    }

    public override void Update()
    {
        m_stats.FrameTime = GetRecordedFrameAverage(m_mainThreadTimeRecorder) * (1e-6f);
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
