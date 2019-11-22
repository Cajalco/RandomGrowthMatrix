using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {
    public GameObject tilePrefab;
    public GameObject parent;
    public List<float> rangeEnds;
    public List<GameObject> tiles;      // Matrix of boardHeight * boardWidth tiles

    private int boardWidth = 10;
    private int boardHeight = 10;
    private float rangeEnd = 1.0f;

void Awake() {
        GameObject tile;
        for (int i = 0; i < boardWidth; ++i) {
            for (int j = 0; j < boardHeight; ++j) {
                tile = Instantiate(tilePrefab, new Vector3(i, j, 0f), Quaternion.identity, parent.transform);
                tiles.Add(tile);
                rangeEnds.Add(rangeEnd);
                ++rangeEnd;
            }
        }
        parent.transform.position = new Vector3(0.5f, 0.5f, 0);
    }
}