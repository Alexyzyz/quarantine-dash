using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class ItemPurchaseController : MonoBehaviour
{

    private LevelSelectScreenController level_select_controller;

    private Button button;
    private Text quantity_ui;

    [SerializeField] private Powerup.Type item_type;
    [SerializeField] private string item_name;
    [SerializeField] private float item_duration;

    [SerializeField] private int item_price;
    private int quantity;

    private void Awake() {
        Initialize();
    }

    public void PurchaseItem() {
        // max number of items bought already
        if (GameController.bought_item_list.Count == GameController.max_bought_items) {
            print("too much items.");
            return;
        }

        // you're too poor
        if (GameController.coins < item_price) {
            print("too poor.");
            return;
        }

        // purchase successful!

        GameController.bought_item_list.Add(new Powerup(item_type, item_name, item_duration));
        GameController.coins -= item_price;

        level_select_controller.UpdateBoughtItems();
        level_select_controller.UpdateCoins();

        quantity_ui.text = "x" + (++quantity);
    }

    // INIT

    private void Initialize() {
        level_select_controller = GameObject.Find("Local Script Holder").GetComponent<LevelSelectScreenController>();

        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(PurchaseItem);

        quantity_ui = transform.parent.Find("Quantity").GetComponent<Text>();
    }

}
