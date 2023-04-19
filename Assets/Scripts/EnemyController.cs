using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float _speed = 4.0f;

    private PlayerController _playerController;
    private Animator _animator;

    [SerializeField] private AudioClip _explosionSoundClip;
    private AudioSource _audioSource;

    //TODO: When using tripleshot, if more than one laser hits the enemy, each laser gets scored.
    //TODO: Sometimes it is possible to hit an enemy right when it spawns and is still not visible on the screen, but it still get you a score for a kill.

    private void Start()
    {
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        if (_playerController == null)
        {
            Debug.LogError("The player is NULL");
        }

        _animator = GetComponent<Animator>();

        if (_animator == null)
        {
            Debug.LogError("The animator is NULL");
        }

        _audioSource = GetComponent<AudioSource>();

        if (_audioSource == null)
        {
            Debug.LogError("The AudioSource is NULL");
        }
        else
        {
            _audioSource.clip = _explosionSoundClip;
        }
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

            _animator.SetTrigger("OnEnemyDeath");

            Destroy(GetComponent<Rigidbody2D>());
            Destroy(GetComponent<BoxCollider2D>());

            _audioSource.Play();

            Destroy(this.gameObject, 1.5f);
        }

        if (other.CompareTag("Laser"))
        {
            Destroy(other.gameObject);
            if (_playerController != null)
            {
                _playerController.AddScore(Random.Range(1, 4) * 5);
            }

            _animator.SetTrigger("OnEnemyDeath");

            Destroy(GetComponent<Rigidbody2D>());
            Destroy(GetComponent<BoxCollider2D>());

            _audioSource.Play();

            Destroy(this.gameObject, 1.5f);
        }
    }
}
