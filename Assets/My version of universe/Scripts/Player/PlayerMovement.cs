using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private enum ControlMode
    {
        /// <summary>
        /// Up moves the character forward, left and right turn the character gradually and down moves the character backwards
        /// </summary>
        Tank,
        /// <summary>
        /// Character freely moves in the chosen direction from the perspective of the camera
        /// </summary>
        Direct
    }

    [SerializeField] private Animator _animator;
    [SerializeField] private ControlMode _controlMode = ControlMode.Direct;
    [SerializeField] private float _moveSpeed = 2;

    public void Construct(SimpleTouchController touchController)
    {
        _touchController = touchController;
    }

    [SerializeField] private float _turnSpeed = 200;
    [SerializeField] private float _jumpForce = 4;
    [Header("Swimming")]
    [SerializeField] private AnimationCurve _curveJumpOutOfWater;
    [SerializeField] private float _swimmingHeight;

    private SimpleTouchController _touchController;

    private Rigidbody _rigidbody;

    private float _currentV = 0;
    private float _currentH = 0;

    private readonly float _interpolation = 10;
    private readonly float _walkScale = 0.33f;
    private readonly float _backwardsWalkScale = 0.16f;
    private readonly float _backwardRunScale = 0.66f;

    private bool _wasGrounded;
    private Vector3 _currentDirection = Vector3.zero;

    private float _jumpTimeStamp = 0;
    private float _minJumpInterval = 0.25f;
    private bool _jumpInput = false;

    private bool _isGrounded = true;

    private PlayerState _currentState;
    private bool _autoMove = false;
    private Vector3 _contactedPlatformPosition;
    private float _amimTime;
    private Vector3 _jumpOutOfWaterStartPos;

    private List<Collider> _collisions = new List<Collider>();

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _currentState = PlayerState.OnLand;
        _animator.SetBool("Grounded", _isGrounded);
    }

    private void FixedUpdate()
    {
        //_animator.SetBool("Grounded", _isGrounded);

        if (!_autoMove)
            switch (_controlMode)
            {
                case ControlMode.Direct:
                    DirectUpdate();
                    break;

                case ControlMode.Tank:
                    TankUpdate();
                    break;

                default:
                    Debug.LogError("Unsupported state");
                    break;
            }
        else
            CurveUpdate();

        //_wasGrounded = _isGrounded;
        //_jumpInput = false;

        //_rigidbody.MovePosition(transform.position + 
        //    (new Vector3(0f,0f,1f) * _leftController.GetTouchPosition.y * Time.fixedDeltaTime * _speed) +
        //    (new Vector3(1f, 0f, 0f) * _leftController.GetTouchPosition.x * Time.fixedDeltaTime * _speed));
    }

    private void TankUpdate()
    {
        //float v = Input.GetAxis("Vertical");
        //float h = Input.GetAxis("Horizontal");
        float v = _touchController.GetTouchPosition.y;        
        float h = _touchController.GetTouchPosition.x;

        bool walk = Input.GetKey(KeyCode.LeftShift);

        if (v < 0)
        {
            if (walk) { v *= _backwardsWalkScale; }
            else { v *= _backwardRunScale; }
        }
        else if (walk)
        {
            v *= _walkScale;
        }

        _currentV = Mathf.Lerp(_currentV, v, Time.deltaTime * _interpolation);
        _currentH = Mathf.Lerp(_currentH, h, Time.deltaTime * _interpolation);

        transform.position += transform.forward * _currentV * _moveSpeed * Time.deltaTime;
        transform.Rotate(0, _currentH * _turnSpeed * Time.deltaTime, 0);

        _animator.SetFloat("MoveSpeed", _currentV);

        JumpingAndLanding();
    }

    private void DirectUpdate()
    {
        float v = _touchController.GetTouchPosition.y;
        float h = _touchController.GetTouchPosition.x;
        
        _currentV = Mathf.Lerp(_currentV, v, Time.deltaTime * _interpolation);
        _currentH = Mathf.Lerp(_currentH, h, Time.deltaTime * _interpolation);

        Vector3 direction = new Vector3(0f, 0f, 1f) * _currentV + new Vector3(1f, 0f, 0f) * _currentH;        

        float directionLength = direction.magnitude;
        //direction.y = 0;
        direction = direction.normalized * directionLength;

        if (_currentState == PlayerState.InWater)
        {
            if (transform.position.y < _swimmingHeight)
            {
                transform.position = new Vector3(transform.position.x, _swimmingHeight, transform.position.z);
            }
        }
        
        if (direction != Vector3.zero)
        {
            _currentDirection = Vector3.Slerp(_currentDirection, direction, Time.deltaTime * _interpolation);

            transform.LookAt(transform.position + _currentDirection);

            transform.position += _currentDirection * _moveSpeed * Time.deltaTime;

            _animator.SetFloat("MoveSpeed", direction.magnitude);
        }

        //JumpingAndLanding();
    }

    private void CurveUpdate()
    {
        var pos = transform.position;

        pos = Vector3.Lerp(_jumpOutOfWaterStartPos, _contactedPlatformPosition, _amimTime);

        pos.y = _curveJumpOutOfWater.Evaluate(_amimTime);               

        transform.position = pos;

        _amimTime += Time.fixedDeltaTime;

        if (_amimTime >= 1f)
        {
            _autoMove = false;
            _amimTime = 0f;
            _rigidbody.useGravity = true;
            _animator.SetBool("Grounded", true);
            _currentV = 0f;
            _currentH = 0f;
        }
    }

    private void JumpingAndLanding()
    {
        bool jumpCooldownOver = (Time.time - _jumpTimeStamp) >= _minJumpInterval;

        if (jumpCooldownOver && _isGrounded && _jumpInput)
        {
            _jumpTimeStamp = Time.time;
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }

        if (!_wasGrounded && _isGrounded)
        {
            _animator.SetTrigger("Land");
        }

        if (!_isGrounded && _wasGrounded)
        {
            _animator.SetTrigger("Jump");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.CompareTag("Surface"))
        //{
        //    ContactPoint[] contactPoints = collision.contacts;
        //    for (int i = 0; i < contactPoints.Length; i++)
        //    {
        //        if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
        //        {
        //            if (!_collisions.Contains(collision.collider))
        //            {
        //                _collisions.Add(collision.collider);
        //            }
        //            _isGrounded = true;
        //        }
        //    }
        //}
    }

    private void OnCollisionStay(Collision collision)
    {
        //if (collision.gameObject.CompareTag("Surface"))
        //{
        //    ContactPoint[] contactPoints = collision.contacts;
        //    bool validSurfaceNormal = false;
        //    for (int i = 0; i < contactPoints.Length; i++)
        //    {
        //        if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
        //        {
        //            validSurfaceNormal = true; break;
        //        }
        //    }

        //    if (validSurfaceNormal)
        //    {
        //        _isGrounded = true;
        //        if (!_collisions.Contains(collision.collider))
        //        {
        //            _collisions.Add(collision.collider);
        //        }
        //    }
        //    else
        //    {
        //        if (_collisions.Contains(collision.collider))
        //        {
        //            _collisions.Remove(collision.collider);
        //        }
        //        if (_collisions.Count == 0) { _isGrounded = false; }
        //    }
        //}

        if (_currentState == PlayerState.InWater && collision.gameObject.CompareTag("Surface"))
        {
            _animator.SetBool("InWater", false);
            _animator.SetBool("Grounded", false);
            _currentState = PlayerState.AutoMove;
            _autoMove = true;
            _contactedPlatformPosition = Vector3.Lerp(transform.position, collision.transform.position, 0.3f);
            _jumpOutOfWaterStartPos = transform.position;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        //if (collision.gameObject.CompareTag("Surface"))
        //{
        //    if (_collisions.Contains(collision.collider))
        //    {
        //        _collisions.Remove(collision.collider);
        //    }
        //    if (_collisions.Count == 0) { _isGrounded = false; }
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            _currentState = PlayerState.InWater;
            _animator.SetBool("InWater", true);
            _rigidbody.useGravity = false;
            LeanTween.moveY(gameObject, _swimmingHeight, 0.5f);
        }
    }
}

public enum PlayerState
{
    OnLand,
    AutoMove,
    InWater
}
