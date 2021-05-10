using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class KeyObjectMover : MonoBehaviour
{
    private void Update()
    {
        
    }

    protected abstract void TryMove();
}
