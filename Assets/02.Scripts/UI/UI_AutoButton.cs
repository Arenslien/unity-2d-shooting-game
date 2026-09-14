using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

public class UI_AutoButton : MonoBehaviour
{
    // 버튼을 클릭하면 토글하고 싶다.
    // - 플레이어의 자동 이동
    // - 플레이어의 자동 공격

    private bool _autoMode = false;
    private Player _player;

    // 버튼 이미지 관련
    [SerializeField] private Sprite _onSprite;
    [SerializeField] private Sprite _offSprite;
    private Image _myImage;

    // 버튼 클릭 시 기능
    private AudioSource _audioSource;

    [Header("클릭 시 애니메이션")]
    [SerializeField] private AnimationCurve _bumpCurve;

    private float _scale = 1.0f;
    private const float BumpDuration = 0.2f;
    private const float BumpScale = 1.1f;
    private bool _isBumping = false;
    private float _elapsedTime = 0;

    private void Start()
    {
        _player = GameObject.FindAnyObjectByType<Player>();
        _myImage = GetComponent<Image>();
        _audioSource = GetComponent<AudioSource>();
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

    public void AutoToggle()
    {
        _autoMode = !_autoMode;

        _player.GetComponent<PlayerFire>().ToggleAutoMode();
        _player.GetComponent<PlayerAutoMove>().ToggleAutoMode();

        _player.GetComponent<PlayerMove>().enabled = !_autoMode;

        _myImage.sprite = _autoMode ? _onSprite : _offSprite;
    }

    // todo: 
    // - UI_ButtonClick --> Animation & Sound
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