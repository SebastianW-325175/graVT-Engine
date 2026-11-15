using System;
using DefaultNamespace;
using UnityEngine;

public class TinyObject : MonoBehaviour
{
    /// <summary>
    /// Reference to the World2D in which the TinyObject is being simulated. Meant to be automatically injected by the World2D.
    /// </summary>
    public World2D world2D;
    private const float RadiansToDegrees = 180f / Mathf.PI;
    
    [Tooltip("Object mass in kg")] [SerializeField]
    private float mass;

    [Tooltip("Object moment of inertia in kg * m^2")] [SerializeField]
    private float momentOfInertia;

    [Tooltip("The collider of the object. This will be used to calculate it's moment of inertia")] [SerializeField]
    private Collider2D collider;
    
    private Vector2 _linearVelocity;
    private float _angularVelocity;
    
    /// <summary>
    /// Applies a force vector to the object at it's center.
    /// </summary>
    /// <param name="force">The force vector to apply, in Newtons</param>
    public void ApplyForce(Vector2 force)
    {
        _linearVelocity += (force * Time.fixedDeltaTime) / mass;
    }

    /// <summary>
    /// Applies a force vector to the object, with an offset from its center.
    /// </summary>
    /// <param name="force">The force vector to apply, in Newtons.</param>
    /// <param name="offset">The offset of the force vector, in meters.</param>
    public void ApplyForce(Vector2 force, Vector2 offset)
    {
        ApplyForce(force);
        ApplyTorque(offset.magnitude * force.magnitude * Mathf.Sin( Vector2.SignedAngle(offset, force) * Mathf.Deg2Rad ));
    }

    /// <summary>
    /// Applies torque to the object.
    /// </summary>
    /// <param name="torque">The amount of torque to apply, in Newton-metres</param>
    public void ApplyTorque(float torque)
    {
        _angularVelocity += (torque / momentOfInertia) * Time.fixedDeltaTime;
    }

    private void OnEnable()
    {
        world2D.RegisterObject(this);
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
    
    private void FixedUpdate()
    {
        gameObject.transform.Translate(_linearVelocity * Time.fixedDeltaTime, Space.World);
        gameObject.transform.Rotate(0, 0, (_angularVelocity * Time.fixedDeltaTime * RadiansToDegrees));
    }
}
