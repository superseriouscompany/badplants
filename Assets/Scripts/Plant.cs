using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour {
	public LineRenderer line;

	public float turtleStepLength = 1f;
	public string state;

	Turtle turtle;
	LSystem lSystem;

	void OnValidate() {
		if (!Application.isPlaying) { return; }
		turtle?.Render(state);
	}

	void Start() {
		line.startWidth = 0.05f;
		line.endWidth = 0.05f;

		turtle = new Turtle(line, turtleStepLength);
		turtle.Render(state);

		lSystem = new LSystem(state, new Dictionary<char, string>() {
			{'F', "F-F+F+FF-F-F+F"}
		});
	}

	void Update() {
		if (Input.GetKeyUp(KeyCode.RightArrow)) {
			state = lSystem.StepForward();
			turtle.Render(state);
		}
	}
}