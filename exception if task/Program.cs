using System.ComponentModel.Design;
using System.Net.NetworkInformation;
using System.Threading.Channels;

namespace exception_if_task
{
    internal class Program
    {
        static void addNum(List<int> exceptions, int num)
        {
            if (!exceptions.Contains(num))
            {
                exceptions.Add(num);
            }
            else if (exceptions.Contains(num))
            {
                throw new Exception($"this {num} is aready in the list");
            }
        }
        static void Main(string[] args)
        {
            List<int> exceptions = new List<int>();
            Console.WriteLine("if u need to Exit the loop = X ");
            for (int i = 0; i != 'X'; i++)
            {
                try
                {
                    Console.WriteLine("Right Number to add ");
                    int num = Convert.ToInt32(Console.ReadLine());
                    addNum(exceptions, num);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error : {e.Message}");
                }
            }

        }
    }
}
