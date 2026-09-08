using Unity.Hierarchy;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBomb : MonoBehaviour
{
    [SerializeField] private GameObject _bombPrefab;
    private float _coolTime = 10f;
    private float _currentTime = 10f;
    private Vector3 _bombDropPosition;
    private float _xRange = 1.5f;

    private void Update()
    {
        if (_currentTime >= _coolTime && Input.GetKeyDown(KeyCode.B))
        {
            CalculateBombDropPosition();
            Bomb();
            _currentTime = 0f;
        }
        else
        {
            _currentTime += Time.deltaTime;
        }
    }

    private void Bomb()
    {
        Instantiate(_bombPrefab, _bombDropPosition, Quaternion.identity);
    }

    private void CalculateBombDropPosition()
    {
        float x = UnityEngine.Random.Range(-_xRange, _xRange);
        float y = UnityEngine.Random.Range(0f, 4f);

        _bombDropPosition = new Vector3(x, y, 0);
    }

    // 키보드 B 키 누르면 폭탄 투하

    // 애니메이션 적용 및 3초 유지

    // 범위 닿는 적 모두 한 방에 죽음

    // 재사용 쿨타임 10초
}