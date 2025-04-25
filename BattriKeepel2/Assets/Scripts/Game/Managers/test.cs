using UnityEngine;

public class test : MonoBehaviour {
    [SerializeField] private Components.Hitbox m_hitbox;

    private void Start() {
        m_hitbox.Init(transform);
    }
}
