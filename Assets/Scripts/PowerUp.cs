using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float _speed = 2.5f;
    [SerializeField] private int _powerUpID;

    [SerializeField] private AudioClip _powerUpCollectedClip;

    private void Start()
    {
        //_powerUpCollectedClip = GetComponent<AudioClip>();

        if (_powerUpCollectedClip == null)
        {
            Debug.LogError("The AudioClip is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_speed * Time.deltaTime * Vector3.down);

        if (transform.position.y < -6f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.transform.GetComponent<PlayerController>();

            AudioSource.PlayClipAtPoint(_powerUpCollectedClip, transform.position);
            GameObject.Find("One shot audio").GetComponent<AudioSource>().spatialBlend = 0;

            if (player != null)
            {
                switch (_powerUpID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedBoostActive();
                        break;
                    case 2:
                        player.ShieldActive();
                        break;
                    default:
                        Debug.Log("Default");
                        break;
                }
            }
            Destroy(this.gameObject);
        }
    }
}
