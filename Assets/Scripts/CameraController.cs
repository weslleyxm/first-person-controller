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
 
    private void LateUpdate()   
    {
#if UNITY_ANDROID
        float deltaTimeMultiplier = 0.006f;
#else
        float deltaTimeMultiplier = 0.03f; 
#endif

        cameraRotate.x = controls.mouseMovementX * xSpeed * deltaTimeMultiplier;
        cameraRotate.y = controls.mouseMovementY * ySpeed * deltaTimeMultiplier;
        cameraRotate.y = ClampAngle(cameraRotate.y, -clampLookY, clampLookY);

        transform.rotation = Quaternion.Euler(0, cameraRotate.x, 0);
        cameraTransform.localRotation = Quaternion.Euler(cameraRotate.y, 0, 0);
    }

    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }
}
 