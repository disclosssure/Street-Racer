using System.Collections;
using Enums;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Pedestrian : MonoBehaviour, IMovable, ISpeed, ITargetable, IDirectable
{
    [Header("Pedestrian params")]
    [SerializeField] private PedestrianState State;
    [SerializeField] private float _waitTime;

    [Header("Moving params")]
    [SerializeField] private float _targetTreshold;
    [SerializeField] private float _currentSpeed;
    [SerializeField] private float _maxSpeed;

    private readonly float _minSpeed = 0;

    [Header("Other components")]
    [SerializeField] private Path _path;

    public float CurrentSpeed => _currentSpeed;
    public float StartSpeed => CurrentSpeed;
    public float MaxSpeed => _maxSpeed;

    public Vector2 Direction => (Target.position - transform.position).normalized;

    public Transform Target => _path.Target;

    private Rigidbody2D _rigidBody;
    private Coroutine _coroutine;

    private void OnEnable()
    {
        _rigidBody = GetComponent<Rigidbody2D>();

        _path.SetNextPoint();
        _coroutine = StartCoroutine(CrossRoadCoroutine());
    }

    private void OnDisable()
    {

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }

    private void Update()
    {
        if (State == PedestrianState.CrossingRoad)
        {
            Move();
            _currentSpeed = _maxSpeed;

            if (Vector3.Distance(Target.position, transform.position) <= _targetTreshold)
            {
                State = PedestrianState.OnPoint;
                _currentSpeed = _minSpeed;
            }
        }
    }

    public void Move()
    {
        _rigidBody.MovePosition(transform.position + (Target.position - transform.position).normalized * CurrentSpeed * Time.deltaTime);
    }

    private IEnumerator CrossRoadCoroutine()
    {
        State = PedestrianState.CrossingRoad;

        yield return new WaitUntil(() => State == PedestrianState.OnPoint);
        yield return new WaitForSeconds(_waitTime);

        _path.SetNextPoint();

        _coroutine = StartCoroutine(CrossRoadCoroutine());
    }

    public void SetTarget(Transform target) {}
}
