#pragma strict

public var colorHint : RectTransform;
public var lineShader : Shader;

private var lineRenderer : LineRenderer;
private var linePos : int = 0;

private var colorUtils : ColorUtils = new ColorUtils();

private var makeNewLine = function(lineColor:Color, lineWidth:float) {
	lineWidth = lineWidth || 0.05;
	var item : GameObject = new GameObject();
	lineRenderer = item.AddComponent(LineRenderer) as LineRenderer;
	lineRenderer.material = new Material(lineShader);
	lineRenderer.material.color = new Color(0, 0, 0);
	lineRenderer.SetColors(lineColor, lineColor);
	lineRenderer.SetWidth (lineWidth, lineWidth);
};

function Start () {
	colorHint.sizeDelta.x = Screen.width;
	colorHint.transform.position.y = 0;
}

function Update () {
	if (Input.touchCount > 0) {
		if (linePos >= 0) {
			var position : Vector3 = Camera.main.transform.position + Camera.main.transform.forward * 1;
			linePos += 1;
			lineRenderer.SetVertexCount(linePos);
			lineRenderer.SetPosition(linePos - 1, position);
		} else {
			linePos = 0;
			var hue : float = (Input.touches [0].position.x / Screen.width) * 360;
			var selectedColor : Color = colorUtils.ColorFromHSV(hue, 1, 1, 1);

			var lineWidth : float = ((Input.touches [0].position.y + 40) / Screen.height) * 0.3;

			makeNewLine (selectedColor, lineWidth);
		}
	} else if(linePos >= 0) {
		linePos = -1;
	}
}