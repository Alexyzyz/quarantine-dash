using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationRotate : MonoBehaviour
{

    [SerializeField] private float speed;

    private void Update() {
        Animate();
    }

    private void Animate() {
        transform.Rotate(0, 0, speed * Time.deltaTime);
    }

}
