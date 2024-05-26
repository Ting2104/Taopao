using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetSpawn : MonoBehaviour
{
    Vector2 startPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Obstacle"))
        {
            Die();
        }
    }
    void Die()
    {
        Respawn();
    }
    public void Respawn()
    {
        transform.position = startPos;
    }
}
