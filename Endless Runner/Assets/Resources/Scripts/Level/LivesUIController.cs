using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesUIController : MonoBehaviour
{

    [SerializeField] private Transform heart_parent;
    private RectTransform heart_parent_rect;

    private GameObject heart_prefab;
    public List<GameObject> heart_list = new List<GameObject>();

    [SerializeField] private float ui_distance;
    private float heart_width = 40;
    private int heart_count;

    private void Awake() {
        Initialize();
    }

    private void Update() {
        // this is somewhat inefficient? i think? oh well
        UpdateHeartPositions();
    }

    // MY FUNCTIONS

    public void UpdateHearts(int life_count) {
        while (life_count > heart_count) {
            // add a new heart ui next to the rightmost one
            Vector3 last_heart_pos;

            if (heart_list.Count == 0)
                last_heart_pos = heart_parent.transform.position;
            else {
                last_heart_pos = heart_list[heart_list.Count - 1].transform.position;
                last_heart_pos.x += heart_width;
            }

            GameObject new_heart = Instantiate(heart_prefab, last_heart_pos, Quaternion.identity, heart_parent);
            heart_list.Add(new_heart);
            heart_count = heart_list.Count;
        }

        while (life_count < heart_count) {
            // remove the rightmost heart
            if (heart_list.Count == 1) return;

            GameObject last_heart = heart_list[heart_list.Count - 1];
            last_heart.GetComponent<Animator>().SetBool("is_gone", true);

            heart_list.Remove(last_heart);
            heart_count = heart_list.Count;
        }
    }

    private void UpdateHeartPositions() {
        float x = heart_parent_rect.anchoredPosition.x
            - ((float)heart_count / 2) * heart_width
            + 0.5f * heart_width;

        foreach (GameObject heart_ui in heart_list) {
            RectTransform heart_ui_rect = heart_ui.GetComponent<RectTransform>();
            Vector3 heart_ui_target_pos = heart_ui_rect.anchoredPosition;
            heart_ui_target_pos.x = x;

            heart_ui_rect.anchoredPosition = Vector3.Lerp(heart_ui_rect.anchoredPosition, heart_ui_target_pos, 0.2f);
            x += heart_width;
        }
    }

    // INIT

    private void Initialize() {
        heart_parent_rect = heart_parent.gameObject.GetComponent<RectTransform>();

        heart_prefab = PrefabLoader.GetPrefab("Level/Heart");
    }

}
