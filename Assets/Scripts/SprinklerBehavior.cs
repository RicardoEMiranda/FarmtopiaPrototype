using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprinklerBehavior : MonoBehaviour
{
    [SerializeField] private ParticleSystem _smallSpray_ps;
    [SerializeField] private ParticleSystem _largeSpray_ps;

    [Range(0, 5.0f)]
    [SerializeField] private float _rotateSpeed = 1.0f;
    [SerializeField] private bool _rotate = true;
    [SerializeField] private bool _reverse = false;
    [Space(10)]
    [SerializeField] private bool _smallSpray = true;
    [SerializeField] private bool _largeSpray = false;
    [Tooltip("Set spray ")]
    [SerializeField] private bool _setSpray = false;
    [Space]
    private bool _canSpray = true;
    private void Awake()
    {
        CheckForNull();
        SetSpray();
    }
    void FixedUpdate()
    {
        if (_setSpray)
            SetSpray();
        if (_rotate && _canSpray)
        {
            float rotateDirection = _reverse ? -1 : 1;
            transform.Rotate(0, rotateDirection * 10 * _rotateSpeed * Time.fixedDeltaTime, 0);
        }
    }
    
    private void SetSpray()
    {
        _setSpray = false;
        if (_smallSpray || (_smallSpray && _largeSpray))
        {
            _canSpray = true;
            _largeSpray = false;
            _smallSpray_ps.Play();
            _largeSpray_ps.Stop();
        }
        else if (_largeSpray)
        {
            _canSpray = true;
            _smallSpray = false;
            _smallSpray_ps.Stop();
            _largeSpray_ps.Play();
        }
        else
        {
            _canSpray = false;
            _smallSpray = false;
            _largeSpray = false;
            _smallSpray_ps.Stop();
            _largeSpray_ps.Stop();
        }
    }

    private void CheckForNull()
    {
        if (!_smallSpray_ps || !_largeSpray_ps)
        {
            gameObject.SetActive(false);
            Debug.Log("Spray is null" + transform.name + transform);
        }
    }
}
