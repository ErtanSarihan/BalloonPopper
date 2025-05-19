using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BalloonPopper.Scripts {
    public class GameManager : MonoBehaviour {
        [Header("UI References")]
        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI timerText;
        public GameObject gameOverPanel;

        [Header("Game Settings")]
        public float gameDuration = 60f;
        public AudioClip popSound;

        private int _score = 0;
        private float _remainingTime;
        private AudioSource _audioSource;
        private bool _gameActive = true;

        private void Start() {
            _remainingTime = gameDuration;
            UpdateScoreText();
            UpdateTimerText();

            if (gameOverPanel) {
                gameOverPanel.SetActive(false);
            }

            _audioSource = GetComponent<AudioSource>();
            if (!_audioSource && popSound) {
                _audioSource = gameObject.AddComponent<AudioSource>();
            }
        }

        private void Update() {
            if (_gameActive) {
                _remainingTime -= Time.deltaTime;
                UpdateTimerText();

                if (_remainingTime <= 0) {
                    EndGame();
                }
            }
        }

        public void AddScore(int points) {
            if (!_gameActive) return;

            _score += points;
            UpdateScoreText();

            if (_audioSource && popSound) {
                _audioSource.PlayOneShot(popSound);
            }
        }

        void UpdateScoreText() {
            if (scoreText) {
                scoreText.text = "Score: " + _score;
                Debug.Log("Score: " + _score);
            }
        }

        void UpdateTimerText() {
            if (timerText) {
                int seconds = Mathf.Max(0, Mathf.FloorToInt(_remainingTime));
                timerText.text = "Time: " + seconds + "s";
            }
        }

        void EndGame() {
            _gameActive = false;
            _remainingTime = 0;
            UpdateTimerText();

            if (gameOverPanel) {
                gameOverPanel.SetActive(true);
            }

            BalloonSpawner spawner = FindFirstObjectByType<BalloonSpawner>();
            if (spawner) {
                spawner.enabled = false;
            }
        }

        public void RestartGame() {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}