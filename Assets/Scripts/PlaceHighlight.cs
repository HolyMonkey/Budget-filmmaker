using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceHighlight : MonoBehaviour
{
    [SerializeField] private Vector3 _speed;

    private void Update()
    {
        transform.Rotate(_speed * Time.deltaTime);
    }
}
