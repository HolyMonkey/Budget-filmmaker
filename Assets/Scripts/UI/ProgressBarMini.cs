using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider), typeof(Animator))]
public class ProgressBarMini : MonoBehaviour
{
    [SerializeField] private float _fillDuration;
    [SerializeField] private string _getBiggerAnimationTrigger;
    [SerializeField] private ParticleSystem _fillingEndEffectTemplate;

    private Slider _slider;
    private Animator _animator;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _animator = GetComponent<Animator>();
    }

    public void Fill()
    {
        StartCoroutine(WaitForEndOfFill());
    }

    private IEnumerator WaitForEndOfFill()
    {
        float passedTime = 0;
        float startValue = _slider.value;

        while (passedTime < _fillDuration)
        {
            _slider.value = Mathf.Lerp(startValue, _slider.maxValue, passedTime / _fillDuration);
            passedTime += Time.deltaTime;
            yield return null;
        }

        _slider.value = _slider.maxValue;
        
        _animator.SetTrigger(_getBiggerAnimationTrigger);

        ParticleSystem fillingEndEffect = Instantiate(_fillingEndEffectTemplate, transform);
        yield return new WaitForSeconds(fillingEndEffect.main.duration);
        Destroy(fillingEndEffect.gameObject);
    }
}
