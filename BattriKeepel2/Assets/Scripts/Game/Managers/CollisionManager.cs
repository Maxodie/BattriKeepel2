using UnityEngine;
using System;
using Components;
using System.Collections.Generic;

public class CollisionManagerLogger : Logger {

}

public class CollisionManager {
    static CollisionManager s_Instance;
    private List<Hitbox> m_elements = new List<Hitbox>();
    private SpatialHash m_spatialHash = new SpatialHash();
    private List<Vector2> m_gridWorldPositions;

    public void SetParameters(SO_CollisionManagerData data) {
        m_spatialHash.SetCellSize(data.cellSize);
    }

    public static CollisionManager GetInstance() {
        if (s_Instance == null) {
            s_Instance = new CollisionManager();
        }

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

    public void Update() {
        PopulateHash();
        DetectCollisions();
    }

    Queue<Tuple<Hitbox, Hit>> m_queue = new();

    private void CallCollisionEvents() {
        while (m_queue.Count != 0) {
            var item = m_queue.Dequeue();
            item.Item1.OnCollisionBehavior(item.Item2);
        }
    }

    private void DetectCollisions() {
        foreach (Vector2 position in m_gridWorldPositions) {
            List<Hitbox> hitboxes = m_spatialHash.QuerySpace(position);

            for (int i = 0; i < hitboxes.Count; i++) {
                Hitbox c_hitbox = hitboxes[i];

                for (int j = i + 1; j < hitboxes.Count; j++) {
                    Hitbox o_hitbox = hitboxes[j];

                    if (GetCollisions(c_hitbox, o_hitbox)) {
                        Hit hit = new Hit(FindContactPoint(c_hitbox, o_hitbox), o_hitbox.GetTransform());
                        c_hitbox.lastHitObject = hit;
                        m_queue.Enqueue(new Tuple<Hitbox, Hit>(c_hitbox, hit));
                        Hit hit1 = new Hit(FindContactPoint(o_hitbox, c_hitbox), c_hitbox.GetTransform());
                        o_hitbox.lastHitObject = hit1;
                        m_queue.Enqueue(new Tuple<Hitbox, Hit>(o_hitbox, hit1));
                    } else {
                        if (o_hitbox.hardCollsion) {
                            Vector2 newPosition = c_hitbox.GetPosition() + c_hitbox.wishVelocity;
                            MockBox mockBox = new (c_hitbox, newPosition);

                            if (GetCollisions(mockBox, o_hitbox)) {

                            }
                            // make a new hitbox and if it is in the hard collision then get the math correct and apply it to the original hitbox's outputVelocity
                            // mock hitbox would be of great use
                        }
                    }
                }
            }
        }
        CallCollisionEvents();
    }

    private bool GetCollisions(Hitbox c_hitbox, Hitbox o_hitbox) {
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
        return hasCollided;
    }

    private Vector2 FindContactPoint(Hitbox a, Hitbox b) {
        if (a.GetHitboxType() == HitboxType.Circle && b.GetHitboxType() == HitboxType.Circle) {
            return FindCircleToCircle(a, b);
        } else if (a.GetHitboxType() == HitboxType.Circle && b.GetHitboxType() == HitboxType.RectangularParallelepiped) {
            return FindCircleToRectangle(a, b);
        } else if (b.GetHitboxType() == HitboxType.Circle && a.GetHitboxType() == HitboxType.RectangularParallelepiped) {
            return FindCircleToRectangle(b, a);
        } else if (b.GetHitboxType() == HitboxType.RectangularParallelepiped && a.GetHitboxType() == HitboxType.RectangularParallelepiped) {
            return FindRectangleToRectangle(a, b);
        }
        Log.Warn<CollisionManagerLogger>("Should not be 0,0 unless it is actually 0,0 but like if it calls this warning then it should not so yeah be careful please.");
        return new Vector2();
    }

    private Vector2 FindCircleToCircle(Hitbox a, Hitbox b) {
        Vector2 ab = b.GetPosition() - a.GetPosition();
        Vector2 dir = ab.normalized;
        Vector2 hitPosition = a.GetPosition() + dir * a.GetDiameter() / 2;
        return hitPosition;
    }

    private Vector2 FindRectangleToRectangle(Hitbox a, Hitbox b) {
        Vector2 aPosition = a.GetPosition();
        Vector2 aMinBound = new Vector2(aPosition.x - (a.GetDimensions().x / 2), aPosition.y - (a.GetDimensions().y / 2));
        Vector2 aMaxBound = new Vector2(aPosition.x + (a.GetDimensions().x / 2), aPosition.y + (a.GetDimensions().y / 2));

        Vector2 bPosition = b.GetPosition();
        Vector2 bMinBound = new Vector2(bPosition.x - (b.GetDimensions().x / 2), bPosition.y - (b.GetDimensions().y / 2));
        Vector2 bMaxBound = new Vector2(bPosition.x + (b.GetDimensions().x / 2), bPosition.y + (b.GetDimensions().y / 2));

        Vector2 contact = new Vector2(
                Math.Max(aMinBound.x, bMinBound.x),
                Math.Max(aMinBound.y, bMinBound.y)
        );

        return contact;
    }

    private Vector2 FindCircleToRectangle(Hitbox circle, Hitbox rectangularParallelepiped) {
        Vector2 circlePosition = circle.GetPosition();
        float circleRadius = circle.GetDiameter() / 2;

        Vector2 rectPosition = rectangularParallelepiped.GetPosition();
        Vector2 rectDimensions = rectangularParallelepiped.GetDimensions();

        Vector2 rectMinBound = new Vector2(rectPosition.x - (rectDimensions.x / 2), rectPosition.y - (rectDimensions.y / 2));
        Vector2 rectMaxBound = new Vector2(rectPosition.x + (rectDimensions.x / 2), rectPosition.y + (rectDimensions.y / 2));

        float closestX = Mathf.Clamp(circlePosition.x, rectMinBound.x, rectMaxBound.x);
        float closestY = Mathf.Clamp(circlePosition.y, rectMinBound.y, rectMaxBound.y);

        Vector2 closestPoint = new Vector2(closestX, closestY);

        Vector2 dir = (closestPoint - circlePosition).normalized;
        Vector2 contact = circlePosition + dir * circleRadius;

        return contact;
    }

    private bool IsCircleRectangleCollision(Hitbox circle, Hitbox rectangularParallelepiped) {
        Vector2 circlePosition = circle.GetPosition();
        float circleRadius = circle.GetDiameter() / 2;

        Vector2 rectPosition = rectangularParallelepiped.GetPosition();
        Vector2 rectDimensions = rectangularParallelepiped.GetDimensions();

        Vector2 rectMinBound = new Vector2(rectPosition.x - (rectDimensions.x / 2), rectPosition.y - (rectDimensions.y / 2));
        Vector2 rectMaxBound = new Vector2(rectPosition.x + (rectDimensions.x / 2), rectPosition.y + (rectDimensions.y / 2));

        float closestX = Mathf.Clamp(circlePosition.x, rectMinBound.x, rectMaxBound.x);
        float closestY = Mathf.Clamp(circlePosition.y, rectMinBound.y, rectMaxBound.y);

        Vector2 closestPoint = new Vector2(closestX, closestY);

        float distance = Vector2.Distance(circlePosition, closestPoint);

        return (distance <= circleRadius);
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
        float combinedSize = (c1.GetDiameter() + c2.GetDiameter()) / 2;

        return distance <= combinedSize;
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

public class Hit {
    public Hit(Vector2 hitPos, Transform hitObj) {
        hitPosition = hitPos;
        hitObject = hitObj;
    }
    public Vector2 hitPosition;
    public Transform hitObject;
}

public class SpatialHash {
    private Dictionary<Vector2Int, List<Hitbox>> m_grid = new Dictionary<Vector2Int, List<Hitbox>>();
    private float m_cellSize;

    public void SetCellSize(float size) {
        m_cellSize = size;
    }

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
        float hitboxSize = hitbox.GetDiameter();

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
