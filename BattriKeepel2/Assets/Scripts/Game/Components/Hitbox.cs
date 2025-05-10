using System;
using UnityEngine;
using UnityEngine.Events;

namespace Components
{
    [System.Serializable]
    public class Hitbox
    {
        protected UnityEvent<Hit> m_onCollision = new UnityEvent<Hit>();
        protected Transform m_transform;
        [SerializeField] public bool hardCollsion = false;
        public HitboxType m_type;

        [SerializeField] protected float m_size;
        [SerializeField] protected Vector2 m_dimensions;

        [SerializeField] protected Vector2 m_offSet = new Vector2();
        protected Vector2 m_position;

        [HideInInspector] public Hit lastHitObject;

        [HideInInspector] public Vector2 wishVelocity;
        [HideInInspector] public Vector2 outputVelocity;

        public void Init(Transform transformHitbox)
        {
            CollisionManager.GetInstance().AddElement(this);
            m_transform = transformHitbox;
        }

        public Vector2 GetClosestPoint(Hitbox other)
        {
            if (GetHitboxType() == HitboxType.Circle)
            {
                if (other.GetHitboxType() == HitboxType.Circle)
                {
                    Vector2 direction = (GetPosition() - other.GetPosition()).normalized;
                    return other.GetPosition() + direction * other.GetDiameter() / 2;
                }
                else
                {
                    Vector2 circlePosition = GetPosition();
                    float circleRadius = GetDiameter() / 2;

                    Vector2 rectPosition = other.GetPosition();
                    Vector2 rectDimensions = other.GetDimensions();

                    Vector2 rectMinBound = new Vector2(rectPosition.x - (rectDimensions.x / 2), rectPosition.y - (rectDimensions.y / 2));
                    Vector2 rectMaxBound = new Vector2(rectPosition.x + (rectDimensions.x / 2), rectPosition.y + (rectDimensions.y / 2));

                    float closestX = Mathf.Clamp(circlePosition.x, rectMinBound.x, rectMaxBound.x);
                    float closestY = Mathf.Clamp(circlePosition.y, rectMinBound.y, rectMaxBound.y);

                    return new Vector2(closestX, closestY);
                }
            }
            else
            {
                if (other.GetHitboxType() == HitboxType.Circle)
                {
                    Vector2 circlePosition = other.GetPosition();
                    float circleRadius = other.GetDiameter() / 2;

                    Vector2 rectPosition = GetPosition();
                    Vector2 rectDimensions = GetDimensions();

                    Vector2 rectMinBound = new Vector2(rectPosition.x - (rectDimensions.x / 2), rectPosition.y - (rectDimensions.y / 2));
                    Vector2 rectMaxBound = new Vector2(rectPosition.x + (rectDimensions.x / 2), rectPosition.y + (rectDimensions.y / 2));

                    float closestX = Mathf.Clamp(circlePosition.x, rectMinBound.x, rectMaxBound.x);
                    float closestY = Mathf.Clamp(circlePosition.y, rectMinBound.y, rectMaxBound.y);

                    Vector2 closestRectPoint = new Vector2(closestX, closestY);
                    Vector2 direction = closestRectPoint - circlePosition;
                    Vector2 dir = direction.normalized;

                    return circlePosition + dir * circleRadius;

                }
                else
                {
                    Vector2 aPosition = GetPosition();
                    Vector2 aMinBound = new Vector2(aPosition.x - (GetDimensions().x / 2), aPosition.y - (GetDimensions().y / 2));
                    Vector2 aMaxBound = new Vector2(aPosition.x + (GetDimensions().x / 2), aPosition.y + (GetDimensions().y / 2));

                    Vector2 bPosition = other.GetPosition();
                    Vector2 bMinBound = new Vector2(bPosition.x - (other.GetDimensions().x / 2), bPosition.y - (other.GetDimensions().y / 2));
                    Vector2 bMaxBound = new Vector2(bPosition.x + (other.GetDimensions().x / 2), bPosition.y + (other.GetDimensions().y / 2));

                    return new Vector2(
                            Math.Max(aMinBound.x, bMinBound.x),
                            Math.Max(aMinBound.y, bMinBound.y)
                            );
                }
            }
        }

        public virtual Vector2 GetPosition()
        {
            return new Vector2(m_transform.position.x, m_transform.position.y) + m_offSet;
        }

        public virtual float GetDiameter()
        {
            return m_size * m_transform.localScale.x;
        }

        public HitboxType GetHitboxType()
        {
            return m_type;
        }

        public Vector2 GetDimensions()
        {
            return m_dimensions * m_transform.localScale;
        }

        public void OnCollisionBehavior(Hit hit)
        {
            m_onCollision.Invoke(hit);
        }

        public void BindOnCollision(UnityAction<Hit> action)
        {
            m_onCollision.AddListener(action);
        }

        public Transform GetTransform()
        {
            return m_transform;
        }
    }

    public enum HitboxType
    {
        Circle,
        RectangularParallelepiped
    }
}
