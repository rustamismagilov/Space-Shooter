using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserProjectiles : MonoBehaviour
{

    [SerializeField] private float _speed = 8f;

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
