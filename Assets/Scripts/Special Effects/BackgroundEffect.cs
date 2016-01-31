using UnityEngine;
using System.Collections;

public class BackgroundEffect : MonoBehaviour {

    public static float[] SpectrumData;

    public int spectrumChannels;
    public GameObject backgroundSquare;

    void Start() {
        Vector3 bottomLeft = Camera.main.ScreenToWorldPoint(Camera.main.ViewportToScreenPoint(new Vector3(0, 0, 0)));
        Vector3 topRight = Camera.main.ScreenToWorldPoint(Camera.main.ViewportToScreenPoint(new Vector3(1, 1, 0)));

        float xMin = bottomLeft.x;
        float xMax = topRight.x;
        float yMin = bottomLeft.y;
        float yMax = topRight.y;

        float width = xMax - xMin;
        float height = yMax - yMin;

        float xMid = (xMax + xMin) / 2;
        float yMid = (yMax + yMin) / 2;

        #region Create Squares
        int r = 0, c = 0;
        for (float x = xMid; x < 2 * width; x += 1) {
            c = 0;
            for (float y = yMid; y < 2 * height; y += 1) {
                spawnSquare(x, y, r, c++);
            }
            c = -1;
            for (float y = yMid - 1; y > -2 * height; y -= 1) {
                spawnSquare(x, y, r, c--);
            }
            r++;
        }
        r = -1;
        for (float x = xMid - 1; x > -2 * width; x -= 1) {
            c = 0;
            for (float y = yMid; y < 2 * height; y += 1) {
                spawnSquare(x, y, r, c++);
            }
            c = -1;
            for (float y = yMid - 1; y > -2 * height; y -= 1) {
                spawnSquare(x, y, r, c--);
            }
            r--;
        }
        #endregion

        SpectrumData = new float[spectrumChannels * 4];
    }

    private void spawnSquare(float x, float y, int r, int c) {
        GameObject square = (GameObject)Instantiate(backgroundSquare, new Vector3(x, y, 0), Quaternion.identity);
        square.transform.parent = transform;
        BackgroundSquare backSquareComponent = square.GetComponent<BackgroundSquare>();
        backSquareComponent.row = r;
        backSquareComponent.col = c;
        backSquareComponent.spectrumChannels = spectrumChannels;
        square.name = "Square (" + r + ", " + c + ")";
    }

    void Update() {
        AudioListener.GetSpectrumData(SpectrumData, 0, FFTWindow.Rectangular);
    }
}
