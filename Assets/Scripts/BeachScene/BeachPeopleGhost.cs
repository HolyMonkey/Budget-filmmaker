using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeachPeopleGhost : KeyObjectGhost
{
    protected override IEnumerator WaitForEndOfDisappear()
    {
        yield return new WaitForSeconds(SuccessDelay);
        Destroy(GhostAnimator.gameObject);
        Destroy(gameObject);
    }

    protected override IEnumerator WaitForEndOfFullyDisappear()
    {
        Animator.SetTrigger(DisappearAnimationTrigger);
        yield return new WaitForSeconds(AnimationDelay);
        Destroy(gameObject);
    }
}
