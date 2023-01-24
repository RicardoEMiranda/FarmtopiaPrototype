using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnComplete_Particles : MonoBehaviour
{
    private ParticleSystem _ps;

    void Awake()
    {
        //check if transform has children else debug.logError
        if (transform.childCount > 0)
        if (transform.GetChild(0).TryGetComponent(out _ps))
        {
            _ps.Play();
        }
        else
        {
            Debug.LogError("No ParticleSystem found on this GameObject");
            gameObject.SetActive(false);
        }
    }
    private void OnEnable()
    {
        //check if particle sytem is not null
        if(_ps)
        {
            _ps.Play();
            _ = StartCoroutine(nameof(DisableAfterDelay));
        }
    }
    
    IEnumerator DisableAfterDelay()
    {
        yield return new WaitForSeconds(_ps.main.duration);
        gameObject.SetActive(false);
    }
}
