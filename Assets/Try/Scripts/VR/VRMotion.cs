using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRMotion : MonoBehaviour
{

    public float speed;
    public Transform _transform;//only for performance
    public GameObject cam;//only for performance
    private float verticaal = 0;
    private float horizontaal = 0;

    private Vector3 dir;
    // Use this for initialization
    void Start()
    {
        _transform = GetComponent<Transform>();
        dir = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 || Input.GetMouseButton(0) || Input.GetAxis("Vertical") > 0.5)
        {
            dir = cam.transform.TransformDirection(Vector3.forward);
            float amountToMove = speed * Time.deltaTime;
            transform.Translate(dir * amountToMove);
        }
    }
}
