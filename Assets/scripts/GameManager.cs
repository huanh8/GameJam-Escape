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
    public int playerDiedCount;
    public string spendTime = "";
    private float startTime = 0;
    private bool isRunning;

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
        finalBlock?.SetActive(false);
        playerDiedCount = 0;
        isRunning = false;
    }


    public int keyCount = 0;
    public int maxKeyCount = 4;

    private void Update()
    {
        OpenTheFinalDoor();
        ShowRoadblock(maxKeyCount - keyCount > 1);
        RecordTime();
    }

    public void AddKey()
    {
        keyCount++;
        AudioManager.instance?.PlayGetKey();
        if (keyCount >= maxKeyCount)
        {
            keyCount = maxKeyCount;
            AudioManager.instance?.PlayUnlockDoor();
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
        Time.timeScale = 1;
        keyCount = 0;
        OnRestart.Invoke();
        player?.DisableDash();
        UIManager.instance?.ShowWinPanel(false);
        AudioManager.instance?.PlayBackground();
        playerDiedCount = 0;
        startTime = Time.time;
        isRunning = true;

    }
    private void ShowRoadblock(bool show)
    {
        roadBlock.SetActive(show);
    }
    public void CountDiedTime()
    {
        playerDiedCount++;
    }
    private void RecordTime()
    {
        if (!isRunning) return;
        float elapsedTime = Time.time - startTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        spendTime = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
