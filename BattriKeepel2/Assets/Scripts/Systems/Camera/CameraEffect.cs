using UnityEngine;

public class CameraEffect
{
    Transform m_cameraTr;
    Vector3 m_cameraDefaultPos;
    float m_shakeSpeed;
    float m_shakeAmount;

    Awaitable m_shakeAwait;

    public CameraEffect(Transform cameraTransform, float shakeSpeed, float shakeAmount)
    {
        m_shakeSpeed = shakeSpeed;
        m_shakeAmount = shakeAmount;
        m_cameraTr = cameraTransform;
        m_cameraDefaultPos = m_cameraTr.localPosition;
    }

    public void StartShake(float duration)
    {
        if(m_shakeAwait != null && !m_shakeAwait.IsCompleted)
        {
            m_shakeAwait.Cancel();
            OnShakeFinished();
        }

        m_shakeAwait = Shake(duration);
    }

    void OnShakeFinished()
    {
        m_cameraTr.localPosition = m_cameraDefaultPos;
    }

    async Awaitable Shake(float duration)
    {
        float timer = duration;
        Vector3 shakePos;
        while(timer > 0.0f)
        {
            shakePos = new Vector3(Mathf.PerlinNoise(Time.time * m_shakeSpeed, 0), Mathf.PerlinNoise(10, Time.time * m_shakeSpeed)) * m_shakeAmount;
            m_cameraTr.localPosition = m_cameraDefaultPos + shakePos;

            timer -= Time.deltaTime;
            await Awaitable.NextFrameAsync();
        }
        OnShakeFinished();
    }
}
