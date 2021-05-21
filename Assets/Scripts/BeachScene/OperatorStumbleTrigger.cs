using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperatorStumbleTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Operator manOperator))
        {
            manOperator.Stumble();
        }
    }
}
