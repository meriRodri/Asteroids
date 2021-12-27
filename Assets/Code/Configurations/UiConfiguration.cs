using UnityEngine;

namespace Code.Configurations
{
    [CreateAssetMenu(fileName = "UiConfiguration", menuName = "ScriptableObjects/UiConfiguration", order = 3)]
    public class UiConfiguration : ScriptableObject
    {
        [SerializeField] private string _gameOverText = "GAME OVER. Press space to restart";
        [SerializeField] private string _startText = "Press SPACE to START";
        [SerializeField] private string _newWaveText = "WAVE ";
        [SerializeField] private string _scoreText = "SCORE: ";

        public string GameOverText => _gameOverText;
        public string StartText => _startText;
        public string NewWaveText => _newWaveText;
        public string ScoreText => _scoreText;
    }
}
