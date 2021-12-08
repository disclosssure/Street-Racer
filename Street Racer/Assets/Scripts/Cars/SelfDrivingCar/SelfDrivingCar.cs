using System;
using System.Collections;
using Enums;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SelfDrivingCar : Car
{
    [Header("Moving params")]
    [SerializeField]
    private float _acceleration;

    [SerializeField] private float _frequency;

    [Header("Other components")]
    [SerializeField]
    private RoadBordersDetector _detector;

    private MovingState State = MovingState.None;

    private Coroutine _coroutine;

    private Rigidbody2D _rb;

    protected override void Awake()
    {
        base.Awake();
        _direction = transform.up;
    }

    private void OnEnable()
    {
        _rb = GetComponent<Rigidbody2D>();

        _detector.OnForwardRayHit += HandleForwardRaycast;
        _detector.OnLeftForwardRayHit += HandleLeftForwardRaycast;
        _detector.OnRightForwardRayHit += HandleRightForwardRaycast;
        _detector.OnLeftRayHit += HandleLeftRaycast;
        _detector.OnRightRayHit += HandleLeftRaycast;
    }

    private void OnDisable()
    {
        _detector.OnForwardRayHit -= HandleForwardRaycast;
        _detector.OnLeftForwardRayHit -= HandleLeftForwardRaycast;
        _detector.OnRightForwardRayHit -= HandleRightForwardRaycast;
        _detector.OnLeftRayHit -= HandleLeftRaycast;
        _detector.OnRightRayHit -= HandleLeftRaycast;
    }

    private void Update()
    {
        Move();
        Rotate();
        Accelerate();
    }

    private void Accelerate()
    {
        if (State == MovingState.Accelerating) return;

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        State = MovingState.Accelerating;
        _coroutine = StartCoroutine(AccelerateCoroutine());
    }

    private IEnumerator AccelerateCoroutine()
    {
        while (true)
        {
            AdjustSpeed(_acceleration);
            yield return new WaitForSeconds(_frequency);
        }
    }

    private void Break()
    {
        if (State == MovingState.Breaking) return;

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        State = MovingState.Breaking;
        _coroutine = StartCoroutine(BreakCoroutine());
    }

    private IEnumerator BreakCoroutine()
    {
        while (true)
        {
            AdjustSpeed(-_acceleration);
            yield return new WaitForSeconds(_frequency);
        }
    }

    private void AdjustSpeed(float value)
    {
        _currentSpeed += value;
        _currentSpeed = Mathf.Clamp(_currentSpeed, 0, MaxSpeed);
    }

    public override void Move()
    {
        _rb.MovePosition(transform.position + new Vector3(Direction.x, Direction.y) * Time.deltaTime * CurrentSpeed);
    }

    private void Rotate()
    {
        if (Direction.magnitude == 0) return;

        Vector3 potentialPosition = transform.position + new Vector3(Direction.x, Direction.y);
        Vector2 lookDirection = new Vector2(potentialPosition.x, potentialPosition.y) - _rb.position;

        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        _rb.rotation = angle;
    }

    private void HandleForwardRaycast(RaycastHit2D hit)
    {
        Break();
    }

    private void HandleLeftForwardRaycast(RaycastHit2D hit)
    {
        Debug.Log(hit.transform.name);
    }

    private void HandleRightForwardRaycast(RaycastHit2D hit)
    {
        Debug.Log(hit.transform.name);
    }

    private void HandleLeftRaycast(RaycastHit2D hit)
    {
        Debug.Log(hit.transform.name);
    }
}