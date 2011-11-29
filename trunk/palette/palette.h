// palette.h
#pragma once
#include "geom.h"

#include <stdio.h>
#include <windows.h>
#include <vector>

#define N_VAL		-1.0
// quadrangle borders
#define B_NO		0x0
#define B_H0		0x1
#define B_H1		0x2
#define B_V0		0x4
#define B_V1		0x8

struct PALFILENAME
{
	char					scalefile[MAX_PATH], ptfile[MAX_PATH], outfile[MAX_PATH];
	inline PALFILENAME()
	{		
		strcpy_s(outfile, MAX_PATH, "out.dat");		
	};
};
// pointz
class pointz : public point
{
public:
	pointz(double _x = 0.0, double _y = 0.0, double _z = 0.0);
// Atributes
public:
	double					z;				// parametr
};
// curve
class curve
{
public:
	~curve();
	size_t size();	
	void clear();	
// Attributes
public:		
	char					layer[16];
	std::vector<pointz>		pts;		
};
enum {BOUNDARY_H0, BOUNDARY_H1, BOUNDARY_V0, BOUNDARY_V1, INSIDE, OUTSIDE};
// quad
class quad
{
public:
	quad();
	int pointIn(point&);
	point gethv(point&, int);
	void horz();
// Attributes
public:	
	char					layers[2][16];	
	double					z[2];
	edge					hedges[2], vedges[2];
}; 