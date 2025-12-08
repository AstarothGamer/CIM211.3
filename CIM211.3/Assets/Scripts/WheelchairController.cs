using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class WheelchairController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb0;
    [SerializeField] private Rigidbody rb1;
    [SerializeField] private Rigidbody rb2;

    public float force = 1;
    public float turnForce = 1;
    public float dist = .45f;
    public float rotSpeed = 5;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool left = Input.GetMouseButton(0);
        bool right = Input.GetMouseButton(1);

        if (left || right)
        {
            var rot = transform.rotation.eulerAngles;
            if (left && !right)
            {
                rot.y -= rotSpeed * Time.fixedDeltaTime;
                rb0.MoveRotation(Quaternion.Euler(rot));
            }
            else if (right && !left)
            {
                rot.y += rotSpeed * Time.fixedDeltaTime;
                rb0.MoveRotation(Quaternion.Euler(rot));
            }
            
            float deltaForce = ((left && right) ? force : turnForce) * Time.fixedDeltaTime;
            
            rb0.AddForce(transform.forward * deltaForce);
        }
        
        /*var fameForce = force * Time.deltaTime;
        if(left && right)
            rb0.AddForce(transform.forward * fameForce);
        else if (left)
            rb0.AddForceAtPosition(transform.forward * fameForce, transform.position + transform.right * dist);
        else if (right)
            rb0.AddForceAtPosition(transform.forward * fameForce, transform.position - transform.right * dist);
            */
        
//            rb2.AddForce(rb2.transform.forward * force);
        
        /*if(left && right)
            rb0.AddForce(transform.forward * (force * 2));
        else if (left)
            rb1.AddForce(rb1.transform.forward * force);
        else if (right)
            rb2.AddForce(rb2.transform.forward * force);*/
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * force / 100);
        var r = transform.position + transform.right * dist;
        Gizmos.DrawLine(r, r + transform.forward * force / 100);
        var l = transform.position - transform.right * dist;
        Gizmos.DrawLine(l, l + transform.forward * force / 100);
    }
}
