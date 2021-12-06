using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ViolatorScanner : MonoBehaviour
{
    [SerializeField] private float _maxSpeedAllowed;
    [SerializeField] private float _range;
    [SerializeField] private float _frequency;

    private Coroutine _coroutine;

    private void OnEnable()
    {
        _coroutine = StartCoroutine(ScanForViolatorsCoroutine());
    }

    private void OnDisable()
    {
        if(_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }   
    }

    private IEnumerator ScanForViolatorsCoroutine()
    {
        while(true)
        {

            yield return new WaitForSeconds(_frequency);
        }
    }

    private void ScanArea()
    {
        var cars = Physics2D.OverlapCircleAll(transform.position, _range).ToList();
        
        // if(cars.Contains(this))

        if(cars.Count >= 1)
        {

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _range);
    }
}
