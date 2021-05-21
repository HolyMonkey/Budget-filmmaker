using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class ActionsDemonstrator : MonoBehaviour
{
    public event UnityAction ActionStarted;
    public event UnityAction<bool> AllActionsCompleted;

    protected void OnActionStarted()
    {
        ActionStarted?.Invoke();
    }

    protected void OnAllActionsCompleted(bool isAllObjectsAtCorrectPlaces)
    {
        AllActionsCompleted?.Invoke(isAllObjectsAtCorrectPlaces);
    }
}
