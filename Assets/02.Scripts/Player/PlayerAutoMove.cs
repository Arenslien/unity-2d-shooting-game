using System;
using UnityEngine;

enum AutoModeState
{
    Idle,
    Patrol,
    Chase,
}

public class PlayerAutoMove : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;
    private Vector3 _direction = Vector3.right;

    private bool _isAutoMode = false;
    private AutoModeState _autoModeState = AutoModeState.Idle;

    // 좌우 패트롤 방향 전환 쿨타임
    private float _leftRightTurnCoolTime = 0.5f;
    private float _currentLeftRightTurnCoolTime = 0;

    private void Update()
    {
        ChangeAutoMode();

        if (_isAutoMode)
        {
            AutoMove();
            if (_autoModeState == AutoModeState.Patrol)
            {
                UpdatePatrolDirection();
            }
        }
    }

    private void ChangeAutoMode()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _isAutoMode = !_isAutoMode;
            _autoModeState = _isAutoMode ? AutoModeState.Patrol : AutoModeState.Idle;
            Debug.Log($"[Player Auto Move] - {(_isAutoMode ? "ON" : "OFF")}");
        }
    }

    private void AutoMove()
    {
        switch (_autoModeState)
        {
            case AutoModeState.Patrol:
                // 1. 좌우 패트롤
                transform.position += _direction * (_speed * Time.deltaTime);

                // 2. 패트롤 중 적 발견 시 Chase 모드로 전환
                // GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");
                //
                // int targetIndex = -1;
                // foreach (GameObject target in targets)
                // {
                //     // 일정 거리 이상인 경우 패스
                //
                //     float distance = Trantarget.transform.position - transform.position;
                // }
                // for 
                //
                // if ()
                // {
                //     _autoModeState = AutoModeState.Chase;
                // }

                break;
            case AutoModeState.Chase:
                GameObject target2 = GameObject.FindWithTag("Enemy");

                _direction = (target2.transform.position - transform.position).normalized;
                _direction.y = transform.position.y;

                transform.position = _direction * (_speed * Time.deltaTime);

                break;
        }
    }

    private void UpdatePatrolDirection()
    {
        if (_currentLeftRightTurnCoolTime < _leftRightTurnCoolTime)
        {
            _currentLeftRightTurnCoolTime += Time.deltaTime;
        }
        else
        {
            _currentLeftRightTurnCoolTime = 0;
            _leftRightTurnCoolTime = UnityEngine.Random.Range(-0.5f, 1.5f);
            _direction = (_direction.x < 0 ? Vector3.right : Vector3.left);

            Debug.Log(_leftRightTurnCoolTime);
        }
    }
}