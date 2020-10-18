using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinLaunch : MonoBehaviour
{
    public Transform target;
    public int launchForce = 500;

    private float dir;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Launch());
    }
    

    private IEnumerator Launch()
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("launch!");
        Vector3 dir = (transform.position - target.position).normalized;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().AddForce((-1) * dir * launchForce);
    }
}
