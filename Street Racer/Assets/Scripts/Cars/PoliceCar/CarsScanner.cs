using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CarsScanner : MonoBehaviour
{
    [SerializeField] private float _range;
    [SerializeField] private float _frequency;

    [SerializeField] private Car _owner;

    public Action<Car> OnCarDetected;

    private Coroutine _coroutine;

    private void OnEnable()
    {
        _coroutine = StartCoroutine(ScanForViolatorsCoroutine());
    }

    private void OnDisable()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }

    private IEnumerator ScanForViolatorsCoroutine()
    {
        while (true)
        {
            ScanArea();
            yield return new WaitForSeconds(_frequency);
        }
    }

    private void ScanArea()
    {
        var cars = new List<Car>();

        var colliders = Physics2D.OverlapCircleAll(transform.position, _range).ToList();
        colliders.ForEach(collider =>
        {
            var car = collider.GetComponent<Car>();
            if (car)
            {
                cars.Add(car);
            }
        });

        if (cars.Contains(_owner))
        {
            cars.Remove(_owner);
        }

        if (cars.Count >= 1)
        {
            OnCarDetected?.Invoke(cars[0]);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _range);
    }
}
