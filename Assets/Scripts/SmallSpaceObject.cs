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
    private Collider2D collider = null;
    
    private Vector2 _linearVelocity;
    private float _angularVelocity;

    private void FixedUpdate()
    {
        gameObject.transform.Translate(_linearVelocity * Time.fixedDeltaTime);
        gameObject.transform.Rotate(0, 0, (_angularVelocity * Time.fixedDeltaTime * RadiansToDegrees));
    }

    public void ApplyForce(Vector2 force)
    {
        _linearVelocity += (force * Time.fixedDeltaTime) / mass;
    }

    public void ApplyTorque(float torque)
    {
        _angularVelocity += (torque / momentOfInertia) * Time.fixedDeltaTime;
    }

    private void Update()
    {
        ApplyTorque(1.0f);
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
