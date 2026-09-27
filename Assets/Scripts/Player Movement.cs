using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public float MovementSpeed;
    public float JumpImpulse;

    private int MovementState = 0; // 0 = idle, 1 = moving, 2 = aerial | To work with switch statement
    
    private Rigidbody TheRigidiestOfBodiesBro;

    public static PlayerMovement Player;
    
    
    void Start()
    {
        TheRigidiestOfBodiesBro = gameObject.GetComponent<Rigidbody>();
        Player = gameObject.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            TheRigidiestOfBodiesBro.linearVelocity += new Vector3(MovementSpeed*Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            TheRigidiestOfBodiesBro.linearVelocity -= new Vector3(MovementSpeed*Time.deltaTime, 0);
        }
        
        if (Input.GetKey(KeyCode.Space) && Timeswap.IsDay && MovementState != 2)
        {
            TheRigidiestOfBodiesBro.linearVelocity += new Vector3(0, JumpImpulse, 0);
            MovementState = 2;
        }
    }

    public void Sundial() // For all functions involving the timeswap mechanic, try to use the same name for the function
    {
        if (Timeswap.IsDay)
        {
            MovementSpeed = MovementSpeed * 1.5f;
        }
        else
        {
            MovementSpeed = MovementSpeed / 1.5f;
        }
    }
}
