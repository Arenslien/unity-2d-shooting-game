using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _delayCoolTime = 3f;

    private float _currentTime = 0;
    private bool _isMagnetMode = false;
    private GameObject _player;


    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        if (!_isMagnetMode)
        {
            CheckItemCoolTime();
        }
        else
        {
            FlyToPlayer();
        }
    }

    private void CheckItemCoolTime()
    {
        if (_currentTime < _delayCoolTime)
        {
            _currentTime += Time.deltaTime;
        }
        else
        {
            _isMagnetMode = true;
        }
    }

    private void FlyToPlayer()
    {
        if (_player == null) return;

        // 1. 방향 설정
        Vector2 direction = (_player.transform.position - transform.position).normalized;

        // 2 이동
        transform.Translate(_speed * Time.deltaTime * direction);
    }
}