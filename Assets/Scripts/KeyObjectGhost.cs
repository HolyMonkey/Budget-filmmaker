using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyObjectGhost : MonoBehaviour
{
    [SerializeField] private Animator _ghostAnimator;
    [SerializeField] private string _ghostDisappearAnimationTrigger;
    [SerializeField] private Animator _animator;
    [SerializeField] private string _disappearAnimationTrigger;
    [SerializeField] private float _animationDelay;
    [SerializeField] private ParticleSystem _starEffect;
    [SerializeField] private ParticleSystem _successEffectTemplate;
    [SerializeField] private float _successDelay;

    public void Disappear()
    {
        StartCoroutine(WaitForEndOfDisappear());
    }

    public void FullyDisappear()
    {
        StartCoroutine(WaitForEndOfFullyDisappear());
    }

    private IEnumerator WaitForEndOfDisappear()
    {
        //_ghostAnimator.SetTrigger(_ghostDisappearAnimationTrigger);
        //yield return new WaitForSeconds(_animationDelay);
        //Destroy(_ghostAnimator.gameObject);

        //ParticleSystem successEffect = Instantiate(_successEffectTemplate, _starEffect.transform.position, _successEffectTemplate.transform.rotation);
        ParticleSystem successEffect = Instantiate(_successEffectTemplate, transform);
        yield return new WaitForSeconds(_successDelay);
        Destroy(_ghostAnimator.gameObject);
        Destroy(_starEffect.gameObject);
        yield return new WaitForSeconds(_successEffectTemplate.main.duration);
        Destroy(gameObject);
    }

    private IEnumerator WaitForEndOfFullyDisappear()
    {
        _animator.SetTrigger(_disappearAnimationTrigger);
        yield return new WaitForSeconds(_animationDelay);
        Destroy(gameObject);
    }
}
