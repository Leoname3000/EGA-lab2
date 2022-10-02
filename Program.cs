using System.Text;
using EGA_lab1;

internal class Program
{
    private static void Main(string[] args)
    {
        const int L = 5;
        const BinaryString.MuMode MODE = BinaryString.MuMode.Rand;

        Console.WriteLine("------======= LANDSCAPE =======------\n");
        BinaryString.CreateLandscape(L, MODE, 32);       

        Console.WriteLine("\n------======= HILL CLIMBING METHOD (DEPTH-FIRST SEARCH) =======------\n");
        Console.WriteLine("Enter N: ");
        int N = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine();

        int i = 0;
        BinaryString maxS = new BinaryString(L, MODE);
        int max = maxS.GetMu();

        while (i < N)
        {
            List<BinaryString> omega = Omega(maxS);

            while (omega.Count > 0 && i < N)
            {
                string omegaString = $"Omega = {OmegaToString(omega)}\n";

                Random random = new Random();
                int randomIndex = random.Next(0, omega.Count);
                BinaryString S = omega[randomIndex];
                omega.RemoveAt(randomIndex);

                string reportString = $"Step {i + 1,BinaryString.COUNTER_FORMAT_WIDTH}: ";
                reportString += $"maxS = { maxS.StrVal}, max = { max, BinaryString.MU_FORMAT_WIDTH}, ";
                reportString += $"S = {S.StrVal}, mu = {S.GetMu(),BinaryString.MU_FORMAT_WIDTH} ";

                if (max < S.GetMu())
                {
                    maxS = S;
                    max = maxS.GetMu();
                    reportString += "<- CHANGE";
                    Console.WriteLine(reportString);
                    Console.WriteLine(omegaString);
                    break;
                }
                Console.WriteLine(reportString);
                Console.WriteLine(omegaString);
                i++;
            }

            if (omega.Count == 0)
            {
                break;
            }
        }

        Console.WriteLine($"Final result: {maxS.StrVal}, mu = {max}");


        List<BinaryString> Omega(BinaryString currentString)
        {
            List<BinaryString> omega = new List<BinaryString>();
            for (int j = 0; j < L; j++)
            {
                StringBuilder nearbyString = new StringBuilder(currentString.StrVal);
                if (nearbyString[j] == '0')
                {
                    nearbyString[j] = '1';
                }
                else if (nearbyString[j] == '1')
                {
                    nearbyString[j] = '0';
                }
                omega.Add(new BinaryString(L, MODE, Convert.ToString(nearbyString)!));
            }
            return omega;
        }

        string OmegaToString(List<BinaryString> omega)
        {
            string result = "{ ";
            if (omega.Count > 0)
                result += $"{omega[0].StrVal}";
            for (int i = 1; i < omega.Count; i++)
            {
                result += $", {omega[i].StrVal}";
            }
            result += " }";
            return result;
        }
    }
}