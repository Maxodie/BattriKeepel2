using UnityEngine;

namespace Components {
    public class MockBox : Hitbox {
        public MockBox(Hitbox obj, Vector2 position) {
            m_position = position;
            m_type = obj.m_type;
            m_size = obj.GetDiameter();
            m_dimensions = obj.GetDimensions();
            m_transform = obj.GetTransform();
            wishVelocity = obj.wishVelocity;
            outputVelocity = obj.outputVelocity;
        }

        public override Vector2 GetPosition() {
            return m_position;
        }
    }
}
