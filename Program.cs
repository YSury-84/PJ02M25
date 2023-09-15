using PJ02M25.resourses;

namespace PJ02M25
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            var bl = new BusinessLogic();
            int i = -1;
            while (i!=0)
                {
                    i = bl.StartMenu();
                    bl.Execute(i);
                }
        }
    }
}