using UnityEngine;
using UnityEngine.UI;

public class UI_ButtonClick : MonoBehaviour
{
    // 기능 관련 컴포넌트
    private AudioSource _audioSource;
    private Button _button;

    // 애니메이션 관련 필드
    [Header("클릭 시 애니메이션")]
    [SerializeField] private AnimationCurve _bumpCurve;

    private bool _isBumping = false;
    private float _elapsedTime = 0;
    private const float BumpDuration = 0.2f;
    private const float BumpScale = 1.1f;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _button = GetComponent<Button>();

        // 동적으로 버튼 클릭 시 실행할 함수 축
        _button.onClick.AddListener(PlayAnimation);
        _button.onClick.AddListener(PlaySound);
    }

    private void Update()
    {
        if (!_isBumping) return;

        // 1. 경과 시간 누적
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime > BumpDuration)
        {
            transform.localScale = Vector3.one;
            _isBumping = false;
            return;
        }

        // 2. 누적 시간과 애니메이션 커브에 따른 스케일 변경
        float time = _elapsedTime / BumpDuration; // 현재 지난 시간 비율
        float curveValue = _bumpCurve.Evaluate(time); // 퍼센트에 따라 커브 애니메이션 값 추출
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * BumpScale, curveValue);
    }

    public void PlaySound()
    {
        _audioSource.Play();
    }

    public void PlayAnimation()
    {
        _isBumping = true;
        _elapsedTime = 0;
    }
}