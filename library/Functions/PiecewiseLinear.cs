using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libfcm.Functions
{
    /// <summary>
    /// Simple piecewise linear function.
    /// </summary>
    public class PiecewiseLinear : Interfaces.IFunction
    {
        #region PROPERTIES & FIELDS
        //----------------------------------------------------//

        //list if connected linear functions
        private List<Piece> piece { get; set; }

        //required computations precision
        private static double precision = 0.0000001;

        #endregion //-----------------------------------------//

        #region CONSTRUCTOR
        //----------------------------------------------------//

        /// <summary>
        /// Create new piecewise linear function
        /// </summary>
        public PiecewiseLinear()
        {
            this.piece = new List<Piece>();
        }

        #endregion //-----------------------------------------//

        #region INTERFACE METHODS
        //----------------------------------------------------//

        /// <summary>
        /// Return basic information about function.
        /// </summary>
        /// <returns> string containing basic information about function</returns>
        public string info()
        {
            return "Simple piecewise linear function.";
        }

        /// <summary>
        /// Return detailed information about function (aka serialization).
        /// </summary>
        /// <returns> string containing point coordinates (x;y) of linear breaks separated by spaces</returns>
        public string get()
        {
            if (piece.Count() < 1)
                return "";
            List<Point> points = pieces2points();
            List<string> coords = new List<string>();
            foreach (Point p in points)
                coords.Add(p.x.ToString() + ";" + p.y.ToString());
            return string.Join(" ", coords);
        }

        /// <summary>
        /// Specify function via predefined set of parameters (aka deserialization).
        /// </summary>
        /// <param name="parameters">string containing coordinates (x;y) {ordered by x} of piecewise linear function breakpoints separated by spaces (or as separate parameters)</param>
        /// <returns>0 if successfull, -1 otherwise</returns>
        public int set(params string[] parameters)
        {
            //check if any points are provided
            if (parameters == null || parameters.Count() == 0)
                return -1;
            //helper variables
            string[] points;
            string[] coords;
            //set point array in case of parameters joined in single string
            if (parameters.Count() == 1)
            {
                char pointDelimiter = ' ';
                points = parameters[0].Split(pointDelimiter);
            }
            //set point array in case of separated parameters
            else
            {
                points = parameters;
            }
            //check if there are at least 2 points
            if (points.Count() < 2)
                return -1;
            //generate point objects
            List<Point> pointObjects = new List<Point>(points.Count());
            char coordDelimiter = ';';
            foreach (string p in points)
            {
                coords = p.Split(coordDelimiter);
                if (coords.Count() != 2)
                    return -1;
                double x = Convert.ToDouble(coords[0]);
                double y = Convert.ToDouble(coords[1]);
                pointObjects.Add(new Point(x, y));
            }
            //check point order
            Point prev = pointObjects[0];
            foreach (Point curr in pointObjects)
            {
                if (prev.x > curr.x)
                    return -1;
                prev = curr;
            }
            //remove duplicite or close points
            removeDuplicitPoints(pointObjects);
            //check if there are at least 2 remaining points
            if (pointObjects.Count() < 2)
                return -1;
            //generate function pieces
            points2pieces(pointObjects);
            //merge consequent pieces with same slope
            this.simplify();
            //success :)
            return 0;
        }

        /// <summary>
        /// Get function derivative.
        /// </summary>
        /// <returns>either PiecewiseLinear function object or null</returns>
        public Interfaces.IFunction getDerivative()
        {
            //return null if function is not set
            if (piece.Count() == 0)
                return null;
            //declare function object
            PiecewiseLinear derivative = new PiecewiseLinear();
            //calculate derivatives of individual pieces
            Point start, end;
            foreach (Piece p in piece)
            {
                start = new Point(p.start.x, p.a);
                end = new Point(p.end.x, p.a);
                derivative.piece.Add(new Piece(start, end));
            }
            //merge consequent pieces with same slope
            derivative.simplify();
            //return derived function
            return derivative;
        }

        /// <summary>
        /// Get inverse function.
        /// </summary>
        /// <returns>either PiecewiseLinear function object or null</returns>
        public Interfaces.IFunction getInverse()
        {
            //get copy of the Piece List
            //MANUALY INVERT LIST!!!
            //List<Piece> copy = new List<Piece>();
            //foreach (Piece p in piece)

            return this;
        }

        /// <summary>
        /// Calculate function output as out=f(in).
        /// </summary>
        /// <param name="parameters">input</param>
        /// <returns>output</returns>
        public double evaluate(double input)
        {
            //TODO
            return 0;
        }

        #endregion //-----------------------------------------//

        #region ADDITIONAL METHODS
        //----------------------------------------------------//

        /// <summary>
        /// Remove unnecessary breakpoints (if two consequent pieces have the same slope 'a', merge them into one piece).
        /// </summary>
        private void simplify()
        {
            if (piece.Count() < 2)
                return;
            int curr = 0;
            int next = 1;
            while (next < piece.Count())
            {
                if (Math.Abs(piece[curr].a - piece[next].a) < precision && Math.Abs(piece[curr].b - piece[next].b) < precision)
                {
                    piece[curr].end = piece[next].end;
                    piece.RemoveAt(next);
                }
                else
                {
                    curr++;
                    next++;
                }
            }
        }

        /// <summary>
        /// Remove duplicit & close points from provided List.
        /// </summary>
        /// <param name="points">List of Point objects sorted by 'x' attribute</param>
        private void removeDuplicitPoints(List<Point> points)
        {
            int i = 0;
            int j = 1;
            while (i < points.Count() - 1)
            {
                if (Math.Abs(points[i].x - points[j].x) < precision && Math.Abs(points[i].y - points[j].y) < precision)
                {
                    points.RemoveAt(i);
                }
                else
                {
                    i++; j++;
                }
            }
        }

        /// <summary>
        /// Convert list of points into list of function pieces.
        /// </summary>
        /// <param name="points">List of (at least two) Point objects sorted by 'x' attribute</param>
        private void points2pieces(List<Point> points)
        {
            //point count check
            if (points.Count() < 2)
                return;
            //local variables
            int i = 0;
            int j = 1;
            int c = points.Count();
            //remove existing function pieces (if any)
            piece.Clear();
            //handle discontinued initial point
            if (Math.Abs(points[0].x - points[1].x) < precision)
            {
                Point additionalStartPoint = new Point(points[0].x - 1, points[0].y);
                piece.Add(new Piece(additionalStartPoint, points[0]));
            }
            //create function pieces
            while (j < c)
            {
                //check for discontinuity
                if (Math.Abs(points[i].x - points[j].x) < precision)
                    //skip discontinous part and correct the precision of x coordinate of the following point
                    points[j].x = points[i].x;
                else
                    //add function piece
                    piece.Add(new Piece(points[i], points[j]));
                //increase counters
                i++; j++;
            }
            //handle discontinued end point
            if (Math.Abs(points[c-2].x - points[c-1].x) < precision)
            {
                Point additionalEndPoint = new Point(points[c - 1].x + 1, points[c - 1].y);
                piece.Add(new Piece(points[c - 1], additionalEndPoint));
            }
        }

        /// <summary>
        /// Convert function pieces into list of points
        /// </summary>
        /// <returns>List of Point objects sorted by 'x' attribute</returns>
        private List<Point> pieces2points()
        {
            //points to return
            List<Point> points = new List<Point>();
            //return no points if there are no function pieces
            if (piece.Count < 1)
                return points;
            //always add initial point
            points.Add(piece.First().start);
            //add remaining points
            Piece prev = null;
            foreach (Piece curr in piece)
            {
                //add start point only in case of discontinuity
                if (prev != null &&
                    Math.Abs(prev.end.y - curr.start.y) >= precision &&
                    Math.Abs(prev.end.x - curr.start.x) < precision)
                    points.Add(curr.start);
                //add end point of each piece
                points.Add(curr.end);
                //store reference to this piece
                prev = curr;
            }
            return points;
        }

        #endregion //-----------------------------------------//

        #region HELPER CLASSES
        //----------------------------------------------------//

        /// <summary>
        /// Piece helper class.
        /// </summary>
        private class Piece
        {
            public Point start;
            public Point end;
            public double a;
            public double b;

            public Piece(Point start, Point end)
            {
                this.start = start;
                this.end = end;
                if (Math.Abs(this.start.x - this.end.x) > precision)
                {
                    this.a = (this.start.y - this.end.y) / (this.start.x - this.end.x);
                    this.b = this.start.y - a * this.start.x;
                }
                else
                {
                    a = 0;
                    b = this.end.y;
                }
            }
        }

        /// <summary>
        /// Point helper class.
        /// </summary>
        private class Point
        {
            public double x;
            public double y;

            public Point(Point p)
            {
                this.x = p.x;
                this.y = p.y;
            }

            public Point(double x, double y)
            {
                init(x, y);
            }

            public Point()
            {
                init(0, 0);
            }

            private void init(double x, double y)
            {
                this.x = x;
                this.y = y;
            }
        }

        #endregion //-----------------------------------------//
    }
}
