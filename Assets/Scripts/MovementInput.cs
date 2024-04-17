using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveWithInput : MonoBehaviour
{
    public string _horizontalInputAxisName = "Horizontal";
    public string _verticalInputAxisName = "Vertical";
    public float _speed = 5.0f;

    private Rigidbody2D _rigidbody2D;

    private Vector2 _desiredDirection = Vector2.zero;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _desiredDirection.x = Input.GetAxis(_horizontalInputAxisName);
        _desiredDirection.y = Input.GetAxis(_verticalInputAxisName);
        _desiredDirection.Normalize();
    }

    private void FixedUpdate()
    {
        Vector2 l_currentPosition = transform.position;
        _rigidbody2D.MovePosition(l_currentPosition + _desiredDirection * _speed * Time.fixedDeltaTime);
    }
}

