using UnityEngine;

public class JohnMovement : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    private Rigidbody2D Rigidbody2D;
    private float Horizontal;
    private bool Grounded;
    private Animator Animator;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         Rigidbody2D = GetComponent<Rigidbody2D>();
         Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        
        Animator.SetBool("running",Horizontal != 0.0F);
        
        //Debug.DrawRay(transform.position, Vector3.down * 0.1f, Color.red);
        Grounded = Physics2D.Raycast(transform.position,Vector2.down,0.1f);
        
        if(Input.GetKeyDown(KeyCode.W) && Grounded)
        {
            Jump();
        }
    }
    
    private void FixedUpdate()
    {
        Rigidbody2D.linearVelocity = new Vector2(Horizontal * Speed, Rigidbody2D.linearVelocity.y);
    }
    
    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
    }
}
