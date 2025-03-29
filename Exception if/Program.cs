namespace Exception_String
{
    internal class Program
    {
        public static void cheak(string vioue, ref bool found)
        {
            string vowels1 = "aouie";
            for (int i = 0; i < vioue.Length; i++)
            {
                if (vowels1.Contains(vioue[i]))
                {
                    found = true;
                    throw new Exception($"{vioue} dosen't countains vowels");
                }
                else if (!found)
                {
                    found = false;
                    Console.WriteLine($"{vioue} countains vowels");
                    break;
                }

            }
        }
        static void Main(string[] args)
        {
            string vowels = " ";
            bool found = false;
            do
            {
                Console.WriteLine("search for vowels");
                vowels = Console.ReadLine().ToLower();
                try
                {
                    cheak(vowels, ref found);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{e.Message}");
                }
            } while (vowels != "X");
        }
    }
}
