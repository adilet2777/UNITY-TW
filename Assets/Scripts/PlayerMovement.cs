using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour , IPlayerCharacteristics , IDamagable
{
    //General
    private InputControls inputManager;

    private InputAction moveAction;

    private Rigidbody2D rigidbody2d;

    //

    //Attack

    [SerializeField] private float attackForce;
    
    [SerializeField] private float radiusOfAttack;

    //

    //Character movement set

    [SerializeField] private float jumpForce;

    [SerializeField] private float speed;

    public float Speed { get => speed; set => speed = value; }

    //

    //cash enemy to damage later -> cause .GetComponent takes too much energy

    private IDamagable damagableEnemy;

    //


    //Check for grounded to jump
    private bool isGrounded;

    [SerializeField] private Transform groundChecker;

    [SerializeField] private LayerMask groundLayer;

    //

    private Vector2 totalVelocity;

    [SerializeField] private float health;

    [SerializeField] private Slider hpSlider;

    private void Awake()
    {
        inputManager = new InputControls();

        moveAction = inputManager.Player.Move;

        rigidbody2d = GetComponent<Rigidbody2D>();

        Damage(0);
    }

    private void OnEnable()
    {
        inputManager.Enable();

        inputManager.Player.Jump.performed += ctx => Jump();

        inputManager.Player.Attack.performed += ctx => Attack();

        // inputManager.Player.Move.performed += ctx => Dir = ctx.ReadValue<float>();
    }

    private void OnDisable()
    {
        inputManager.Disable();

      // inputManager.Player.Move.performed -= ctx => Dir = ctx.ReadValue<float>();
    }

    private void FixedUpdate()
    {
        var direction = Move();

        totalVelocity.x = direction;

        rigidbody2d.AddForce(totalVelocity);

    }


    //Simple Attacking

    private void Attack()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, radiusOfAttack );

        //Debug.Log("HERE");
        foreach (Collider2D obj in objects)
        {
            //Debug.Log("HERE");
            if(obj.tag == "Enemy")
            {
                //Debug.Log("Player Attacks");
                damagableEnemy.Damage(attackForce);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radiusOfAttack);
    }

    //Since there is only one boss I've cashed him 
    //But if there would more mobs I would write another way 
    public void InitializeEnemy(in GameObject enemy)
    {
        damagableEnemy = enemy.GetComponent<IDamagable>();
    }

    //Simple Jump mechanics
    private void Jump()
    {
        CheckGrounded();

        if (isGrounded)
        {
            //Debug.Log("GER");
            rigidbody2d.AddForce(Vector2.up * jumpForce);
        }
        //Debug.Log(isGrounded);
    }

    //Simple Move mechanics
    private float Move()
    {
        var direction = moveAction.ReadValue<float>();

        var summary = direction * speed * Time.deltaTime;

        return summary;
    }

    //Gets Damage 
    public void Damage(float damage)
    {
        Debug.Log("OHhh damage");

        health -= damage;

        hpSlider.value = health;
    }


    private void CheckGrounded()
    {
        var raycastHit = Physics2D.CircleCast(groundChecker.position, 1, Vector2.down , 0.1f ,  groundLayer);
        if (raycastHit.collider != null)
        {
            isGrounded = true;
        }
        else 
            isGrounded = false;
    }

}

public interface IPlayerCharacteristics
{
    float Speed { set; }
}
