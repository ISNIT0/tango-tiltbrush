using UnityEngine;
using System.Collections;

public class TouchManager : MonoBehaviour {

	public RectTransform colorHint;
	public Shader lineShader;
	private LineRenderer lineRenderer;
	private int linePos = 0;

	// Use this for initialization
	void Start () {
		var sizeDelta = colorHint.sizeDelta;
		sizeDelta.x = Screen.width;
		colorHint.sizeDelta = sizeDelta;
		var tmpPos = colorHint.transform.position;
		tmpPos.y = 0;
		colorHint.transform.position = tmpPos;
	}

	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0) {
			if (linePos >= 0) {
				Vector3 position = Camera.main.transform.position + Camera.main.transform.forward * 1;
				linePos += 1;
				lineRenderer.SetVertexCount (linePos);
				lineRenderer.SetPosition (linePos - 1, position);
			} else {
				linePos = 0;
				float hue = (Input.touches [0].position.x / (float)Screen.width) * (float)360.0;
				Color selectedColor = ColorUtils.ColorFromHSV (hue, 1, 1, 1);

				float lineWidth = ((Input.touches [0].position.y + (float)40.0) / (float)Screen.height) * (float)0.3;

				makeNewLine (selectedColor, lineWidth);
			}
		} else if(linePos >= 0) {
			linePos = -1;
		}
	}

	void makeNewLine(Color lineColor, float lineWidth = (float)0.05) {
		GameObject item = new GameObject ();
		lineRenderer = item.AddComponent<LineRenderer>();
		lineRenderer.material = new Material(lineShader);
		lineRenderer.material.color = new Color(0, 0, 0);
		lineRenderer.SetColors(lineColor, lineColor);
		lineRenderer.SetWidth (lineWidth, lineWidth);
	}
}
