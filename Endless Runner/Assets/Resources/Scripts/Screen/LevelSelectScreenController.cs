using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class LevelSelectScreenController : MonoBehaviour
{

    [SerializeField] private Text coins_ui;
    [SerializeField] private Text bought_items_ui;

    private void Awake() {
        UpdateCoins();
    }

    // PUBLIC FUNCTIONS

    public void UpdateCoins() {
        coins_ui.text = "" + GameController.coins;
    }

    public void UpdateBoughtItems() {
        int slots_left = GameController.max_bought_items - GameController.bought_item_list.Count;
        bought_items_ui.text = "Purchase\n<b>" + slots_left + " more item(s)\n</b>for your next run!";
    }

}
