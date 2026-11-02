using System;
using UnityEngine;

public class TinyObject : MonoBehaviour
{
    private const float RadiansToDegrees = 180f / Mathf.PI;
    
    [Tooltip("Object mass in kg")] [SerializeField]
    private float mass;

    [Tooltip("Object moment of inertia in kg * m^2")] [SerializeField]
    private float momentOfInertia;

    [Tooltip("The collider of the object. This will be used to calculate it's moment of inertia")] [SerializeField]
    private Collider2D collider;
    
    private Vector2 _linearVelocity;
    private float _angularVelocity;

    private void FixedUpdate()
    {
        gameObject.transform.Translate(_linearVelocity * Time.fixedDeltaTime, Space.World);
        gameObject.transform.Rotate(0, 0, (_angularVelocity * Time.fixedDeltaTime * RadiansToDegrees));
    }

    public void ApplyForce(Vector2 force)
    {
        _linearVelocity += (force * Time.fixedDeltaTime) / mass;
    }

    public void ApplyForce(Vector2 force, Vector2 offset)
    {
        ApplyForce(force);
        ApplyTorque(offset.magnitude * force.magnitude * Mathf.Sin( Vector2.SignedAngle(offset, force) * Mathf.Deg2Rad ));
    }

    public void ApplyTorque(float torque)
    {
        _angularVelocity += (torque / momentOfInertia) * Time.fixedDeltaTime;
    }

    private void Start()
    {
        // Calculate the moment of inertia based on the collider
        momentOfInertia = MomentOfInteriaSolver.Solve(collider, mass);
    }

    private void Awake()
    {
        if (collider == null)
        {
            Debug.Log("Collider not set. Assigning automatically.");
            collider = GetComponent<Collider2D>();
        }
    }
}
