using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody _playerRB;
    [SerializeField] float _speed = 12f;
    [SerializeField] int _maxDashCount = 2;

    int _currentDashCount;

    private void Awake()
    {
        _currentDashCount = _maxDashCount;
    }

    private void FixedUpdate()
    {
        //basic start to movment
        //refine movement
        //update this to a input manager
        MovePlayer();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(1)) ActiveDash();
        //if (_currentDashCount <= _maxDashCount) Timer();
    }

    private void MovePlayer()
    {
        Vector3 finalDir = GetNormelizedDirection();

        finalDir *= _speed;
        finalDir.y = _playerRB.velocity.y;

        _playerRB.velocity = finalDir;
    }
    private void ActiveDash()
    {
        if (_currentDashCount<=0)
        {
            //add animation on fail
            return;
        }

        Debug.Log("Dashed");
    }

    private Vector3 GetDirection()
    {
        Vector3 finalDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        
        return finalDir;
    }
    private Vector3 GetNormelizedDirection()
    {
        Vector3 finalDir = GetDirection();
        finalDir = finalDir.normalized;

        return finalDir; 
    }
}
