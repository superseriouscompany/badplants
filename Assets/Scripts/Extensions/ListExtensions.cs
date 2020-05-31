using System.Collections.Generic;

public static class ListExtensions {
	public static string ToReadableString<T>(this List<T> list) => $"[{string.Join(", ", list)}]";
}