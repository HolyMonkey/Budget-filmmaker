using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _cameraRig;
    [SerializeField] private Vector2 _limitX;
    [SerializeField] private Vector2 _limitZ;

    private Vector3 _mousePreviousPosition;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _mousePreviousPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 movementDirection = -(Camera.main.ScreenToViewportPoint(Input.mousePosition) - _mousePreviousPosition).normalized;
            Vector3 newPosition = _cameraRig.position;
            newPosition += movementDirection * _speed * Time.deltaTime;
            //newPosition.x = Mathf.Clamp(newPosition.x, _limitX.x, _limitX.y);
            //newPosition.z = Mathf.Clamp(newPosition.z, -_limitZ.x, _limitZ.y);
            _cameraRig.position = newPosition;
            _mousePreviousPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        }
    }



    //private const string _mouseXAxis = "Mouse X";
    //private const string _mouseYAxis = "Mouse Y";

    //private void Update()
    //{
    //    if (Input.GetMouseButton(0))
    //    {
    //        Vector3 position = _cameraRig.position;
    //        position.x -= Input.GetAxis(_mouseXAxis) * _speed * Time.deltaTime;
    //        position.z -= Input.GetAxis(_mouseYAxis) * _speed * Time.deltaTime;

    //        position.x = Mathf.Clamp(position.x, _limitX.x, _limitX.y);
    //        position.z = Mathf.Clamp(position.z, -_limitZ.x, _limitZ.y);
    //        _cameraRig.position = position;
    //    }
    //}



    //private Vector3 _difference;
    //private Vector3 _origin;
    //private bool _isDragging;

    //private void LateUpdate()
    //{
    //    Vector3 position = _cameraRig.position;

    //    if (Input.GetMouseButton(0))
    //    {
    //        _difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - _cameraRig.transform.position;
    //        if (_isDragging == false)
    //        {
    //            _isDragging = true;
    //            _origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //        }
    //    }
    //    else
    //    {
    //        _isDragging = false;
    //    }

    //    if (_isDragging)
    //    {
    //        position = _origin - _difference;
    //    }

    //    position.y = _cameraRig.position.y;
    //    position.x = Mathf.Clamp(position.x, _limitX.x, _limitX.y);
    //    position.z = Mathf.Clamp(position.z, -_limitZ.x, _limitZ.y);
    //    _cameraRig.position = position;
    //}
}
