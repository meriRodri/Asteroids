using Code.Configurations;
using Code.Data;
using Code.Objects;
using Code.Utils;
using UnityEngine;

namespace Code.Managers
{
    public class BulletsManager : MonoBehaviour
    {
        private Pool<Bullet> _bullets;
        [SerializeField] private BulletConfiguration _configuration;

        public void ShootBullet(BulletData data)
        {
            Bullet pooledBullet = _bullets.GetPooledObject();
            if (pooledBullet != null)
            {
                data.Speed = _configuration.BulletSpeed;
                data.Time = _configuration.BulletTime;
                pooledBullet.Movement(data);
            }
        }

        public void CreatePooledBullets()
        {
            if (_bullets != null)
            {
                return;
            }

            GameObject bulletPool = new GameObject(_configuration.BulletsPool);
            _bullets = new Pool<Bullet>(_configuration.SizeBulletsPool, _configuration.BulletPrefab, bulletPool.transform);
        }

        public void RemoveAllBullets()
        {
            int length = _bullets.Count;
            for (int i = 0; i < length; i++)
            {
                _bullets.GetPooledObjects()[i].gameObject.SetActive(false);
            }
        }
    }
}
