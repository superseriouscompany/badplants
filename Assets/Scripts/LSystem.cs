using System.Collections.Generic;
using System.Text;

public class LSystem {
  StringBuilder sb = new StringBuilder();
  public string State { get { return state; } }
  string state;

  Dictionary<char, string> productions;
  public LSystem(string axiom, Dictionary<char, string> productions) {
    this.state = axiom;
    this.productions = productions;
  }

  public string StepForward() {
    sb.Clear();
    foreach (char c in state) {
      if (!productions.ContainsKey(c)) {
        sb.Append(c);
        continue;
      }

      sb.Append(productions[c]);
    }
    state = sb.ToString();
    return state;
  }
}