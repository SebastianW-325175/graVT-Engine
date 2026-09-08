using System;
using UnityEngine;

public class TinyObject : MonoBehaviour
{
    [Tooltip("Object mass in kg")]
    [SerializeField] private float mass;

    private Vector2 _linearVelocity;
    private float _angularVelocity;

    private void FixedUpdate()
    {
        gameObject.transform.Translate(_linearVelocity * Time.fixedDeltaTime);
    }

    public void ApplyForce(Vector2 force)
    {
        // This is probably wrong
        _linearVelocity += force * Time.fixedDeltaTime;
    }
}
