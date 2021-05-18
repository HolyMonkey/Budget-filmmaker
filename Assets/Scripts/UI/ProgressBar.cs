using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ProgressBar : MonoBehaviour
{
    [SerializeField] private ProgressBarSquare _squareTemplate;
    [SerializeField] private Sprite _currentLevelNumberSprite;
    [SerializeField] private Sprite _nextLevelNumberSprite;
    [SerializeField] private ProgressBarMini _miniBarTemplate;
    [SerializeField] private KeyObject[] _keyObjects;
    [SerializeField] private string _disappearAnimationTrigger;
    [SerializeField] private KeyObjectMover[] _objectsMovers;

    private Animator _animator;
    private List<ProgressBarMini> _miniBars = new List<ProgressBarMini>();

    private void Awake()
    {
        ProgressBarSquare squarePart = Instantiate(_squareTemplate, transform);
        squarePart.Init(_currentLevelNumberSprite);

        foreach (KeyObject keyObject in _keyObjects)
        {
            _miniBars.Add(Instantiate(_miniBarTemplate, transform));
        }

        squarePart = Instantiate(_squareTemplate, transform);
        squarePart.Init(_nextLevelNumberSprite);

        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        foreach (KeyObjectMover mover in _objectsMovers)
        {
            mover.TargetReached += OnTargetReached;
        }
    }

    private void OnDisable()
    {
        foreach (KeyObjectMover mover in _objectsMovers)
        {
            mover.TargetReached -= OnTargetReached;
        }
    }

    public void Disappear()
    {
        _animator.SetTrigger(_disappearAnimationTrigger);
    }

    private void OnTargetReached()
    {
        if (_miniBars.Count > 0)
        {
            _miniBars[0].Fill();
            _miniBars.RemoveAt(0);
        }
    }
}
