using System.Collections;
using UnityEngine;
using Zenject;

public class CameraFollower : MonoBehaviour
{
    
    [SerializeField] private Vector3 _forwardDiraction;
    [SerializeField] private float _speed;
    [SerializeField] private float _angle;
    [SerializeField] private float _distanceLandScape;
    [SerializeField] private float _distancePortrait;
    [SerializeField] private float _maxVectorLength = 2;
    [SerializeField] private float _delayBeforeFollow;

    private Vector3 _nextPosition;
    private bool _follow = false;
    private Rigidbody _defaultTarget;
    private Rigidbody _target;
    
    [Inject]
    public void Construct(Player player)
    {
        _target = player.TargetRigitbody;
    }

    private void Start()
    {
        _defaultTarget = _target;
        UpdateRotation();
        _follow = true;
    }

    private void FixedUpdate()
    {
        if (_follow)
        {
            float distance = _distancePortrait;
            _nextPosition = _target.position + Vector3.ClampMagnitude(_target.velocity, _maxVectorLength);
            _nextPosition += Vector3.forward * Mathf.Sin(Mathf.Deg2Rad * _angle) * distance;
            _nextPosition += -_forwardDiraction * Mathf.Cos(Mathf.Deg2Rad * _angle) * distance;

            transform.position = Vector3.Lerp(transform.position, _nextPosition, _speed * Time.fixedDeltaTime);             
            
            UpdateRotation();
        }
    }

    private void UpdateRotation()
    {
        float rotationY = Mathf.Rad2Deg * Mathf.Asin(_forwardDiraction.z / _forwardDiraction.magnitude);
        transform.rotation = Quaternion.Euler(_angle, rotationY, transform.rotation.eulerAngles.z);
    }

    private IEnumerator DelayBeforeFollow()
    {
        yield return new WaitForSeconds(_delayBeforeFollow);
        _follow = true;
        UpdateRotation();
    }

    public void StopFollow() { _follow = false; }
    
    //public void LookAtTargetAndBack()
    //{
        //StartCoroutine(DoLookAtTargetAndBack(, 3f));
    //{

    private IEnumerator DoLookAtTargetAndBack(Rigidbody target, float time)
    {
        yield return new WaitForSeconds(time / 4);
        _target = target;
        yield return new WaitForSeconds(time / 2 + time / 4);
        _target = _defaultTarget;
    }
}
