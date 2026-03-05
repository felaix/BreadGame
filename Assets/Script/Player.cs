using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private SpriteRenderer gunSR;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;

    private BreadInpuSystem input;
    private float nextFireTime;
    public float fireCooldown;
    private float startX;

    private Vector2 moveInput;

    private void Awake()
    {
        input = new BreadInpuSystem();
        Instance = this;
    }

    private void OnEnable()
    {
        input.Enable();

        input.Player.Attack.performed += OnAttackPerformed;

        // Movement input
        input.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        input.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        startX = transform.position.x;

        GameStatsUI.Instance.SetGameUI(StatType.Cooldown, fireCooldown.ToString());
        GameStatsUI.Instance.SetGameUI(StatType.Speed, moveSpeed.ToString());
    }

    private void OnDisable()
    {
        input.Player.Attack.performed -= OnAttackPerformed;

        input.Player.Move.performed -= ctx => moveInput = ctx.ReadValue<Vector2>();
        input.Player.Move.canceled -= ctx => moveInput = Vector2.zero;

        input.Disable();
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        float moveX = moveInput.x * moveSpeed * Time.deltaTime;
        transform.Translate(new Vector3(moveX, 0, 0));

    }

    private void OnAttackPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        TryShoot();
    }

    public void AssignNewGun(Sprite newGun)
    {
        gunSR.sprite = newGun;
    }

    private void TryShoot()
    {
        if (Time.time < nextFireTime) return;
        nextFireTime = Time.time + fireCooldown;

        Shoot();
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    public void AddSpeed(float amount)
    {
        moveSpeed += amount;
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    public void AssignNewBullet(GameObject bullet)
    {
        bulletPrefab = bullet;

    }

    public float GetDistance()
    {
        return transform.position.x - startX;
    }
}