using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class ExhaustBehavior : MonoBehaviour
{
    [SerializeField]
    private bool _isOn = true;
    [SerializeField] [Range(0.0f, 1.0f)]
    private float _throttle = 0f;
    [Space(25)]
    
    [SerializeField] [Range(0.0f, 2.0f)]
    private float _exhaustSpeedMin = 1.0f;
    [SerializeField] [Range(0.0f, 2.0f)]
    private float _exhaustSpeedMax = 1;
    private float _exhaustSpeed = 0;
    [Space]
    
    private ParticleSystem _ps;
    private ParticleSystem.MainModule _psMain;
    private ParticleSystem.ColorOverLifetimeModule _exhaustColor;
    [Space(25)]
    
    [SerializeField] private Gradient _exhaustLight;
    [SerializeField] private Gradient _exhaustMid;
    [SerializeField] private Gradient _exhaustHeavy;
    [Space(20)]

    [SerializeField] private bool _isLight = true;
    [SerializeField] private bool _isMid = false;
    [SerializeField] private bool _isHeavy = false;


    void Start()
    {
        _ps = GetComponentInChildren<ParticleSystem>();
        _exhaustColor = _ps.colorOverLifetime;
        _psMain = _ps.main;
    }
    void FixedUpdate()
    {
        if (_isOn != _ps.isPlaying)
            PlayPause();

        //call SetGradient if any state change from last frame
        if (_isLight || _isMid || _isHeavy)
            SetGradient();

        //_exhaustSpeed between 2 constants
        _exhaustSpeed = Mathf.Lerp(_exhaustSpeedMin, _exhaustSpeedMax, _throttle);
        _psMain.startSpeed = _exhaustSpeed;
    }
    
    private void SetGradient()
    {
        if (_isLight)
        {
            _exhaustColor.color = _exhaustLight;
            _isLight = false;
        }
        if (_isMid)
        {
            _exhaustColor.color = _exhaustMid;
            _isMid = false;
        }
        if (_isHeavy)
        {
            _exhaustColor.color = _exhaustHeavy;
            _isHeavy = false;
        }
    }
    
    private void PlayPause()
    {
        if (!_isOn) _ps.Stop();
        else _ps.Play();
    }
}
