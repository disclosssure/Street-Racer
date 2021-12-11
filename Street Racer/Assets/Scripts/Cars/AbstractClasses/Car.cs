using System.Collections;
using UnityEngine;

public abstract class Car : MonoBehaviour, IMovable, ISpeed, IDirectable, IObstacle
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

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<Pedestrian>())
        {
            StartCoroutine(KillPedestrian());
        }
    }

    private IEnumerator KillPedestrian()
    {
        float maxSpeed = _maxSpeed;
        float currentSpeedpeed = _currentSpeed;
        _currentSpeed = 0;
        _maxSpeed = 0;

        yield return new WaitForSeconds(3f);
        _maxSpeed = maxSpeed;
        _currentSpeed = _startSpeed;
    }
}
