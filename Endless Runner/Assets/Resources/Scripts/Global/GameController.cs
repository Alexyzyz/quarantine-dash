using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public static GameController Instance { get; private set; }

    [SerializeField] private float gravity = 40;

    // things that carry over between scenes
    public static int coins = 200;

    public static List<Powerup> bought_item_list = new List<Powerup>();
    public static int max_bought_items = 3;

    public static int[] high_score = new int[3];
    public static int[] required_score = { 100, 200 };
    
    private void Awake() {
        // initialize singleton
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        Initialize();
    }

    private void Update() {
        Debug();
    }

    // MY FUNCTIONS

    private void Debug() {
        // debug commands are only accessible while holding Ctrl
        if (!Input.GetKey(KeyCode.LeftControl)) return;

        if (Input.GetKeyDown(KeyCode.R)) {
            // reload the scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown(KeyCode.Q)) {
            // exit to shop
            SceneManager.LoadScene("Level Select");
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            // exit the game
            Application.Quit();
        }
    }

    // INIT

    private void Initialize() {
        Physics2D.gravity = new Vector2(0, -gravity);
    }

}
