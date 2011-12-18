// format.cpp

#include <math.h>
#include <stdio.h>
#include <windows.h>

#define ROUND(x)				((x - floor(x) > 0.5f) ? ceil(x) : floor(x))

int main(int argc, char* argv[])
{
	FILE *istream, *ostream;
	
	if (fopen_s(&istream, argv[1], "r + t") == 0) 
	{		
		int n = 0;
		double value;
		fopen_s(&ostream, "out.txt", "w + t");
		
		while (!feof(istream))
		{
			fscanf_s(istream, "%lf ", &value);
			
			value *= 100.0f;
			value = ROUND(value) / 100.0f;

			fprintf_s(ostream, "    %.4f", -value);
			n++;
			
			if (n == 6) 
			{
				fprintf_s(ostream, "\n");
				n = 0;
			}

		}
	}
	

	return 0;
}