using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float JumpForce = 10f;
    public float Speed = 5f;
    public float MouseSensitivity = 100f;
    public float MaxHealth = 100f;
    
    private CharacterController _controller;
    private Animator _animator;
    private GameManager _gameManager;

    private Vector3 direction;
    private float verticalVelocity;
    private float gravity = 20f;
    private float health;
    private bool isDead = false;
    private float timer = 0;

    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _controller = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
        health = MaxHealth;
    }

    void Update()
    {
        if (!isDead) {
            HandleMovement();
            HandleCamera();
        } else {
            timer += Time.deltaTime;
            if (timer > 5) {
                Application.Quit();
            }
        }
    }

    void HandleMovement()
    {
        direction = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        _animator.SetBool("run", direction.magnitude > 0);
        _animator.SetBool("is_in_air", !_controller.isGrounded);

        direction = transform.TransformDirection(direction);
        direction *= Speed * Time.deltaTime;

        verticalVelocity -= gravity * Time.deltaTime;

        if (_controller.isGrounded) {
            if (Input.GetButtonDown("Jump")) {
                verticalVelocity = JumpForce;
            }
        }

        direction.y = verticalVelocity * Time.deltaTime;
        _controller.Move(direction);
    }

    void HandleCamera()
    {
        float mouseX = Input.GetAxis("Mouse X");
        Vector3 rotator = transform.right * mouseX;
        rotator = transform.InverseTransformDirection(rotator);
        float turnValue = Mathf.Atan2(rotator.x, rotator.z);
        transform.Rotate(0, turnValue * MouseSensitivity * Time.deltaTime, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        BulletComponent component = other.gameObject.GetComponent<BulletComponent>();
        if (component != null) {
            ApplyDamage(component.Damage);
        }
    }

    void ApplyDamage(float val)
    {
        health -= val;
        _gameManager.GetHUDObject().transform.Find("Health").GetComponent<Text>().text = health.ToString();


        if (health <= 0) {
            isDead = true;
            GameObject endUI = _gameManager.GetEndUIObject();
            endUI.transform.Find("Text").GetComponent<Text>().text = "You Died :(";
            endUI.SetActive(true);
        }
    }
}
