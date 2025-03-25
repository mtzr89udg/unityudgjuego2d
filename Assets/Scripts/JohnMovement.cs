using UnityEngine;

public class JohnMovement : MonoBehaviour
{
    public GameObject BulletPrefab;
    public float Speed;
    public float JumpForce;
    private Rigidbody2D Rigidbody2D;
    private float Horizontal;
    private bool Grounded;
    private Animator Animator;
    private float LastShoot;
    
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
        
        if (Horizontal < 0) transform.localScale = new Vector3(-1.0f,1.0f,1.0f);
        else if (Horizontal > 0.0f) transform.localScale = new Vector3(1.0f,1.0f,1.0f);
        
        Animator.SetBool("running",Horizontal != 0.0f);
        
        //Debug.DrawRay(transform.position, Vector3.down * 0.1f, Color.red);
        Grounded = Physics2D.Raycast(transform.position,Vector2.down,0.1f);
        
        if(Input.GetKeyDown(KeyCode.W) && Grounded)
        {
            Jump();
        }
        
        if (Input.GetKey(KeyCode.Space) && Time.time > LastShoot + 0.25f)
        {
            Shoot();
            LastShoot = Time.time;
        }
    }
    
    private void Shoot()
    {
        Vector3 direction;
        if (transform.localScale.x == 1.0f) direction = Vector2.right;
        else direction = Vector2.left;
        GameObject bullet = Instantiate(BulletPrefab,transform.position + direction * 0.1f,Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction);
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
