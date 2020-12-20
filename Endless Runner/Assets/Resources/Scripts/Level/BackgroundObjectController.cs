using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundObjectController : MonoBehaviour
{

    [SerializeField] private PlayerController player_controller;
    
    [SerializeField] private Transform background_object_parent;
    private List<GameObject> background_object_list = new List<GameObject>();
    private List<GameObject> active_background_objects = new List<GameObject>();

    [SerializeField] private int background_object_cap = 10;
    [SerializeField] private float spawn_pos_x = 20;
    [SerializeField] private float spawn_pos_z_min = 20, spawn_pos_z_max = 100;
    [SerializeField] private float background_object_gap_width = 10;

    private float player_distance_until_next_spawn;

    private void Awake() {
        Initialize();
    }

    private void Update() {
        UpdateBackgroundObjects();
    }

    // MY FUNCTIONS

    private void UpdateBackgroundObjects() {
        if (player_controller.GetX() >= player_distance_until_next_spawn) {
            // generate the next background object
            GenerateBackgroundObject();
            player_distance_until_next_spawn += background_object_gap_width;

            // destroy the oldest background object
            if (active_background_objects.Count > background_object_cap)
                Destroy(active_background_objects[0]);
        }
    }

    private void GenerateBackgroundObject() {
        GameObject background_object_prefab = GetRandomBackgroundObjectPrefab();

        Vector3 next_pos = (active_background_objects.Count == 0) ?
            // position of the first background object
            new Vector3(0, 0, Random.Range(spawn_pos_z_min, spawn_pos_z_max)) :
            // position next to the current background object
            new Vector3(
                active_background_objects[active_background_objects.Count - 1].transform.position.x + background_object_gap_width,
                0,
                Random.Range(spawn_pos_z_min, spawn_pos_z_max)
            );

        GameObject new_background_object = Instantiate(background_object_prefab, next_pos, Quaternion.identity, background_object_parent);
        active_background_objects.Add(new_background_object);
    }

    private GameObject GetRandomBackgroundObjectPrefab() {
        int index = (background_object_list.Count > 1) ?
            Random.Range(0, background_object_list.Count - 1) : 0;
        return background_object_list[index];
    }
    
    // INIT

    private void Initialize() {
        background_object_list = PrefabLoader.GetPrefabCollection("background_object");

        player_distance_until_next_spawn = -background_object_cap * background_object_gap_width;
    }

}
