using UnityEngine;

public class Player : MonoBehaviour
{
    bool IsGrounded;
    public  float normalGravity = 1;
    public  float fallingGravity = 10;

    [SerializeField] float force;
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] SpriteRenderer myRend;

    private void Update()
    {
        if(IsGrounded && Input.GetMouseButtonDown(0))
        {
            rigid.velocity = Vector2.zero;
            rigid.AddForce(force * Vector2.up, ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        rigid.gravityScale = rigid.velocity.y > 0 ? normalGravity : fallingGravity;
    }

    public void SetAlive(bool IsAlive)
    {
        rigid.isKinematic = !IsAlive;
        gameObject.SetActive(IsAlive);
        transform.position = Vector3.zero;
    }

    public void SetSprite(Sprite icon)
    {
        myRend.sprite = icon;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("obstacle"))
        {
            Manager.Instance.TakeDamage(); ;
        }
        else if (collision.collider.CompareTag("ground"))
        {
            IsGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("ground"))
        {
            IsGrounded = false;
        }
    }
}
