using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyObjectGhost : MonoBehaviour
{
    [SerializeField] private string _ghostDisappearAnimationTrigger;
    [SerializeField] private ParticleSystem _successEffectTemplate;
    [SerializeField] protected Animator GhostAnimator;
    [SerializeField] protected Animator Animator;
    [SerializeField] protected string DisappearAnimationTrigger;
    [SerializeField] protected float AnimationDelay;
    [SerializeField] protected float SuccessDelay;

    public void Disappear()
    {
        StartCoroutine(WaitForEndOfDisappear());
    }

    public void FullyDisappear()
    {
        StartCoroutine(WaitForEndOfFullyDisappear());
    }

    protected virtual IEnumerator WaitForEndOfDisappear()
    {
        ParticleSystem successEffect = Instantiate(_successEffectTemplate, transform);
        yield return new WaitForSeconds(SuccessDelay);
        Destroy(GhostAnimator.gameObject);
        yield return new WaitForSeconds(_successEffectTemplate.main.duration);
        Destroy(gameObject);
    }

    protected virtual IEnumerator WaitForEndOfFullyDisappear()
    {
        Animator.SetTrigger(DisappearAnimationTrigger);
        yield return new WaitForSeconds(AnimationDelay);
        Destroy(gameObject);
    }
}
