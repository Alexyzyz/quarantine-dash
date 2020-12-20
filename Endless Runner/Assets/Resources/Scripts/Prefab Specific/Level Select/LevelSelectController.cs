using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectController : MonoBehaviour
{

    public Button button;

    public string scene_name;

    private void Awake() {
        Initialize();
    }

    public void GoToScene() {
        SceneManager.LoadScene(scene_name);
    }

    // INIT

    private void Initialize() {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(GoToScene);
    }

}
