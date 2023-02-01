using UnityEngine;

public class CharacterController: MonoBehaviour
{
    [SerializeField]
    private GameObject _eggPrefab;
    [SerializeField]
    private float _jumpForce = 10f;
    private Rigidbody2D _rb;

    private void Start() => _rb = GetComponent<Rigidbody2D>();

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShootEgg(); //Calling the shootegg() when the player clicks the left-mouse button
        }
    }

    private void ShootEgg()
    {
        //Shootegg function calls all the functions required to shoot the egg towards the screenspace the player clicks in
        var direction = GetProjectileDirection();
        var projectile = ProjectileEgg(direction);
        ApplyJumpForceOppToShootDir(projectile, direction);
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

    private GameObject ProjectileEgg(Vector2 direction)
    {
        return Instantiate(_eggPrefab, (Vector2)transform.position + direction, Quaternion.identity); //intanciating the eggs towards the direction the player is clicking in
    }

    private void ApplyJumpForceOppToShootDir(GameObject projectile, Vector2 direction)
    {
        projectile.GetComponent<Rigidbody2D>().velocity = direction * _jumpForce; //applying force to the rigidbody to make the player jump
        _rb.velocity = -direction * _jumpForce; //the rigibody will jump in the direct opposite direction from where the (mousepos var / direction the player clicks in) is
    } 
}