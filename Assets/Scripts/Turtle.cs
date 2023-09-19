using System.Collections.Generic;
using UnityEngine;
using Shapes;
using System;

public class Turtle {
	public Color color;
	public float stepLength;
	public float duration = 2;

	/// <summary>
	/// represents speed in unit vectors drawn per second
	/// </summary>
	public float speed = 9;

	public Vector2 topLeft;
	public Vector2 bottomRight;

	Stack<TurtleState> stack = new Stack<TurtleState>();
	int theta;
	float startTime;

	public Turtle(float stepLength = 1f, int theta = 90, Color color = new Color()) {
		this.stepLength = stepLength;
		this.theta = theta;
		this.color = color;
		Draw.LineGeometry = LineGeometry.Flat2D;
		Draw.LineThicknessSpace = ThicknessSpace.Pixels;
		Draw.LineEndCaps = LineEndCap.None;
		Draw.LineThickness = 4;
		ResetAnimation();
	}

	public void ResetAnimation() {
		startTime = Time.time;
	}

	public void Render(string instructions) {
		var position = Vector3.zero;
		var angle = 90f;
		float ink = speed * (Time.time - startTime);
		var amount = Mathf.Min(1, duration * (Time.time - startTime));

		foreach (char c in instructions) {
			// calculate next position using angle and step length
			var nextPosition = new Vector3(
				position.x + Mathf.Cos(angle * Mathf.PI / 180) * stepLength,
				position.y + Mathf.Sin(angle * Mathf.PI / 180) * stepLength,
				position.z
			);

			// recalculate bounds of figure for camera framing
			RecalculateBounds(nextPosition);

			switch (c) {
				case 'R':
				case 'L':
				case 'F':
					// calculate how much of the line to draw
					var difference = nextPosition - position;
					var magnitude = 1 - Mathf.Clamp01(ink);
					if (ink > 0) {
						Draw.Line(position, nextPosition - (difference * magnitude), color);
						ink--;
					}
					position.x = nextPosition.x;
					position.y = nextPosition.y;
					break;
				case '[':
					stack.Push(new TurtleState() {
						position = position,
						angle = angle
					});
					break;
				case ']':
					var state = stack.Pop();
					position = state.position;
					angle = state.angle;
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
					throw new Exception($"Didn't understand character {c}");
			}
		}
	}

	void RecalculateBounds(Vector3 nextPosition) {
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
	}
}