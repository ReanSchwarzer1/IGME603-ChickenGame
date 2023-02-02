using UnityEngine;

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
    private Rigidbody2D _rb;
    private SpriteRenderer _chickenRenderer;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _chickenRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShootEgg(); //Calling the shootegg() when the player clicks the left-mouse button
        }

        CameraFollow();
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
        _rb.velocity = (_jumpForce * -direction); //the rigibody will jump in the direct opposite direction from where the (mousepos var / direction the player clicks in) is
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
        if (direction.x > 0)
        {
            _chickenRenderer.flipX = false;
        }
        else
        {
            _chickenRenderer.flipX = true;
        }
    }
    private void CameraFollow()
    {
        _cameraTransform.position = Vector3.Lerp(
            _cameraTransform.position,
            new Vector3(transform.position.x, transform.position.y,
            _cameraTransform.position.z), Time.deltaTime * _cameraSpeed);

        _cameraTransform.rotation = Quaternion.identity;
    }

}