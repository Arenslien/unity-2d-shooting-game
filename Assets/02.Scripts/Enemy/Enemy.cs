using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float Health = 100;
    public float MoveSpeed = 2;
    private float _minPositionY = -5.5f;

    private void Update()
    {
        Move(); // 기본 이동 방식 --> 각각의 자식 클래스 메서드 적용

        DestroyIfOutOfBounds(); // 기본 오브젝트 제거 (하단 범위 벗어날 경우)
    }

    protected abstract void Move();

    private void DestroyIfOutOfBounds()
    {
        if (transform.position.y <= _minPositionY)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    }
}