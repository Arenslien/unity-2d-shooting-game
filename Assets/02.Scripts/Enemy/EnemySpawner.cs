using UnityEngine;

// 역할: 일정 시간마다 적을 생성해주고 싶다.
public class EnemySpawner : MonoBehaviour
{
    // 필요 속성
    [Header("스폰 간격")][SerializeField] private float _spawnInterval = 3f;
    private float _timer;

    // - 생성할 프리팹
    [Header("스폰할 적 프리팹")][SerializeField] private Enemy _enemyPrefab; // Enemy로 받을 경우 해당 클래스를 스크립트로 가진 경우만 가능

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _spawnInterval)
        {
            _timer = 0;

            Spawn();
        }
    }

    private void Spawn()
    {
        Enemy enemy = Instantiate(_enemyPrefab);
        enemy.transform.position = transform.position;
    }
}