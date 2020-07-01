using System.Collections.Generic;
using UnityEngine;
using Shapes;

public class Turtle {
	public float stepLength;
	public float duration = 2;

	public Vector2 topLeft;
	public Vector2 bottomRight;

	int theta;
	float startTime;

	public Turtle(float stepLength = 1f, int theta = 90) {
		this.stepLength = stepLength;
		this.theta = theta;
		Draw.LineGeometry = LineGeometry.Volumetric3D;
		Draw.LineThicknessSpace = ThicknessSpace.Pixels;
		Draw.LineEndCaps = LineEndCap.Round;
		Draw.LineThickness = 4;
		ResetAnimation();
	}

	public void ResetAnimation() {
		startTime = Time.time;
	}

	public void Render(string instructions) {
		var position = Vector3.zero;
		var angle = 90;
		var amount = Mathf.Min(1, duration * (Time.time - startTime));
		foreach (char c in instructions) {
			var nextPosition = new Vector3(
				position.x + Mathf.Cos(angle * Mathf.PI / 180) * stepLength,
				position.y + Mathf.Sin(angle * Mathf.PI / 180) * stepLength,
				position.z
			);

			if (nextPosition.x < topLeft.x) {
				topLeft.x = nextPosition.x;
			} else if (nextPosition.x > bottomRight.x) {
				bottomRight.x = nextPosition.x;
			}
			if (nextPosition.y < topLeft.y) {
				topLeft.y = nextPosition.y;
			} else if (nextPosition.y > bottomRight.y) {
				bottomRight.y = nextPosition.y;
			}
			switch (c) {
				case 'R':
				case 'L':
				case 'F':
					Draw.Line(position, nextPosition * amount, Color.magenta);
					position.x = nextPosition.x;
					position.y = nextPosition.y;
					break;
				case '+':
					angle += theta;
					break;
				case '-':
					angle -= theta;
					break;
				case 'f':
					position.x = nextPosition.x;
					position.y = nextPosition.y;
					break;
				default:
					throw new System.Exception($"Didn't understand character {c}");
			}
		}
	}
}