using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AirplaneScene : ActionScene
{
    [SerializeField] private Airplane _airplane;
    [SerializeField] private Animator _airplaneAnimator;
    [SerializeField] private string _flyingAnimationTrigger;
    [SerializeField] private float _finalPositionX;
    [SerializeField] private float _speed;
    [SerializeField] private Bomb _airplaneBombTemplate;
    //[SerializeField] private float _delayBeforeStartThrowing;
    //[SerializeField] private float _delayBetweenBombs;
    //[SerializeField] private float _bombsThrowingFinishX;
    [SerializeField] private BombPlace[] _bombPlaces;
    [SerializeField] private float _lastBombImpactDelay;
    [SerializeField] private FilmCrewUnderPlane _filmCrewUnderPlane;
    [SerializeField] private ParticleSystem _filmCrewDeathEffectTemplate;

    private bool _shouldAirplaneFly;
    private int _bombsCount;
    //private bool _shouldThrowBombs;
    //private float _timeToNextBomb;

    public override void Run()
    {
        Debug.Log("Start airplane scene");

        _shouldAirplaneFly = true;
        _airplaneAnimator.SetTrigger(_flyingAnimationTrigger);
        //StartCoroutine(SceneRoutine());
    }

    public void Update()
    {
        if (_shouldAirplaneFly && _airplane != null)
        {
            //if (_shouldThrowBombs)
            //{
            //    if (_timeToNextBomb >= _delayBetweenBombs)
            //    {
            //        Instantiate(_airplaneBombTemplate, _airplane.transform.position, _airplaneBombTemplate.transform.rotation);
            //        _timeToNextBomb = 0;
            //    }

            //    _timeToNextBomb += Time.deltaTime;
            //}

            Vector3 finalPosition = _airplane.transform.position;
            finalPosition.x = _finalPositionX;
            _airplane.transform.position = Vector3.MoveTowards(_airplane.transform.position, finalPosition, Time.deltaTime * _speed);
            
            if (_airplane.transform.position == finalPosition)
            {
                Destroy(_airplane.gameObject);
                _airplane = null;
                CompleteScene();
            }
        }
    }

    public void ThrowBomb()
    {
        Instantiate(_airplaneBombTemplate, _airplane.transform.position, _airplaneBombTemplate.transform.rotation);
        _bombsCount++;
        if (_bombsCount == _bombPlaces.Length)
        {
            if (_airplane.IsInCorrectPlace == false)
            {
                StartCoroutine(LastBombImpactRoutine());
            }
        }
    }

    private IEnumerator LastBombImpactRoutine()
    {
        yield return new WaitForSeconds(_lastBombImpactDelay);
        _filmCrewUnderPlane.gameObject.SetActive(false);
        ParticleSystem deathEffect = Instantiate(_filmCrewDeathEffectTemplate, _filmCrewDeathEffectTemplate.transform.position, _filmCrewDeathEffectTemplate.transform.rotation);
        yield return new WaitForSeconds(_filmCrewDeathEffectTemplate.main.duration);
        Destroy(deathEffect.gameObject);
    }

    //private IEnumerator SceneRoutine()
    //{
    //    _shouldAirplaneFly = true;
    //    _airplaneAnimator.SetTrigger(_flyingAnimationTrigger);
    //    yield return new WaitForSeconds(_delayBeforeStartThrowing);
    //    _shouldThrowBombs = true;

    //    while (_airplane.transform.position.x > _bombsThrowingFinishX)
    //    {
    //        yield return null;
    //    }

    //    _shouldThrowBombs = false;
    //}
}
