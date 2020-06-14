using System.Collections.Generic;

public class KochCurve {
	public string axiom;
	public Dictionary<char, string> productions;
  public int theta;

  public KochCurve(string axiom, Dictionary<char, string> productions, int theta = 90) {
    this.axiom = axiom;
    this.productions = productions;
    this.theta = theta;
  }
}

public enum KochSample {
  Quadratic,
  Triangle,
  Islands,
  Final,

  SierpinskiGasket,
}