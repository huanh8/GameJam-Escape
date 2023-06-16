using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
  // create a OnRestart delegate
    public static UnityAction OnRestart ;
    
    public GameObject finalTrigger;
    public GameObject roadblock;
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

    public int keyCount = 0;
    public int maxKeyCount = 4;

    private void Update()
    {
        OpenTheFinalDoor();
        ShowRoadblock(maxKeyCount - keyCount != 1);
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
    }

    public void RestartGame()
    { 
        OnRestart.Invoke();  
        keyCount = 0;        
    }
    private void ShowRoadblock(bool show)
    {
        roadblock.SetActive(show);
    }
}
