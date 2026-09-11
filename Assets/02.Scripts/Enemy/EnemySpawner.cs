using UnityEngine;

// 역할: 일정 시간마다 적을 생성해주고 싶다.
public class EnemySpawner : MonoBehaviour
{
    // ScriptableObject 기반 데이터 테이블 관리
    [SerializeField] private EnemySpawnDataTableSO _spawnDataTable;

    // 필요 속성
    private float _spawnInterval = 3f;
    private float _minSpawnInterval = 1f;
    private float _maxSpawnInterval = 3f;
    private float _timer;

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _spawnInterval)
        {
            _timer = 0;

            _spawnInterval = UnityEngine.Random.Range(_minSpawnInterval, _maxSpawnInterval);
            SpawnRandomEnemy();
        }
    }

    private void SpawnRandomEnemy()
    {
        // Todo: Scriptable Object를 사용해서 리팩토링
        // - 이유 1: 배열을 사용하나 각 아이템이 어떤 프리팹인지 알 수 없음
        // - 이유 2: 각 Enemy 스폰 확률을 매직 넘버로 하드 코딩해서 유지보수가 어려움

        // 가중치 랜덤 선택
        // - 각 아이템에 가중치 부여
        // - 가중치가 클수록 높은 확률로 선택되도록 하는 방식

        // 1. 추첨할 수 있는 모든 가중치를 더한다.
        int totalWeight = 0;
        foreach (EnemySpawnData data in _spawnDataTable.Datas)
        {
            totalWeight += data.Weight;
        }

        // 2. 전체 가중치 범위에서 랜덤한 정수 뽑기
        int randomWeight = Random.Range(0, totalWeight);

        // 3. 가중치를 누적하면서 선택된 구간을 찾는다.
        int cumulativeWeight = 0;

        foreach (EnemySpawnData data in _spawnDataTable.Datas)
        {
            cumulativeWeight += data.Weight;
            if (randomWeight < cumulativeWeight)
            {
                GameObject enemy = Instantiate(data.EnemyPrefab);
                enemy.transform.position = transform.position;
                break;
            }
        }
    }
}