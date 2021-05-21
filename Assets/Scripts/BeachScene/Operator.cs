using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Operator : KeyObject
{
    [SerializeField] private Animator _animator;
    [SerializeField] private string _stumbleAnimationTrigger;

    public void Stumble()
    {
        _animator.SetTrigger(_stumbleAnimationTrigger);
        //_animator.SetLayerWeight(1, 0);
    }
}
