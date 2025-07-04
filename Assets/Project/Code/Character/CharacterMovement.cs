using UnityEngine;
using UnityEngine.UIElements;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private Transform characterTransform;
    [SerializeField] private float speed = 8.0f;
    [SerializeField] private CustomCollision collision;
    
    private PlayerInputActions inputActions;

    private Vector3 lastPosition;
    private Vector3 position;
    private Vector2 moveX;
    private Vector2 moveY;
    private Vector2 input;


    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable() => inputActions.Enable();
    private void OnDisable() => inputActions.Disable();

    void Update()
    {
        lastPosition = characterTransform.position;
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        input = inputActions.Movement.Move.ReadValue<Vector2>();

        if (input.sqrMagnitude < 0.01f) { return; }

        //Move one axis at the time
        if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
            input.y = 0;
        else
            input.x = 0;

        Vector2 movementDelta = input * speed * Time.deltaTime;
        position = characterTransform.position;

        moveX = new Vector2(movementDelta.x, 0f);
        moveY = new Vector2(0f, movementDelta.y);

        bool shouldMoveX = true;
        bool shouldMoveY = true;

        if (collision)
        {
            shouldMoveX = !collision.CheckForCollision((Vector2)position + moveX);
            shouldMoveY = !collision.CheckForCollision((Vector2)position + moveY);
        }

        if (shouldMoveX) position.x += moveX.x;
        if (shouldMoveY) position.y += moveY.y;

        characterTransform.position = position;
        HandleFlipping();
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
