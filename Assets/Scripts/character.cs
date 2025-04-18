using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private SpriteRenderer rendererRef;
    [SerializeField]
    private Rigidbody2D characterBody;
    private float movementX;
    private float moveForce = 4;
    private float jumpForce = 6;
    public AudioSource portalSound;
    public static bool onGround;
    public AudioSource jumpSound;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.truePause){
            moveCharacter();
            if(Input.GetButtonDown("Vertical") && onGround && characterBody.velocity.y < 0.05 && characterBody.velocity.y > -0.05){
                characterJump();
            }
            if(characterBody.velocity.y < 0.05 && characterBody.velocity.y > -0.05){
                onGround = true;
            }
            if(characterBody.velocity.y > 0.05 || characterBody.velocity.y < -0.05){
                onGround = false;
            }
            if(!onGround){
                anim.SetBool("jump",true);
            }
            else{
                anim.SetBool("jump",false);
            }
            if(transform.position.x < -10){
                transform.position = new Vector3(-10f, transform.position.y, transform.position.z);
            }
            else if(transform.position.x > 10){
                transform.position = new Vector3(10f, transform.position.y, transform.position.z);
            }
        }else{
            anim.SetBool("jump",false);
            anim.SetBool("run",false);
        }
    }
    void moveCharacter(){
        movementX = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(movementX, 0f, 0f) * Time.deltaTime * moveForce;
        if(movementX < 0){
            rendererRef.flipX = true;
            if(onGround){
                anim.SetBool("run",true);
            }
        }
        else if (movementX > 0){
            rendererRef.flipX = false;
            if(onGround){
                anim.SetBool("run",true);
            }
        }
        else{
            anim.SetBool("run",false);
        }
    }
    void characterJump(){
        characterBody.AddForce(new Vector2(0f,jumpForce),ForceMode2D.Impulse);
        jumpSound.Play();
    }
    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Ground")&& (characterBody.velocity.y > 0.05 && characterBody.velocity.y > -0.05)){
            onGround = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Ground" )){
            onGround = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.CompareTag("Portal")){
            portalSound.Play();
            Destroy(gameObject);
            GameManager.level++;
            GameManager.newLevel = true;
            
        }
    }
}
