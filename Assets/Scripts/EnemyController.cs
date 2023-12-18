using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Rigidbody2D enemyBody;
    [SerializeField] float moveSpeed = 3f;

    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        enemyBody.velocity = new Vector2(moveSpeed, 0);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // on reversing direction we need to reverse speed
        moveSpeed = -moveSpeed;
        FlipEnemy();
    }

    void FlipEnemy()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(enemyBody.velocity.x)), 1f);
    }
}
