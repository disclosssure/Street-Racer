using System;
using UnityEngine;

public class RoadBordersDetector : MonoBehaviour
{
    [SerializeField] private float _forwardRayLenght;
    [SerializeField] private float _borderRayLenght;
    [SerializeField] private float _angleRayLenght;

    public Action<RaycastHit2D> OnForwardRayHit;

    public Action<RaycastHit2D> OnLeftForwardRayHit;
    public Action<RaycastHit2D> OnRightForwardRayHit;

    public Action<RaycastHit2D> OnLeftRayHit;
    public Action<RaycastHit2D> OnRightRayHit;

    private void Update()
    {
        CastForwardRay();
        CastAngleRays();
        CastSideRays();
    }

    private void CastForwardRay()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, _forwardRayLenght);
        if (hit.collider != null) OnForwardRayHit?.Invoke(hit);
    }

    private void CastAngleRays()
    {
        RaycastHit2D hit;

        hit = Physics2D.Raycast(transform.position, -transform.right, _borderRayLenght);
        if (hit.collider != null) OnLeftForwardRayHit?.Invoke(hit);

        hit = Physics2D.Raycast(transform.position, transform.right, _borderRayLenght);
        if (hit.collider != null) OnRightForwardRayHit?.Invoke(hit);
    }

    private void CastSideRays()
    {
        RaycastHit2D hit;

        hit = Physics2D.Raycast(transform.position, (transform.up - transform.right).normalized, _angleRayLenght);
        if (hit.collider != null) OnLeftRayHit?.Invoke(hit);

        hit = Physics2D.Raycast(transform.position, (transform.up + transform.right).normalized, _angleRayLenght);
        if (hit.collider != null) OnRightRayHit?.Invoke(hit);
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
