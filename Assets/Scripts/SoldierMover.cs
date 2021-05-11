using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierMover : KeyObjectMover
{
    [SerializeField] private MeshCollider _islandCollider;

    protected override void TryMove()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                if (hitInfo.transform.TryGetComponent(out Soldier soldier))
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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (_islandCollider.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity))
            {
                transform.position = hitInfo.point;
            }
        }
    }
}
