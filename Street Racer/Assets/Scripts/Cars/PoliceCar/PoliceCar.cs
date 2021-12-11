using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PoliceCar : Car, ITargetable
{
    [Header("Police params")]
    [SerializeField] private float _maxSpeedAllowed;

    [Header("Other components")]
    [SerializeField] private CarsScanner _scanner;

    public event Action OnPlayerCaught;

    private Transform _target;
    public Transform Target => _target;
    private bool isChasing;

    private Rigidbody2D _rb;

    protected override void Awake()
    {
        base.Awake();

        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _scanner.OnCarDetected += HandleOnViolatorDetected;
    }

    private void OnDisable()
    {
        _scanner.OnCarDetected -= HandleOnViolatorDetected;
    }

    private void Update()
    {
        if (isChasing)
        {
            _direction = (Target.position - transform.position).normalized;
        }
        else
        {
            _direction = Vector2.zero;
        }

        Move();
        Rotate();
    }

    public override void Move()
    {
        _rb.MovePosition(transform.position + new Vector3(Direction.x, Direction.y) * Time.deltaTime * CurrentSpeed);
    }

    private void HandleOnViolatorDetected(Car car)
    {
        if (!isChasing)
        {
            if (car.CurrentSpeed >= _maxSpeedAllowed)
            {
                _target = car.transform;
                isChasing = true;
            }
        }
    }

    private void Rotate()
    {
        if (Direction.magnitude == 0)
            return;

        Vector3 potentialPosition = transform.position + new Vector3(Direction.x, Direction.y);
        Vector2 lookDirection = new Vector2(potentialPosition.x, potentialPosition.y) - _rb.position;

        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        _rb.rotation = angle;
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (isChasing)
        {
            if (other.GetComponent<PlayerCar>())
            {
                OnPlayerCaught?.Invoke();
            }
        }
    }
}
