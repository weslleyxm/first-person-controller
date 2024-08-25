using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    [SerializeField] private float movemetVelocity = 2;
    [SerializeField] private float sprinterVelocity = 6;
    [SerializeField] private float jumpStrength = 5;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask groundMask;
    private Rigidbody rbody;
    private float currentVelocity;
    private Controls controls;
    private bool isGrounded;

    void Start()
    {
        controls = GetComponent<Controls>();
        rbody = GetComponent<Rigidbody>();
        currentVelocity = movemetVelocity;
    } 

    void Update()
    { 
        isGrounded = IsGrounded();
    } 

    private void FixedUpdate()
    {
        PerformaceMovement();
    }

    public void PerformaceMovement()
    {
        currentVelocity = Mathf.Lerp(currentVelocity, controls.sprinter ? sprinterVelocity : movemetVelocity, 5 * Time.deltaTime);
        if (!isGrounded) currentVelocity = movemetVelocity; 

        Vector3 moveDirection = (transform.forward * controls.vertical + transform.right * controls.horizontal).normalized;
        Vector3 velocity = moveDirection * currentVelocity * 100 * Time.fixedDeltaTime;
        velocity.y = rbody.velocity.y;
        rbody.velocity = velocity; 

        if (isGrounded && controls.jump)
        {
            rbody.AddForce(new Vector3(0, jumpStrength, 0), ForceMode.Impulse);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, groundCheckRadius);
    }

    public bool IsGrounded()
    {
        return Physics.CheckSphere(transform.position, groundCheckRadius, groundMask);
    }
}
