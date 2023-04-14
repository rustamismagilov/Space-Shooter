using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float _speed = 4.0f;

    private PlayerController _playerController;

    private void Start()
    {
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_speed * Time.deltaTime * Vector3.down);

        if (transform.position.y < -5f)
        {
            float randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX, 7, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.transform.name + " hit " + this.transform.name);

        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.transform.GetComponent<PlayerController>();

            if (playerController != null)
            {
                playerController.DamageThePlayer();
            }

            Destroy(this.gameObject);
        }

        if (other.CompareTag("Laser"))
        {
            Destroy(other.gameObject);
            if (_playerController != null)
            {
                _playerController.AddScore(Random.Range(5,10));
            }
            Destroy(this.gameObject);
        }
    }
}
