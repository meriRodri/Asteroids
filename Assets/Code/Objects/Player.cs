using Code.Configurations;
using Code.Data;
using Code.Managers;
using Code.Utils;
using System.Collections;
using UnityEngine;

namespace Code.Objects
{
    public class Player : ObjectWrap
    {
        [SerializeField] private Rigidbody2D _rigidBody;
        [SerializeField] private Transform _bulletSpawner;
        [SerializeField] private GameObject _shield;
        [SerializeField] private PlayerConfiguration _configuration;

        private float _rotation;
        private float _movement;
        private bool _isShooting;

        public delegate void Damaged();
        public event Damaged OnDamage;

        public delegate void Shoot(BulletData data);
        public event Shoot OnShoot;

        private void OnEnable()
        {
            _isShooting = false;
            StopAllCoroutines();
        }

        private void Update()
        {
            _movement = Input.GetAxis("Vertical");
            _rotation = -Input.GetAxis("Horizontal");

            Shot();
            Rotate();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Shot()
        {
            if (Input.GetKeyDown(KeyCode.Z) && !_isShooting)
            {
                _isShooting = true;

                OnShoot(new BulletData(transform.up, _bulletSpawner.position, _bulletSpawner.rotation));

                StartCoroutine(nameof(CanShoot));
            }
        }

        private IEnumerator CanShoot()
        {
            yield return new WaitForSeconds(_configuration.TimeShooting);
            _isShooting = false;
        }

        private void Rotate()
        {
            transform.Rotate(0, 0, _rotation * _configuration.RotationSpeed * Time.deltaTime);
        }

        private void Move()
        {
            if (_movement >= 0)
            {
                _rigidBody.AddForce(transform.up * Time.deltaTime * _configuration.MovementSpeed * _movement);

                if (_rigidBody.velocity.magnitude > _configuration.MaxVelocity)
                {
                    _rigidBody.velocity = _rigidBody.velocity.normalized * _configuration.MaxVelocity;
                }

                Protect(false);
            }
            else
            {
                Protect(true);
            }
        }

        private void Protect(bool active)
        {
            if (_shield.activeSelf != active)
            {
                _shield.SetActive(active);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag.Equals(General.AsteroidTag) && !DownArrowIsPressed)
            {
                ActivePlayer(false);
                OnDamage();
            }
        }

        public void ActivePlayer(bool active)
        {
            gameObject.SetActive(active);
        }

        private bool DownArrowIsPressed => _movement < 0;

        public void Restart()
        {
            transform.position = new Vector2(0, 0);
            transform.rotation = new Quaternion(0, 0, 0, 0);
            ActivePlayer(true);
        }
    }
}
