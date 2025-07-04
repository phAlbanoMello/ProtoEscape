using System;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class CustomCollision : MonoBehaviour
{
    [SerializeField] private Vector2 scaleOffset;
    [SerializeField] private Vector2 positionOffset;

    [Tooltip("Trigger Colliders are not obstacles. They don't block movement")]
    public bool isTrigger;
    [Tooltip("If true, this object's collision center get's updated every frame")]
    public bool isDynamic;

    [Header("Editor")]
    [SerializeField] private bool previewBounds;
    public bool Collided { get; private set; }

    public event Action OnCollision;

    private Bounds collisionBounds;

    public Bounds GetBounds() => collisionBounds;

    private void Awake()
    {
        UpdateBounds(transform.position);
    }

    public void UpdateBounds(Vector3 position)
    {
        collisionBounds = CalculateBounds(position);
    }

    private Bounds CalculateBounds(Vector3 position)
    {
        return new Bounds(position + (Vector3)positionOffset, (Vector3)scaleOffset);
    }

    public bool IsCollidingWith(CustomCollision other)
    {
        return collisionBounds.Intersects(other.GetBounds());
    }

    /// <summary>
    /// Returns true if movement is valid (no collision).
    /// </summary>
    public bool CheckForCollision(Vector2 nextPosition)
    {
        Collided = false;

        Bounds nextBounds = CalculateBounds(nextPosition);

        foreach (CustomCollision other in ObstacleManager.obstacles)
        {
            if (other == this) continue; 
            if (other.isTrigger) continue;

            if (nextBounds.Intersects(other.GetBounds()))
            {
                Collided = true;
                OnCollision?.Invoke();
                return Collided;
            }
        }

        return Collided;
    }
   
    private void OnDrawGizmosSelected()
    {
        if (!previewBounds) return;

        Bounds preview = CalculateBounds(transform.position);

        Gizmos.color = isTrigger ? Color.yellow : Color.cyan;
        Gizmos.DrawWireCube(preview.center, preview.size);
    }
}
