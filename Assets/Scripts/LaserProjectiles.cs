using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserProjectiles : MonoBehaviour
{

    private float _speed = 8f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_speed * Time.deltaTime * Vector3.up);

        if (transform.position.y > 8f)
        {
            Destroy(this.gameObject);
        }
    }
}
