using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            // Extras
            GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;

            // Getting the Particle System
            ParticleSystem _particles;
            TryGetComponent<ParticleSystem>(out _particles);

            // Playing Particle Effects
            if(_particles != null)
                _particles.Play();

            // Destorying Enemy, If Hit enemy
            if (collision.gameObject.CompareTag("Enemy"))
            {
                ScoreTracker.score += 1;
                Destroy(collision.gameObject);
            }

            // Destroying the Bullet
            Destroy(gameObject, (_particles == null)? 0f : _particles.main.duration);
        }
    }
}