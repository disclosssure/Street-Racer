using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour, ITargetable
{
    [SerializeField] private List<Transform> _path;

    private Queue<Transform> _points = new Queue<Transform>();

    public Transform Target => _points.Peek();

    

    public Transform SetNextPoint()
    {
        if (_points.Count == 0)
            InitPath();

        var nextPoint = _points.Dequeue();
        _points.Enqueue(nextPoint);
        return nextPoint;
    }

    public void SetTarget(Transform target) { }

    private void InitPath() => _path.ForEach(x => _points.Enqueue(x));
}
