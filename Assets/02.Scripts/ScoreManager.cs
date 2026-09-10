using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // 싱글톤 패턴
    // 1. 전역적으로 접근 가능하다.
    // 2. 인스턴스(생성된 객체)가 하나임을 보장한다.
    private static ScoreManager _instance = null;
    public static ScoreManager Instance => _instance;

    // 관리: 특정 데이터에 대한 무결성과 CRUD 등과 관련된 게임 로직
    private int _bestScore;
    private int _currentScore = 0;
    private int _lastRefreshScore = -1;

    private const string SaveKey = "BestScore";

    // UI 책임 추가
    [SerializeField] private TextMeshProUGUI _bestScoreTextUI;
    [SerializeField] private TextMeshProUGUI _currentScoreTextUI;

    private void Awake()
    {
        // 늦게 태어난 매니저는 삭제
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }

    private void Start()
    {
        // 입력: Input
        // 저장/불러오기: PlayerPrefs
        if (PlayerPrefs.HasKey(SaveKey))
        {
            _bestScore = PlayerPrefs.GetInt(SaveKey);
        }
        // _bestScore = PlayerPrefs.GetInt(SaveKey, 0);

        Refresh();
    }


    public void AddScore(int score)
    {
        if (score <= 0) return; // 방어 코드

        _currentScore += score;
        if (_currentScore > _bestScore)
        {
            _bestScore = _currentScore;

            // 저장: Set~ 시리즈를 이용해서 int/float/string을 저장 가능하다.
            // 내 컴퓨터 어딘가에 저장이 된다..
            // 자주하면 렉걸림 -> 왜 렉걸리는 지 알아야 기술면접.. 정처기를...
            PlayerPrefs.SetInt(SaveKey, _bestScore);
            PlayerPrefs.Save();
        }

        Refresh();
    }

    private void Refresh()
    {
        if (_lastRefreshScore == _currentScore) return;

        _bestScoreTextUI.text = $"BestScore: {_bestScore}";
        _currentScoreTextUI.text = $"Score: {_currentScore}";

        _lastRefreshScore = _currentScore;
    }
}