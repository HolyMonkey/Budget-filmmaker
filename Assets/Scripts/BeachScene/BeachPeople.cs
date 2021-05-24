using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeachPeople : KeyObject
{
    [SerializeField] private BeachMan[] _beachMen;
    [SerializeField] private string _beachManFloatingAnimationTrigger;

    protected override IEnumerator SuccessMovementRoutine()
    {
        while (transform.position != ObjectGhost.transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, ObjectGhost.transform.position, PositionAdjustmentSpeed * Time.deltaTime);
            
            foreach (BeachMan man in _beachMen)
            {
                man.transform.position = Vector3.MoveTowards(man.transform.position, man.Ghost.transform.position, PositionAdjustmentSpeed * Time.deltaTime);
            }

            yield return null;
        }

        IsInCorrectPlace = true;

        foreach (BeachMan man in _beachMen)
        {
            man.Ghost.Disappear();
            man.Animator.SetTrigger(_beachManFloatingAnimationTrigger);
        }

        ObjectGhost.Disappear();
    }
}
