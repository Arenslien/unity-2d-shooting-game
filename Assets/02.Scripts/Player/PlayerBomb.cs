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
}