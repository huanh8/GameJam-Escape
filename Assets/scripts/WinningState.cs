using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinningState : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.instance.WinPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
