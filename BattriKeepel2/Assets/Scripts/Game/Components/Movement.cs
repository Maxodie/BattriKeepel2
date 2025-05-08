using UnityEngine;

namespace Components {
    public abstract class Movement {
        public abstract Vector2 WishMovement();
        public abstract void ApplyMovement(Vector2 velocity);
    }
}
