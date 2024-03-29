﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour {
	public float turtleStepLength = 1f;
	public KochSample sample;
	public Color color;

	string state;

	Turtle turtle;
	LSystem lSystem;
	Dictionary<KochSample, KochCurve> samples;

	void Start() {
		LoadCurves();
		var kochCurve = samples[sample];
		lSystem = new LSystem(kochCurve.axiom, kochCurve.productions);
		state = kochCurve.axiom;

		turtle = new Turtle(turtleStepLength, kochCurve.theta, color);
		turtle.duration = 0.8f;
		turtle.Render(state);
	}

	int transitions = 0;
	void Update() {
		if (Input.GetKeyUp(KeyCode.RightArrow)) {
			state = lSystem.StepForward();
			turtle.ResetAnimation();
			turtle?.Render(state);
			var center = (turtle.bottomRight + turtle.topLeft) / 2;
			transform.position = center.ToVector3(transform.position.z - (transitions++ * transitions * transitions));
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
			},
			{
				KochSample.SierpinskiGasket,
				new KochCurve(
					"R",
					new Dictionary<char, string>() {
						{'L', "R+L+R"},
						{'R', "L-R-L"}
					},
					60
				)
			},
			{
				KochSample.BasicAxialTree,
				new KochCurve(
					"F[+F][-F[-F]F]F[+F][-F]",
					new Dictionary<char, string>(),
					45
				)
			},
			{
				KochSample.PlantA,
				new KochCurve(
					"F",
					new Dictionary<char, string>() {
						{'F', "F[+F]F[-F]F"}
					},
					26
				)
			},
			{
				KochSample.PlantB,
				new KochCurve(
					"F",
					new Dictionary<char, string>() {
						{'F', "F[+F]F[-F][F]"}
					},
					20
				)
			},
			{
				KochSample.PlantC,
				new KochCurve(
					"F",
					new Dictionary<char, string>() {
						{'F', "FF-[-F+F+F]+[+F-F-F]"}
					},
					22
				)
			},
		};
	}
}