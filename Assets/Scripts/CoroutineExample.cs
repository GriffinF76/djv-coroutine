using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
public class CoroutineExample : MonoBehaviour
{
    private Renderer _renderer;
    private Rigidbody _rigidbody;

    private Color _startColor;
    private Vector3 _startPosition;
    private Quaternion _startRotation;
    private Coroutine _resetAnimationCoroutine;

    protected void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();

        _startColor = _renderer.material.color;
        _startPosition = transform.position;
        _startRotation = transform.rotation;
    }

    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && _resetAnimationCoroutine == null)
        {
            _resetAnimationCoroutine = StartCoroutine(ResetAnimationCoroutine());
        }

        if (Input.GetKeyDown(KeyCode.Z) && _resetAnimationCoroutine != null)
        {
            StopCoroutine(_resetAnimationCoroutine);
            _renderer.material.color = _startColor;
            _rigidbody.isKinematic = false;
            _resetAnimationCoroutine = null;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            _rigidbody.AddExplosionForce(500f, transform.position + 0.5f * Vector3.down, 1f);
        }
    }

    private IEnumerator ResetAnimationCoroutine()
    {
        _rigidbody.isKinematic = true;
        _renderer.material.color = Color.yellow;

        var currentPosition = transform.position;
        var currentRotation = transform.rotation;

        const float animationDuration = 4f;
        var animationTimeLeft = animationDuration;
        while (animationTimeLeft > 0f)
        {
            var normalizedTime = 1f - animationTimeLeft / animationDuration;
            transform.position = Vector3.Lerp(currentPosition, _startPosition, normalizedTime);
            transform.rotation = Quaternion.Lerp(currentRotation, _startRotation, normalizedTime);

            animationTimeLeft -= Time.deltaTime;
            yield return null;
        }

        _renderer.material.color = _startColor;
        transform.position = _startPosition;
        transform.rotation = _startRotation;

        _rigidbody.isKinematic = false;
        _resetAnimationCoroutine = null;
    }

    protected void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"collision: {collision.relativeVelocity}");
    }
}
