using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.rotation = FindObjectOfType<Weapon>().transform.rotation;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.right, 4 * Time.deltaTime);
    }
}
