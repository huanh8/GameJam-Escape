using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMovement : MonoBehaviour
{
    public float speed = 10f;

    // Update is called once per frame
    void Update()
    {
        // Since the arrow spawnpoint has rotated,
        // the Vector2 needs rotated as well
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Trap"))
        {
            Destroy(gameObject);
        }
    }
}
