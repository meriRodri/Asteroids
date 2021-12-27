using Code.Objects;
using UnityEngine;

namespace Code.Configurations
{
    [CreateAssetMenu(fileName = "BulletConfiguration", menuName = "ScriptableObjects/BulletConfiguration", order = 4)]
    public class BulletConfiguration : ScriptableObject
    {
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private int _bulletSpeed = 500;
        [SerializeField] private int _bulletTime = 2;
        [SerializeField] private int _sizeBulletsPool;
        [SerializeField] private string _bulletsPool = "BulletsPool";

        public Bullet BulletPrefab => _bulletPrefab;
        public int BulletSpeed => _bulletSpeed;
        public int BulletTime => _bulletTime;
        public int SizeBulletsPool => _sizeBulletsPool;
        public string BulletsPool => _bulletsPool;
    }
}
