using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class CharacterController: MonoBehaviour
{
    [SerializeField]
    private GameObject _eggPrefab;
    [SerializeField]
    private float _jumpForce = 10f;
    [SerializeField]
    private Transform _cameraTransform;
    [SerializeField]
    private float _cameraSpeed = 2f;
    [SerializeField]
    private float SHOOT_COOLDOWN;
    Rigidbody2D _rb;
    SpriteRenderer _chickenRenderer;
    BoxCollider2D _chickenCC;
    Animator _chickenAnimator;
    Vector2 _moveInput;
    private float _cooldown;


    [SerializeField] float launchMin;
    [SerializeField] float launchMax;

    [SerializeField] KeyCode pauseKey;
    [SerializeField] GameObject pauseMenu;

    public Texture2D _mouseCursor;

    Vector2 _hotSpot = new Vector2(0, 0);
    CursorMode _mouseMode = CursorMode.Auto;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _chickenAnimator = GetComponent<Animator>();
        _chickenRenderer = GetComponent<SpriteRenderer>();
        //_moveInput = GetComponent<Vector2>();
        _chickenCC = GetComponent<BoxCollider2D>();
        Time.timeScale = 1.0f;

        Vector3 spawnPoint = GameObject.Find("Checkpoint Tracker").GetComponent<CheckpointTracker>().SpawnPoint;
        if(spawnPoint != Vector3.zero)
        {
            transform.position = spawnPoint;
            _cameraTransform.position = new Vector3(spawnPoint.x, spawnPoint.y, _cameraTransform.position.z);
        }

        Cursor.SetCursor(_mouseCursor, _hotSpot, _mouseMode); //secursor function changes the look of the cursor in-game

    }

    private void Update()
    {
        // Check for the game being paused
        if (Input.GetKeyDown(pauseKey))
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
            Time.timeScale = (Time.timeScale > 0.0f ? 0.0f : 1.0f);
        }
        // Only process input if the game isn't paused
        if(Time.timeScale > 0)
        {
            if (_cooldown <= 0 && Input.GetMouseButtonDown(0))
            {
                ShootEgg(); //Calling the shootegg() when the player clicks the left-mouse button
                _cooldown = SHOOT_COOLDOWN;
            }

            CameraFollow();
            //_chickenAnimator.SetBool("isJumping", false);

            if(_cooldown > 0) {
                _cooldown -= Time.deltaTime;
            }
        }
    }

    void OnMove()
    {
        Debug.Log(_moveInput);
    }

    private Vector2 GetProjectileDirection()
    {
        //to get the transform/position where the player is clickin in
        /* https://stackoverflow.com/questions/68009424/launching-character-in-the-direction-of-the-mouse */
        var mousePos = Input.mousePosition;
        mousePos.z = Camera.main.transform.position.z;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos); 
        return (Vector2)(mousePos - transform.position).normalized;
    }


    private void ApplyJumpForceOppToShootDir(GameObject projectile, Vector2 direction)
    {
        projectile.GetComponent<Rigidbody2D>().velocity = direction * _jumpForce; //applying force to the rigidbody to make the player jump
        Vector2 velocity = (_jumpForce * -direction);
        float velocityMag = velocity.magnitude;
        velocityMag = Mathf.Clamp(velocityMag, launchMin, launchMax);
        _rb.velocity = velocity.normalized * velocityMag;

        //_rb.velocity = clampedVel; //the rigibody will jump in the direct opposite direction from where the (mousepos var / direction the player clicks in) is


        //if (!_chickenCC.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }

        //_chickenAnimator.SetBool("isJumping", true);
    }

    private void ShootEgg()
    {
        //Shootegg function calls all the functions required to shoot the egg towards the screenspace the player clicks in
        var direction = GetProjectileDirection();
        var projectile = Instantiate(_eggPrefab, (Vector2)transform.position + direction, Quaternion.identity); //intanciating the eggs towards the direction the player is clicking in
        ApplyJumpForceOppToShootDir(projectile, direction);

        FlipChicken(direction);
    }
    private void FlipChicken(Vector2 direction)
    {
        //code to flip the chicken towards the side they are jumping in / flip the chicken opposite the side they are throwing the eggs
        //if (direction.x > 0)
        //{
        //    _chickenRenderer.flipX = false;
        //}
        //else
        //{
        //    _chickenRenderer.flipX = true;
        //}
        Vector2 oppositeDirection = -direction;
        _rb.rotation = (float)(Math.Atan2(direction.y, direction.x) / Math.PI) * 180 + 90;
    }
    private void CameraFollow()
    {
        _cameraTransform.position = Vector3.Lerp(
            _cameraTransform.position,
            new Vector3(transform.position.x, transform.position.y,
            _cameraTransform.position.z), Time.deltaTime * _cameraSpeed);

        _cameraTransform.rotation = Quaternion.identity;
    }

    public void Die()
    {
        // restart the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}