using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Respawn : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Start()
         {
             rb = GetComponent<Rigidbody2D>();
         }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Respawn"))
        {
            rb.position = new Vector3 (-128.2f,18.34f);
            rb.linearVelocity = Vector2.zero;
        }
    }
}
