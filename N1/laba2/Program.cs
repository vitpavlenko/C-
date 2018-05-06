using System;
using System.Collections.Generic;

namespace lab1
{
    class Program
    {
        class Segment
        {
            public int point1;
            public int point2;
            public double cost;

            public Segment(int point1, int point2, double cost)
            {
                this.point1 = point1;
                this.point2 = point2;
                this.cost = cost;
            }
        }

        class Point
        {
            double x;
            double y;

            public Point(double x, double y)
            {
                this.x = x;
                this.y = y;
            }

            public Point()
            {
                x = 0;
                y = 0;
            }

            public double GetX()
            {
                return x;
            }

            public double GetY()
            {
                return y;
            }

            public void SetX(double x)
            {
                this.x = x;
            }

            public void SetY(double y)
            {
                this.y = y;
            }
        }

        static private Point[] Points;
        static private double[,] w;
        static private int number;
        static private int s4 = 0;
        static private Segment[] tree;
        static private Segment[] segments;
        static private int m;
        static private int m1;
        static private bool[] visit;
        static private HashSet<Segment> Allsegments;

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Enter number of Points:");
                String s = Console.ReadLine();
                number = int.Parse(s);

                Points = new Point[number];
                w = new double[number, number];

                for (int i = 0; i < number; i++)
                    for (int j = 0; j < number; j++)
                        w[i, j] = Double.PositiveInfinity;

                Console.WriteLine("Enter your choise:\n1. Generate Points\n2. Enter Points");
                char choise = Convert.ToChar(Console.ReadLine());
                switch (choise)
                {
                    case '1':
                        GeneratePoints();
                        break;
                    case '2':
                        EnterPoints();
                        break;
                    default:
                        Console.WriteLine("Incorrect input");
                        break;
                }

                m1 = (number * (number - 1) / 2);

                segments = new Segment[m1];
                Allsegments = new HashSet<Segment>();
                tree = new Segment[m1];

                Intialize();
                
                visit = new bool[number];

                PrimAlgorithm();

                Output();
            } catch {
                Console.WriteLine("Incorrect input");
                Console.WriteLine("Press any key to continue . . .");
                Console.ReadKey();
            }
        }

        static double GetCost(Point point1, Point point2)
        {
            return Math.Sqrt(Math.Pow((point1.GetX() - point2.GetX()), 2) + Math.Pow((point1.GetY() - point2.GetY()), 2));
        }

        static void Output()
        {
            for (int i = 0; i < s4; i++)
            {
                Console.WriteLine("\nSegment №" + (i + 1) + ":");
                Console.WriteLine("Point №" + tree[i].point1 + ":\nx: " + Points[tree[i].point1 - 1].GetX() + "  y: " + Points[tree[i].point1 - 1].GetY());
                Console.WriteLine("Point №" + tree[i].point2 + ":\nx: " + Points[tree[i].point2 - 1].GetX() + "  y: " + Points[tree[i].point2 - 1].GetY());
                Console.WriteLine("Cost:" + tree[i].cost);
            }
            Console.WriteLine("Press any key to continue . . .");
            Console.ReadKey();
        }

        static void GeneratePoints()
        {
            Random random = new Random();
            for (int i = 0; i < number; i++)
            {
                double x = random.Next(-100, 100);
                double y = random.Next(-100, 100);

                Points[i] = new Point(x, y);
            }
        }

        static void EnterPoints()
        {
            for (int i = 0; i < number; i++)
            {
                Console.WriteLine("Point №" + (i + 1) + "\nEnter x: ");
                String s1 = Console.ReadLine();
                double x = double.Parse(s1);

                Console.WriteLine("Enter y: ");
                String s2 = Console.ReadLine();
                double y = double.Parse(s2);

                Points[i] = new Point(x, y);
            }
        }

        static void Intialize()
        {
            int index = 0;
            for (int i = 0; i < number; i++)
                for (int j = 0; j < number; j++)
                {
                    if (i < j)
                    {
                        segments[index++] = new Segment(i, j, GetCost(Points[i], Points[j]));
                        w[i, j] = GetCost(Points[i], Points[j]);
                        w[j, i] = w[i, j];
                    }
                }
        }

        static void GenerateSegments()
        {
            Random random = new Random();
            for (int i = 0; i < m; i++)
            {
                bool flag = true;
                while (flag)
                {
                    int x = random.Next(0, number);
                    int y = random.Next(0, number);

                    if (Allsegments.Add(new Segment(x, y, 0)) && x != y)
                    {
                        segments[i] = new Segment(x - 1, y - 1,
                            
                            0);
                        w[x, y] = 0;
                        w[y, x] = 0;
                        flag = false;
                    }
                }
            }
        }

        static void PrimAlgorithm()
        {
            for (int i = 0; i < number; i++)
            {
                visit[i] = false;
            }

            bool e = true;
            double segment = Double.PositiveInfinity;
            int v1 = -1;
            for (int i = 0; i < m1; i++)
            {
                if (segment > segments[i].cost)
                {
                    segment = segments[i].cost;
                    v1 = segments[i].point1;
                }
            }
            visit[v1] = true;
            while (e)
            {
                e = false;
                bool e1 = false;
                if (!e1)
                {
                    int point1 = -1;
                    int point2 = -1;
                    double segment1 = Double.PositiveInfinity;
                    for (int i = 0; i < number; i++)
                    {
                        for (int j = 0; j < number; j++)
                        {
                            if ((visit[i] == true) && (visit[j] == false) && (w[i, j] < segment1))
                            {
                                point1 = i;
                                point2 = j;
                                segment1 = w[i, j];
                            }
                        }

                    }
                    if ((point1 != -1) && (point2 != -1))
                    {
                        e1 = true;
                        visit[point2] = true;
                        tree[s4++] = new Segment(point1 + 1, point2 + 1, segment1);
                    }
                }
                for (int i = 0; i < number; i++)
                    if (visit[i] == false)
                        e = true;
            }
        }
    }
}
