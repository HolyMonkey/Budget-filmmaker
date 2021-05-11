using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionDemonstrator : MonoBehaviour
{
    [SerializeField] private Button _andActionButton;
    [SerializeField] private Animator _andActionButtonAnimator;
    [SerializeField] private string _buttonDisappearAnimationTrigger;
    [SerializeField] private CameraRotator _cameraRotator;

    private bool _isCameraReset;

    private void OnEnable()
    {
        _andActionButton.onClick.AddListener(StartAction);
        _cameraRotator.CameraReset += OnCameraReset;
    }

    private void OnDisable()
    {
        _andActionButton.onClick.RemoveListener(StartAction);
        _cameraRotator.CameraReset -= OnCameraReset;
    }

    private void StartAction()
    {
        StartCoroutine(WaitForEndOfAction());
    }

    private void OnCameraReset()
    {
        Debug.Log("In OnCameraReset");
        _isCameraReset = true;
    }

    private IEnumerator WaitForEndOfAction()
    {
        _cameraRotator.ResetRotation();
        _andActionButtonAnimator.SetTrigger(_buttonDisappearAnimationTrigger);

        while (_isCameraReset == false)
        {
            yield return null;
        }

        Debug.Log("Actions can be started");
    }
}
