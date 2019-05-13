using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField] private bool killable;
    [SerializeField] private float speed;

    private const int PlayerLayer = 8;
    private const int SwordLayer = 9;

    private void FixedUpdate() {
        transform.position += Time.deltaTime * speed * Vector3.left;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.layer == PlayerLayer) {
            other.gameObject.GetComponent<Player>().Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (killable && other.gameObject.layer == SwordLayer) {
            Destroy(gameObject);
        }
    }
}