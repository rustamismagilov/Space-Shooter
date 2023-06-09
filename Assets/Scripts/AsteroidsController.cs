using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsController : MonoBehaviour
{
    public float size = 0.8f;

    public float minSize = 0.5f;
    public float maxSize = 1.5f;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;

    [SerializeField] private float _speed = 4.0f;
    [SerializeField] private float _rotationSpeed;

    private PlayerController _playerController;
    private Animator _animator;

    [SerializeField] private AudioClip _explosionSoundClip;
    private AudioSource _audioSource;

    private SpawnManager _spawnManager;
    private GameObject _asteroidContainer;

    //TODO: add explosion sound clip if player dies because of the asteroid

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        _rotationSpeed = Random.Range(-45f, 45f);

        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        _audioSource = GetComponent<AudioSource>();

        if (_playerController == null)
        {
            Debug.LogError("The player is NULL");
        }

        _animator = GetComponent<Animator>();

        if (_animator == null)
        {
            Debug.LogError("The animator is NULL");
        }

        if (_audioSource == null)
        {
            Debug.LogError("The AudioSource is NULL");
        }
        else
        {
            _audioSource.clip = _explosionSoundClip;
        }

        this.transform.localScale = Vector3.one * this.size;

        _rigidbody.mass = this.size;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Rotate(0f, 0f, _rotationSpeed * Time.deltaTime);

        transform.Translate(_speed * Time.deltaTime * Vector3.down);

        if (transform.position.y < -7f)
        {
            Destroy(this.gameObject);
        }

        if (transform.position.x > 12f || transform.position.x < -12f)
        {
            Destroy(this.gameObject);
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

            _animator.SetTrigger("OnAsteroidDestroyed");

            Destroy(GetComponent<CircleCollider2D>());

            _audioSource.Play();

            Destroy(this.gameObject, 1.5f);
        }

        if (other.CompareTag("Laser"))
        {

            if ((this.size * 0.5f) >= this.minSize)
            {
                AsteroidSplit();
                AsteroidSplit();
            }

            Destroy(other.gameObject);

            if (_playerController != null)
            {
                _playerController.AddScore(Random.Range(1, 2) * 5);
            }

            _animator.SetTrigger("OnAsteroidDestroyed");

            Destroy(GetComponent<CircleCollider2D>());

            Destroy(this.gameObject, 1.5f);
            _audioSource.Play();
        }
    }

    private void AsteroidSplit()
    {
        Vector2 position = this.transform.position;
        position += Random.insideUnitCircle * 0.5f;

        AsteroidsController halfOfTheAsteroid = Instantiate(this, position, this.transform.rotation);
        halfOfTheAsteroid.size = this.size * 0.5f;
        halfOfTheAsteroid.transform.parent = _spawnManager._asteroidContainer.transform;

        _audioSource.Play();
    }
}
