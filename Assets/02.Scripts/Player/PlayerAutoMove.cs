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

    // 타겟
    private GameObject _target = null;

    // 거리
    private float _minDistance = 3f;

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

                // 2. 적 탐색
                FindNearestTarget();

                break;
            case AutoModeState.Chase:
                if (_target == null)
                {
                    _autoModeState = AutoModeState.Patrol;
                    break;
                }

                _direction = (_target.transform.position - transform.position).normalized;
                transform.position += _direction * (_speed * Time.deltaTime);

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

    private void FindNearestTarget()
    {
        // 1. 모든 적 Find
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length == 0) return;

        // 2. 타겟 지정
        foreach (GameObject enemy in enemies)
        {
            // 2.1 타겟과의 거리 측정
            float distance = Vector2.Distance(enemy.transform.position, transform.position);

            if (distance < _minDistance)
            {
                _target = enemy;
                _autoModeState = AutoModeState.Chase;
            }
        }
    }
}