using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using TMPro;
using System;

public class Plant : MonoBehaviour {
	public TextMeshProUGUI text;
	public LineRenderer line;

	string w = "b";
	StringBuilder sb = new StringBuilder();
	int tick = 0;
	List<Vector3> positions = new List<Vector3>() {
		new Vector3(0,0,0),
		new Vector3(0,1,0),
		new Vector3(-1,2,0),
		new Vector3(0,1,0),
		new Vector3(1,2,0)
	};

	void Start() {
		line.startWidth = 0.05f;
		line.endWidth = 0.05f;

		var turtle = new Turtle(line);
		turtle.Render("F-F-F-F");
	}

	void FixedUpdate () {
		return;
		tick++;
		if (tick % 25 != 0) { return; }

		sb.Clear();
		foreach(char c in w) {
			switch(c) {
				case 'b':
					sb.Append("a");
					break;
				case 'a':
					sb.Append("ab");
					break;
			}
		}

		w = sb.ToString();

		line.positionCount = positions.Count;
		line.SetPositions(positions.ToArray());
		foreach (char c in w) {

		}
	}
}

public class Turtle {
	Vector3 position = new Vector3(0,0,0);
	float theta = 90;
	List<Vector3> points = new List<Vector3>();
	LineRenderer line;
	public Turtle(LineRenderer line) {
		this.line = line;
	}

	public void Render(string instructions) {
		points.Clear();
		foreach(char c in instructions) {
			switch(c) {
				case 'F':
					points.Add(new Vector3(position.x, position.y, position.z));
					position.x = position.x + Mathf.Cos(theta * Mathf.PI / 180);
					position.y = position.y + Mathf.Sin(theta * Mathf.PI / 180);
					break;
				case '+':
					theta -= 90;
					break;
				case '-':
					theta += 90;
					break;
			}
		}

		Debug.Log($"{String.Join(",",points)}");
	}
}