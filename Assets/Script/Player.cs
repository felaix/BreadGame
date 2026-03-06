using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static Player Instance;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private SpriteRenderer gunSR;
    [SerializeField] private GameObject bombPrefab;

    [SerializeField] private List<SpriteRenderer> eyes;

    public int BombAmount = 1;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;

    private BreadInpuSystem input;
    private float nextFireTime;
    public float fireCooldown;
    private float startX;

    private Vector2 moveInput;

    [Header("Interaction")]
    [SerializeField] private float interactRadius;
    [SerializeField] private LayerMask interactLayer;

    private void Awake()
    {
        input = new BreadInpuSystem();
        Instance = this;
    }

    private void OnEnable()
    {
        input.Enable();

        input.Player.Attack.performed += OnAttackPerformed;
        input.Player.Bomb.performed += OnBombPerformed;
        input.Player.Interact.performed += OnInteractPerformed;

        input.Player.Move.performed += OnMovePerformed;
        input.Player.Move.canceled += OnMoveCanceled;

        startX = transform.position.x;

        GameStatsUI.Instance.SetGameUI(StatType.Cooldown, fireCooldown.ToString());
        GameStatsUI.Instance.SetGameUI(StatType.Speed, moveSpeed.ToString());
    }

    private void OnDisable()
    {
        input.Player.Attack.performed -= OnAttackPerformed;
        input.Player.Bomb.performed -= OnBombPerformed;
        input.Player.Interact.performed -= OnInteractPerformed;

        input.Player.Move.performed -= OnMovePerformed;
        input.Player.Move.canceled -= OnMoveCanceled;

        input.Disable();
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        float moveX = moveInput.x * moveSpeed * Time.deltaTime;
        transform.Translate(new Vector3(moveX, 0f, 0f));
    }

    private void OnMovePerformed(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext ctx)
    {
        moveInput = Vector2.zero;
    }

    private void OnAttackPerformed(InputAction.CallbackContext ctx)
    {
        TryShoot();
    }

    private void OnBombPerformed(InputAction.CallbackContext ctx)
    {
        ThrowBomb();
    }

    private void OnInteractPerformed(InputAction.CallbackContext ctx)
    {
        TryInteract();
    }

    private void ThrowBomb()
    {
        if (bombPrefab == null || firePoint == null) return;
        Debug.Log("Throw Bomb");

        if (BombAmount <= 0) return;
        else
        {
            Instantiate(bombPrefab, firePoint.position, firePoint.rotation);
            BombAmount--;
            GameStatsUI.Instance.SetGameUI(StatType.Bomb, BombAmount.ToString());
        }

    }

    public void AddBomb()
    {
        BombAmount++;
        GameStatsUI.Instance.SetGameUI(StatType.Bomb, BombAmount.ToString());
    }

    public void SmokeWeed()
    {
        foreach (SpriteRenderer sr in eyes)
        {
            sr.color = Color.red;
        }
    }

    private void TryInteract()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, interactRadius, interactLayer);

        float closestDistance = Mathf.Infinity;
        IInteractable closestInteractable = null;

        foreach (Collider2D hit in hits)
        {
            IInteractable interactable = hit.GetComponent<IInteractable>();
            if (interactable == null) continue;

            float distance = Vector2.Distance(transform.position, hit.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestInteractable = interactable;
            }
        }

        closestInteractable?.Interact();
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
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}