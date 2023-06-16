using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    // trap tag
    public string trapTag = "Trap";
    public string abilityTag = "Ability";
    private PlayerMovement _player;

    private void Start()
    {
        _player = GetComponent<PlayerMovement>();
    }
    //on trigger enter
    private void OnTriggerEnter2D(Collider2D other)
    {
        // if the tag is trap
        if (other.tag == trapTag)
        {
            if (_player.Invincible) return;
            _player.PlayerDead();
        }
        if (other.tag == abilityTag)
        {
            _player.EnableDash();
        }
    }
}
