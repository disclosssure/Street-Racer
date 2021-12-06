using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerCar : Car
{
    [Header("Other components")]
    [SerializeField] private InputProvider _input;
    
    private Rigidbody2D _rb;
    private Camera _mainCamera;

    private void OnEnable()
    {
        _mainCamera = Camera.main;

        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        var input = _input.InputAxis;
        _direction = new Vector3(input.x, input.y, 0);

        Move();
        Rotate();
    }

    public override void Move()
    {
        _rb.MovePosition(transform.position + new Vector3(Direction.x, Direction.y) * Time.deltaTime * CurrentSpeed);
    }

    private void Rotate()
    {
        Vector3 potentialPosition = transform.position + new Vector3(Direction.x, Direction.y);
        Vector2 lookDirection = new Vector2(potentialPosition.x, potentialPosition.y) - _rb.position;

        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        _rb.rotation = angle;
    }
}
