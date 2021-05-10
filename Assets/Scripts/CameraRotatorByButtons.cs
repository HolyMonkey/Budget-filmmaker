using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraRotatorByButtons : MonoBehaviour
{
    [SerializeField] private Button _rotateLeftButton;
    [SerializeField] private Button _rotateRightButton;
    [SerializeField] private float _rotationPerClick;
    [SerializeField] private float _rotateDuration;
    [SerializeField] private Transform _pointToFollow;

    private bool _isRotating;

    private void OnEnable()
    {
        _rotateRightButton.onClick.AddListener(OnRotateRightButtonClicked);
        _rotateLeftButton.onClick.AddListener(OnRotateLeftButtonClicked);
    }

    private void OnDisable()
    {
        _rotateRightButton.onClick.RemoveListener(OnRotateRightButtonClicked);
        _rotateLeftButton.onClick.RemoveListener(OnRotateLeftButtonClicked);
    }
    private void OnRotateLeftButtonClicked()
    {
        StartCoroutine(RotateAround(_rotationPerClick, _rotateDuration));
    }

    private void OnRotateRightButtonClicked()
    {
        StartCoroutine(RotateAround(-_rotationPerClick, _rotateDuration));
    }

    private IEnumerator RotateAround(float angle, float duration)
    {
        if (_isRotating == false)
        {
            _isRotating = true;
            float passedTime = 0;
            _pointToFollow.RotateAround(Vector3.zero, new Vector3(0, 1, 0), angle);
            Quaternion startRotation = Camera.main.transform.rotation;
            Vector3 startPosition = Camera.main.transform.position;

            while (passedTime < duration)
            {
                Camera.main.transform.position = Vector3.Lerp(startPosition, _pointToFollow.position, passedTime / duration);
                Camera.main.transform.rotation = Quaternion.Lerp(startRotation, _pointToFollow.rotation, passedTime / duration);
                passedTime += Time.deltaTime;
                yield return null;
            }

            Camera.main.transform.position = _pointToFollow.position;
            Camera.main.transform.rotation = _pointToFollow.rotation;
            _isRotating = false;
        }
    }
}
