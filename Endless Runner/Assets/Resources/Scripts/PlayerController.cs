using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private LevelController level_controller;

    private Rigidbody2D rb;
    private Animator animator;

    [SerializeField] private float max_speed, max_speed_time;
    [SerializeField] private float speed;
    private float speed_up_rate;
    
    [SerializeField] private float base_jump_speed, bonus_jump_speed;
    private float jump_speed;

    private bool is_jumping, is_falling;

    [SerializeField] private float inv_time;
    private float inv_alarm;

    // safetynet for jumping off of platforms
    [SerializeField] private float jump_buffer, jump_buffer_time;
    // safetynet for landing on platforms
    [SerializeField] private float landable_offset;
    
    private void Awake() {
        Initialize();
    }
    
    private void Update() {
        HandleMovement();
        UpdateInv();
        CheckDeath();
    }

    // MY FUNCTIONS

    private void HandleMovement() {
        Vector2 new_velocity = rb.velocity;

        jump_speed = base_jump_speed;

        // take powerups into consideration
        if (level_controller.active_powerup != null) {
            if (level_controller.active_powerup.type == Powerup.Type.HighJump)
                jump_speed += bonus_jump_speed;
        }

        if (InputController.Tap() && jump_buffer == 0) {
            // reset the jump buffer
            jump_buffer = jump_buffer_time;
        }
        // tick down the jump buffer
        jump_buffer = (jump_buffer > 0) ? (jump_buffer - 1) : 0;

        if (jump_buffer > 0 && !is_falling) {
            // jump while the buffer is still on
            new_velocity.y = jump_speed;
            jump_buffer = 0;
            is_jumping = true;
            is_falling = true;
        }
        if (!InputController.HoldTap() && is_jumping) {
            // stop increasing the jump height
            if (new_velocity.y > 0)
                new_velocity.y *= 0.5f;
            is_jumping = false;
        }

        new_velocity.x = speed;
        rb.velocity = new_velocity;

        speed += speed_up_rate * Time.deltaTime;
    }

    private void UpdateInv() {
        // tick down the invincibility alarm
        if (inv_alarm > 0)
            inv_alarm -= Time.deltaTime;
        else
            SetInvulnerable(false);
    }

    private void CheckDeath() {
        if (transform.position.y < -20) {
            // check for death
            level_controller.TriggerFail();
        }
    }

    // MY PUBLIC FUNCTIONS

    public float GetX() {
        return transform.position.x;
    }

    public bool IsInvulnerable() {
        if (level_controller.active_powerup != null) {
            // invulnerable powerup
            if (level_controller.active_powerup.type == Powerup.Type.Invulnerable)
                return true;
        }
        
        return inv_alarm > 0;
    }

    public void SetInvulnerable(bool state) {
        inv_alarm = state ? inv_time : 0;
        animator.SetBool("is_invulnerable", state);
    }

    // INITIALIZATION

    private void Initialize() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        speed_up_rate = (max_speed - speed) / max_speed_time;
    }

    // COLLISION

    void OnCollisionEnter2D(Collision2D c) {
        Collider2D collider = c.collider;
        GameObject gameobj = collider.gameObject;

        Vector3 other_position = gameobj.transform.position;
        Vector3 my_position = transform.position;

        string tag = gameobj.tag;
        if (tag == "floor") {
            // platform collision
            // this gives a bit of a leeway for the player
            if (my_position.y >= other_position.y - landable_offset) {
                Vector3 pos = my_position;
                pos.y = other_position.y;

                transform.position = pos;
                my_position = transform.position;
            }

            if (other_position.y <= my_position.y) {
                // the player has landed
                is_jumping = false;
                is_falling = false;

                Vector2 new_velocity = rb.velocity;
                new_velocity.y = 0;
                rb.velocity = new_velocity;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D c) {
        Collider2D collider = c.collider;
        GameObject gameobj = collider.gameObject;

        Vector3 other_position = gameobj.transform.position;
        Vector3 my_position = transform.position;

        string tag = gameobj.tag;
        if (tag == "floor") {
            //is_falling = true;
            //jump_buffer = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D c) {
        Collider2D collider = c;
        GameObject gameobj = collider.gameObject;
        Vector3 position = gameobj.transform.position;

        string tag = gameobj.tag;
        if (tag == "coin") {
            // collect the coin
            CoinController coin_controller = gameobj.GetComponent<CoinController>();
            if (!coin_controller.has_been_collected) {
                level_controller.UpdateCoins();
                coin_controller.has_been_collected = true;
                coin_controller.SetSpriteToBlink();

                gameobj.GetComponent<Animator>().SetBool("has_been_collected", true);
            }
        }
        else

        if (tag == "enemy") {
            // hit the enemy
            if (rb.velocity.y < 0) {
                // bonk them in the head
                Vector3 temp = rb.velocity;
                temp.y = 1.2f * jump_speed;
                rb.velocity = temp;

                is_jumping = true;
                is_falling = true;

                gameobj.GetComponent<Animator>().Play("Squash");
            } else
            if (!IsInvulnerable()) {
                // get hit by the enemy
                level_controller.UpdateLives(-1);
                SetInvulnerable(true);
            }
        }
    }

}
