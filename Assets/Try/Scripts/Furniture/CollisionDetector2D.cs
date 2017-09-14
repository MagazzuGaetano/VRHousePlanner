using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector2D : MonoBehaviour
{

    public static bool is2DColliding = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition, Vector3.one);

            if (hit.collider.tag == "triggered" && hit.collider.GetComponent<Collider2D>())
                is2DColliding = true;

            if (hit.collider.name=="body")
                is2DColliding = false;
        }
    }
}
