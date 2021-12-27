using Code.Data;
using Code.Managers;
using UnityEngine;

namespace Code.Objects
{
    public class Asteroid : ObjectWrap
    {
        [SerializeField] private Rigidbody2D _rigidBody;
        [SerializeField] private SpriteRenderer _sprite;
        [SerializeField] private Renderer _renderer;

        private AsteroidData _data;

        public AsteroidData Data => _data;

        public delegate void Damaged(Asteroid asteroid);
        public event Damaged OnDamage;

        public void SetData(Sprite sprite, Vector3 scale, AsteroidData data, int range, Vector2 position)
        {
            _data = data;
            transform.localScale = scale;
            _sprite.sprite = sprite;
            transform.position = position;
            gameObject.SetActive(true);
            _rigidBody.AddForce(new Vector2(RandomNumber(range), RandomNumber(range)) * data.MovimentSpeed * Time.deltaTime);
            _rigidBody.AddTorque(RandomNumber(range) * data.RotationSpeed * Time.deltaTime);
        }

        private int RandomNumber(int range)
        {
            int random = 0;
            while (random == 0)
            {
                random = Random.Range(-range, range);
            }
            return random;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag.Equals(General.BulletTag))
            {
                gameObject.SetActive(false);
                OnDamage(this);
            }
        }

        public bool IsVisible => _renderer.isVisible;
    }
}
