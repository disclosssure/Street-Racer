using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PoliceCarAnimationController : MonoBehaviour
{
    [SerializeField] private bool _isChasing;

    private Animator _animator;

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnDisable()
    {
        
    }

    private void Update()
    {
        if(_isChasing)
        {
            _animator.SetBool("Chasing", true);
        }
    }
}
