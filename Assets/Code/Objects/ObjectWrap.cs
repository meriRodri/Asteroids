using Code.Managers;
using UnityEngine;

namespace Code.Objects
{
    public class ObjectWrap : MonoBehaviour
    {
        private void OnBecameInvisible()
        {
            Vector2 newPos = transform.position;

            if (transform.position.x > General.MaxX || transform.position.x < -General.MaxX)
            {
                newPos.x = -transform.position.x;
            }
            if (transform.position.y > General.MaxY || transform.position.y < -General.MaxY)
            {
                newPos.y = -transform.position.y;
            }

            transform.position = newPos;
        }

    }
}
