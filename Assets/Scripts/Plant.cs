using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class Plant : MonoBehaviour {
	public LineRenderer line;

	public float turtleStepLength = 1f;
	public string state;

	// check if turtle rendering is working
	// FFF-FF-F-F+F+FF-F-FFF

	StringBuilder sb = new StringBuilder();
	int tick = 0;
	Turtle turtle;

	void OnValidate() {
		if (!Application.isPlaying) { return; }
		turtle?.Render(state);
	}

	void Start() {
		line.startWidth = 0.05f;
		line.endWidth = 0.05f;

		turtle = new Turtle(line, turtleStepLength);
		turtle.Render(state);
	}

	void Update() {
		if (Input.GetKeyUp(KeyCode.RightArrow)) {
			Step();
			turtle.Render(state);
		}
	}

	void Step() {
		sb.Clear();
		foreach(char c in state) {
			switch(c) {
				case 'F':
					sb.Append("F-F+F+FF-F-F+F");
					break;
				default:
					sb.Append(c);
					break;
			}
		}

		state = sb.ToString();
	}
}

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