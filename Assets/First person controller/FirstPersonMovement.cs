using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : Singleton<FirstPersonMovement>
{

    [Range(0.01f, 0.99f)]
    [SerializeField] private float crounchSpeedFactor = 0.5f;
    [SerializeField] private float timeBetweenSteps = 2f;
    public float speed = 5;

    private Crouch _crouch = null;

    private Key _keyReference = null;
    private List<KeyScriptable> _keys = new List<KeyScriptable>();
    public bool HasKey(KeyScriptable key) => _keys.Contains(key);

    private float _timeFootstep = 0f;

    private Rigidbody _rb = null;
    private FootstepController _footstepController = null;

    public bool CanInteract()
    {
        return _keyReference == null;
    }

    protected override void Init()
    {
        _rb = GetComponent<Rigidbody>();
        _footstepController = GetComponentInChildren<FootstepController>();
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
                _keys.Add(_keyReference.Scriptable);
                _keyReference.GetKey();
            }
        }
    }

    void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        float finalSpeed = speed * (_crouch.IsCrouched ? crounchSpeedFactor : 1f);

        Vector2 velocity;
        velocity.x = Input.GetAxis("Horizontal") * finalSpeed * Time.fixedDeltaTime;
        velocity.y = Input.GetAxis("Vertical") * finalSpeed * Time.fixedDeltaTime;
        // _rb.velocity = new Vector3(velocity.x, 0, velocity.y); // TODO Melhorar posteriormente, personagem anda meio travado do jeito atual
        transform.Translate(velocity.x, 0, velocity.y);

        if ((velocity.x != 0f || velocity.y != 0f) && _timeFootstep < Time.time)
        {
            _timeFootstep = Time.time + timeBetweenSteps / (_crouch.IsCrouched ? crounchSpeedFactor * 1.35f : 1f);
            _footstepController.PlayFootstep();
        }
        else if (velocity.x == 0f && velocity.y == 0f)
            _timeFootstep = Time.time + timeBetweenSteps;
    }
}
