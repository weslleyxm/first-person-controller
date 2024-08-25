using UnityEngine;

public class Controls : MonoBehaviour
{
    public float vertical;
    public float horizontal;
    public bool sprinter;
    public bool jump;
    public float mouseMovementX;
    public float mouseMovementY;

    [SerializeField] private MobileInputs mobileInputs;

    private void Update()
    { 
        sprinter = Input.GetKey(KeyCode.LeftShift) && vertical > 0 || mobileInputs.sprinter && vertical > 0;
        jump = Input.GetKey(KeyCode.Space) || mobileInputs.jump;

#if UNITY_ANDROID
        UpdateJoystick();
        UpdateTouch();
#else
        UpdateMouse();
        UpdateKeyboard();
#endif 
    }

    public void UpdateMouse()
    {
        mouseMovementX += Input.GetAxis("Mouse X");
        mouseMovementY -= Input.GetAxis("Mouse Y");
    }

    public void UpdateTouch()
    {
        mouseMovementX = mobileInputs.Touch().x;
        mouseMovementY = mobileInputs.Touch().y;
    }

    public void UpdateJoystick()
    {
        vertical = mobileInputs.joystick.joystickY;
        horizontal = mobileInputs.joystick.joystickX;
    }

    public void UpdateKeyboard()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal"); 
    }
}
