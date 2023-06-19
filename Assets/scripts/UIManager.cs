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
    public TextMeshProUGUI WinText;
    public int keyLeft;
    public string hint = "Hint : You will briefly enter nothingness during the DASHING.";
    public GameObject DashKey;


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
        UpdateWinText();
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
            DashKey.SetActive(true);
        }
        else
        {
            abilityText.text = "";
            DashKey.SetActive(false);
        }
    }

    public void ShowWinPanel(bool show)
    {
        WinPanel.SetActive(show);
    }
    private void UpdateWinText()
    {
        if (WinText.text == null) return;
        WinText.text = $"You have died {GameManager.instance.playerDiedCount} times.\n" +
            $"Total time: {GameManager.instance.spendTime}.\n";
    }
}
