using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class ItemController : MonoBehaviour
{

    private LevelController level_controller;
    private ItemsUIController items_ui_controller;

    private Button button;

    [SerializeField] private int item_index;

    private void Awake() {
        Initialize();
    }

    // MY FUNCTIONS

    private void UseItem() {
        // another powerup is already active
        if (level_controller.active_powerup != null) return;

        // this slot is empty
        if (GameController.bought_item_list.Count - 1 < item_index) return;
        if (GameController.bought_item_list[item_index] == null) return;

        Powerup item = GameController.bought_item_list[item_index];
        level_controller.ActivatePowerup(item);

        GameController.bought_item_list.RemoveAt(item_index);

        items_ui_controller.UpdateImage();
    }

    // INIT

    private void Initialize() {
        level_controller = GameObject.Find("Script Holder").GetComponent<LevelController>();
        items_ui_controller = GameObject.Find("Script Holder").GetComponent<ItemsUIController>();

        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(UseItem);
    }

}
