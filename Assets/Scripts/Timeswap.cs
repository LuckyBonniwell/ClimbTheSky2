using UnityEngine;

public class Timeswap : MonoBehaviour
{
    public static bool IsDay = true;
    private bool Annoyance;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q) && !Annoyance)
        {
            Sundial();
            Annoyance = true;
        }
        if (Input.GetKeyUp(KeyCode.Q)) // Because using UnityEngine.InputSystem; doesn't work.
        {
            Annoyance = false;
        }
    }

    void Sundial() // For all functions involving the timeswap mechanic, try to use the same name for the function
    {
        if (IsDay)
        {
            Camera.main.backgroundColor = new Color(.0390625f, .0625f, .1015625f, 1);
            IsDay = false;
        }
        else
        {
            Camera.main.backgroundColor = new Color(.32421875f, .484375f, .75390625f, 1);
            IsDay = true;
        }
        PlayerMovement.Player.Sundial();
    }
}
