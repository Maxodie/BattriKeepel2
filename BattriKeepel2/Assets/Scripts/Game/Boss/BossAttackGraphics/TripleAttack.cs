using UnityEngine;

public class TripleAttack : BossAttackParent {
    [SerializeField] SO_TripleAttackData data;

    GameObject origin;
    Vector2 bossPosition;

    public void Init(Vector3 bossPosition, GameEntity.Player player) {
        origin = new GameObject();
        origin.transform.position = bossPosition;
        this.bossPosition = bossPosition;

        float angle = Mathf.Atan2
            (player.position.y - bossPosition.y, player.position.x - bossPosition.x);

        angle = angle * Mathf.Rad2Deg;
    }

    public void Update() {

    }
}
