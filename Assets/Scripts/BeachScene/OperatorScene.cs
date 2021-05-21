using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperatorScene : ActionScene
{
    [SerializeField] private Operator _operator;
    [SerializeField] private Animator _operatorAnimator;
    [SerializeField] private float _rotationTime;
    [SerializeField] private string _operatorWalkAnimationTrigger;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private Transform _targetPoint;
    [SerializeField] private string _operatorStumbleAnimationTrigger;
    [SerializeField] private float _timeToFall;
    [SerializeField] private float _timeToCameraLand;
    [SerializeField] private TVCamera _tvCamera;
    [SerializeField] private ParticleSystem _electricityEffectTemplate;
    [SerializeField] private ParticleSystem _smokeEffectTemplate;

    public override void Run()
    {
        Debug.Log("Start soldier scene");
        if (_operator.IsInCorrectPlace == false)
        {
            StartCoroutine(FailRoutine());
        }
        else
        {
            CompleteScene();
        }
    }


    private IEnumerator FailRoutine()
    {
        float passedTime = 0;
        Quaternion startRotation = _operator.transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0, -90, 0);

        while (passedTime < _rotationTime)
        {
            _operator.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, passedTime / _rotationTime);
            passedTime += Time.deltaTime;
            yield return null;
        }

        passedTime = 0;
        _operatorAnimator.SetTrigger(_operatorWalkAnimationTrigger);

        while (passedTime < _timeToFall)
        {
            _operator.transform.position = Vector3.MoveTowards(_operator.transform.position, _targetPoint.position, _walkSpeed * Time.deltaTime);
            passedTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(_timeToCameraLand);
        ParticleSystem electricityEffect = Instantiate(_electricityEffectTemplate, _tvCamera.transform.position, _electricityEffectTemplate.transform.rotation);
        foreach (Transform spawnPoint in _tvCamera.SmokeSpawnPositions)
        {
            ParticleSystem smokeEffect = Instantiate(_smokeEffectTemplate, spawnPoint.position, _smokeEffectTemplate.transform.rotation);
        }
        yield return new WaitForSeconds(electricityEffect.main.duration);

        CompleteScene();
    }
}
