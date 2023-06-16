using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject finalTrigger;
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
        if (keyCount >= maxKeyCount)
        {
            finalTrigger.SetActive(true);
        }
    }
}
