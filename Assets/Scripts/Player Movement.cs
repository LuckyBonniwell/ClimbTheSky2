using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public float MovementSpeed;
    public float JumpImpulse;
    public float WallStickDistance;

    private int MovementState = 0; // 0 = idle, 1 = moving, 2 = aerial | To work with switch statement
    
    private Rigidbody TheRigidiestOfBodiesBro;
    
    public static PlayerMovement Player;
    
    public LayerMask LayerHit;
    
    
    void Start()
    {
        TheRigidiestOfBodiesBro = gameObject.GetComponent<Rigidbody>();
        Player = gameObject.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        switch (MovementState)
        {
            case 0:
                StandardMovement();
                // Idle animate
                break;
            case 1:
                StandardMovement();
                break;
            case 2:
                AerialCheck();
                break;
            case 3:
                Wallrun();
                break;
        }
    }

    private void StandardMovement()
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

        if (!Timeswap.IsDay)
        {
            if (Physics.Linecast(new Vector3((transform.position.x - WallStickDistance), 0), new Vector3((transform.position.x + WallStickDistance), 0), LayerHit))
            {
                Debug.Log("Linecast hit");
                MovementState = 3;
            }
        }
    }

    private void AerialCheck()
    {
        if (TheRigidiestOfBodiesBro.linearVelocity.y <= 0)
        {
            Ray RonaldRaygun = new Ray(gameObject.transform.position, new Vector3(0, -1, 0));
            if (Physics.Raycast(RonaldRaygun, 1.05f, LayerHit))
            {
                Debug.Log("Raycast hit");
                MovementState = 1;
            }
        }
    }

    private void Wallrun()
    {
        if (Input.GetKey(KeyCode.D))
        {
            TheRigidiestOfBodiesBro.linearVelocity += new Vector3(0, MovementSpeed*Time.deltaTime);
            Ray RonaldRaygun = new Ray(gameObject.transform.position, new Vector3(0, 1, 0));
            if (Physics.Raycast(RonaldRaygun, 1.05f, LayerHit))
            {
                Debug.Log("Raycast hit");
                MovementState = 1;
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            TheRigidiestOfBodiesBro.linearVelocity -= new Vector3(0, MovementSpeed*Time.deltaTime);
            Ray RonaldRaygun = new Ray(gameObject.transform.position, new Vector3(0, -1, 0));
            if (Physics.Raycast(RonaldRaygun, 1.05f, LayerHit))
            {
                Debug.Log("Raycast hit");
                MovementState = 1;
            }
        }
    }

    public void Sundial() // For all functions involving the timeswap mechanic, try to use the same name for the function
    {
        if (Timeswap.IsDay)
        {
            MovementSpeed = MovementSpeed / 1.5f;
        }
        else
        {
            MovementSpeed = MovementSpeed * 1.5f;
            if (MovementState == 3)
            {
                MovementState = 2;
            }
        }
    }
}
