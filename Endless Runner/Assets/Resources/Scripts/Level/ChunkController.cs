using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkController : MonoBehaviour
{

    [SerializeField] private int debug_chunk;

    // FOR DEBUGGING PURPOSES

    [SerializeField] private PlayerController player_controller;

    [SerializeField] private Transform chunk_parent;
    private List<GameObject> chunk_list = new List<GameObject>();
    private List<GameObject> active_chunks = new List<GameObject>();
    
    [SerializeField] private int chunk_cap = 3;
    [SerializeField] private int chunk_width = 40;

    private float player_distance_until_next_spawn;

    private void Awake() {
        Initialize();
    }

    void Update() {
        UpdateChunks();
    }

    // MY FUNCTIONS

    private void UpdateChunks() {
        if (player_controller.GetX() >= player_distance_until_next_spawn) {
            // generate the next chunk
            GenerateChunk();
            player_distance_until_next_spawn += chunk_width;

            // destroy the oldest chunk
            if (active_chunks.Count > chunk_cap)
                Destroy(active_chunks[0]);
        }
    }

    private void GenerateChunk() {
        GameObject chunk_prefab = GetRandomChunk();
        if (debug_chunk != -1)
            chunk_prefab = chunk_list[debug_chunk];

        Vector3 next_pos = (active_chunks.Count == 0) ?
            // position of the first chunk
            new Vector3(0, 0, 0) :
            // position next to the current chunk
            new Vector3(active_chunks[active_chunks.Count - 1].transform.position.x + chunk_width, 0, 0);

        GameObject new_chunk = Instantiate(chunk_prefab, next_pos, Quaternion.identity, chunk_parent);
        active_chunks.Add(new_chunk);
    }

    private GameObject GetRandomChunk() {
        int index = (chunk_list.Count > 1) ?
            Random.Range(0, chunk_list.Count) : 0;
        return chunk_list[index];
    }

    // INIT

    private void Initialize() {
        chunk_list = PrefabLoader.GetPrefabCollection("chunk");

        player_distance_until_next_spawn = -chunk_width;
    }

}
