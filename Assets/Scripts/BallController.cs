using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public GameObject rayObj;

    [HideInInspector] public bool canLaunch;
    [HideInInspector] public Vector3 launchDir;
    [HideInInspector] public int launchForce;

    Vector3 curPos, lastPos;
    Rigidbody rb;

    float waitTime = 2f, elapsed = 0;

    bool showRay;

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody>();

        curPos = transform.position;
        rayObj.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (rb.IsSleeping())
        {
            rayObj.SetActive(true);
            showRay = true;
        }

        if (canLaunch)
        {
            canLaunch = false;
            Launch(launchDir);
        }
        

    }

    public void Launch(Vector3 dir)
    {
        Vector3 realDir = (transform.position - dir).normalized;
        rb.isKinematic = false;
        rb.AddForce((-1) * realDir * launchForce);
    }
    

    public void OnCollisionStay(Collision collision)
    {
        if (!rb.IsSleeping() && elapsed > waitTime && (rb.velocity.magnitude < 0.1f || rb.angularVelocity.magnitude < 0.1f))
        {
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
        }
        elapsed += Time.fixedDeltaTime;

    }

    private void OnCollisionExit(Collision collision)
    {
        rayObj.SetActive(false);
        showRay = false;
        elapsed = 0;
    }

}