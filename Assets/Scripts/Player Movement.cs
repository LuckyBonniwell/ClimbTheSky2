using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public float MovementSpeed;
    public float JumpImpulse;
    public float WallStickDistance;

    private int WallDirectionality = 1; // 1 = Wall is to left, -1 = Wall is to right, for use in wallclimbing

    private int MovementState = 0; // 0 = idle, 1 = moving, 2 = aerial | To work with switch statement
    
    private Rigidbody TheRigidiestOfBodiesBro;
    
    public static PlayerMovement Player;
    
    public LayerMask LayerHit;

    public GameObject TestCube;
    public GameObject TestLeftBound;
    public GameObject TestRightBound;
    
    
    void Start()
    {
        TheRigidiestOfBodiesBro = gameObject.GetComponent<Rigidbody>();
        Player = gameObject.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        TestCube.transform.position = transform.position;
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
            if (Physics.Linecast(new Vector3((transform.position.x - WallStickDistance/3), transform.position.y, 0), new Vector3((transform.position.x + WallStickDistance/3), transform.position.y, 0), out RaycastHit hit, LayerHit))
            {
                if (hit.transform.position.x < transform.position.x)
                {
                    Debug.Log("Wall hit to left");
                    WallDirectionality = -1;
                }
                else
                {
                    Debug.Log("Wall hit to right");
                    WallDirectionality = 1;
                }
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
        if (!Timeswap.IsDay)
        {
            Vector3 LineLeftBound = new Vector3((transform.position.x - WallStickDistance/3), transform.position.y, 0);
            if (Physics.Linecast(LineLeftBound, new Vector3((transform.position.x + WallStickDistance/3), transform.position.y, 0), LayerHit))
            {
                Debug.Log("Linecast hit");
                MovementState = 3;
            }
        }
    }

    private void Wallrun()
    {
        if (Input.GetKey(KeyCode.D))
        {
            TheRigidiestOfBodiesBro.linearVelocity += new Vector3(0, MovementSpeed*Time.deltaTime*WallDirectionality);
            Ray RonaldRaygun = new Ray(gameObject.transform.position, new Vector3(0, 1*WallDirectionality, 0));
            if (Physics.Raycast(RonaldRaygun, 1.05f, LayerHit))
            {
                Debug.Log("Raycast hit");
                MovementState = 1;
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            TheRigidiestOfBodiesBro.linearVelocity -= new Vector3(0, MovementSpeed*Time.deltaTime*WallDirectionality);
            Ray RonaldRaygun = new Ray(gameObject.transform.position, new Vector3(0, -1*WallDirectionality, 0));
            if (Physics.Raycast(RonaldRaygun, 1.05f, LayerHit))
            {
                Debug.Log("Raycast hit");
                MovementState = 1;
            }
        }
        if (!Physics.Linecast(new Vector3((transform.position.x - WallStickDistance/3), transform.position.y), new Vector3((transform.position.x + WallStickDistance/3), transform.position.y), LayerHit))
        {
            Debug.Log("Linecast not hit");
            MovementState = 1;
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
