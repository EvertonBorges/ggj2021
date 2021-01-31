﻿using UnityEngine;

public class FirstPersonMovement : Singleton<FirstPersonMovement>
{

    [Range(0.01f, 0.99f)]
    [SerializeField] private float crounchSpeedFactor = 0.5f;
    public float speed = 5;
    Vector2 velocity;

    private Crouch _crouch = null;

    private Key _keyReference = null;
    private bool _hasKey = false;
    public bool HasKey => _hasKey;

    private Rigidbody _rb = null;

    public bool CanInteract()
    {
        return _keyReference == null;
    }

    protected override void Init()
    {
        _rb = GetComponent<Rigidbody>();
        _crouch = GetComponentInChildren<Crouch>();
    }

    void Update()
    {
        CheckKeyRay();

        OnInteract();
    }

    private void CheckKeyRay()
    {
        var ray = Camera.main.ScreenPointToRay(Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f, 0f)));
        if (Physics.Raycast(ray, out RaycastHit info, 1f, 1 << LayerMask.NameToLayer(Layers.Key.ToString())))
        {
            var key = info.transform.GetComponent<Key>();
            if (key)
            {
                _keyReference = key;
                _keyReference.Show();
            }
        }
        else
        {
            if (_keyReference)
            {
                _keyReference.Hide();
                _keyReference = null;
            }
        }
    }

    private void OnInteract()
    {
        if (Input.GetButtonDown(InputButton.Interact.ToString()))
        {
            if (_keyReference)
            {
                _hasKey = true;
                _keyReference.GetKey();
            }
        }
    }

    void FixedUpdate()
    {
        float finalSpeed = speed * (_crouch.IsCrouched ? crounchSpeedFactor : 1f);

        velocity.y = Input.GetAxis("Vertical") * finalSpeed * Time.deltaTime;
        velocity.x = Input.GetAxis("Horizontal") * finalSpeed * Time.deltaTime;
        // _rb.velocity = new Vector3(velocity.x, 0, velocity.y); // TODO Melhorar posteriormente, personagem anda meio travado do jeito atual
        transform.Translate(velocity.x, 0, velocity.y);
    }
}
