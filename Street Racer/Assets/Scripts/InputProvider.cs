using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputProvider : MonoBehaviour
{
    [SerializeField] private Vector2 _inputAxis;

    public Vector2 InputAxis => _inputAxis.normalized;

    private void Update()
    {
        _inputAxis.x = Input.GetAxis("Horizontal");
        _inputAxis.y = Input.GetAxis("Vertical");
    }
}
