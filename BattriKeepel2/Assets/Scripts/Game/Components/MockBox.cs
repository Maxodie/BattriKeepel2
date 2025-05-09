using UnityEngine;

namespace Components {
    public class MockBox : Hitbox {
        public MockBox(Hitbox obj, Vector2 position) {
            m_position = obj.GetPosition() + position;
            m_type = obj.m_type;
            m_size = obj.GetDiameter();
            m_dimensions = obj.GetDimensions();
        }

        public override Vector2 GetPosition() {
            return m_position;
        }

        public override Vector2 GetDimensions()
        {
            return m_dimensions * m_transform.localScale;
        }
    }
}
