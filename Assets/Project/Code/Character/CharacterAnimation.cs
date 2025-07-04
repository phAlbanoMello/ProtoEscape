using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private string animationId = "F1_";

    private string currentAnimation;
    private string currentDirection = "down_";

    private void Update()
    {
        UpdateAnimation();
    }

    public void PlayAnimation(string name)
    {
        animator.Play(name);
    }

    private void UpdateAnimation()
    {
        //Vector2 input = inputActions.Movement.Move.ReadValue<Vector2>(); //I need to know if the player is moving or idle
        //bool isMoving = input.sqrMagnitude > 0.01f;
        //string state = isMoving ? "walk" : "idle";
        //string direction = currentDirection; //I need to know the direction of the movement

        ////Vector3 delta = characterTransform.position - lastPosition; //Getting the movement direction

        //if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
        //{
        //    direction = "side_";
        //}
        //else if (delta.y > 0)
        //{
        //    direction = "up_";
        //}
        //else if (delta.y < 0)
        //{
        //    direction = "down_";
        //}

        //string newAnimation = animationId + direction + state;
        //if (newAnimation != currentAnimation)
        //{
        //    currentDirection = direction;
        //    currentAnimation = newAnimation;
        //    PlayAnimation(newAnimation);
        //}
    }
}
