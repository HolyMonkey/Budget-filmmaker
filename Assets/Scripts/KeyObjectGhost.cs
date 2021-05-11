using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyObjectGhost : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private string _disappearAnimationTrigger;
    [SerializeField] private float _animationDelay;
    [SerializeField] private ParticleSystem _starEffect;
    [SerializeField] private ParticleSystem _successEffectTemplate;
    [SerializeField] private float _successDelay;

    public void Disappear()
    {
        //_animator.SetTrigger(_disappearAnimationTrigger);
        StartCoroutine(WaitForEndOfDisappear());
    }

    //public void Destroy()
    //{
    //    Destroy(_animator.gameObject);
    //}

    private IEnumerator WaitForEndOfDisappear()
    {
        _animator.SetTrigger(_disappearAnimationTrigger);
        yield return new WaitForSeconds(_animationDelay);
        Destroy(_animator.gameObject);

        ParticleSystem successEffect = Instantiate(_successEffectTemplate, _starEffect.transform.position, _successEffectTemplate.transform.rotation);
        yield return new WaitForSeconds(_successDelay);
        Destroy(_starEffect.gameObject);
        yield return new WaitForSeconds(_successEffectTemplate.main.duration);
        Destroy(gameObject);
    }
}
