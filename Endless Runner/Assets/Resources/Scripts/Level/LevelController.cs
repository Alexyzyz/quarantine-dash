using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{

    [SerializeField] private PlayerController player_controller;
    [SerializeField] private LivesUIController lives_ui_controller;

    [SerializeField] private Text distance_ui, coins_ui, next_life_ui;
    [SerializeField] private Text active_powerup_name_ui, active_powerup_duration_ui;

    [SerializeField] private List<Sprite> powerup_sprite_list;
    [SerializeField] private GameObject powerup_bar;

    [SerializeField] private int level;

    private float distance;
    public int coins, lives = 3;

    private int[] next_life_at = {
        999 // 5, 10, 15, 20, 30, 40, 50, 100, 200, 300, 400, 500, 999
        // 10, 30, 60, 100, 150, 200, 250, 300, 400, 500, 750, 999
    };
    private int next_life_index;

    public Powerup active_powerup;
    private float active_powerup_duration;

    private void Start() {
        Initialize();
    }
    
    private void Update() {
        UpdateDistance();
        UpdateActivePowerup();
    }

    // MY FUNCTIONS

    private void UpdateActivePowerup() {
        if (active_powerup_duration > 0) {
            active_powerup_duration -= Time.deltaTime;

            active_powerup_name_ui.text = active_powerup.name;
            active_powerup_duration_ui.text = active_powerup_duration.ToString("F2") + "s left";

            powerup_bar.GetComponent<Animator>().Play("PowerupBarSlideIn");

        } else {
            // remove effect
            active_powerup = null;
            active_powerup_duration = 0;

            powerup_bar.GetComponent<Animator>().Play("PowerupBarSlideOut");
        }
    }

    public void TriggerFail() {
        GameController.coins = 200;
        GameController.bought_item_list.Clear();

        if (distance > GameController.high_score[level])
            GameController.high_score[level] = (int)distance;

        SceneManager.LoadScene("Level Select");
    }

    // PUBLIC FUNCTIONS

    public void ActivatePowerup(Powerup item) {
        if (item.type == Powerup.Type.LiveUp) {
            // live up!
            UpdateLives(1);
            return;
        }
        active_powerup = item;
        active_powerup_duration = item.duration;

        powerup_bar.transform
            .Find("Powerup Detail")
            .transform.Find("Image")
            .GetComponent<Image>().sprite = powerup_sprite_list[(int)active_powerup.type];
    }

    // UI RELATED

    public void UpdateCoins() {
        coins_ui.text = "<b>" + (++coins) + "</b>";
        coins_ui.gameObject.GetComponent<Animator>().Play("CoinsUIBlink");

        if (next_life_index > next_life_at.Length - 1) return;

        if (coins >= next_life_at[next_life_index]) {
            // life get!
            UpdateLives(1);
            next_life_index++;
        }

        next_life_ui.text = (next_life_index > next_life_at.Length - 1) ?
            "No more extra lives!" :
            "Next life at <b>" + (next_life_at[next_life_index]) + " coins.</b>";
    }

    public void UpdateLives(int n) {
        if (lives + n <= 0)
            TriggerFail();

        lives += n;
        lives_ui_controller.UpdateHearts(lives);
    }

    private void UpdateDistance() {
        distance = player_controller.gameObject.transform.position.x;
        distance_ui.text = "<b>" + ((int)distance).ToString("N0") + " m</b>";
    }

    // INIT

    private void Initialize() {
        // reset the stored coins
        GameController.coins = 0;
        lives_ui_controller.UpdateHearts(lives);
    }

}
