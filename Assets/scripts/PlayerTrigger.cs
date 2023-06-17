using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    // trap tag
    public string trapTag = "Trap";
    public string abilityTag = "Ability";
    public string finalDoor = "FinalDoor";
    private PlayerMovement _player;

    private void Start()
    {
        _player = GetComponent<PlayerMovement>();
    }
    //on trigger enter
    private void OnTriggerEnter2D(Collider2D other)
    {
        // if the tag is trap
        if (other.CompareTag(trapTag))
        {
            if (_player.Invincible) return;
            _player.PlayerDead();
        }
        if (other.CompareTag(abilityTag))
        {
            _player.EnableDash();
        }
        if (other.CompareTag(finalDoor))
        {
            UIManager.instance.WinPanel.SetActive(true);
            AudioManager.instance?.PlayWin();
            _player.CanMove = false;   
            Time.timeScale = 0; 
        }
    }
}
