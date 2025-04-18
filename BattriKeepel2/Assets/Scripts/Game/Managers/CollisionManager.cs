using UnityEngine;
using Components;
using System.Collections.Generic;

public class CollisionManager : MonoBehaviour {
    static CollisionManager s_Instance;
    private List<Hitbox> m_elements = new List<Hitbox>();
    [SerializeField] private SpatialHash m_spatialHash;
    private List<Vector2> m_gridWorldPositions;

    private void Awake() {
        if (s_Instance != null && s_Instance != this) {
            Destroy(this.gameObject);
        } else {
            s_Instance = this;
        }
    }

    public static CollisionManager GetInstance() {
        return s_Instance;
    }

    public void AddElement(Hitbox element) {
        if (m_elements.Contains(element)) {
            return;
        }
        m_elements.Add(element);
    }

    public void RemoveElement(Hitbox element) {
        if (!m_elements.Contains(element)) {
            return;
        }

        m_elements.Remove(element);
    }

    private void FixedUpdate() {
        PopulateHash();
        DetectCollisions();
    }

    private void DetectCollisions() {
        foreach (Vector2 position in m_gridWorldPositions) {
            List<Hitbox> hitboxes = m_spatialHash.QuerySpace(position);

            for (int i = 0; i < hitboxes.Count; i++) {
                Hitbox c_hitbox = hitboxes[i];

                for (int j = i + 1; j < hitboxes.Count; j++) {
                    Hitbox o_hitbox = hitboxes[j];

                    bool hasCollided = false;

                    if (c_hitbox.m_type == HitboxType.Circle && o_hitbox.m_type == HitboxType.Circle) {
                        hasCollided = IsCircleCollision(c_hitbox, o_hitbox);
                    }
                    else if (c_hitbox.m_type == HitboxType.RectangularParallelepiped && o_hitbox.m_type == HitboxType.RectangularParallelepiped) {
                        hasCollided = IsRectangleCollision(c_hitbox, o_hitbox);
                    }
                    else if (c_hitbox.m_type == HitboxType.Circle && o_hitbox.m_type == HitboxType.RectangularParallelepiped) {
                        hasCollided = IsCircleRectangleCollision(c_hitbox, o_hitbox);
                    }
                    else if (c_hitbox.m_type == HitboxType.RectangularParallelepiped && o_hitbox.m_type == HitboxType.Circle) {
                        hasCollided = IsCircleRectangleCollision(o_hitbox, c_hitbox);
                    }

                    if (hasCollided) {
                        c_hitbox.OnCollisionBehavior(o_hitbox.GetTransform());
                        o_hitbox.OnCollisionBehavior(c_hitbox.GetTransform());
                    }
                }
            }
        }
    }


    private bool IsCircleRectangleCollision(Hitbox circle, Hitbox rectangularParallelepiped) {
        Vector2 circlePosition = circle.GetPosition();
        float circleRadius = circle.GetSize() / 2;

        Vector2 rectPosition = rectangularParallelepiped.GetPosition();
        Vector2 rectDimensions = rectangularParallelepiped.GetDimensions();

        Vector2 rectMinBound = new Vector2(rectPosition.x - (rectDimensions.x / 2), rectPosition.y - (rectDimensions.y / 2));
        Vector2 rectMaxBound = new Vector2(rectPosition.x + (rectDimensions.x / 2), rectPosition.y + (rectDimensions.y / 2));

        float closestX = Mathf.Clamp(circlePosition.x, rectMinBound.x, rectMaxBound.x);
        float closestY = Mathf.Clamp(circlePosition.y, rectMinBound.y, rectMaxBound.y);

        Vector2 closestPoint = new Vector2(closestX, closestY);

        float distance = Vector2.Distance(circlePosition, closestPoint);

        return distance <= circleRadius;
    }

    private bool IsRectangleCollision(Hitbox r1, Hitbox r2) {
        Vector2 r1Position = r1.GetPosition();
        Vector2 r1MinBound = new Vector2(r1Position.x - (r1.GetDimensions().x / 2), r1Position.y - (r1.GetDimensions().y / 2));
        Vector2 r1MaxBound = new Vector2(r1Position.x + (r1.GetDimensions().x / 2), r1Position.y + (r1.GetDimensions().y / 2));

        Vector2 r2Position = r2.GetPosition();
        Vector2 r2MinBound = new Vector2(r2Position.x - (r2.GetDimensions().x / 2), r2Position.y - (r2.GetDimensions().y / 2));
        Vector2 r2MaxBound = new Vector2(r2Position.x + (r2.GetDimensions().x / 2), r2Position.y + (r2.GetDimensions().y / 2));

        if (r1MaxBound.x < r2MinBound.x || r1MinBound.x > r2MaxBound.x) {
            return false;
        }

        if (r1MaxBound.y < r2MinBound.y || r1MinBound.y > r2MaxBound.y) {
            return false;
        }

        return true;
    }

    private bool IsCircleCollision(Hitbox c1, Hitbox c2) {
        float distance = Vector2.Distance(c1.GetPosition(), c2.GetPosition());
        float combinedSize = (c1.GetSize() + c2.GetSize()) / 2;

        if (distance <= combinedSize) {
            return true;
        }

        return false;
    }


    private void PopulateHash() {
        m_gridWorldPositions = new List<Vector2>();
        m_spatialHash.Wipe();
        foreach (Hitbox hitbox in m_elements) {
            Vector2 position = hitbox.GetPosition();
            m_spatialHash.Insert(hitbox);
            if (!m_gridWorldPositions.Contains(position)) {
                m_gridWorldPositions.Add(position);
            }
        }
    }
}

[System.Serializable]
public class SpatialHash {
    private Dictionary<Vector2Int, List<Hitbox>> m_grid = new Dictionary<Vector2Int, List<Hitbox>>();
    [SerializeField] private float m_cellSize;

    private Vector2Int GetCell(Vector2 position) {
        return new Vector2Int(Mathf.FloorToInt(position.x / m_cellSize), Mathf.FloorToInt(position.y / m_cellSize));
    }

    public void Insert(Hitbox hitbox) {
        if (hitbox.m_type == HitboxType.Circle) {
            InsertCircle(hitbox);
            return;
        }

        InsertRectangle(hitbox);
    }

    private void InsertRectangle(Hitbox hitbox) {
        Vector2 hitboxPosition = hitbox.GetPosition();

        Vector2 minBound = new Vector2(hitboxPosition.x - (hitbox.GetDimensions().x / 2), hitboxPosition.y - (hitbox.GetDimensions().y / 2));
        Vector2 maxBound = new Vector2(hitboxPosition.x + (hitbox.GetDimensions().x / 2), hitboxPosition.y + (hitbox.GetDimensions().y / 2));

        for (float x = minBound.x; x < maxBound.x + m_cellSize; x += m_cellSize) {
            for (float y = minBound.y; y < maxBound.y + m_cellSize; y += m_cellSize) {
                Vector2Int cell = GetCell(new Vector2(x, y));
                TryInsertCell(cell, hitbox);
            }
        }
    }

    private void InsertCircle(Hitbox hitbox) {
        Vector2 hitboxPosition = hitbox.GetPosition();
        float hitboxSize = hitbox.GetSize();

        Vector2Int centerCell = GetCell(hitboxPosition);

        HashSet<Vector2Int> visitedCells = new HashSet<Vector2Int>();
        Queue<Vector2Int> queue = new Queue<Vector2Int>();
        queue.Enqueue(centerCell);

        while (queue.Count > 0) {
            Vector2Int cell = queue.Dequeue();

            if (visitedCells.Contains(cell)) {
                continue;
            }

            visitedCells.Add(cell);

            Vector2 cellCenter = (Vector2)cell * m_cellSize + Vector2.one * (m_cellSize / 2);
            Vector2 direction = cellCenter - hitboxPosition;

            Vector2 edgeCell = GetCell(hitboxPosition + direction.normalized * hitboxSize);

            if (edgeCell == cell || centerCell == cell) {
                TryInsertCell(cell, hitbox);


                queue.Enqueue(new Vector2Int(cell.x + 1, cell.y));
                queue.Enqueue(new Vector2Int(cell.x - 1, cell.y));
                queue.Enqueue(new Vector2Int(cell.x, cell.y + 1));
                queue.Enqueue(new Vector2Int(cell.x, cell.y - 1));
            }
        }
    }

    private void TryInsertCell(Vector2Int cell, Hitbox hitbox) {
        if (!m_grid.ContainsKey(cell)) {
            m_grid[cell] = new List<Hitbox>();
        }
        m_grid[cell].Add(hitbox);
    }

    public List<Hitbox> QuerySpace(Vector2 worldPosition) {
        Vector2Int cell = GetCell(worldPosition);
        return QueryCell(cell);
    }

    public List<Hitbox> QueryCell(Vector2Int cell) {
        if (m_grid.ContainsKey(cell)) {
            return m_grid[cell];
        }
        return new List<Hitbox>();
    }

    public void Wipe() {
        m_grid.Clear();
    }
}
