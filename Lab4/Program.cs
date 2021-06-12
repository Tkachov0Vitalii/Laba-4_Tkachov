using System;
using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;


namespace Laba4
{
    class MainClass
    {
        static void Main(string[] args)
        {
            List<Invoice> invoice = new List<Invoice>();
            Console.Write("Оберіть дію:" +
                 "\n1 - Записати нові дані у файли." +
                 "\n2 - Вивести збережені дані з файлів." +
                 "\nВведіть ваш вибір: ");
            int ReadChar = int.Parse(Console.ReadLine());

            if (ReadChar == 1)
            {
                Console.WriteLine("Введіть дані за зразком\n" +
                    "207, 25.01.2016, ALADIN, 54, 10000");
                invoice = Input();
                SaveData(invoice);
                Console.WriteLine("\nДані забережено");
            }
            else if (ReadChar == 2)
            {
                Console.Write("Оберіть тип файлу:" +
                "\n1 - TXT." +
                "\n2 - XML." +
                "\nВведіть ваш вибір: ");
                int TXTorXML = int.Parse(Console.ReadLine());

                if (TXTorXML == 1)
                {
                    invoice = ReadTXT();
                }
                else if (TXTorXML == 2)
                {
                    invoice = ReadXML();
                }
          
                SaveData(invoice);
                
            }
            invoice.Sort();
            for (int i = 0; i < invoice.Count; i++)
                OutputData(invoice[i]);
            Console.WriteLine("Введіть номер контракту який вас цікавить");
                    int account = int.Parse(Console.ReadLine());

            for (int i = 0; i < invoice.Count; i++)
                if (account == invoice[i].ContractOfnumber)
                {
                    OutputData(invoice[i]);
                }
        }
        public static List<Invoice> Input()
        {
            List<Invoice> data = new List<Invoice>();
            string input = "";


            while (true)
            {
                Console.Write("Введіть дані: ");
                input = Console.ReadLine();
                if (input == "")
                    break;
                data.Add(new Invoice(input));
            }

            return data;
        }
        public static void SaveData(List<Invoice> invoice)
        {
            using (StreamWriter save = new StreamWriter("Invoice_TXT.txt"))
            {
                foreach (Invoice data in invoice)
                    save.WriteLine(data);
            }

            XmlSerializer formatter = new XmlSerializer(typeof(List<Invoice>));

            using FileStream fs = new FileStream("Invoice_XML.xml", FileMode.Create);
            formatter.Serialize(fs, invoice);
        }
        public static List<Invoice> ReadTXT()
        {
            List<Invoice> invoice = new List<Invoice>();
            string[] dataFromFile = File.ReadAllLines("Invoice_TXT.txt");
            for (int i = 0; i < dataFromFile.Length; i++)
                invoice.Add(new Invoice(dataFromFile[i]));

            return invoice;
        }

        public static List<Invoice> ReadXML()
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<Invoice>));
            List<Invoice> invoice = new List<Invoice>();

            using (FileStream fs = new FileStream("Invoice_XML.xml", FileMode.OpenOrCreate))
            {
                invoice = (List<Invoice>)formatter.Deserialize(fs);
            }

            return invoice;
        }
        public static string MoneyFormatter(int Amount)
        {
            int cop = Amount % 100;
            int hrn = (Amount - cop) / 100;
            string HrnCop = hrn + " грн. " + cop + " коп.";
            return HrnCop;
        }
        public static void OutputData(Invoice invoice)
        {
                    Console.WriteLine("Номер цього рахунку {0}\n" +
                        "Дата оформлення цього рахунку {1}\n" +
                        "Назва контрагента {2}\n" +
                        "Номер контракту {3}\n" +
                        "Сума до оплати {4}", invoice.accountOfnumber, invoice.data, invoice.name, invoice.ContractOfnumber, MoneyFormatter(invoice.Amount));
        }
    }
}


