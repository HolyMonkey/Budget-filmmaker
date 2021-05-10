using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    [SerializeField] private float _sensitivity;
    [SerializeField] private Transform _cameraRig;

    private Vector3 _mousePreviousPosition;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _mousePreviousPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 passedDistance = Camera.main.ScreenToViewportPoint(Input.mousePosition) - _mousePreviousPosition;
            //Camera.main.transform.RotateAround(Vector3.zero, new Vector3(0, 1, 0), -passedDistance.x * 180 * Time.deltaTime * _sensitivity);
            _cameraRig.Rotate(new Vector3(0, 1, 0), -passedDistance.x * 180 * Time.deltaTime * _sensitivity);
            _mousePreviousPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        }
    }
}
