using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementController : MonoBehaviour
{
    [SerializeField] private float _speed;

    [SerializeField] private InputProvider _input;
    
    public Vector2 Direction => _direction;

    private Vector3 _direction;

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

    private void Move()
    {
        _rb.MovePosition(transform.position + _direction * Time.deltaTime * _speed);
    }

    private void Rotate()
    {
        Vector3 potentialPosition = transform.position + _direction;
        Vector2 lookDirection = new Vector2(potentialPosition.x, potentialPosition.y) - _rb.position;

        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        _rb.rotation = angle;
    }
}
