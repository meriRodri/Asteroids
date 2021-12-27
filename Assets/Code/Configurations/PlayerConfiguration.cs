using UnityEngine;

namespace Code.Configurations
{
    [CreateAssetMenu(fileName = "PlayerConfiguration", menuName = "ScriptableObjects/PlayerConfiguration", order = 2)]
    public class PlayerConfiguration : ScriptableObject
    {
        [SerializeField] private int _rotationSpeed = 200;
        [SerializeField] private int _movementSpeed = 100;
        [SerializeField] private float _timeShooting = 0.5f;
        [SerializeField] private float _maxVelocity = 3f;

        public int RotationSpeed => _rotationSpeed;
        public int MovementSpeed => _movementSpeed;
        public float TimeShooting => _timeShooting;
        public float MaxVelocity => _maxVelocity;
    }
}
