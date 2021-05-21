using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ActionsDemonstratorBeachScene : ActionsDemonstrator
{
    [SerializeField] private Button _andActionButton;
    [SerializeField] private Animator _andActionButtonAnimator;
    [SerializeField] private string _buttonDisappearAnimationTrigger;
    [SerializeField] private ProgressBar _progressBar;
    [SerializeField] private CameraRotator _cameraRotator;
    [SerializeField] private KeyObject[] _keyObjects;
    [SerializeField] private float _ghostsDisappearDelay;
    [SerializeField] private OperatorScene _operatorScene;
    [SerializeField] private float _delayBetweenActions;
    [SerializeField] private float _delayAfterActions;

    private bool _isCameraReset;
    private bool _isAllObjectsAtCorrectPlaces = true;
    private Queue<ActionScene> _runningActionsQueue = new Queue<ActionScene>();

    private void OnEnable()
    {
        _andActionButton.onClick.AddListener(StartPreparation);
        _cameraRotator.CameraReset += OnCameraReset;
        _operatorScene.ActionSceneCompleted += OnActionSceneCompleted;
    }

    private void OnDisable()
    {
        _andActionButton.onClick.RemoveListener(StartPreparation);
        _cameraRotator.CameraReset -= OnCameraReset;
        _operatorScene.ActionSceneCompleted -= OnActionSceneCompleted;
    }

    private void StartPreparation()
    {
        StartCoroutine(WaitForEndOfPreparation());
    }

    private void OnCameraReset()
    {
        _isCameraReset = true;
    }

    private IEnumerator WaitForEndOfPreparation()
    {
        OnActionStarted();
        _cameraRotator.ResetRotation();
        _andActionButtonAnimator.SetTrigger(_buttonDisappearAnimationTrigger);
        _progressBar.Disappear();

        foreach (KeyObject keyObject in _keyObjects)
        {
            if (keyObject.IsInCorrectPlace == false)
            {
                _isAllObjectsAtCorrectPlaces = false;
                keyObject.ObjectGhost.FullyDisappear();
            }
        }

        if (_isAllObjectsAtCorrectPlaces == false)
        {
            yield return new WaitForSeconds(_ghostsDisappearDelay);
        }

        while (_isCameraReset == false)
        {
            yield return null;
        }

        StartActions();
    }

    private void StartActions()
    {
        Debug.Log("Actions can be started");
        StartCoroutine(StartActionsRoutine());
    }

    private IEnumerator StartActionsRoutine()
    {
        //if (_isAllObjectsAtCorrectPlaces == false)
        //{
        //    foreach (KeyObject keyObject in _keyObjects)
        //    {
        //        if (keyObject.IsInCorrectPlace == false)
        //        {
        //            if (keyObject is Soldier)
        //            {
        //                _soldierScene.Run();
        //                _runningActionsQueue.Enqueue(_soldierScene);
        //            }
        //            else if (keyObject is Airplane)
        //            {
        //                _airplaneScene.Run();
        //                _runningActionsQueue.Enqueue(_airplaneScene);
        //            }
        //        }
        //    }
        //}
        //else
        //{
        //    _soldierScene.Run();
        //    _runningActionsQueue.Enqueue(_soldierScene);
        //    yield return new WaitForSeconds(_delayBetweenActions);
        //    _airplaneScene.Run();
        //    _runningActionsQueue.Enqueue(_airplaneScene);
        //    yield return new WaitForSeconds(_delayBetweenActions * 2);
        //    _squadScene.Run();
        //    _runningActionsQueue.Enqueue(_squadScene);
        //}

        yield return null;
        _operatorScene.Run();
        _runningActionsQueue.Enqueue(_operatorScene);
    }

    private void OnActionSceneCompleted(ActionScene action)
    {
        if (_runningActionsQueue.Count > 0)
        {
            _runningActionsQueue.Dequeue();
        }
        
        if (_runningActionsQueue.Count == 0)
        {
            StartCoroutine(WaitForResults());
        }
    }

    private IEnumerator WaitForResults()
    {
        yield return new WaitForSeconds(_delayAfterActions);
        OnAllActionsCompleted(_isAllObjectsAtCorrectPlaces);
    }
}
