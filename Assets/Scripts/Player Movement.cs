using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public float MovementSpeed;
    public float JumpImpulse;

    private int MovementState = 0; // 0 = idle, 1 = moving, 2 = aerial | To work with switch statement
    
    private Rigidbody TheRigidiestOfBodiesBro;

    private bool IsNightform = false;
    
    
    void Start()
    {
        TheRigidiestOfBodiesBro = gameObject.GetComponent<Rigidbody>();
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
        
        if (Input.GetKey(KeyCode.Space) && !IsNightform && MovementState != 2)
        {
            TheRigidiestOfBodiesBro.linearVelocity += new Vector3(0, JumpImpulse, 0);
            MovementState = 2;
        }
    }
}
