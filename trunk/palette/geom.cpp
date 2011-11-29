// geom.cpp
#include "geom.h"

double dotProduct(const point& p, const point& q)
{
	return (p.x * q.x + p.y * q.y);
}
int edgeType(point& a, edge& e)
{
	point v = e.org, 
		  w = e.dest;
	
	switch (a.classify(e)) 
	{
		case LEFT: return ((v.y < a.y) && (a.y <= w.y)) ? CROSSING : INESSENTIAL; 
		case RIGHT: return ((w.y < a.y) && (a.y <= v.y)) ? CROSSING : INESSENTIAL; 
		case BETWEEN:
		case ORIGIN:
		case DESTINATION: return TOUCHING;
		default: return INESSENTIAL;
	}
}
point operator* (double s, const point& p)
{
	return point(s * p.x, s * p.y);
}
// point
point::point(double _x, double _y): x(_x), y(_y)
{	
}
int point::operator != (const point& p)
{
	return !(*this == p);
}
point point::operator + (const point& p)
{
	return point(x + p.x, y + p.y);
}
point point::operator - (const point& p)
{
	return point(x - p.x, y - p.y);
}
int point::operator < (const point& p)
{
	return ((x < p.x) || ((x == p.x) && (y < p.y)));
}
int point::operator == (const point& p) 
{
	return (EPS >= fabs(x - p.x)) && (EPS >= fabs(y - p.y));
}
int point::operator > (const point& p)
{
	return ((x > p.x) || ((x == p.x) && (y > p.y)));
}
double point::operator [] (int i)
{
	return (i == 0) ? x : y;
}
double point::distance(edge& e)
{
	edge ab = e;
	ab.flip().rot(); 
 
	point n(ab.dest -  ab.org);                     
	n = (1.0 / n.length()) * n;
                               
	edge f(*this, *this + n);
                              
	double t;                 
	f.intersect(e, t);        
                              
	return t;
}
double point::length()
{
	return sqrt(x * x + y * y);
}
double point::polarAngle()
{	
	if (0.0 == x) 
	{
		if (0.0 == y) return -1.0;
		return ((0.0 < y) ? 90 : 270);
	}
	
	double theta = atan(y / x);
	theta *= 360 / (2 * PI);

	if (0.0 < x) return ((0.0 <= y) ? theta : 360 + theta);
	else return (180 + theta);
}
int point::classify(point& p0, point& p1)
{
	point p2 = *this;
	point a = p1 - p0;
	point b = p2 - p0;
	
	double sa = a. x * b.y - b.x * a.y;
	
	if (EPS < sa) return LEFT;
	if (-EPS > sa) return RIGHT;
	
	if ((a.x * b.x < 0.0) || (a.y * b.y < 0.0)) return BEHIND;
	if (a.length() < b.length()) return BEYOND;
	
	if (p0 == p2) return ORIGIN;
	if (p1 == p2) return DESTINATION;
	
	return BETWEEN;
}
int point::classify(edge& e)
{
	return classify(e.org, e.dest);
}
// edge
edge::edge(point& _org, point& _dest) : org(_org), dest(_dest)
{
}
edge::edge() : org(point(0, 0)), dest(point (1, 0))
{
}
edge& edge::rot()
{
	point m = 0.5 * (org + dest),
		  v = dest - org,
		  n(v.y, -v.x);

	org = m - 0.5 * n;
	dest = m + 0.5 * n;
	return *this;
}
edge& edge::flip()
{
  return rot().rot();
}
bool edge::isVertical()
{
  return (org.x == dest.x);
}
double edge::length()
{
	return (dest - org).length();
}
double edge::slope()
{
	if (org.x != dest.x) return (dest.y - org.y) / (dest.x - org.x);
	return DBL_MAX;
}
double edge::y(double x)
{
	return slope() * (x - org.x) + org.y;
}
int edge::cross(edge& e, double& t)
{
	double s;
	int crossType = e.intersect(*this, s);
	
	if ((COLLINEAR == crossType) || (PARALLEL == crossType)) return crossType;
	
	if ((0.0 > s) || (1.0f < s)) return SKEW_NO_CROSS;
	intersect(e, t);
	
	if ((0.0 <= t) && (1.0 >= t)) return SKEW_CROSS;
	else return SKEW_NO_CROSS;
}
int edge::intersect(edge& e, double& t)
{
	point a = org, b = dest, 
		  c = e.org, d = e.dest,
		  n = point((d - c).y, (c - d).x);
	
	double denom = dotProduct(n, b - a);
	if (0.0 == denom) 
	{  
		int aclass = org.classify(e);
		if ((LEFT == aclass) || (RIGHT == aclass)) return PARALLEL;
		else return COLLINEAR;
	}
	double num = dotProduct(n, a - c);
	t = -num / denom;
	return SKEW;
}
point edge::middle()
{
	return org + 0.5 * (dest - org);
}
point edge::_point(double t)
{
  return org + t * (dest - org);
}