using UnityEngine;

public class TestSpaceship : MonoBehaviour
{
    public Rigidbody2D myRigidBody2D;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myRigidBody2D.linearVelocity = new Vector2(0.0f, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
