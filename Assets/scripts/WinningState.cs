using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinningState : MonoBehaviour
{
    private AudioManager _audioManager;
    
    private void Start()
    {
        _audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.instance.WinPanel.SetActive(true);
            _audioManager.PlayWin();
            Time.timeScale = 0;
        }
    }
}
