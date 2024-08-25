using UnityEngine; 

public class CameraController : MonoBehaviour
{
    [SerializeField] float clampLookY;
    [SerializeField] float xSpeed = 2, ySpeed = 2; 
    [SerializeField] Transform cameraTransform;  

    private Controls controls;  
    private Vector2 cameraRotate; 

    void Start()
    {
        controls = GetComponent<Controls>();     
    }
       
    void Update()
    {
        cameraRotate.x = controls.mouseMovementX * xSpeed * Time.fixedDeltaTime;
        cameraRotate.y = controls.mouseMovementY * ySpeed * Time.fixedDeltaTime;  
        cameraRotate.y = Mathf.Clamp(cameraRotate.y, -clampLookY, clampLookY);  
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0, cameraRotate.x, 0);
        cameraTransform.localRotation = Quaternion.Euler(cameraRotate.y, 0, 0);
    }
}
