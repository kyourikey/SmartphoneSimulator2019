using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetChaser : MonoBehaviour
{
    [SerializeField]
    GameObject _target;
    [SerializeField]
    GameObject _camera;
    [SerializeField]
    float _chaseSpeedSmooth = 0.05f;

    private Vector3 _distance;

    void Start()
    {
        _distance = _camera.transform.position - _target.transform.position;
    }

    void Update()
    {
        var targetPos = _target.transform.position + _distance;
        var nowSpeed = Vector3.zero;
        var pos = Vector3.SmoothDamp(_camera.transform.position, targetPos, ref nowSpeed, _chaseSpeedSmooth);

        _camera.transform.position = pos;
    }
}
