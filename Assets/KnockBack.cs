using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class KnockBack : MonoBehaviour
{
    public float knockbackPower = 20f;
    public float knockbackDuration = 1f;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(PlayerController1.instance.KnockBack(knockbackDuration, knockbackPower, this.transform ));
        }
    }
}
