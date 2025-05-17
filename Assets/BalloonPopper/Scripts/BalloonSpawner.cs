using UnityEngine;
using Random = UnityEngine.Random;

namespace BalloonPopper.Scripts {
    public class BalloonSpawner : MonoBehaviour {
        [Header("Spawning Settings")]
        public GameObject balloonPrefab;
        public float spawnRatePerMinute = 20f;
        private float _spawnDelay;
        public float spawnWidth = 8.0f;

        [Header("Balloon Variety")]
        public Color[] balloonColors;
        public float minSize = 0.8f;
        public float maxSize = 1.2f;
        
        private float _nextSpawnTime;

        private void Start() {
            _spawnDelay = 60/spawnRatePerMinute;
            _nextSpawnTime = Time.time + _spawnDelay;
        }

        public void Update() {
            if (Time.time >= _nextSpawnTime) {
                SpawnBalloon();
                _nextSpawnTime = Time.time + _spawnDelay;
            }
        }

        private void SpawnBalloon() {
            float xPosition = Random.Range(-spawnWidth / 2, spawnWidth / 2);
            Vector3 spawnPosition = new Vector3(xPosition, transform.position.y, 0);
            
            GameObject newBalloon = Instantiate(balloonPrefab, spawnPosition, Quaternion.identity);

            if (balloonColors.Length > 0) {
                Color randomColor = balloonColors[Random.Range(0, balloonColors.Length)];
                
                Renderer balloonRenderer = newBalloon.GetComponent<Renderer>();
                if (balloonRenderer) {
                    balloonRenderer.material.color = randomColor;
                }
            }
            
            float randomSize = Random.Range(minSize, maxSize);
            newBalloon.transform.localScale = new Vector3(randomSize, randomSize, randomSize);
        }
    }
}