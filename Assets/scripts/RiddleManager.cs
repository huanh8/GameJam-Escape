using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RiddleManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> PushingBoxes = new List<GameObject>();
    public TriggerChest ChestBox;
    public List<Vector3> Box01DefaultPos = new List<Vector3>(new Vector3[3]);

    void OnEnable()
    {
        PlayerMovement.OnPlayerReset += ResetPushingBoxes;
        GameManager.OnRestart += ResetChest;

    }
    void OnDisable()
    {
        PlayerMovement.OnPlayerReset -= ResetPushingBoxes;
        GameManager.OnRestart -= ResetChest;
    }
    void Start()
    {
        foreach (GameObject box in PushingBoxes)
        {
            Box01DefaultPos.Add(box.transform.position);
        }
    }

    void ResetPushingBoxes()
    {
        foreach (GameObject box in PushingBoxes)
        {
            box.transform.position = Box01DefaultPos[PushingBoxes.IndexOf(box)];
        }
        Debug.Log("Reset pushing boxes");
    }

    void ResetChest()
    {
        ChestBox.ResetChest();
    }

}
