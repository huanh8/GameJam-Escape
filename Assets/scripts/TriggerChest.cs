using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cainos.PixelArtPlatformer_VillageProps;

public class TriggerChest : MonoBehaviour
{
    private Chest chest;
    public Animator keyAnimator;
    private bool hasKey = true;
    // Start is called before the first frame update
    void Start()
    {
        chest = GetComponentInParent<Chest>();
    }

    //trigger chest when collider with player
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player")
        {
            chest.Open();
            if (hasKey)
            {
                keyAnimator.SetBool("IsKey", true);
                GameManager.instance.AddKey();
                hasKey = false; 
            }

        }
    }
    
}
