using UnityEngine; 
using UnityEngine.EventSystems; 

[RequireComponent(typeof(RectTransform))]
public class Joystick : UIBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{ 
    public GameObject[] sides = new GameObject[0];

    [SerializeField] RectTransform _joystickGraphic;
    public bool isDragging;

    [HideInInspector] public float joystickY; 
    [HideInInspector] public float joystickX;

    private RectTransform _rectTransform;
    private Vector2 _axis;
    private GameObject _LasSides;

    public RectTransform rectTransform
    {
        get
        {
            if (!_rectTransform)
            {
                _rectTransform = transform as RectTransform;
            }
            return _rectTransform;
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!IsActive())
        {
            return;
        }
        EventSystem.current.SetSelectedGameObject(gameObject, eventData);
        Vector2 newAxis = transform.InverseTransformPoint(eventData.position);
        newAxis.x /= rectTransform.sizeDelta.x * 0.5f;
        newAxis.y /= rectTransform.sizeDelta.y * 0.5f;
        SetAxisMS(newAxis);
        isDragging = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;


        if (_LasSides) _LasSides.SetActive(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (sides.Length > 0)
        {
            if (_LasSides) _LasSides.SetActive(false);

            if (_axis.x > 0 && _axis.y > 0)
                _LasSides = sides[0];
            else if (_axis.x > 0 && _axis.y < 0)
                _LasSides = sides[1];
            else if (_axis.x < 0 && _axis.y < 0)
                _LasSides = sides[2];
            else
                _LasSides = sides[3];
        }

        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out _axis);
        _axis.x /= rectTransform.sizeDelta.x * 0.5f;
        _axis.y /= rectTransform.sizeDelta.y * 0.5f;
        SetAxisMS(_axis);

        if (_LasSides) _LasSides.SetActive(true);
    }

    void OnDeselect()
    {
        isDragging = false;
    }

    void LateUpdate()
    {
        if (!isDragging)
        {
            if (_axis != Vector2.zero)
            {
                Vector2 newAxis = _axis - (_axis * Time.deltaTime * 25.0f);
                if (newAxis.sqrMagnitude <= 0.1f)
                {
                    newAxis = Vector2.zero;
                }
                SetAxisMS(newAxis);
            }


            joystickY = 0;
            joystickX = 0;
        }
    }

    public void SetAxisMS(Vector2 axis)
    {
        _axis = Vector2.ClampMagnitude(axis, 1);
        UpdateJoystickGraphic();
        joystickY = _axis.y;
        joystickX = _axis.x;
    }

    void UpdateJoystickGraphic()
    {
        if (_joystickGraphic)
        {
            _joystickGraphic.localPosition = _axis * Mathf.Max(rectTransform.sizeDelta.x, rectTransform.sizeDelta.y) * 0.5f;
        }
    }

    private new void OnDisable()
    {
        base.OnDisable();

        joystickY = 0;
        joystickX = 0;
        isDragging = false;

        foreach (var item in sides)
        {
            if (item)
            {
                item.gameObject.SetActive(false);
            }
        }

        _joystickGraphic.localPosition = Vector3.zero;
    }

#if UNITY_EDITOR
    protected override void OnValidate()
    {
        base.OnValidate();
        UpdateJoystickGraphic();
    }
#endif
}