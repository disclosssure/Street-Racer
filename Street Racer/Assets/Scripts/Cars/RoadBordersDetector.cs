using System;
using System.Collections;
using UnityEngine;

public class RoadBordersDetector : MonoBehaviour
{
    [Header("Detection params")]
    [SerializeField]
    private float _frequency;

    [Header("Rays params")]
    [SerializeField]
    private float _forwardRayLenght;

    [SerializeField] private float _borderRayLenght;
    [SerializeField] private float _angleRayLenght;

    public Action<RaycastHit2D> OnForwardRayHit;

    public Action<RaycastHit2D> OnLeftForwardRayHit;
    public Action<RaycastHit2D> OnRightForwardRayHit;

    public Action<RaycastHit2D> OnLeftRayHit;
    public Action<RaycastHit2D> OnRightRayHit;

    private Coroutine _coroutine;

    private void OnEnable()
    {
        _coroutine = StartCoroutine(ScanEnvironment());
    }

    private void OnDisable()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }

    private void CastForwardRay()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, _forwardRayLenght);
        if (WasObstacleHit(hit)) OnForwardRayHit?.Invoke(hit);
    }

    private void CastAngleRays()
    {
        RaycastHit2D hit;

        hit = Physics2D.Raycast(transform.position, -transform.right, _borderRayLenght);
        if (WasObstacleHit(hit)) OnLeftForwardRayHit?.Invoke(hit);

        hit = Physics2D.Raycast(transform.position, transform.right, _borderRayLenght);
        if (WasObstacleHit(hit)) OnRightForwardRayHit?.Invoke(hit);
    }

    private void CastSideRays()
    {
        RaycastHit2D hit;

        hit = Physics2D.Raycast(transform.position, (transform.up - transform.right).normalized, _angleRayLenght);
        if (WasObstacleHit(hit)) OnLeftRayHit?.Invoke(hit);

        hit = Physics2D.Raycast(transform.position, (transform.up + transform.right).normalized, _angleRayLenght);
        if (WasObstacleHit(hit)) OnRightRayHit?.Invoke(hit);
    }

    private bool WasObstacleHit(RaycastHit2D hit)
    {
        var hitTransform = hit.transform;

        if (hitTransform != null)
        {
            if (hitTransform.GetComponent<IObstacle>() != null)
            {
                return true;
            }
        }
        return false;
    }

    private IEnumerator ScanEnvironment()
    {
        while (true)
        {
            CastForwardRay();
            CastAngleRays();
            CastSideRays();
            yield return new WaitForSeconds(_frequency);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, transform.up * _forwardRayLenght);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.right * _borderRayLenght);
        Gizmos.DrawRay(transform.position, -transform.right * _borderRayLenght);

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, (transform.up + transform.right).normalized * _angleRayLenght);
        Gizmos.DrawRay(transform.position, (transform.up - transform.right).normalized * _angleRayLenght);
    }
}