using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SmartphoneController : MonoBehaviour
{
    [SerializeField]
    private InputManager _inputManager;
    [SerializeField]
    private GameObject _smartphoneGameObject;
    [SerializeField]
    private float _jumpPower = 5f;
    [SerializeField]
    private TrailRenderer _trail;
    [SerializeField]
    private ParticleSystem _jetParticle;
    [SerializeField]
    private ParticleSystem _sparkParticle;
    [SerializeField]
    private  ParticleSystem _bombParticle;
    
    private Rigidbody _rigidbody;
    private Vector3 _touchStartPosition;

    private readonly float _rigidbodyStopThreshold = 0.01f;
    private readonly Vector3 _raiseHeight = new Vector3(0f, 0.06915f, 0f);

    enum ShootStatus
    {
        READY,
        AIM,
        MOVE,
    }

    private ShootStatus _shootStatus;

    void Start()
    {
        _rigidbody = _smartphoneGameObject.GetComponent<Rigidbody>();
        _shootStatus = ShootStatus.READY;
        
        _trail.enabled = false;
        _jetParticle.Stop();
        _sparkParticle.Stop();
        _bombParticle.Stop();
    }

    void Update()
    {
        MoveCheck();

        TransformUpdate();

        EffectUpdate();
    }

    private void MoveCheck()
    {
        if (_shootStatus != ShootStatus.MOVE)
            return;

        if(_rigidbody.velocity.magnitude <= _rigidbodyStopThreshold)
            _shootStatus = ShootStatus.READY;
    }

    private void TransformUpdate()
    {
        if (_shootStatus == ShootStatus.READY)
        {
            if (IsTouch(TouchPhase.Began))
            {
                _touchStartPosition = _smartphoneGameObject.transform.position + _raiseHeight;
                _rigidbody.isKinematic = true;
                _shootStatus = ShootStatus.AIM;
            }
        }
        else if (_shootStatus == ShootStatus.AIM)
        {
            if (IsTouch())
            {
                _smartphoneGameObject.transform.position = _touchStartPosition;
                _smartphoneGameObject.transform.rotation = _inputManager.GyroQuaternion;
            }

            if (IsTouch(TouchPhase.Ended))
            {
                _rigidbody.isKinematic = false;
                _rigidbody.AddForce(_smartphoneGameObject.transform.forward * _jumpPower, ForceMode.Impulse);
                _shootStatus = ShootStatus.MOVE;
            }

            if (IsTouch(TouchPhase.Canceled))
            {
                _rigidbody.isKinematic = false;
                _shootStatus = ShootStatus.MOVE;
            }
        }
    }

    private void EffectUpdate()
    {
        switch (_shootStatus)
        {
            case ShootStatus.MOVE:
                if(_trail.enabled == false)
                    _trail.enabled = true;
                if(_jetParticle.isPlaying == false)
                    _jetParticle.Play();
                if(_sparkParticle.isPlaying)
                    _sparkParticle.Stop();
                if(_bombParticle.isPlaying == false)
                    _bombParticle.Play();
                break;
            case ShootStatus.AIM:
                if (_sparkParticle.isPlaying  == false)
                    _sparkParticle.Play();
                break;
            case ShootStatus.READY:
                if (_trail.enabled)
                    _trail.enabled = false;
                if (_jetParticle.isPlaying)
                    _jetParticle.Stop();
                if (_sparkParticle.isPlaying)
                    _sparkParticle.Stop();
                if (_bombParticle.isPlaying)
                    _bombParticle.Stop();
                break;
        }
    }

    private bool IsTouch(TouchPhase touchPhase)
    {
        return Input.touches.Any(touch => touch.phase == touchPhase);
    }

    private bool IsTouch()
    {
        foreach (var touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began ||
                touch.phase == TouchPhase.Moved ||
                touch.phase == TouchPhase.Stationary)
            {
                return true;
            }
        }
        return false;
    }
}
