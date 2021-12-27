using Code.Data;
using Code.Objects;
using UnityEngine;

namespace Code.Configurations
{
    [CreateAssetMenu(fileName = "AsteroidsConfiguration", menuName = "ScriptableObjects/AsteroidsConfiguration", order = 1)]
    public class AsteroidsConfiguration : ScriptableObject
    {
        [SerializeField] private Sprite[] _sprites;
        [SerializeField] private Asteroid _asteroidPrefab;
        [SerializeField] private AsteroidData[] _levels;
        [SerializeField] private int _rangeMovimentAndPosition;
        [SerializeField] private int _firstWave;
        [SerializeField] private int _increaseWave;
        [SerializeField] private string _asteroidsPool = "AsteroidsPool";

        public AsteroidData[] Levels => _levels;
        public Sprite[] Sprites => _sprites;
        public int FirstWave => _firstWave;
        public int IncreaseWave => _increaseWave;
        public int RangeMovimentAndPosition => _rangeMovimentAndPosition;
        public Asteroid AsteroidPrefab => _asteroidPrefab;
        public string AsteroidsPool => _asteroidsPool;
    }
}
