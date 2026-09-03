using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float Speed;

    private void Update()
    {
        // 1. 방향 설정
        Vector2 direction = new Vector2(0, -1); // Vector2.down;

        // 2. 오브젝트 이동
        transform.Translate(direction * Speed * Time.deltaTime);
    }
}