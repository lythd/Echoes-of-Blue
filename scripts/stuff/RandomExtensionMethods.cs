using System;
using System.Numerics;
using Vector2 = Godot.Vector2;

namespace EchoesofBlue.scripts.stuff;

public static class RandomExtensionMethods
{
	/// <summary>
	/// Returns a random long from min (inclusive) to max (exclusive)
	/// </summary>
	/// <param name="random">The given random instance</param>
	/// <param name="min">The inclusive minimum bound</param>
	/// <param name="max">The exclusive maximum bound.  Must be greater than min</param>
	public static long NextLong(this Random random, long min, long max)
	{
		if (max <= min)
			throw new ArgumentOutOfRangeException(nameof(max), "max must be > min!");

		//Working with ulong so that modulo works correctly with values > long.MaxValue
		ulong uRange = (ulong)(max - min);

		//Prevent a modolo bias; see https://stackoverflow.com/a/10984975/238419
		//for more information.
		//In the worst case, the expected number of calls is 2 (though usually it's
		//much closer to 1) so this loop doesn't really hurt performance at all.
		ulong ulongRand;
		do
		{
			var buf = new byte[8];
			random.NextBytes(buf);
			ulongRand = (ulong)BitConverter.ToInt64(buf, 0);
		} while (ulongRand > ulong.MaxValue - ((ulong.MaxValue % uRange) + 1) % uRange);

		return (long)(ulongRand % uRange) + min;
	}

	/// <summary>
	/// Returns a random long from 0 (inclusive) to max (exclusive)
	/// </summary>
	/// <param name="random">The given random instance</param>
	/// <param name="max">The exclusive maximum bound.  Must be greater than 0</param>
	public static long NextLong(this Random random, long max)
	{
		return random.NextLong(0, max);
	}

	/// <summary>
	/// Returns a random long over all possible values of long (except long.MaxValue, similar to
	/// random.Next())
	/// </summary>
	/// <param name="random">The given random instance</param>
	public static long NextLong(this Random random)
	{
		return random.NextLong(long.MinValue, long.MaxValue);
	}

	public static Vector2 NextVector(this Random random)
	{
		return new Vector2(random.NextSingle()*2f-1f, random.NextSingle()*2f-1f).Normalized();
	}

	public static float SquaredDist(this IDamageableEntity first, IDamageableEntity second)
	{
		return first.Pos.DistanceSquaredTo(second.Pos);
	}

	public static float Dist(this IDamageableEntity first, IDamageableEntity second)
	{
		return first.Pos.DistanceTo(second.Pos);
	}
	
	// thousand, million, billion, trillion, quadrillion, Quintillion, sextillion, Septillion, octillion, nonillion, decillion,
	// undecillion, Duodecillion, Tredecillion, quattuordecillion, QuinDecillion, sexdecillion, SeptenDecillion, Octodecillion, Movemdecillion, Vigintillion
	// TriginTillion, quadragintillion, QuinquaGintillion, sexagintillion, SeptuaGintillion, octogintillion, nongintillion, Centillion
	// did pretty good at following the patterns, V and C can be capitalized since they are milestone numbers and there are no other v's or c's to distinguish (otherwise the higher would get it)
	private static readonly string[] Suffixes = ["", "k",
		"m", "b", "t", "q", "Q", "s", "S", "o", "n", "d",
		"u", "D", "T", "qd", "QD", "sd", "SD", "O", "N", "V",
		"uv", "dv", "tv", "qv", "QV", "sv", "SV", "ov", "nv", "TT",
		"ut", "dt", "tt", "qt", "QT", "st", "ST", "ot", "nt", "qg",
		"uq", "dq", "tq", "qq", "Qq", "sq", "Sq", "oq", "nq", "QG",
		"uQ", "dQ", "tQ", "qQ", "QQ", "sQ", "SQ", "oQ", "nQ", "sg",
		"us", "ds", "ts", "qs", "Qs", "ss", "Ss", "os", "ns", "SG",
		"uS", "dS", "tS", "qS", "QS", "sS", "SS", "oS", "nS", "og",
		"uo", "do", "to", "qo", "QO", "so", "SO", "oo", "no", "ng",
		"un", "dn", "tn", "qn", "QN", "sn", "SN", "on", "nn", "C",
	];

	// examples: 3, 34, 345, 3.45k, 34.5k, 345k, 3.45m, 34.5m, etc, once it exceeds its maximum suffix it returns ∞ as to not exceed any string length
	public static string ToShortString(this BigInteger d)
	{
		int index = 0;
		if(d < 1000) return $"{d}";
		d *= 1000;
		// if this multiplier wasn't here the decimals would be cutoff, ie 3k instead of 3.45k, and you might be wondering why not 100 but then it wouldn't round,
		// ie if it were 3456 and the multiplier was just 100 then it would display 3.45k not 3.46k 
		while (d >= 1000000)
		{
			d /= 1000;
			index++;
			if(index == Suffixes.Length) return "∞";
		}
		return $"{(Decimal)d/1000:G3}{Suffixes[index]}";
	}
}