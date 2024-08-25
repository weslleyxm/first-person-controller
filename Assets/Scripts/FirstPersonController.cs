using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    [SerializeField] private float movemetVelocity = 2;
    [SerializeField] private float sprinterVelocity = 6;
    [SerializeField] private float jumpHeight = 5;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask groundMask;
    private CharacterController characterController;
    private float currentVelocity;
    private Controls controls;
    private bool isGrounded;
    private float verticalVelocity;
    public float gravity = -15.0f;

    private void Start()
    {
        controls = GetComponent<Controls>();
        characterController = GetComponent<CharacterController>();
        currentVelocity = movemetVelocity;
    }

    private void Update()
    {
        isGrounded = IsGrounded();

        PerformaceMovement();
        Gravity();
    }

    public void PerformaceMovement()
    {
        currentVelocity = Mathf.Lerp(currentVelocity, controls.sprinter ? sprinterVelocity : movemetVelocity, 5 * Time.deltaTime);
        if (!isGrounded) currentVelocity = movemetVelocity;
        Vector3 moveDirection = (transform.forward * controls.vertical + transform.right * controls.horizontal).normalized;
        characterController.Move(moveDirection * (currentVelocity * Time.deltaTime) + new Vector3(0.0f, verticalVelocity, 0.0f) * Time.deltaTime);
    }

    private void Gravity()
    {
        if (isGrounded)
        {
            if (verticalVelocity < 0.0f)
            {
                verticalVelocity = -2f;
            }

            if (controls.jump)
                verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, groundCheckRadius);
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(transform.position, groundCheckRadius, groundMask);
    }
}
