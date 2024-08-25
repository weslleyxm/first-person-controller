using UnityEngine; 
using UnityEngine.EventSystems; 

public class MobileInputs : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{  
    private Vector2 touchMovement;  
    private bool pressed; 
    public bool jump;
    public bool sprinter;
    public Joystick joystick; 

    public Vector2 Touch()
    {
        return touchMovement; 
    }

    public bool IsTouchingArea()
    {
        return pressed;
    }
     
    void Update()
    {
        if (pressed)
        {
            Touch firstFinger = Input.GetTouch(0);
            if (firstFinger.phase == TouchPhase.Moved)
            {
                touchMovement.x += firstFinger.deltaPosition.x;
                touchMovement.y -= firstFinger.deltaPosition.y; 
            } 
        }
        else
        {
            touchMovement = new Vector2(); 
        }
    } 

    public void Jump(bool state)
    {
        jump = state;
    }

    public void Run(bool state)
    {
        sprinter = state;
    } 

    public void OnPointerDown(PointerEventData eventData)
    {
        pressed = true; 
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pressed = false; 
    }
}
