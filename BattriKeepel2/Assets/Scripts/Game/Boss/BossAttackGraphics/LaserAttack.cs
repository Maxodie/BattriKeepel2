using System.Collections.Generic;
using UnityEngine;

public class LaserAttack : BossAttackParent {
    [SerializeField] SO_LaserAttackData data;
    LaserGraphics visual;

    float appearTime;
    float lastingTime;
    float laserScale;
    float followTime;
    List<Vector2> positions = new List<Vector2>();

    GameEntity.Player m_player;

    public override void Init(SO_BossAttackData data, AttackGraphicsPool attackPool, GameEntity.Player player) {
        this.data = (SO_LaserAttackData)data;
        appearTime = this.data.m_timeToAppear;
        lastingTime = this.data.m_timeLasting;
        laserScale = this.data.m_laserScale;
        followTime = this.data.m_followTime;

        m_player = player;
        PartitionSpace();
        visual = GraphicsManager.Get().GenerateVisualInfos<LaserGraphics>
            (this.data.m_laserGraphics, positions[this.data.m_position], Quaternion.identity, this, false);
        Vector3 localScale = visual.transform.lossyScale;
        visual.transform.localScale = new Vector3(laserScale, localScale.y , localScale.z);

        if (this.data.m_followPlayer) {
            followTime = 0.75f * appearTime;
        }
    }

    public override void Update() {
        if (data.m_followPlayer && followTime > 0) {
            HandleFollowPlayer();
        }
        if (appearTime > 0) {
            HandleAppearTime();
        } else if (lastingTime > 0) {
            HandleLastingTime();
        } else if (lastingTime <= 0) {
            Clean();
        }
    }

    private void HandleFollowPlayer() {
        followTime -= Time.deltaTime;

        Vector2 playerPosition = m_player.position;
        Vector3 currentPosition = visual.transform.position;
        float angle = Mathf.Atan2(playerPosition.y - currentPosition.y, playerPosition.x - currentPosition.x);
        angle = angle * Mathf.Rad2Deg;

        Vector3 currentRotation = visual.transform.eulerAngles;
        visual.transform.eulerAngles = new Vector3(currentRotation.x, currentRotation.y, angle + 90);
    }

    private void HandleAppearTime() {
        appearTime -= Time.deltaTime;
        visual.SetFillInSize(LerpTime(appearTime, data.m_timeToAppear));
    }

    private void HandleLastingTime() {
        visual.TriggerLaser();
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

    public override void Clean()
    {
        if(visual)
        {
            Object.Destroy(visual.gameObject);
            visual = null;
        }
    }
}
