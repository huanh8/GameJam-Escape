using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public TextMeshProUGUI keyText;
    public TextMeshProUGUI taskText;
    public TextMeshProUGUI abilityText;
    public int keyLeft;

    private void Awake() {
        if(instance == null)
        {
            instance = this;
        }else{
            Destroy(gameObject);
        }
    }

    private void Update() {

        UpdateKeyText(GameManager.instance.keyCount);
        keyLeft= GameManager.instance.maxKeyCount - GameManager.instance.keyCount;
        UpdateTaskText();
    }

    public void UpdateKeyText(int keyCount)
    {
        keyText.text = "x "+ keyCount.ToString();
    }
    public void UpdateTaskText()
    {
        if (keyLeft == 0)
        {
            taskText.text = $"Task : Find the door to escape.";
            return;
        }
        taskText.text = $"Task : Find {keyLeft.ToString()} key(s) to unlock the door.";
    }
}
