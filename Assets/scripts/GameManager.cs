using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    // create a OnRestart delegate
    public static UnityAction OnRestart;

    public GameObject finalTrigger;
    public GameObject roadBlock;
    public GameObject finalBlock;

    public PlayerMovement player;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        finalBlock.SetActive(false);
    }


    public int keyCount = 0;
    public int maxKeyCount = 4;

    private void Update()
    {
        OpenTheFinalDoor();
        ShowRoadblock(maxKeyCount - keyCount > 1);
    }

    public void AddKey()
    {
        keyCount++;
        if (keyCount >= maxKeyCount)
        {
            keyCount = maxKeyCount;
        }
    }
    public void UseKey()
    {
        keyCount--;
        if (keyCount <= 0)
        {
            keyCount = 0;
        }
    }
    public void OpenTheFinalDoor()
    {
        finalTrigger.SetActive(keyCount >= maxKeyCount);
        finalBlock.SetActive(keyCount >= maxKeyCount);
    }

    public void RestartGame()
    {
        OnRestart.Invoke();
        keyCount = 0;
        player?.DisableDash();
    }
    private void ShowRoadblock(bool show)
    {
        roadBlock.SetActive(show);
    }
}
