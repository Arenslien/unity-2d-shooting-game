using UnityEngine;

enum AutoModeState
{
    Idle,
    Patrol,
    Chase,
}

public class PlayerAutoMove : MonoBehaviour
{
    private bool _isAutoMode = false;
    private AutoModeState _autoModeState = AutoModeState.Idle;

    private void Update()
    {
        ChangeAutoMode();

        if (_isAutoMode) AutoMove();
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
                // 1. 좌우 + 상하 패트롤
                break;
            case AutoModeState.Chase:
                // 2. 가까이 가서 싸우는 게 아니라 흠..
                break;
        }
    }
}