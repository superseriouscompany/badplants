using System.Collections.Generic;
using UnityEngine;

public class Turtle {
	public float stepLength;

	Vector3 position = new Vector3(0,0,0);
	float theta = 90;
	List<Vector3> points = new List<Vector3>();
	LineRenderer line;
	public Turtle(LineRenderer line, float stepLength = 1f) {
		this.line = line;
		this.stepLength = stepLength;
	}

	public void Render(string instructions) {
		points.Clear();
		position = Vector3.zero;
		points.Add(Vector3.zero);
		foreach(char c in instructions) {
			switch(c) {
				case 'F':
					position.x = position.x + Mathf.Cos(theta * Mathf.PI / 180) * stepLength;
					position.y = position.y + Mathf.Sin(theta * Mathf.PI / 180) * stepLength;
					points.Add(new Vector3(position.x, position.y, position.z));
					break;
				case '+':
					theta += 90;
					break;
				case '-':
					theta -= 90;
					break;
			}
		}

		line.positionCount = points.Count;
		line.SetPositions(points.ToArray());
	}
}