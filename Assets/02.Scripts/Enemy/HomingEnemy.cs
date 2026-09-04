using UnityEngine;

public class HomingEnemy : Enemy
{
    // 캐싱 : 자주 쓸법한 데이터 메모리에 올리기
    private GameObject _playerObject;

    private void Start()
    {
        _playerObject = GameObject.FindWithTag("Player"); // 테스트   
    }

    protected override void Move()
    {
        if (_playerObject == null)
        {
            return;
        }

        Vector2 direction = (_playerObject.transform.position - transform.position).normalized;

        transform.Translate(direction * (_moveSpeed * Time.deltaTime));
    }
}