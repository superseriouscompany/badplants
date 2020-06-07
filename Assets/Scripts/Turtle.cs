using System.Collections.Generic;
using UnityEngine;

public class Turtle {
	public float stepLength;

	int theta;

	public Turtle(float stepLength = 1f, int theta = 90) {
		this.stepLength = stepLength;
		this.theta = 90;
	}

	public void Render(string instructions) {
		var position = Vector3.zero;
		var angle = 90;
		foreach(char c in instructions) {
			var nextPosition = new Vector3(
				position.x + Mathf.Cos(angle * Mathf.PI / 180) * stepLength,
				position.y + Mathf.Sin(angle * Mathf.PI / 180) * stepLength,
				position.z
			);

			switch(c) {
				case 'F':
					Debug.DrawLine(position, nextPosition, Color.magenta, .25f);
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