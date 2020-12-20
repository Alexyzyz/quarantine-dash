using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollAndWrap : MonoBehaviour
{

    private float speed = 100;
    
    [SerializeField] private float my_width;
    [SerializeField] private int element_count;
    
    private void Update() {
        Move();
    }

    private void Move() {
        transform.position += speed * Vector3.right * Time.deltaTime;

        if (transform.position.x < -my_width) {
            transform.position += (element_count + 1) * Vector3.right * my_width;
        } else
        if (transform.position.x > element_count * my_width) {
            transform.position -= (element_count + 1) * Vector3.right * my_width;
        }
    }

}
