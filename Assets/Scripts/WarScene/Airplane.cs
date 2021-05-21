using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airplane : KeyObject
{
    [SerializeField] private AirplaneScene _airplaneScene;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent(out BombPlace place))
        {
            _airplaneScene.ThrowBomb();
        }
    }
}
