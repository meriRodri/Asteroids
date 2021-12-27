using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using Code.Configurations;

namespace Code.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] _lives;
        [SerializeField] private Text _generalText;
        [SerializeField] private Text _scoreText;
        [SerializeField] private GameObject _instructionsText;
        [SerializeField] private UiConfiguration _configuration;

        public void SetLives()
        {
            int length = _lives.Length;
            for (int i = length - 1; i >= 0; i--)
            {
                if (_lives[i].activeSelf)
                {
                    _lives[i].SetActive(false);
                    break;
                }
            }
        }

        public void StartGame()
        {
            SetText("");
            ActivateLives(true);
            DefaultScoreText();
            _instructionsText.SetActive(true);
        }

        public void StartScreen()
        {
            SetText(_configuration.StartText);
            ActivateLives(false);
            _scoreText.gameObject.SetActive(false);
            _instructionsText.SetActive(false);
        }

        private void ActivateLives(bool active)
        {
            int length = _lives.Length;
            for (int i = 0; i < length; i++)
            {
                _lives[i].SetActive(active);
            }
        }

        public void SetText(string text)
        {
            _generalText.text = text;
        }

        public void NewWave(int counter)
        {
            SetText(_configuration.NewWaveText + counter);
            StartCoroutine(nameof(HideText));
        }

        public void FinishGame()
        {
            SetText(_configuration.GameOverText);
        }

        private IEnumerator HideText()
        {
            yield return new WaitForSeconds(General.TimeBetweenWaves);
            SetText("");
        }

        public void SetScore(int points)
        {
            _scoreText.text = _configuration.ScoreText + points;
        }

        private void DefaultScoreText()
        {
            SetScore(0);
            _scoreText.gameObject.SetActive(true);
        }
    }
}
