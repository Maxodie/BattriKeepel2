using System.Collections.Generic;
using GameEntity;
using UnityEngine;

public class LaserAttack : BossAttackParent {
    [SerializeField] SO_LaserAttackData data;

    float appearTime;
    float lastingTime;
    float laserScale;
    float followTime;
    List<Vector2> positions = new List<Vector2>();

    private void Start() {
        PartitionSpace();
        data.m_laserGraphics = GraphicsManager.Get().GenerateVisualInfos<LaserGraphics>
            (data.m_laserGraphics, positions[data.m_position], Quaternion.identity, this, false);
        Vector3 localScale = data.m_laserGraphics.transform.localScale;
        data.m_laserGraphics.transform.localScale = new Vector3(laserScale, localScale.y , localScale.z);
        appearTime = data.m_timeToAppear;
        lastingTime = data.m_timeLasting;

        if (data.m_followPlayer) {
            followTime = 0.75f * appearTime;
        }
    }

    private void Update() {
        if (data.m_followPlayer && followTime > 0) {
            HandleFollowPlayer();
        }
        if (appearTime > 0) {
            HandleAppearTime();
        } else if (lastingTime > 0) {
            HandleLastingTime();
        } else if (0 >= lastingTime) {
            Destroy(data.m_laserGraphics.gameObject);
        }
    }

    private void HandleFollowPlayer() {
        followTime -= Time.deltaTime;

        Vector2 playerPosition = Player.GetInstance().position;
        Vector3 currentPosition = data.m_laserGraphics.transform.position;
        float angle = Mathf.Atan2(playerPosition.y - currentPosition.y, playerPosition.x - currentPosition.x);
        angle = angle * Mathf.Rad2Deg;

        Log.Info(angle);
        Vector3 currentRotation = data.m_laserGraphics.transform.eulerAngles;
        data.m_laserGraphics.transform.eulerAngles = new Vector3(currentRotation.x, currentRotation.y, angle + 90);
    }

    private void HandleAppearTime() {
        appearTime -= Time.deltaTime;
        data.m_laserGraphics.SetFillInSize(LerpTime(appearTime, data.m_timeToAppear));
    }

    private void HandleLastingTime() {
        data.m_laserGraphics.TriggerLaser();
        lastingTime -= Time.deltaTime;
    }

    private void PartitionSpace() {
        Vector3 maxBounds = GraphicsManager.Get().BoundsMax(Camera.main);

        float xLengths = maxBounds.x * 2;
        float step = xLengths / data.m_spacePartitions;
        laserScale = step;

        Vector2 origin = new Vector2(maxBounds.x - xLengths + step * 0.5f, maxBounds.y);
        positions.Add(origin);
        for (int i = 1; i < data.m_spacePartitions; i++) {
            positions.Add(new Vector2(origin.x + step * i, origin.y));
        }
    }

    private float LerpTime(float currentTime, float maxTime) {
        if (0 > currentTime) {
            return 1;
        }
        return 1 - currentTime / maxTime;
    }
}
