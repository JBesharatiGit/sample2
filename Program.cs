using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LottoRader
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleKeyInfo k=new ConsoleKeyInfo();
            
            LottoPerson LottoRad;// = new LottoPerson();
            DicSave dataView = new DicSave();
            string name = "";
            int counter = 100;

            while (name != "\r")
            {
                LottoRad = new LottoPerson(7);
                name = "";
                
                Console.Write("Enter name and Family: ");
                //Läser Namn bokstav efter bokstav och
                //kontrollerar om användaren skickar tom linje eller ej
                do
                {
                    k = Console.ReadKey();
                    name += (char)k.KeyChar;

                } while ((int)k.KeyChar != 13);

                if (name != "\r")
                {
                    name = name.Substring(0, name.Length - 1);
                    name = (name.Length > 15) ? name.Substring(0, 15) : name;
                    LottoRad.Name = name;
                    Console.WriteLine();
                    for (int i = 0; i < LottoRad.ArrLottoNo.Length; i++)
                    {
                        Console.Write($"Enter Nr{i+1} : ");
                        LottoRad.ArrLottoNo[i] = (int)LottoRad.GetValidValue();
                    }
                    LottoRad.Id = counter;

                    LottoRad.sort(7);
                    dataView.MyLottList.Add(LottoRad.Id, LottoRad);
                    counter++;
                    Console.Clear();
                }

            }

            //Tar fram slumpmäsigt lottorad
            Random randomValue = new Random();
            
            LottoPerson LPAdmin = new LottoPerson(11);

            LPAdmin.Name = "RättRad";
            for (int i = 0; i < LPAdmin.ArrLottoNo.Length; i++)
            {
                int val = randomValue.Next(1, 101);
                if (LPAdmin.ArrLottoNo.Contains(val))
                {
                    i--;
                }
                else                     
                    LPAdmin.ArrLottoNo[i] = val;
               
            }

            LPAdmin.sort(11);
            //lägger varje lottorad till list
            

            //dataView.MyLottList.Select(dataView.MyLottList.ContainsKey==99);
            int[] right = new int[7];
            int[] supplment = new int[4];
            for (int i = 0; i < 7; i++)
            {
                right[i] = LPAdmin.ArrLottoNo[i];
            }
            for (int i = 0; i < 4; i++)
            {
                supplment[i] = LPAdmin.ArrLottoNo[i+7];
            }

           
            //
            foreach (KeyValuePair<int,LottoPerson> item in dataView.MyLottList)
            {
                int countRight = 0;
                int countSupplment = 0;
                for (int i = 0; i < 7; i++)
                {
                    if (right.Contains(item.Value.ArrLottoNo[i]))
                        countRight += 1;
                    else
                         if (supplment.Contains(item.Value.ArrLottoNo[i]))
                        countSupplment += 1;
                }
                item.Value.RightNo = countRight;
                item.Value.SupplementNo = countSupplment;                               
            }
            dataView.MyLottList.Add(99, LPAdmin);

            //sparar till filen
            dataView.SaveTofile();

        }

    }
}
