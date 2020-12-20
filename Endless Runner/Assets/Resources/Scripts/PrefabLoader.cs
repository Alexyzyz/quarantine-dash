using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabLoader : MonoBehaviour
{

    public static PrefabLoader Instance { get; private set; }

    private void Awake() {
        // initialize singleton
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    // MY FUNCTIONS

    public static GameObject GetPrefab(string path) {
        if (System.IO.File.Exists("Assets/Resources/Prefabs/" + path + ".prefab")) {
            return (GameObject)Resources.Load("Prefabs/" + path);
        } else {
            print("Prefab not found.");
            return null;
        }
    }

    public static List<GameObject> GetPrefabCollection(string prefab_name) {
        List<GameObject> list = new List<GameObject>();

        int i = 0;

        string path_prefix = "Assets/Resources/";
        string path_suffix = ".prefab";

        string prefab_path_indexless = "Prefabs/Collections/" + prefab_name + "/" + prefab_name + "_";
        string prefab_path = prefab_path_indexless + i;

        while (System.IO.File.Exists(path_prefix + prefab_path + path_suffix)) {
            GameObject prefab = (GameObject)Resources.Load(prefab_path);
            list.Add(prefab);
            print(prefab_path);

            prefab_path = prefab_path_indexless + (++i);
        }

        return list;
    }

}
