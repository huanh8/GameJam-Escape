using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake() {
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
}
