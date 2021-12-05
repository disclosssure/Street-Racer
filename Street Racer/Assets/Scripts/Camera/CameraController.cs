using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    [SerializeField] private MovementController _targetRb;
    [SerializeField] private float _smoothness;
    [SerializeField] private float _predictionRange;

    private Vector3 _cameraOffset = new Vector3(0, 0, -10);

    private void Update()
    {
        Follow();
    }

    private void Follow()
    {
        Vector3 targetNextPosition = _targetRb.Direction;

        transform.position = Vector3.Lerp(transform.position, _targetRb.transform.position + targetNextPosition * _predictionRange  + _cameraOffset, Time.deltaTime * _smoothness);
    }
}