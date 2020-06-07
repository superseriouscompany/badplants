using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour {
	public float turtleStepLength = 1f;
	string state;

	Turtle turtle;
	LSystem lSystem;

	void OnValidate() {
		if (!Application.isPlaying) { return; }
		if (state == null) { return; }
		turtle?.Render(state);
	}

	void Start() {
		turtle = new Turtle(turtleStepLength);

		var quadraticIsland = new KochCurve(
			"F-F-F-F",
			new Dictionary<char, string>() {
				{'F', "F-F+F+FF-F-F+F"}
			}
		);

		var triangle = new KochCurve(
			"-F",
			new Dictionary<char, string>() {
				{'F', "F+F-F-F+F"}
			}
		);

		var islands = new KochCurve(
			"F+F+F+F",
			new Dictionary<char, string>() {
				{'F', "F+f-FF+F+FF+Ff+FF-f+FF-F-FF-Ff-FFF"},
				{'f', "ffffff"}
			}
		);


		var kochCurve = quadraticIsland;
		lSystem = new LSystem(kochCurve.axiom, kochCurve.productions);
		state = kochCurve.axiom;

		turtle.Render(state);
	}

	void Update() {
		if (Input.GetKeyUp(KeyCode.RightArrow)) {
			state = lSystem.StepForward();
		}
		turtle.Render(state);
	}
}