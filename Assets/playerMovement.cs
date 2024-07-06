using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Di chuyển sang trái
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Vector2.left * moveSpeed);
        }

        // Di chuyển sang phải
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Vector2.right * moveSpeed);
        }

        // Nhảy
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
