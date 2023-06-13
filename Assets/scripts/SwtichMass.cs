using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwtichMass : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float bigMass = 2000f;
    [SerializeField] private float smallMass = 10f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // if it collider with player change the mass form 2000 to 10
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            rb.mass = smallMass;
        }else
        {
            rb.mass = bigMass;
        }
    }

}
