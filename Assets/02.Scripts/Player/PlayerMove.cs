using UnityEngine;
using System;

public class PlayerMove : MonoBehaviour
{
    // 목적: 키보드 입력에 따라서 플레이어 이동 처리를 하고 싶다.
    
    // 필요 필드:
    public float Speed;
    public float MinY = -4.6f;
    public float MaxY = -0.58f;
    public float LimitX = 2.9f;
    public float WarpX = 1.85f;

    private void Start()
    {
        // 초기 위치 설정
        float initialY = -2.58f;
        transform.position = new Vector3(0, initialY, 0);
    }
    
    // Update 메서드는 매 프레임마다 실행
    // 초당 프레임 실행 횟수는: 별다른 설정이 없을 경우 가능한 많이 실행
    private void Update()
    {
        // [기본적인 이동 메커니즘]
        // 1. 키보드 입력을 받는다.
        float h = Input.GetAxisRaw("Horizontal"); // 키보드 좌우 입력 상태에 따라 -1f ~ 0 ~ 1f
        float v = Input.GetAxisRaw("Vertical");  // 키보드 상하 입력 상태에 따라 -1f ~ 0 ~ 1f
        // Debug.Log($"h: {h}, v: {v}");
        
        // 2. 키보드 입력에 따라 방향을 구한다.
        // 게임에는 벡터라는 타입이 있다. (벡터: 크기와 방향)
        Vector2 direction = new Vector2(h, v); 
        // Vector2 direction = Vector2.left; // = Vector2(-1, 0) : 내부적으로 같은 코드
        
        // 3. 방향과 속력에 따라 이동한다.
        // 속도 = 방향 + 속력
        // 매직넘버: 보는 사람에 따라 의미가 달라지 수 있는 헷갈리는 숫자
        // transform.Translate(direction * Speed * Time.deltaTime);
        // deltaTime: 이전 프레임으로부터 지금 프레임까지 시간이 얼마나 지났는지 ms(1/1000)로 반환

        // 3. 키보드 E, Q에 스피드 업, 다운 기능 구현
        if (Input.GetKey(KeyCode.E))
        {
            Speed += 1.0f * Time.deltaTime;
            Debug.Log($"현재 속도: {Speed}");
        }
        if (Input.GetKey(KeyCode.Q))
        {
            Speed -= 1.0f * Time.deltaTime;
            Debug.Log($"현재 속도: {Speed}");
        }
        
        Vector2 normalizedSpeed = direction.normalized * Speed; // 벡터의 길이 1로 변환. 즉, 방향만 유지
        
        // 새로운 위치 = 현재 위치 + v(방향 * 속력) x t(시간)
        Vector2 newPosition = transform.position + (Vector3)normalizedSpeed * Time.deltaTime;
        
        // 1. 영역 안에서 움직이도록 고정
        newPosition.y = Mathf.Clamp(newPosition.y, MinY, MaxY);
        
        // 2. 좌우 좌표 한계 초과 시 텔레포트
        if (transform.position.x < -LimitX)
        {
            newPosition.x = WarpX;
        }
        if (transform.position.x > LimitX)
        {
            newPosition.x = -WarpX;
        }
        
        transform.position = newPosition;
    }
}
