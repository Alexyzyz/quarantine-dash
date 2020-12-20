using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    
    private void Update() {
        if (Tap()) {
            GetTappedObjectName();
        }
    }

    public static bool Tap() {
        return Input.GetKeyDown(KeyCode.Mouse0);
    }

    public static string GetTappedObjectName() {
        Vector3 tap_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 tap_pos_2d = new Vector2(tap_pos.x, tap_pos.y);

        RaycastHit2D hit = Physics2D.Raycast(tap_pos_2d, Vector2.zero);
        if (hit.collider != null) {
            Debug.Log(hit.collider.gameObject.name);
            return hit.collider.gameObject.name;
        }

        return null;
    }

    public static bool HoldTap() {
        return Input.GetKey(KeyCode.Mouse0);
    }

}
