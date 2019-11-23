using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowthManager : MonoBehaviour {
    public bool continueGrowing = true;

    private BoardManager board;
    private int tileCount;

    private float randomGrowthValue;
    private int correspondingTile;

    private float growthModifier = 1.0f;
    private float colorChangeModifier = .02f;
    private int growthSpeed = 1;
    private int frameCount = 0;

    void Start() {
        board = GetComponent<BoardManager>();
        tileCount = board.tiles.Count;
        //growthModifier = ConfigurationManager.Instance.getGrowthModifier();
        //colorChangeModifier = ConfigurationManager.Instance.getColorChangeModifier();
    }

    void FixedUpdate() {
        if (frameCount == growthSpeed) {
            if (tileCount < 50) {
                grow();
            }
            for (int i = 0; i < tileCount / 50; ++i) {
                grow();
            }
            frameCount = -1;
        }
        frameCount++;
    }

    private void grow() {
        float totalRangeEnd = board.rangeEnds[tileCount - 1];
        randomGrowthValue = Random.Range(0.0f, totalRangeEnd);

        // Find GameObject with nearest key (growthRangeEnd) greater than or equal to randomGrowthIndex.
        // If (index >= 0), index corresponds to the correct (existing) GameObject. Else, the (~index) corresponds to the correct GameObject.
        int index = board.rangeEnds.BinarySearch(randomGrowthValue);
        if (index < 0) {
            index = (~index);
        }
        if (growthModifier > 0) {
            changeRangeEnds(index);
        }

            //  Moving walls game

            //Color tileColor = board.tiles[index].GetComponent<SpriteRenderer>().color;
            //if (tileColor == new Color(0, 0, 0, 1)) {
            //    board.tiles[index].GetComponent<SpriteRenderer>().color = new Color(.80f, .80f, .80f, 1);   // Corridor
            //}
            //if (tileColor == new Color(.80f, .80f, .80f, 1)) {
            //    board.tiles[index].GetComponent<SpriteRenderer>().color = new Color(.40f, .40f, .40f, 1);   // Transitioning Wall
            //}
            //if (tileColor == new Color(.40f, .40f, .40f, 1)) {
            //    board.tiles[index].GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1);            // Wall
            //}

        // Change corresponding GameObject's sprite renderer's color.
        Color tileColor = board.tiles[index].GetComponent<SpriteRenderer>().color;
        float h, s, v;
        Color.RGBToHSV(new Color(tileColor.r, tileColor.g, tileColor.b, 1), out h, out s, out v);
        board.tiles[index].GetComponent<SpriteRenderer>().color = Color.HSVToRGB((h + colorChangeModifier), 1, 1);
    }

    private void changeRangeEnds(int index) {
        for (int i = index; i < tileCount; ++i) {
            board.rangeEnds[i] += growthModifier;
        }
    }
}
