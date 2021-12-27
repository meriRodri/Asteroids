using Code.Data;
using System.Collections;
using UnityEngine;

namespace Code.Objects
{
    public class Bullet : ObjectWrap
    {
        [SerializeField] private Rigidbody2D _rigidBody;

        public void Movement(BulletData data)
        {
            transform.position = data.Position;
            transform.rotation = data.Rotation;
            gameObject.SetActive(true);
            _rigidBody.AddForce(data.Direction * data.Speed * Time.timeScale);
            IEnumerator coroutine = DestroyBullet(data.Time);
            StartCoroutine(coroutine);
        }

        private IEnumerator DestroyBullet(float time)
        {
            yield return new WaitForSeconds(time);
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            gameObject.SetActive(false);
            StopCoroutine(nameof(DestroyBullet));
        }
    }
}
