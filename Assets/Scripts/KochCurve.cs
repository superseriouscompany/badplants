using System.Collections.Generic;

public class KochCurve {
	public string axiom;
	public Dictionary<char, string> productions;

  public KochCurve(string axiom, Dictionary<char, string> productions) {
    this.axiom = axiom;
    this.productions = productions;
  }
}