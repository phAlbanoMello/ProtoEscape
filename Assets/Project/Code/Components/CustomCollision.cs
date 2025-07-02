using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CustomCollision : MonoBehaviour
{
    [SerializeField] private Vector2 scaleOffset;
    [SerializeField] private Vector2 positionOffset;

    [Tooltip("Trigger Colliders are not obstacles. They don't block movement")]
    public bool isTrigger;

    [Header("Editor")]
    [SerializeField] private bool previewBounds;

    private Bounds collisionBounds;

    public bool Collided { get; private set; }
    public event Action OnCollision;

    public Bounds GetBounds() => collisionBounds;

    private void Awake()
    {
        collisionBounds = CalculateBounds(transform.position);
    }

    private Bounds CalculateBounds(Vector2 center)
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Vector3 size = sr.bounds.size + (Vector3)scaleOffset;
        Vector3 offset = (Vector3)positionOffset;
        return new Bounds(center + (Vector2)offset, size);
    }

    public void UpdateBounds(Vector2 position)
    {
        collisionBounds = CalculateBounds(position);
    }

    /// <summary>
    /// Returns true if movement is valid (no collision).
    /// </summary>
    public bool ValidateMovement(Vector2 nextPosition)
    {
        Collided = false;

        Bounds nextBounds = CalculateBounds(nextPosition);

        foreach (CustomCollision other in ObstacleManager.obstacles)
        {
            if (other == this) continue; // ignora colisão com si mesmo
            if (other.isTrigger) continue; // triggers não bloqueiam

            if (nextBounds.Intersects(other.GetBounds()))
            {
                Collided = true;
                OnCollision?.Invoke();
                return false;
            }
        }

        return true;
    }

    public bool IsCollidingWith(CustomCollision other)
    {
        return collisionBounds.Intersects(other.GetBounds());
    }

    private void OnDrawGizmosSelected()
    {
        if (!previewBounds) return;

        Bounds preview = CalculateBounds(transform.position);

        Gizmos.color = isTrigger ? Color.yellow : Color.cyan;
        Gizmos.DrawWireCube(preview.center, preview.size);
    }
}
