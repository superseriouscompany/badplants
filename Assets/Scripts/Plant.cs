using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using TMPro;

public class Plant : MonoBehaviour {
	public TextMeshProUGUI text;
	string w = "b";
	StringBuilder sb = new StringBuilder();
	int tick = 0;

	void FixedUpdate () {
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
		text.text = w;
	}
}