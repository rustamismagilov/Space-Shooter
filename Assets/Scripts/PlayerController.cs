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

    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _tripleShotPrefab;
    [SerializeField] private GameObject _shieldPrefab;

    private SpawnManager _spawnManager;

    private bool _isTripleShotActive = false;
    private bool _isSpeedBoostActive = false;
    private bool _isShieldActive = false;

    [SerializeField] private int _score;

    [SerializeField] private UIManager _uIManager;

    [SerializeField] private GameObject _leftEngine, _rightEngine;
    GameObject[] _engines;

    [SerializeField] private AudioClip _laserSoundClip;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _engines = new GameObject[] { _leftEngine, _rightEngine };

        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        _audioSource = GetComponent<AudioSource>();

        if (_spawnManager == null)
        {
            Debug.LogError("The SpawnManager is NULL");
        }

        if (_uIManager == null)
        {
            Debug.LogError("The UIManager is NULL");
        }

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
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFire)
        {
            FireLaser();
        }
    }

    // Update is called once per frame
    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new(horizontalInput, verticalInput, 0);

        transform.Translate(_speed * Time.deltaTime * direction);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y <= -3.8f)
        {
            transform.position = new Vector3(transform.position.x, -3.8f, 0);
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
}
