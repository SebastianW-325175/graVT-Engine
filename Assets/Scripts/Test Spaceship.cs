using UnityEngine;
using UnityEngine.InputSystem;

public class TestSpaceship : MonoBehaviour
{
    private TinyObject _tinyObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _tinyObject = gameObject.GetComponent<TinyObject>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Keyboard.current.spaceKey.isPressed)
        {
            _tinyObject.ApplyForce(_tinyObject.transform.up * 3);
        }

        if (Keyboard.current.shiftKey.isPressed)
        {
            _tinyObject.ApplyForce(_tinyObject.transform.up * -3);
        }

        if (Keyboard.current.leftArrowKey.isPressed)
        {
            _tinyObject.ApplyTorque(0.2f);
        }

        if (Keyboard.current.rightArrowKey.isPressed)
        {
            _tinyObject.ApplyTorque(-0.2f);
        }
    }
}
