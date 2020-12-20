using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class ItemsUIController : MonoBehaviour
{

    [SerializeField] private List<Sprite> item_sprite_list;
    [SerializeField] private List<GameObject> item_object_list;

    private void Awake() {
        Initialize();
    }

    // MY FUNCTIONS

    private int ItemTypeToSpriteIndex(Powerup.Type type) {
        switch (type) {
            case Powerup.Type.HighJump      : return 0;
            case Powerup.Type.Invulnerable  : return 1;
            case Powerup.Type.LiveUp        : return 2;
            default: return -1;
        }
    }

    // PUBLIC FUNCTIONS

    public void UpdateImage() {
        for (int i = 0; i < item_object_list.Count; i++) {
            // update the images in each ui
            Image image = item_object_list[i].transform.Find("Item").GetComponent<Image>();

            Color new_color;

            if (GameController.bought_item_list.Count - 1 < i) {
                // wow, the list doesn't even go this far
                new_color = image.color;
                new_color.a = 0;
                image.color = new_color;
                continue;
            }

            if (GameController.bought_item_list[i] == null) {
                // do the same thing to this fucker
                new_color = image.color;
                new_color.a = 0;
                image.color = new_color;
                continue;
            }

            new_color = image.color;
            new_color.a = 1;
            image.color = new_color;

            image.sprite = item_sprite_list[(int)GameController.bought_item_list[i].type];
        }
    }

    // INIT

    private void Initialize() {
        UpdateImage();
    }

}
