using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline), typeof(Animator))]
public class KeyObject : MonoBehaviour
{
    [SerializeField] private string _selectAnimationTrigger;

    private Outline _outline;
    private Animator _animator;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
        _animator = GetComponent<Animator>();
    }

    public void Select()
    {
        _outline.enabled = true;
        _animator.SetTrigger(_selectAnimationTrigger);
    }

    public void Deselect()
    {
        _outline.enabled = false;
    }
}
