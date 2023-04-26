using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 3.5f;
    [SerializeField] private float _speedMultiplier = 2f;
    [SerializeField] private float _fireRate = 0.15f;
    private float nextFire = 0.0f;

    [SerializeField] private int _lives = 3;
    [SerializeField] private int _score;

    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _tripleShotPrefab;
    [SerializeField] private GameObject _shieldPrefab;

    private bool _isTripleShotActive = false;
    private bool _isSpeedBoostActive = false;
    private bool _isShieldActive = false;

    [SerializeField] private GameObject _leftEngine, _rightEngine;
    GameObject[] _engines;

    [SerializeField] private AudioClip _laserSoundClip;

    [SerializeField] private UIManager _uIManager;
    private AudioSource _audioSource;
    private SpawnManager _spawnManager;
    private GameManager _gameManager;

    public bool isPlayerOne = false;
    public bool isPlayerTwo = false;

    // Start is called before the first frame update
    void Start()
    {
        _engines = new GameObject[] { _leftEngine, _rightEngine };

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (_gameManager.isCoopMode == false)
        {
            transform.position = new Vector3(0, 0, 0);
        }

        if (_gameManager == null)
        {
            Debug.LogError("The GameManager is NULL");
        }

        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("The SpawnManager is NULL");
        }

        _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_uIManager == null)
        {
            Debug.LogError("The UIManager is NULL");
        }

        _audioSource = GetComponent<AudioSource>();

        if (_audioSource == null)
        {
            Debug.LogError("The AudioSource is NULL");
        }
        else
        {
            _audioSource.clip = _laserSoundClip;
        }
    }
    private void Update()
    {
        if (isPlayerOne == true)
        {
            CalculateMovement();
            if ((Input.GetKeyDown(KeyCode.Space) && Time.time > nextFire) && isPlayerOne == true)
            {
                FireLaser();
            }
        }

        if (isPlayerTwo == true)
        {
            CalculateMovement();
            if ((Input.GetKeyDown(KeyCode.RightControl) && Time.time > nextFire) && isPlayerTwo == true)
            {
                FireLaser();
            }
        }

    }

    // Update is called once per frame
    void CalculateMovement()
    {
        float horizontalInput = isPlayerOne ? Input.GetAxis("Horizontal") : Input.GetAxis("Horizontal2");
        float verticalInput = isPlayerOne ? Input.GetAxis("Vertical") : Input.GetAxis("Vertical2");

        Vector3 direction = new(horizontalInput, verticalInput, 0);

        transform.Translate(_speed * Time.deltaTime * direction);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4.3f, 0), 0);

        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y <= -4.3f)
        {
            transform.position = new Vector3(transform.position.x, -4.3f, 0);
        }

        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    private void FireLaser()
    {
        nextFire = Time.time + _fireRate;

        if (_isTripleShotActive == true)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.0f, 0), Quaternion.identity);
        }

        _audioSource.Play();
    }

    public void DamageThePlayer()
    {
        if (_isShieldActive == true)
        {
            _isShieldActive = false;
            _shieldPrefab.SetActive(false);
            return;
        }

        _lives--;

        EngineDamaged();

        _uIManager.UpdateLives(_lives);

        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }

    public void SpeedBoostActive()
    {
        _isSpeedBoostActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedBoostActive = false;
        _speed /= _speedMultiplier;
    }

    public void ShieldActive()
    {
        _isShieldActive = true;
        _shieldPrefab.SetActive(true);
    }

    public void AddScore(int points)
    {
        _score += points;
        _uIManager.UpdateScore(_score);
    }

    public void EngineDamaged()
    {
        int randomEngineIndex = Random.Range(0, _engines.Length);
        GameObject randomEngine = _engines[randomEngineIndex];

        if (!randomEngine.activeSelf)
        {
            randomEngine.SetActive(true);
        }
        else
        {
            // Activate another engine if the randomly selected one is already active
            for (int i = 0; i < _engines.Length; i++)
            {
                if (i != randomEngineIndex && !_engines[i].activeSelf)
                {
                    _engines[i].SetActive(true);
                    break;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Enemy") || other.CompareTag("Asteroid"))
    {
        DamageThePlayer();
    }
}
}
