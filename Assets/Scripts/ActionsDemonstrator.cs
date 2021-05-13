using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ActionsDemonstrator : MonoBehaviour
{
    [SerializeField] private Button _andActionButton;
    [SerializeField] private Animator _andActionButtonAnimator;
    [SerializeField] private string _buttonDisappearAnimationTrigger;
    [SerializeField] private CameraRotator _cameraRotator;
    [SerializeField] private KeyObject[] _keyObjects;
    [SerializeField] private float _ghostsDisappearDelay;
    [SerializeField] private SoldierScene _soldierScene;
    [SerializeField] private AirplaneScene _airplaneScene;
    [SerializeField] private SquadScene _squadScene;
    [SerializeField] private float _delayBetweenActions;

    private bool _isCameraReset;

    public event UnityAction ActionStarted;

    private void OnEnable()
    {
        _andActionButton.onClick.AddListener(StartPreparation);
        _cameraRotator.CameraReset += OnCameraReset;
    }

    private void OnDisable()
    {
        _andActionButton.onClick.RemoveListener(StartPreparation);
        _cameraRotator.CameraReset -= OnCameraReset;
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
        ActionStarted?.Invoke();
        _cameraRotator.ResetRotation();
        _andActionButtonAnimator.SetTrigger(_buttonDisappearAnimationTrigger);
        bool isAllObjectsAtCorrectPlaces = true;

        foreach (KeyObject keyObject in _keyObjects)
        {
            if (keyObject.IsInCorrectPlace == false)
            {
                isAllObjectsAtCorrectPlaces = false;
                keyObject.ObjectGhost.FullyDisappear();
            }
        }

        if (isAllObjectsAtCorrectPlaces == false)
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
        StartCoroutine(StartActionsRoutine());
    }

    private IEnumerator StartActionsRoutine()
    {
        bool allObjectsAtCorrectPlaces = true;

        foreach (KeyObject keyObject in _keyObjects)
        {
            if (keyObject.IsInCorrectPlace == false)
            {
                allObjectsAtCorrectPlaces = false;
                if (keyObject is Soldier)
                {
                    _soldierScene.Run();
                }
                else if (keyObject is Airplane)
                {
                    _airplaneScene.Run();
                }
            }
        }

        if (allObjectsAtCorrectPlaces)
        {
            _soldierScene.Run();
            yield return new WaitForSeconds(_delayBetweenActions);
            _airplaneScene.Run();
            yield return new WaitForSeconds(_delayBetweenActions);
            _squadScene.Run();
        }
    }
}
