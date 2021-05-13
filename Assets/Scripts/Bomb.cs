using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Bomb : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosionEffectTemplate;
    [SerializeField] private GameObject _bombModel;
    [SerializeField] private float _bombDestroyDelay;

    private Rigidbody _rigidbody;

    public Rigidbody Rigidbody => _rigidbody;
    public event UnityAction ExplosionHappened;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.transform.TryGetComponent(out Ground ground))
        //{
            StartCoroutine(WaitForEndOfExplosion());
            ExplosionHappened?.Invoke();
        //}
    }

    private IEnumerator WaitForEndOfExplosion()
    {
        ParticleSystem explosionEffect = Instantiate(_explosionEffectTemplate, transform.position, _explosionEffectTemplate.transform.rotation);
        yield return new WaitForSeconds(_bombDestroyDelay);
        Destroy(_bombModel);
        yield return new WaitForSeconds(_explosionEffectTemplate.main.duration);
        Destroy(explosionEffect.gameObject);
        Destroy(gameObject);
    }
}
