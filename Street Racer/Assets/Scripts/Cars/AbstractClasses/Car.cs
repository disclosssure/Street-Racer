using UnityEngine;

public abstract class Car : MonoBehaviour, IMovable, ISpeed, IDirectable
{
    [Header("Speed params")]
    [SerializeField] protected float _currentSpeed;
    [SerializeField] protected float _startSpeed;
    [SerializeField] protected float _maxSpeed;

    [Header("Direction")]
    [SerializeField] protected Vector2 _direction;

    public float CurrentSpeed => _currentSpeed;
    public float StartSpeed => _startSpeed;
    public float MaxSpeed => _maxSpeed;

    public Vector2 Direction => _direction.normalized;


    protected virtual void Awake()
    {
        _currentSpeed = _startSpeed;
    }

    public abstract void Move();
}
