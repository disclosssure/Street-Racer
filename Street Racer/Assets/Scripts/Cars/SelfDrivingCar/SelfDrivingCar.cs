using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SelfDrivingCar : Car
{
    [Header("Other components")]
    [SerializeField] private RoadBordersDetector _detector;

    private Rigidbody2D _rb;

    private void OnEnable()
    {
        _rb = GetComponent<Rigidbody2D>();

        _detector.OnForwardRayHit += Handle;
    }

    private void Handle(RaycastHit2D hit)
    {
        if (hit.transform != null)
        {
            Debug.Log(hit.collider.name);
        }
    }

    private void OnDisable()
    {

    }

    private void Update()
    {
        Move();
        Rotate();
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
}
