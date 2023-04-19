using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsController : MonoBehaviour
{
    public Sprite[] asteroidSprites;

    public float size = 0.9f;

    public float minSize = 0.5f;
    public float maxSize = 1.5f;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        _spriteRenderer.sprite = asteroidSprites[Random.Range(0, asteroidSprites.Length)];

        this.transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
        this.transform.localScale = Vector3.one * this.size;

        _rigidbody.mass = this.size;
    }

}
