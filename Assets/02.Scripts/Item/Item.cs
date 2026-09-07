using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemType _type;
    [SerializeField] private float _value;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Player player = other.GetComponent<Player>();

        if (player == null)
        {
            Debug.LogWarning("Tag가 Player인 게임 오브젝트를 찾지 못했습니다.");
            return;
        }

        if (_type == ItemType.Health)
        {
            player.RestoreHealth((int)_value);
            Debug.Log($"현재 체력: {player.GetHealth()}");
        }
        else if (_type == ItemType.AttackSpeed)
        {
            PlayerFire playerFire = player.GetComponent<PlayerFire>();
            playerFire.IncreaseAttackSpeed(_value);
            Debug.Log("공격 속도 증가");
        }
        else
        {
            PlayerMove playerMove = player.GetComponent<PlayerMove>();
            playerMove.IncreaseSpeed(_value);
            Debug.Log("이동 속도 증가");
        }

        Destroy(gameObject);
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
        // 1. 방향 설정
        Vector2 direction = (_player.transform.position - transform.position).normalized;

        // 2 이동
        transform.Translate(_speed * Time.deltaTime * direction);
    }
}