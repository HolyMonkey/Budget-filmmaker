using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPlace : MonoBehaviour
{
    [SerializeField] private KeyObject _keyObject;
    [SerializeField] private KeyObject _requiredKeyObject;
    [SerializeField] private ParticleSystem _highlightEffect;
    [SerializeField] private Material _rightPlaceMaterial;
    [SerializeField] private Material _wrongPlaceMaterial;

    private ParticleSystemRenderer _highlightEffectRenderer;
    private ParticleSystem.MainModule _highlightEffectMainModule;

    private void Awake()
    {
        _highlightEffectRenderer = _highlightEffect.GetComponent<ParticleSystemRenderer>();
        _highlightEffectMainModule = _highlightEffect.main;
    }

    private void Start()
    {
        if (_keyObject != null)
        {
            ChangeHighlight();
        }
    }

    private void ChangeHighlight()
    {
        _highlightEffect.Stop();
        if (_keyObject == _requiredKeyObject)
        {
            _highlightEffectMainModule.startColor = Color.white;
            _highlightEffectRenderer.material = _rightPlaceMaterial;
        }
        else
        {
            _highlightEffectMainModule.startColor = Color.red;
            _highlightEffectRenderer.material = _wrongPlaceMaterial;
        }
        _highlightEffect.Play();
    }
}
