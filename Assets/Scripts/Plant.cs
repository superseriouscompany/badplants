using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour {
	public float turtleStepLength = 1f;
	public KochSample sample;

	string state;

	Turtle turtle;
	LSystem lSystem;
	Dictionary<KochSample, KochCurve> samples;

	void Start() {
		turtle = new Turtle(turtleStepLength);

		LoadCurves();
		var kochCurve = samples[sample];
		lSystem = new LSystem(kochCurve.axiom, kochCurve.productions);
		state = kochCurve.axiom;

		turtle.Render(state);
	}

	void Update() {
		if (Input.GetKeyUp(KeyCode.RightArrow)) {
			state = lSystem.StepForward();
		}
	}

	void OnPostRender() {
		turtle?.Render(state);
	}

	void LoadCurves() {
		samples = new Dictionary<KochSample, KochCurve>() {
			{
				KochSample.Quadratic,
				new KochCurve(
					"F-F-F-F",
					new Dictionary<char, string>() {
						{'F', "F-F+F+FF-F-F+F"}
					}
				)
			},
			{
				KochSample.Triangle,
				new KochCurve(
					"-F",
					new Dictionary<char, string>() {
						{'F', "F+F-F-F+F"}
					}
				)
			},
			{
				KochSample.Islands,
				new KochCurve(
					"F+F+F+F",
					new Dictionary<char, string>() {
						{'F', "F+f-FF+F+FF+Ff+FF-f+FF-F-FF-Ff-FFF"},
						{'f', "ffffff"}
					}
				)
			},
			{
				KochSample.Final,
				new KochCurve(
					"F-F-F-F",
					new Dictionary<char, string>() {
						{'F', "F-F+F-F-F"}
					}
				)
			}
		};
	}
}