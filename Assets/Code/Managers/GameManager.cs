using Code.Data;
using Code.Objects;
using System.Collections;
using UnityEngine;

namespace Code.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private UIManager _uiManager;
        [SerializeField] private AsteroidsManager _asteroidsManager;
        [SerializeField] private BulletsManager _bulletsManager;

        private int _lives = 5;
        private int _score = 0;

        private void Start()
        {
            StartScreen();
        }

        private void OnEnable()
        {
            _player.OnDamage += DamageHealth;
            _player.OnShoot += ShootBullet;
            _asteroidsManager.OnNewAsteroidsWave += SetNewWave;
            _asteroidsManager.OnSetScore += SetScore;
        }

        private void OnDisable()
        {
            _player.OnDamage -= DamageHealth;
            _player.OnShoot -= ShootBullet;
            _asteroidsManager.OnNewAsteroidsWave -= SetNewWave;
            _asteroidsManager.OnSetScore -= SetScore;
            StopCoroutine(nameof(RestartPlayer));
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && General.GameState == State.Start)
            {
                StartGame();
            }
        }

        private void StartScreen()
        {
            _uiManager.StartScreen();
        }

        private void StartGame()
        {
            General.GameState = State.Playing;
            _lives = 5;
            _score = 0;
            _player.Restart();
            _uiManager.StartGame();
            _asteroidsManager.InstantiateFirstAsteroidsWave();
            _bulletsManager.CreatePooledBullets();
        }

        private void FinishGame()
        {
            General.GameState = State.Start;
            _uiManager.FinishGame();
            _asteroidsManager.RemoveAllAsteroids();
        }

        private void DamageHealth()
        {
            _lives--;

            _uiManager.SetLives();

            if (_lives <= 0)
            {
                FinishGame();
            }
            else
            {
                StartCoroutine(nameof(RestartPlayer));
            }
        }

        private IEnumerator RestartPlayer()
        {
            yield return new WaitForSeconds(General.TimeRestart);
            _player.Restart();
        }

        private void SetNewWave(int counter)
        {
            _uiManager.NewWave(counter);
            _player.ActivePlayer(false);
            _bulletsManager.RemoveAllBullets();
            StartCoroutine(nameof(RestartPlayer));
        }

        private void SetScore(int counter)
        {
            _score += counter;
            _uiManager.SetScore(_score);
        }

        private void ShootBullet(BulletData data)
        {
            _bulletsManager.ShootBullet(data);
        }
    }
}

