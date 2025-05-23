using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BalloonPopper.Scripts {
    public class BalloonBehavior : MonoBehaviour {
        [Header("Movement Settings")]
        public float floatSpeed = 1.5f;
        public float swayAmount = 1f;
        public float swaySpeed = 1.0f;

        [Header("Gameplay Settings")]
        public int pointValue = 1;
        public GameObject popEffect;

        private Vector3 _startPosition;
        private float _randomOffset;
        private GameManager _gameManager;

        private void Start() {
            _startPosition = transform.position;

            _randomOffset = Random.Range(0f, 2f * Mathf.PI);

            float randomX = Random.Range(-1f, 1f);
            transform.position = new Vector3(_startPosition.x + randomX, _startPosition.y, _startPosition.z);
            _gameManager = FindFirstObjectByType<GameManager>();
        }

        private void Update() {
            transform.Translate(Vector3.up * (floatSpeed * Time.deltaTime));

            float swayOffset = (float)Math.Sin((Time.time + _randomOffset) * swaySpeed) * swayAmount;
            transform.position = new Vector3(_startPosition.x + swayOffset, transform.position.y, _startPosition.z);

            if (transform.position.y > 10f) {
                Destroy(gameObject);
            }
        }

        private void OnMouseDown() {
            if (_gameManager && _gameManager.IsGameActive()) {
                Pop();
            }
        }

        private void Pop() {
            GameManager gameManager = FindFirstObjectByType<GameManager>();
            if (gameManager) {
                gameManager.AddScore(pointValue);
            }

            Debug.Log("Balloon popped! Points: " + pointValue);

            if (popEffect != null) {
                Instantiate(popEffect, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
}