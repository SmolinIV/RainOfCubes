using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    private Renderer _renderer;
    private Color _defaultColor;

    private Coroutine _disappearance;

    private int _minLiveTime = 2;
    private int _maxLiveTime = 5;
    private int _liveTime;

    private bool _isAlreadyGrounded = false;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _defaultColor = _renderer.material.color;
    }

    private void OnDisable()
    {
        if (_disappearance != null)
            StopCoroutine(_disappearance);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isAlreadyGrounded) 
            return;

        if (collision.collider.TryGetComponent(out Platform component))
        {
            _isAlreadyGrounded = true;
            ChangeParamsAfterTouch();
            _disappearance = StartCoroutine(DisappearWithDelay());
        }
    }

    private void ChangeParamsAfterTouch()
    {
        _liveTime = Random.Range(_minLiveTime, _maxLiveTime);
        _renderer.material.color = Random.ColorHSV();
    }

    private void ResetCondition()
    {
        gameObject.SetActive(false);

        _renderer.material.color = _defaultColor;
        _isAlreadyGrounded = false;
    }

    private IEnumerator DisappearWithDelay()
    {
        yield return new WaitForSeconds(_liveTime);

        ResetCondition();
    }
}
