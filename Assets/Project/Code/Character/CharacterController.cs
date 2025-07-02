using UnityEngine;

[RequireComponent(typeof(CharacterAnimation))]
public class CharacterController : MonoBehaviour
{
    [SerializeField] private Transform characterTransform;
    [SerializeField] private float speed = 8.0f;
    [SerializeField] private string animationId = "F1_";

    private PlayerInputActions inputActions;
    private CharacterAnimation characterAnimation;
    private CustomCollision collision;

    private Vector3 lastPosition;
    private string currentAnimation;
    private string currentDirection = "down_";

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        characterAnimation = GetComponent<CharacterAnimation>();
        collision = GetComponentInChildren<CustomCollision>();
    }

    private void OnEnable() => inputActions.Enable();
    private void OnDisable() => inputActions.Disable();

    void Update()
    {
        lastPosition = characterTransform.position;
        UpdateMovement();
        UpdateAnimation();
    }

    private void UpdateMovement()
    {
        Vector2 input = inputActions.Movement.Move.ReadValue<Vector2>();

        // Força movimento em apenas um eixo
        if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
            input.y = 0;
        else
            input.x = 0;

        Vector2 movement = input * speed * Time.deltaTime;
        Vector3 position = characterTransform.position;

        // Movimento por eixo com checagem de colisão
        Vector2 moveX = new Vector2(movement.x, 0f);
        Vector2 moveY = new Vector2(0f, movement.y);

        if (collision.ValidateMovement((Vector2)position + moveX))
            position += (Vector3)moveX;

        if (collision.ValidateMovement((Vector2)position + moveY))
            position += (Vector3)moveY;

        characterTransform.position = position;
        HandleFlipping();
    }

    private void UpdateAnimation()
    {
        Vector2 input = inputActions.Movement.Move.ReadValue<Vector2>();
        bool isMoving = input.sqrMagnitude > 0.01f;
        string state = isMoving ? "walk" : "idle";
        string direction = currentDirection;

        Vector3 delta = characterTransform.position - lastPosition;

        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
        {
            direction = "side_";
        }
        else if (delta.y > 0)
        {
            direction = "up_";
        }
        else if (delta.y < 0)
        {
            direction = "down_";
        }

        string newAnimation = animationId + direction + state;
        if (newAnimation != currentAnimation)
        {
            currentDirection = direction;
            currentAnimation = newAnimation;
            characterAnimation.PlayAnimation(newAnimation);
        }
    }

    private void HandleFlipping()
    {
        float deltaX = characterTransform.position.x - lastPosition.x;

        if (deltaX < -0.01f)
            characterTransform.localScale = Vector3.one;
        else if (deltaX > 0.01f)
            characterTransform.localScale = new Vector3(-1, 1, 1);
    }
}
