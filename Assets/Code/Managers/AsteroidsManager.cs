using Code.Configurations;
using Code.Data;
using Code.Objects;
using Code.Utils;
using System.Collections;
using UnityEngine;

namespace Code.Managers
{
    public class AsteroidsManager : MonoBehaviour
    {
        [SerializeField] private AsteroidsConfiguration _configuration;

        private int _counter;
        private Pool<Asteroid> _asteroids;

        public delegate void NewAsteroidsWave(int counter);
        public event NewAsteroidsWave OnNewAsteroidsWave;

        public delegate void SetScore(int score);
        public event SetScore OnSetScore;

        public void InstantiateFirstAsteroidsWave()
        {
            GameObject asteroidPool = new GameObject(_configuration.AsteroidsPool);
            _asteroids = new Pool<Asteroid>(_configuration.FirstWave, _configuration.AsteroidPrefab, asteroidPool.transform);
            InstantiateAsteroidsWaves(_configuration.FirstWave);
        }

        private void InstantiateAsteroidsWaves(int length)
        {
            _asteroids.AddObjectsToPool(length * (_configuration.Levels.Length + 1));
            for (int i = 0; i < length; i++)
            {
                float x = Random.Range(-General.MaxX, General.MaxX);
                float y = Random.Range(-General.MaxY, General.MaxY);
                Vector2 position = new Vector2(x, y);
                CreateAsteroid(position, _configuration.Levels.Length);
            }
        }

        private void CreateAsteroid(Vector2 position, int level)
        {
            Asteroid pooledAsteroid = _asteroids.GetPooledObject();

            if (pooledAsteroid == null)
            {
                return;
            }

            Sprite sprite = _configuration.Sprites[Random.Range(0, _configuration.Sprites.Length)];
            AsteroidData asteroidData = _configuration.Levels[level - 1];
            Vector3 size = Vector3.one * asteroidData.Size;
            pooledAsteroid.SetData(sprite, size, asteroidData, _configuration.RangeMovimentAndPosition, position);
            pooledAsteroid.OnDamage += AsteroidCollisioned;
        }

        private void AsteroidCollisioned(Asteroid asteroid)
        {
            asteroid.OnDamage -= AsteroidCollisioned;

            OnSetScore(asteroid.Data.Points);

            if (asteroid.Data.Level > 1)
            {
                SplitAsteroid(asteroid);
            }
            else
            {
                if (AllAsteroidsAreDestroyed)
                {
                    CreateNewWave();
                }
            }
        }

        private void SplitAsteroid(Asteroid asteroid)
        {
            int length = Random.Range(asteroid.Data.Level, asteroid.Data.Level + 1);
            for (int i = 0; i < length; i++)
            {
                CreateAsteroid(asteroid.gameObject.transform.position, Random.Range(1, asteroid.Data.Level));
            }
        }

        private bool AllAsteroidsAreDestroyed => _asteroids.Count == 0 || !NoVisibleAsteroids();

        private bool NoVisibleAsteroids()
        {
            int length = _asteroids.Count;
            for (int i = 0; i < length; i++)
            {
                if (_asteroids.GetPooledObjects()[i].IsVisible)
                {
                    return true;
                }
            }
            return false;
        }

        private void CreateNewWave()
        {
            RemoveAllAsteroids();
            StartCoroutine(nameof(NewWave));
            _counter++;
            OnNewAsteroidsWave(_counter);
        }

        public void RemoveAllAsteroids()
        {
            int length = _asteroids.Count;
            for (int i = 0; i < length; i++)
            {
                _asteroids.GetPooledObjects()[i].OnDamage -= AsteroidCollisioned;
                _asteroids.GetPooledObjects()[i].gameObject.SetActive(false);
            }
        }

        private IEnumerator NewWave()
        {
            yield return new WaitForSeconds(General.TimeBetweenWaves);
            InstantiateAsteroidsWaves(_configuration.FirstWave + _configuration.IncreaseWave * _counter);
        }

        private void OnDisable()
        {
            StopCoroutine(nameof(NewWave));
        }
    }
}
