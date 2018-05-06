using System;
using System.Collections.Generic;

namespace lab2
{
    public class Program
    {
        static void Main(string[] args)
        {
            string[] p = new string[1];
            p[0] = "duity";

            Worker[] w = new Worker[1];
            w[0] = new Coder("second", "secondBoss", 288, p, null);

            Worker[] w1 = new Worker[2];
            w1[0] = new Coder("second", "secondBoss", 123, p, null);
            w1[1] = new Coder("seventh", "secondBoss", 321, p, null);

            Group MyGroup = new Group();
            MyGroup.Changing += ChangingListener;
            MyGroup.Add(new Manager("first", "firstBoss", 1488, p, w));
            MyGroup.Add(new Coder("third", "firstBoss", 328, p, w1));
            MyGroup.Insert(0, new Developer("fivs", "firstBoss", 69, p, w1) + new Developer("six", "sixBoss", 47, p, w));
            MyGroup.Delete(1);
            Console.WriteLine(MyGroup);

            try
            {
                MyGroup.Get(100);
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("IndexOutOfRangeException");
            }

            Console.WriteLine(MyGroup.Get(0));
            Console.WriteLine((Developer)MyGroup.Get(0) == new Developer("fivs", "firstBoss", 116, p, w1));
            Console.WriteLine("Press any key to continue . . .");
            Console.ReadKey();
        }

        public static void ChangingListener(ChangingType change)
        {
            switch (change)
            {
                case ChangingType.Add:
                    Console.WriteLine("Added Element");
                    break;
                case ChangingType.Delete:
                    Console.WriteLine("Deleted Element");
                    break;
                case ChangingType.Set:
                    Console.WriteLine("Setted Element");
                    break;
                case ChangingType.Insert:
                    Console.WriteLine("Inserted Element");
                    break;
            }
        }
    }


    public abstract class Worker
    {
        public string surname;
        public string bossSurname;
        public string[] duties;
        public Worker []subordinates;
        public int salary;
        public abstract string ListDuties();
        public abstract string ListOfSubordinates();

        public abstract override string ToString();
        public abstract override bool Equals(object obj);
        public abstract override int GetHashCode();
    }

    public class Manager : Worker
    {
        public Manager(string surname, string bossSurname, int salary, string[] duties, Worker[] subordinates)
        {
            base.surname = surname;
            base.bossSurname = bossSurname;
            base.subordinates = subordinates;
            base.duties = duties;
            base.salary = salary;
        }

        public Manager() { }

        public override string ListDuties()
        {
            var res = "";
            foreach (var r in base.duties)
                res += r + " ";
            return res;
        }

        public override string ListOfSubordinates()
        {
            var res = "";
            foreach (var r in base.subordinates)
            {
                res += " name: " + r.surname + " duties: " + r.ListDuties();
            }

            return res;
        }

        public static Manager operator +(Manager x, Manager y)
        {
            Manager tmp = new Manager();
            tmp.surname = x.surname;
            tmp.bossSurname = x.bossSurname;
            tmp.salary = x.salary + y.salary;

            string[] tm = new string[x.duties.Length + y.duties.Length];
            Array.Copy(x.duties, tm, x.duties.Length);
            Array.Copy(y.duties, tm, y.duties.Length);
            tmp.duties = tm;

            Worker[] tm1 = new Worker[x.subordinates.Length + y.subordinates.Length];
            Array.Copy(x.subordinates, tm1, x.subordinates.Length);
            Array.Copy(y.subordinates, tm1, y.subordinates.Length);
            tmp.subordinates = tm1;

            return tmp;
        }

        public override bool Equals(object x)
        {
            Manager obj = (Manager)x;
            if (this.surname == obj.surname && this.salary == obj.salary && this.bossSurname == obj.bossSurname) return true;
            else return false;
        }

        public static bool operator ==(Manager x, Manager y)
        {
            return Equals(x, y);
        }

        public static bool operator !=(Manager x, Manager y)
        {
            return !Equals(x, y);
        }

        public override int GetHashCode()
        {
            return surname.GetHashCode() + 13 * bossSurname.GetHashCode() + 169 * salary.GetHashCode();
        }

        public override string ToString()
        {
            return "Manager: (" + surname.ToString() + "," +
                   bossSurname.ToString() + "," + salary.ToString() + ")";
        }

    }

    public class Developer : Worker
    {

        public Developer(string surname, string bossSurname, int salary, string[] duties, Worker[] subordinates)
        {
            base.surname = surname;
            base.bossSurname = bossSurname;
            base.subordinates = subordinates;
            base.duties = duties;
            base.salary = salary;
        }

        public Developer() { }


        public override string ListDuties()
        {
            var res = "";
            foreach (var r in base.duties)
                res += r + " ";
            return res;
        }

        public override string ListOfSubordinates()
        {
            var res = "";
            foreach (var r in base.subordinates)
            {
                res += " name: " + r.surname + " duties: " + r.ListDuties();
            }

            return res;
        }

        public static Developer operator +(Developer x, Developer y)
        {
            Developer tmp = new Developer();
            tmp.surname = x.surname;
            tmp.bossSurname = x.bossSurname;
            tmp.salary = x.salary + y.salary;

            string[] tm = new string[x.duties.Length + y.duties.Length];
            Array.Copy(x.duties, tm, x.duties.Length);
            Array.Copy(y.duties, tm, y.duties.Length);
            tmp.duties = tm;

            Worker[] tm1 = new Worker[x.subordinates.Length + y.subordinates.Length];
            Array.Copy(x.subordinates, tm1, x.subordinates.Length);
            Array.Copy(y.subordinates, tm1, y.subordinates.Length);
            tmp.subordinates = tm1;

            return tmp;
        }

        public override bool Equals(object x)
        {
            Developer obj = (Developer)x;
            if (this.surname == obj.surname && this.salary == obj.salary && this.bossSurname == obj.bossSurname) return true;
            else return false;
        }

        public static bool operator ==(Developer x, Developer y)
        {
            return Equals(x, y);
        }

        public static bool operator !=(Developer x, Developer y)
        {
            return !Equals(x, y);
        }

        public override int GetHashCode()
        {
            return surname.GetHashCode() + 13 * bossSurname.GetHashCode() + 169 * salary.GetHashCode();
        }

        public override string ToString()
        {
            return "Developer: (" + surname.ToString() + "," +
                   bossSurname.ToString() + "," + salary.ToString() + ")";
        }
    }

    public class Coder : Worker
    {
        public Coder(string surname, string bossSurname, int salary, string[] duties, Worker[] subordinates)
        {
            base.surname = surname;
            base.bossSurname = bossSurname;
            base.subordinates = subordinates;
            base.duties = duties;
            base.salary = salary;
        }

        public Coder() { }


        public override string ListDuties()
        {
            var res = "";
            foreach (var r in base.duties)
                res += r + " ";
            return res;
        }

        public override string ListOfSubordinates()
        {
            var res = "";
            foreach (var r in base.subordinates)
            {
                res += " name: " + r.surname + " duties: " + r.ListDuties();
            }

            return res;
        }

        public static Coder operator +(Coder x, Coder y)
        {
            Coder tmp = new Coder();
            tmp.surname = x.surname;
            tmp.bossSurname = x.bossSurname;
            tmp.salary = x.salary + y.salary;

            string[] tm = new string[x.duties.Length + y.duties.Length];
            Array.Copy(x.duties, tm, x.duties.Length);
            Array.Copy(y.duties, tm, y.duties.Length);
            tmp.duties = tm;

            Worker[] tm1 = new Worker[x.subordinates.Length + y.subordinates.Length];
            Array.Copy(x.subordinates, tm1, x.subordinates.Length);
            Array.Copy(y.subordinates, tm1, y.subordinates.Length);
            tmp.subordinates = tm1;

            return tmp;
        }

        public override bool Equals(object x)
        {
            Coder obj = (Coder)x;
            if (this.surname == obj.surname && this.salary == obj.salary && this.bossSurname == obj.bossSurname) return true;
            else return false;
        }

        public static bool operator ==(Coder x, Coder y)
        {
            return Equals(x, y);
        }

        public static bool operator !=(Coder x, Coder y)
        {
            return !Equals(x, y);
        }

        public override int GetHashCode()
        {
            return surname.GetHashCode() + 13 * bossSurname.GetHashCode() + 169 * salary.GetHashCode();
        }

        public override string ToString()
        {
            return "Coder: (" + surname.ToString() + "," +
                   bossSurname.ToString() + "," + salary.ToString() + ")";
        }
    }

    public enum ChangingType { Add, Insert, Delete, Set }
    public delegate void ChangeHandler(ChangingType change);

    public class Group
    {
        public List<Worker> Elements
        {
            get;
        }

        public event ChangeHandler Changing;

        public Group()
        {
            Elements = new List<Worker>();
        }

        public void Add(Worker element)
        {
            Elements.Add(element);
            if (Changing != null) Changing(ChangingType.Add);
        }

        public Worker Get(int index)
        {
            if (index < 0 || index >= Elements.Count) throw new IndexOutOfRangeException();
            return Elements[index];
        }

        public void Set(int index, Worker element)
        {
            if (index < 0 || index >= Elements.Count) throw new IndexOutOfRangeException();
            Elements[index] = element;
            if (Changing != null) Changing(ChangingType.Set);
        }

        public void Insert(int index, Worker element)
        {
            if (index < 0 || index >= Elements.Count) throw new IndexOutOfRangeException();
            Elements.Insert(index, element);
            if (Changing != null) Changing(ChangingType.Insert);
        }

        public void Delete(int index)
        {
            if (index < 0 || index >= Elements.Count) throw new IndexOutOfRangeException();
            Elements.RemoveAt(index);
            if (Changing != null) Changing(ChangingType.Delete);
        }

        public Worker getBySurname(string surname)
        {
            foreach (Worker k in Elements)
            {
                if (k.surname == surname)
                {
                    return k;
                }
            }

            return null;
        }

        public override string ToString()
        {
            return "\nMy Group:\n" + string.Join("\n", Elements) + "\n";
        }
    }

    public class MyExeption : Exception
    {
        MyExeption(String str) : base("MyExeption: " + str) {}
    }
}
