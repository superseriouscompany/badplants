using Shapes;
using UnityEngine;

public class AnimatedLine : MonoBehaviour {
  public float duration = 1f;
  float startTime;

  void Start() {
		Draw.LineGeometry = LineGeometry.Volumetric3D;
		Draw.LineThicknessSpace = ThicknessSpace.Pixels;
		Draw.LineEndCaps = LineEndCap.Round;
		Draw.LineThickness = 4;

    startTime = Time.time;
  }

  void OnPostRender() {
    var amount = Mathf.Clamp(duration * (Time.time - startTime), 0, 1);
    print($"amount={amount}");
    Draw.Line(Vector3.zero, Vector3.one * amount, Color.magenta);
  }
}