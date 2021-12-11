using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Pedestrian _pedestrian;

    private void Update()
    {
        bool moving = _pedestrian.CurrentSpeed > 0;
        if (moving)
        {
            _animator.SetFloat("X", _pedestrian.Direction.x);
            _animator.SetFloat("Y", _pedestrian.Direction.y);
            _animator.SetBool("Moving", true);
        }
        else
        {
            _animator.SetFloat("X", 0);
            _animator.SetFloat("Y", 0);
            _animator.SetBool("Moving", false);
        }
    }
}
