using Components;
using Game.Entities;
using Inputs;

namespace Game.Player
{
    public class Player : Entity {
        [SerializeField] private InputManager m_inputManager;
        [SerializeField] private PlayerMovement m_movement;
        [SerializeField] private Hitbox m_hitBox;
        private Transform transform;

        private Vector2 m_currentTarget = new Vector2();
        private void Start() {
            Init();
            BindActions();
        }

        private void Init() {
            transform = GetEntityGraphics().transform;
            m_hitBox.Init(transform);
            m_movement.m_transform = transform;
        }

        private void BindActions() {
            m_inputManager.BindPosition(m_movement.OnPosition);
            m_inputManager.BindPress(m_movement.OnPress);
            m_hitBox.BindOnCollision(HandleCollisions);
        }

        private void Update() {
            m_movement.HandleMovement(m_currentTarget);
            m_currentTarget = m_movement.targetPosition;
        }

        public bool IsScreenPressed() {
            return m_movement.IsScreenPressed();
        }

        //TODO: faire le truc pour update dans le bon ordre parce que sinon casse les couilles en sah

        private void HandleCollisions(Transform other) {
            if (other.gameObject.GetComponent<Wall>()) {
                Hitbox hit = other.gameObject.GetComponent<Wall>().m_hitBox;
                Vector2 hitPosition = hit.GetPosition();
                Vector2 playerPosition = this.transform.position;

                if (Mathf.Abs(hitPosition.x - playerPosition.x) < Mathf.Abs(hitPosition.y - playerPosition.y)) {
                    m_currentTarget.y = playerPosition.y;
                } else {
                    m_currentTarget.x = playerPosition.x;
                }
            }
        }
    }
}
