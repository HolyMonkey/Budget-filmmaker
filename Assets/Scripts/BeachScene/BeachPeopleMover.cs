using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeachPeopleMover : KeyObjectMover
{
    [SerializeField] private BeachMan[] _beachMen;

    protected override void TryMove()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                if (hitInfo.transform.TryGetComponent(out BeachPeople people))
                {
                    IsDragging = true;
                    StartDragging();
                }
            }
        }

        if (Input.GetMouseButtonUp(0) && IsDragging)
        {
            IsDragging = false;
            IsCloseToTarget();
            EndDragging();
        }

        if (Input.GetMouseButton(0) && IsDragging)
        {
            float planeY = transform.position.y;
            Plane plane = new Plane(Vector3.up, Vector3.up * planeY);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(ray, out float distance))
            {
                Vector3 targetPoint = ray.GetPoint(distance);
                targetPoint.y = transform.position.y;
                targetPoint.z = transform.position.z;
                bool isMoving = targetPoint.x != transform.position.x;
                bool isMovingTowards = targetPoint.x > transform.position.x;
                transform.position = Vector3.MoveTowards(transform.position, targetPoint, DraggingSpeed * Time.deltaTime);

                if (isMoving)
                {
                    foreach (BeachMan man in _beachMen)
                    {
                        Vector3 manTargetPosition = isMovingTowards ? man.Ghost.transform.position : man.StartPosition;
                        man.transform.position = Vector3.MoveTowards(man.transform.position, manTargetPosition, DraggingSpeed * Time.deltaTime);
                    }
                }
            }
        }
    }

    protected override void OnPointerDown(Vector2 mousePosition)
    {
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //if (Physics.Raycast(ray, out RaycastHit hitInfo))
        //{
        //    if (hitInfo.transform.TryGetComponent(out Airplane airplane))
        //    {
        //        IsDragging = true;
        //        StartDragging();
        //    }
        //}
    }

    protected override void IsCloseToTarget()
    {
        if (Vector3.Distance(_beachMen[0].Ghost.transform.position, _beachMen[0].transform.position) <= MinDistanceToTarget)
        {
            IsTargetReached = true;
            ReachTarget();
        }
        else
        {
            transform.position = StartPosition;

            foreach (BeachMan man in _beachMen)
            {
                man.transform.position = man.StartPosition;
            }
        }
    }
}
