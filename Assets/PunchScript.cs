using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchScript : MonoBehaviour
{

    [SerializeField()]
    private GameObject stenPin;

    [SerializeField()]
    private bool rapidFire;

    [SerializeField()]
    private float punchMultiplier;
    [SerializeField()]
    private float yeetMultiplier;

    private bool punchedThisFrame;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (rapidFire)
        {
            if (Input.GetMouseButton(1))
            {
                ThrowPin();
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(1))
            {
                ThrowPin();
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            rapidFire = !rapidFire;
        }
    }

    void ThrowPin()
    {
        Vector3 insPosition = transform.position + transform.forward;
        GameObject temp = Instantiate(stenPin, insPosition, transform.rotation * Quaternion.Euler(0, 180, 0));
        Rigidbody tempRb = temp.GetComponent<Rigidbody>();
        tempRb.AddForce(Vector3.Normalize(temp.transform.position - transform.position) * yeetMultiplier, ForceMode.VelocityChange);
    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log(other);

        Rigidbody rb = other.GetComponent<Rigidbody>();

        if (rb == null)
        {
            rb = other.transform?.parent?.GetComponent<Rigidbody>();
            if (rb == null)
            {
                punchedThisFrame = false;
                return;
            }
        }

        Punch(rb, other);

    }

    void Punch(Rigidbody rb, Collider other)
    {
        if (rb.CompareTag("PhysicsObject"))
        {
            //Debug.Log("colliding");
            if (Input.GetMouseButton(0) && !punchedThisFrame)
            {
                //Debug.Log("Punching");
                Vector3 punchForce = other.transform.position - transform.position;
                punchForce = punchForce.normalized * punchMultiplier;
                rb.AddForce(punchForce, ForceMode.VelocityChange);
                punchedThisFrame = true;
            }
            else
            {
                punchedThisFrame = false;
            }
        }
        else
        {
            punchedThisFrame = false;
        }
    }
}