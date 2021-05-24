using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierScene : ActionScene
{
    [SerializeField] private Bomb _bomb;
    [SerializeField] private float _explosionImpactDelay;
    [SerializeField] private GameObject _explosionGroundImpact;
    [SerializeField] private Animator _explosionImpactAnimator;
    [SerializeField] private string _explosionImpactAppearAnimationTrigger;
    [SerializeField] private Soldier _soldier;
    [SerializeField] private Animator _soldierAnimator;
    [SerializeField] private string _soldierDieAnimationTrigger;
    [SerializeField] private ParticleSystem _explosionEffectTemplate;

    private void OnEnable()
    {
        _bomb.ExplosionHappened += OnExplosionHappened;
    }

    private void OnDisable()
    {
        _bomb.ExplosionHappened -= OnExplosionHappened;
    }

    public override void Run()
    {
        Debug.Log("Start soldier scene");
        _bomb.Rigidbody.isKinematic = false;
    }

    private void OnExplosionHappened()
    {
        StartCoroutine(ExplosionImpactRoutine());
    }

    private IEnumerator ExplosionImpactRoutine()
    {
        if (_soldier.IsObjectInCorrectPlace == false)
        {
            _soldierAnimator.SetTrigger(_soldierDieAnimationTrigger);
            yield return new WaitForSeconds(_explosionImpactDelay);
            yield return new WaitForSeconds(_soldierAnimator.GetCurrentAnimatorClipInfo(0).Length);
        }
        else
        {
            yield return new WaitForSeconds(_explosionImpactDelay);
            yield return new WaitForSeconds(_explosionEffectTemplate.main.duration);
        }
        
        CompleteScene();

        //_explosionGroundImpact.SetActive(true);
        //_explosionImpactAnimator.SetTrigger(_explosionImpactAppearAnimationTrigger);
    }
}
