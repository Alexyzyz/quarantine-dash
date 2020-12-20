using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{

    [SerializeField] private Sprite blink_sprite;

    [SerializeField] private Transform player;
    [SerializeField] private Vector3 target;

    [SerializeField] private float accel, max_speed;
    private float speed;

    private Vector3 accel_vec, speed_vec;

    public bool has_been_collected;

    private void Awake() {
        target += GameObject.Find("Player").transform.position;
    }

    private void Update() {
        if (has_been_collected)
            Nyoom();
    }

    public void SetSpriteToBlink() {
        transform.Find("Rig").GetComponent<SpriteRenderer>().sprite = blink_sprite;
    }

    private void Nyoom() {
        if (transform.position == target)
            Destroy(gameObject);

        // update the accel vector
        Vector3 dir = (target - transform.position).normalized;
        speed_vec += dir * speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, target) <= speed_vec.magnitude) {
            // snap the object to this position
            transform.position = target;
        } else
            transform.position += speed_vec;

        float new_speed = speed + accel * Time.deltaTime;
        speed = (new_speed < max_speed) ? new_speed : max_speed;
    }

}
