using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 15f;
    [SerializeField] public bool isMovingRight = true;

    Rigidbody2D bulletBody;

    void Start()
    {
        bulletBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // moving right flag decides direction the bullet should go towards
        bulletBody.velocity = new Vector2(bulletSpeed * (isMovingRight == true ? 1 : -1), 0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
}
