using UnityEngine;

namespace Code.Data
{
    public class BulletData
    {
        public Vector3 Direction;
        public Vector2 Position;
        public Quaternion Rotation;
        public int Speed;
        public int Time;

        public BulletData(BulletData data, int speed, int time)
        {
            Direction = data.Direction;
            Position = data.Position;
            Rotation = data.Rotation;
            Speed = speed;
            Time = time;
        }

        public BulletData(Vector3 direction, Vector2 position, Quaternion rotation)
        {
            Direction = direction;
            Position = position;
            Rotation = rotation;
        }
    }
}
