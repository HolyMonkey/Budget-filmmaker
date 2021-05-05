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
    [SerializeField] private ParticleSystem _appearEffect;
    [SerializeField] private Transform _appearEffectSpawnPoint;
    [SerializeField] private float _appearDelay;

    private ParticleSystemRenderer _highlightEffectRenderer;
    private ParticleSystem.MainModule _highlightEffectMainModule;

    public KeyObject KeyObject => _keyObject;

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

    public void ChangeObject(KeyObject keyObject)
    {
        StartCoroutine(WaitForEndOfAppear(keyObject));
    }

    public void Select()
    {
        _keyObject.Select();
    }

    public void Deselect()
    {
        _keyObject.Deselect();
    }

    private IEnumerator WaitForEndOfAppear(KeyObject keyObject)
    {
        ParticleSystem appearEffect = Instantiate(_appearEffect, _appearEffectSpawnPoint.position, _appearEffect.transform.rotation);
        yield return new WaitForSeconds(_appearDelay);

        keyObject.transform.position = transform.position;
        keyObject.transform.rotation = transform.rotation;
        _keyObject = keyObject;
        ChangeHighlight();
        yield return new WaitForSeconds(appearEffect.main.duration);

        Destroy(appearEffect.gameObject);
    }
}
