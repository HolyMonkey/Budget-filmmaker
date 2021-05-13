using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class ActionScene : MonoBehaviour
{
    public event UnityAction ActionSceneCompleted;

    public abstract void Run();

    protected void CompleteScene()
    {
        Debug.Log("Scene completed");
        ActionSceneCompleted?.Invoke();
    }
}
