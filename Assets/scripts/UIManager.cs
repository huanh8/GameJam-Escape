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
    public GameObject WinPanel;
    public int keyLeft;
    public string hint = "Hint :  Press 'SPACE' to dash and you will be invincible.";


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        WinPanel.SetActive(false);
    }

    private void Update()
    {

        UpdateKeyText(GameManager.instance.keyCount);
        keyLeft = GameManager.instance.maxKeyCount - GameManager.instance.keyCount;
        UpdateTaskText();
    }

    public void UpdateKeyText(int keyCount)
    {
        keyText.text = "x " + keyCount.ToString();
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


    public void ShowHint(bool enable)
    {
        if (enable)
        {
            abilityText.text = hint;
        }
        else
        {
            abilityText.text = "";
        }
    }

    public void ShowWinPanel(bool show)
    {
        WinPanel.SetActive(show);
    }
}
