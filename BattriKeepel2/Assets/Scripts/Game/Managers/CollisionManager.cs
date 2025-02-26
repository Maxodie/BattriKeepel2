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

                    float distance = Vector2.Distance(c_hitbox.GetPosition(), o_hitbox.GetPosition());
                    float combinedSize = (c_hitbox.GetSize() + o_hitbox.GetSize()) / 2;

                    if (distance <= combinedSize) {
                        c_hitbox.OnCollisionBehavior(o_hitbox.m_transform);
                        o_hitbox.OnCollisionBehavior(c_hitbox.m_transform);
                    }
                }
            }
        }
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
            // get cellCenter to world pos, get direction from hitbox origin, if radius in direction is in cell, then add, if not then remove;
            if (Vector2.Distance(cellCenter, hitboxPosition) <= hitboxSize || centerCell == cell) {
                if (!m_grid.ContainsKey(cell)) {
                    m_grid[cell] = new List<Hitbox>();
                }
                m_grid[cell].Add(hitbox);

                queue.Enqueue(new Vector2Int(cell.x + 1, cell.y));
                queue.Enqueue(new Vector2Int(cell.x - 1, cell.y));
                queue.Enqueue(new Vector2Int(cell.x, cell.y + 1));
                queue.Enqueue(new Vector2Int(cell.x, cell.y - 1));
            }
        }
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
