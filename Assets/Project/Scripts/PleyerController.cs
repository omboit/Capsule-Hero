using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100;
    private int currentHealth;
    public Slider healthBar;
    private bool isImmune = false;

    [Header("Movement Settings")]
    public float moveSpeed = 7f;
    public float jumpForce = 5f; // Kekuatan melompat
    public LayerMask groundLayer;
    public Transform groundCheck;
    private bool isGrounded;

    [Header("Animation Settings")]
    public Animator anim; // Masukkan komponen Animator Hero di sini

    [Header("Shooting Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;

    [Header("FPS Settings")]
    public Camera fpsCamera; 
    public float mouseSensitivity = 150f;
    public GameObject crosshairUI; 
    
    private bool isFPSMode = false;
    private float xRotation = 0f;

    private float moveX;
    private float moveZ;
    private Rigidbody rb;
    private Camera mainCamera;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        mainCamera = Camera.main; 

        currentHealth = maxHealth;
        
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }

        if (fpsCamera != null) fpsCamera.enabled = false;
        if (crosshairUI != null) crosshairUI.SetActive(false);
    }

    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveZ = Input.GetAxisRaw("Vertical");

        // --- SISTEM ANIMASI BERJALAN ---
        // Kira kelajuan pergerakan dan hantar ke Animator
        float speedValue = new Vector2(moveX, moveZ).magnitude;
        if (anim != null)
        {
            anim.SetFloat("Speed", speedValue);
        }

        // --- SISTEM MELOMPAT ---
        // Semak jika Hero memijak lantai (kalau tak letak groundCheck, kita anggap Y dekat dengan 0)
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            if (anim != null) anim.SetTrigger("Jump");
        }

        // TOGGLE FPS
        if (Input.GetMouseButtonDown(1)) 
        {
            isFPSMode = true;
            if (mainCamera != null) mainCamera.enabled = false;
            if (fpsCamera != null) fpsCamera.enabled = true;
            Cursor.lockState = CursorLockMode.Locked; 
            if (crosshairUI != null) crosshairUI.SetActive(true);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            isFPSMode = false;
            if (mainCamera != null) mainCamera.enabled = true;
            if (fpsCamera != null) fpsCamera.enabled = false;
            Cursor.lockState = CursorLockMode.None; 
            if (crosshairUI != null) crosshairUI.SetActive(false);
        }

        AimWithMouse();

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        if (isFPSMode)
        {
            Vector3 move = transform.right * moveX + transform.forward * moveZ;
            rb.MovePosition(rb.position + move.normalized * moveSpeed * Time.fixedDeltaTime);
        }
        else
        {
            Vector3 moveDirection = new Vector3(moveX, 0f, moveZ).normalized;
            if (moveDirection != Vector3.zero)
            {
                rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
            }
        }
    }

    void AimWithMouse()
    {
        if (isFPSMode && fpsCamera != null)
        {
            float mouseXInput = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseYInput = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseYInput;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f); 

            fpsCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); 
            transform.Rotate(Vector3.up * mouseXInput); 
        }
        else if (!isFPSMode && mainCamera != null)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, transform.position);
            float rayDistance;

            if (groundPlane.Raycast(ray, out rayDistance))
            {
                Vector3 point = ray.GetPoint(rayDistance);
                Vector3 lookPoint = new Vector3(point.x, transform.position.y, point.z);
                transform.LookAt(lookPoint);
            }
        }
    }

    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            if (isFPSMode && fpsCamera != null)
            {
                Ray ray = fpsCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
                RaycastHit hit;
                Vector3 targetPoint;

                if (Physics.Raycast(ray, out hit))
                {
                    targetPoint = hit.point;
                }
                else
                {
                    targetPoint = ray.GetPoint(100); 
                }

                Vector3 directionWithoutSpread = targetPoint - firePoint.position;
                Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(directionWithoutSpread));
            }
            else
            {
                Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (isImmune) return; 

        currentHealth -= damage;
        if (healthBar != null) healthBar.value = currentHealth;
        
        if (isFPSMode && fpsCamera != null)
        {
            CameraShake shake = fpsCamera.GetComponent<CameraShake>();
            if (shake != null) StartCoroutine(shake.Shake(0.2f, 0.4f));
        }
        else if (!isFPSMode && Camera.main != null)
        {
            CameraShake shake = Camera.main.GetComponent<CameraShake>();
            if (shake != null) StartCoroutine(shake.Shake(0.2f, 0.4f));
        }

        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.Play();
        }

        if (currentHealth <= 0) Die();
    }

    void Die()
    {
        if (GameManager.instance != null) GameManager.instance.ShowGameOver();
        gameObject.SetActive(false); 
        Cursor.lockState = CursorLockMode.None; 
    }

    public void ActivateImmunity(float duration)
    {
        StartCoroutine(ImmunityRoutine(duration));
    }

    private IEnumerator ImmunityRoutine(float duration)
    {
        isImmune = true;
        yield return new WaitForSeconds(duration);
        isImmune = false;
    }
}