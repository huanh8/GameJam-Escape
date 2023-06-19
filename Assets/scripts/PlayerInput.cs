using UnityEngine;

public class PlayerInput : MonoBehaviour, IPlayerInput
{
    public bool IsMoveLeft()
    {
        return Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
    }

    public bool IsMoveRight()
    {
        return Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
    }

    public bool IsMoveUp()
    {
        return Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
    }

    public bool IsMoveDown()
    {
        return Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
    }

    public bool IsDash()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
}
