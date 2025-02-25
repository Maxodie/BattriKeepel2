using UnityEngine;

public class CollisionManager : MonoBehaviour {
    static CollisionManager s_Instance;

    public CollisionManager GetInstance() {
        if (s_Instance == null) {
            s_Instance = this;
        }

        return s_Instance;
    }
}
