using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] AudioClip coinPickupSFX;
    [SerializeField] int scoreValue = 1;

    bool wasCollected = false;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !wasCollected)
        {
            wasCollected = true;
            AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);
            GameManager.instance.IncreaseScore(scoreValue);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
