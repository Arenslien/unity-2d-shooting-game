using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private float _coolTime = 3f;
    private float _currentTime = 0f;
    private List<Enemy> _targetEnemies = new List<Enemy>();

    private void Update()
    {
        _currentTime += Time.deltaTime;

        if (_currentTime >= _coolTime)
        {
            Explode();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;

        Enemy enemy = other.GetComponent<Enemy>();

        if (enemy == null) return;

        _targetEnemies.Add(enemy);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;

        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy == null) return;

        _targetEnemies.Remove(enemy);
    }

    private void Explode()
    {
        foreach (Enemy enemy in _targetEnemies.ToArray())
        {
            if (enemy == null) continue;
            enemy.TakeExplosion();
        }

        Destroy(gameObject);
    }
}