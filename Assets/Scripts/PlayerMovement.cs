using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed;
    public Transform orientator;

    private Vector3 inputVector;
    private Vector3 moveDir;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once peoer frame
    void Update()
    {
        //Getting input --> Change to new INpout System later
        inputVector.x = Input.GetAxisRaw("Horizontal");
        inputVector.y = Input.GetAxisRaw("Vertical");
    }
    private void FixedUpdate()
    {
        if(inputVector.magnitude > 0)
        {
            moveDir = orientator.forward * inputVector.y + orientator.right * inputVector.x;
            rb.AddForce(moveDir.normalized * walkSpeed * 10f, ForceMode.Force);
        }
    }
}
