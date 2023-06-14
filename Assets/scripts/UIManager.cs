using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public TextMeshProUGUI keyText;

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
    }
    public void UpdateKeyText(int keyCount)
    {
        keyText.text = "x "+keyCount.ToString();
    }
}
