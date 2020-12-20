using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform player_trans;

    public float x_offset;
    
    void Start() {
        
    }
    
    void Update() {
        UpdatePosition();
    }

    // My functions

    private void UpdatePosition() {
        Vector3 new_pos = transform.position;
        new_pos.x = player_trans.position.x + x_offset;

        transform.position = new_pos;
    }

}
