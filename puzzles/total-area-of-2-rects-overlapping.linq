<Query Kind="Program" />

/*
Calculate the TOTAL area of 2 rectangles.

should be AREA(Rect1) + AREA(Rect2) but if Rect1 and Rect2 overlap then the overlapped
area should not count twice.

Solve for overlapped area (if any exists)

Calculate total area

Subtract out any overlapped area.

Calculte overlapped area by:
* find the innermost Top Right x values of the 2 Rects. This is the Min of the 2 x values
* find innermost Bottom Right x values of the 2 Rects. This is the Max of the 2 x values
* find the inner most y values of Top and Bottom in same way.
* use these values to calculate length and width
* if either lenght or width are negative then there is no overloap so no overlapping are to subtact out
* calcuate overlapping area Length * Height
*/
void Main()
{
	var s = new Solution();
	
	// correct answer is 2
	var answer = s.ComputeArea(-1500000001, 0, -1500000000, 1, 1500000000, 0, 1500000001, 1);
	Console.WriteLine(answer);
}

public class Solution
{
	public int ComputeArea(int A, int B, int C, int D, int E, int F, int G, int H)
	{
		// Rect ABCD = (A,B),(C,D)
		// Rect EFGH = (E,F),(G,H)
		long oarea = 0;

		Console.WriteLine($"Rect ABCD = (A,B),(C,D) -> ({A},{B}),({C},{D})");
		Console.WriteLine($"Rect EFGH = (E,F),(G,H) -> ({E},{F}),({G},{H})");

		// the puzzle says area can fit in an int, but addition/subtraction
		// on edge x and y values can overflow so use long for intermediate values
		long olength = (long)Math.Min(C, G) - (long)Math.Max(A, E);
		long oheight = (long)Math.Min(D, H) - (long)Math.Max(B, F);

		Console.WriteLine($"olength={Math.Min(C, G)} - {Math.Max(A, E)}");
		Console.WriteLine($"olength={olength} oheight={oheight}");

		// if either lenght or width are negative then the rects don't overlap
		if (olength >= 0 && oheight >= 0)
		{
			oarea = olength * oheight;
		}

		int aarea = (C - A) * (D - B);
		int earea = (G - E) * (H - F);
		int rc = (int)((aarea + earea) - oarea);

		Console.WriteLine($"oarea={oarea} aarea={aarea} earea={earea} rc={rc}");
		return rc;
	}
}