using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;
using Vector2 = UnityEngine.Vector2;

public class TestSpaceship : MonoBehaviour
{
    private TinyObject _tinyObject;
    private VisualEffect _leftEngineEffect;
    private VisualEffect _rightEngineEffect;
    private VisualEffect _leftManeuverEngineEffect;
    private VisualEffect _rightManeuverEngineEffect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _tinyObject = gameObject.GetComponent<TinyObject>();
        _leftEngineEffect = gameObject.transform.Find("Left Engine Trail").gameObject.GetComponent<VisualEffect>();
        _rightEngineEffect = gameObject.transform.Find("Right Engine Trail").gameObject.GetComponent<VisualEffect>();
        _leftManeuverEngineEffect = gameObject.transform.Find("Left Maneuver Engine Trail").gameObject.GetComponent<VisualEffect>();
        _rightManeuverEngineEffect = gameObject.transform.Find("Right Maneuver Engine Trail").gameObject.GetComponent<VisualEffect>();
        _leftEngineEffect.Stop();
        _rightEngineEffect.Stop();
        _leftManeuverEngineEffect.Stop();
        _rightManeuverEngineEffect.Stop();
    }
    
    void FixedUpdate()
    {
        if (Keyboard.current.spaceKey.isPressed)
        {
            _tinyObject.ApplyForce(
                Utils.RotateVector(Vector2.up * 3.0f, gameObject.transform.rotation.eulerAngles.z * Mathf.Deg2Rad )
            );
        }

        if (Keyboard.current.leftArrowKey.isPressed)
        {
            _tinyObject.ApplyForce(
                Utils.RotateVector(Vector2.up, (gameObject.transform.rotation.eulerAngles.z - 45) * Mathf.Deg2Rad), 
                Utils.RotateVector(new Vector2(0.2f, -0.15f), (gameObject.transform.rotation.eulerAngles.z) * Mathf.Deg2Rad )
            );
        }

        if (Keyboard.current.rightArrowKey.isPressed)
        {
            _tinyObject.ApplyForce(
                Utils.RotateVector(Vector2.up, (gameObject.transform.rotation.eulerAngles.z + 45) * Mathf.Deg2Rad), 
                Utils.RotateVector(new Vector2(-0.2f, -0.15f), (gameObject.transform.rotation.eulerAngles.z) * Mathf.Deg2Rad )
            );
        }
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            _leftEngineEffect.Play();
            _rightEngineEffect.Play();
        }
        else if (Keyboard.current.spaceKey.wasReleasedThisFrame)
        {
            _leftEngineEffect.Stop();
            _rightEngineEffect.Stop();
        }

        if (Keyboard.current.rightArrowKey.wasPressedThisFrame) _leftManeuverEngineEffect.Play();
        else if (Keyboard.current.rightArrowKey.wasReleasedThisFrame) _leftManeuverEngineEffect.Stop();
        
        if (Keyboard.current.leftArrowKey.wasPressedThisFrame) _rightManeuverEngineEffect.Play();
        else if (Keyboard.current.leftArrowKey.wasReleasedThisFrame) _rightManeuverEngineEffect.Stop();
    }
}
