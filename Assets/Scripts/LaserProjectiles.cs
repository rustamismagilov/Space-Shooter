using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserProjectiles : MonoBehaviour
{
    [SerializeField] private float _speed = 8f;
    private bool _isEnemyLaser = false;

    private AudioSource _audioSource;
    [SerializeField] private AudioClip _playerExplosionSoundClip;

    // Start is called before the first frame update
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        if (_audioSource == null)
        {
            Debug.LogError("The AudioSource is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_isEnemyLaser == false)
        {
            MoveUp();
        }
        else
        {
            MoveDown();
        }
    }

    void MoveUp()
    {
        transform.Translate(_speed * Time.deltaTime * Vector3.up);

        if (transform.position.y > 8f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(this.gameObject);
        }
    }

    void MoveDown()
    {
        transform.Translate(_speed * Time.deltaTime * Vector3.down);

        if (transform.position.y < -8f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(this.gameObject);
        }
    }

    public void AssignEnemyLaser()
    {
        _isEnemyLaser = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && _isEnemyLaser == true)
        {
            PlayerController player = other.GetComponent<PlayerController>();

            if (player != null)
            {
                player.DamageThePlayer();

                AudioSource.PlayClipAtPoint(_playerExplosionSoundClip, transform.position);
                GameObject.Find("One shot audio").GetComponent<AudioSource>().spatialBlend = 0;

                Destroy(this.gameObject);
            }
        }
    }
}
