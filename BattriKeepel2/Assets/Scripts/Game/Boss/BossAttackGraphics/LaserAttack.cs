using System.Collections.Generic;
using GameEntity;
using UnityEngine;

public class LaserAttack : BossAttackParent {
    [SerializeField] float m_timeToAppear;
    [SerializeField] float m_timeLasting;
    [SerializeField] LaserGraphics m_laserGraphics;
    [SerializeField] int m_spacePartitions;
    [SerializeField] int m_position; // from zero to m_spacePartitions - 1
    [SerializeField] bool m_followPlayer;

    float appearTime;
    float lastingTime;
    float laserScale;
    float followTime;
    List<Vector2> positions = new List<Vector2>();

    private void Start() {
        PartitionSpace();
        m_laserGraphics = GraphicsManager.Get().GenerateVisualInfos<LaserGraphics>
            (m_laserGraphics, positions[m_position], Quaternion.identity, this, false);
        Vector3 localScale = m_laserGraphics.transform.localScale;
        m_laserGraphics.transform.localScale = new Vector3(laserScale, localScale.y , localScale.z);
        appearTime = m_timeToAppear;
        lastingTime = m_timeLasting;

        if (m_followPlayer) {
            followTime = 0.75f * appearTime;
        }
    }

    private void Update() {
        if (m_followPlayer && followTime > 0) {
            HandleFollowPlayer();
        }
        if (appearTime > 0) {
            HandleAppearTime();
        } else if (lastingTime > 0) {
            HandleLastingTime();
        } else if (0 >= lastingTime) {
            Destroy(m_laserGraphics.gameObject);
        }
    }

    private void HandleFollowPlayer() {
        followTime -= Time.deltaTime;

        Vector2 playerPosition = Player.GetInstance().position;
        Vector3 currentPosition = m_laserGraphics.transform.position;
        float angle = Mathf.Atan2(playerPosition.y - currentPosition.y, playerPosition.x - currentPosition.x);
        angle = angle * Mathf.Rad2Deg;

        Log.Info(angle);
        Vector3 currentRotation = m_laserGraphics.transform.eulerAngles;
        m_laserGraphics.transform.eulerAngles = new Vector3(currentRotation.x, currentRotation.y, angle + 90);
    }

    private void HandleAppearTime() {
        appearTime -= Time.deltaTime;
        m_laserGraphics.SetFillInSize(LerpTime(appearTime, m_timeToAppear));
    }

    private void HandleLastingTime() {
        m_laserGraphics.TriggerLaser();
        lastingTime -= Time.deltaTime;
    }

    private void PartitionSpace() {
        Vector3 maxBounds = GraphicsManager.Get().BoundsMax(Camera.main);

        float xLengths = maxBounds.x * 2;
        float step = xLengths / m_spacePartitions;
        laserScale = step;

        Vector2 origin = new Vector2(maxBounds.x - xLengths + step * 0.5f, maxBounds.y);
        positions.Add(origin);
        for (int i = 1; i < m_spacePartitions; i++) {
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
