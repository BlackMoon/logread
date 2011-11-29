// geom.h
#pragma once
#include <math.h>

#define DBL_MAX		1.7976931348623158e+308
#define	EPS			1e-3
#define PI			3.1415926

enum {LEFT,  RIGHT,  BEYOND, BEHIND, BETWEEN, ORIGIN, DESTINATION};
class edge;
class point
{
public:
	point(double _x = 0.0, double _y = 0.0);
	int operator == (const point&);
	int operator != (const point&);
	int operator < (const point&);
	int operator > (const point&);
	point operator + (const point&);  
	point operator - (const point&);  
	double operator[] (int);
	
	friend point operator* (double, const point&);	
	
	double distance(edge&);
	double length();	
	double polarAngle();	
	
	int classify(point&, point&);
	int classify(edge&);		
// Attributes
public:
	double					x, y;
};
enum {COLLINEAR, PARALLEL, SKEW, SKEW_CROSS, SKEW_NO_CROSS};
// edge
class edge 
{
public:
	edge(point& _org, point& _dest);
	edge();
	edge& rot();
	edge& flip ();
	
	bool isVertical();
	int cross(edge&, double&);	
	int intersect(edge&, double&);	
	double length();
	double slope();
	double y(double);
	point middle();
	point _point(double);	
// Attributes
public:
	point					org, dest;
};
enum {TOUCHING, CROSSING, INESSENTIAL};
int edgeType(point& a, edge& e);