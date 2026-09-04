using UnityEngine;

enum Enemies
{
    StraightEnemy,
    SniperEnemy,
    HomingEnemy
}

// 역할: 일정 시간마다 적을 생성해주고 싶다.
public class EnemySpawner : MonoBehaviour
{
    // 필요 속성
    [SerializeField] private float _spawnInterval = 3f;
    private float _timer;

    // - 생성할 프리팹
    [SerializeField] private Enemy[] _enemyPrefabs = new Enemy[3];

    private int _spawnEnemyIndex = (int)Enemies.StraightEnemy;
    // 확률에 따라 Enemy 다양하게 스폰
    // - 50%: Downward, 30%: Aimed, 20%: Homing

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _spawnInterval)
        {
            _timer = 0;

            _spawnInterval = UnityEngine.Random.Range(1f, 3f);
            // _spawnInterval = UnityEngine.Random.Range(1, 3);
            SelectRandomEnemy();

            Spawn();
        }
    }

    private void Spawn()
    {
        Enemy enemy = Instantiate(_enemyPrefabs[_spawnEnemyIndex]);
        enemy.transform.position = transform.position;
    }

    private void SelectRandomEnemy()
    {
        int randomNumber = UnityEngine.Random.Range(0, 100);

        // Todo: Scriptable Object를 사용해서 리팩토링
        // - 이유 1: 배열을 사용하나 각 아이템이 어떤 프리팹인지 알 수 없음
        // - 이유 2: 각 Enemy 스폰 확률을 매직 넘버로 하드 코딩해서 유지보수가 어려움
        if (randomNumber < 50)
        {
            _spawnEnemyIndex = (int)Enemies.StraightEnemy;
        }
        else if (randomNumber < 80)
        {
            _spawnEnemyIndex = (int)Enemies.SniperEnemy;
        }
        else
        {
            _spawnEnemyIndex = (int)Enemies.HomingEnemy;
        }
    }
}