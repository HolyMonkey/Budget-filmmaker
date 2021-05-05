using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyObjectsSwapper : MonoBehaviour
{
    private KeyPlace _choosedKeyPlace;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                if (hitInfo.transform.TryGetComponent(out KeyPlace keyPlace))
                {
                    if (_choosedKeyPlace != null)
                    {
                        if (_choosedKeyPlace != keyPlace)
                        {
                            keyPlace.Deselect();
                            _choosedKeyPlace.Deselect();
                            KeyObject objectToSwap = _choosedKeyPlace.KeyObject;
                            _choosedKeyPlace.ChangeObject(keyPlace.KeyObject);
                            keyPlace.ChangeObject(objectToSwap);
                            _choosedKeyPlace = null;
                        }
                        else
                        {
                            _choosedKeyPlace.Deselect();
                            _choosedKeyPlace = null;
                        }
                    }
                    else
                    {
                        keyPlace.Select();
                        _choosedKeyPlace = keyPlace;
                    }
                }
            }
            //else
            //{
            //    if (_choosedKeyPlace != null)
            //    {
            //        _choosedKeyPlace.Deselect();
            //    }
            //    _choosedKeyPlace = null;
            //}
        }
    }
}
