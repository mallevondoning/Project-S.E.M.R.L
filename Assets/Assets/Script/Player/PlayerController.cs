using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Rigidbody _playerRB;
    [SerializeField] Material _playerMaterial;

    [Header("General")]
    [SerializeField] float _speed = 12f;

    [Header("Dash")]
    [SerializeField] AnimationCurve _dashCurve;
    [SerializeField] Gradient _activeDash;
    [SerializeField] float _dashModifier = 0.6f;
    [SerializeField] float _dashTimerRecharge = 8f;
    [SerializeField] float _dashTimerDuration = 1f;
    [SerializeField] int _maxDashCount = 2;
    float _dashTimerRechargeStart = 0;
    float _dashTimerDurationStart = 0;
    int _currentDashCount;
    bool _isDashing = false;

    private void Awake()
    {
        _currentDashCount = _maxDashCount;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void Update()
    {
        //generlize
        //update this to a input manager
        if (Input.GetKeyDown(KeyCode.Space) && !_isDashing) ActiveDash();

        if (Time.time >= _dashTimerDurationStart + _dashTimerDuration)
            _isDashing = false;

        if (_currentDashCount <= _maxDashCount) return;
    }

    //basic start to movment
    //refine movement
    private void MovePlayer()
    {
        Vector3 finalDir = GetNormalizedDirection();

        float finalDashMod = 1f;
        float dashTimer = (Time.time - _dashTimerDurationStart) / _dashTimerDuration;
        if (_isDashing)
        {
            finalDashMod = 1 + (_dashModifier * _dashCurve.Evaluate(dashTimer));

            _activeDash.Evaluate(_dashCurve.Evaluate(dashTimer)); //<= adds a color when active (does nothing now)
        }

        finalDir *= _speed;
        finalDir *= finalDashMod;
        finalDir.y = _playerRB.velocity.y;

        _playerRB.velocity = finalDir;
    }
    private void ActiveDash()
    {
        if (_currentDashCount <= 0)
        {
            //add animation on fail
            return;
        }

        _isDashing= true;
        _dashTimerDurationStart = Time.time;
        
        //_currentDashCount--;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //should negate damage when dashing
        if (_isDashing) return;

    }

    private Vector3 GetDirection()
    {
        Vector3 finalDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        
        return finalDir;
    }
    private Vector3 GetNormalizedDirection()
    {
        Vector3 finalDir = GetDirection();
        finalDir = finalDir.normalized;

        return finalDir; 
    }
}
