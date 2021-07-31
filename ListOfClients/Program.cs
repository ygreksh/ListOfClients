using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ListOfClients
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Client> Clients = new List<Client>();
            string FullName;
            string PassportNumber;
            int Money = 0;
            Client newClient;
            Random random = new Random();

            /*
            //Ввод новых клиентов вручную
            for (int i=0; i<10; )
            {
                Console.WriteLine("Введите номер паспорта:");
                PassportNumber = Console.ReadLine();
                Console.WriteLine("Введите полное имя:"); 
                FullName = Console.ReadLine();

                Console.WriteLine("Введите сумму:");
                try
                {
                    Money = Int32.Parse(Console.ReadLine());
                }
                catch (Exception)
                {

                    Console.WriteLine("Неправильно указана сумма. Введите только целое число");
                }

                newClient = new Client { PassportNumber = PassportNumber, FullName = FullName, Money = Money };
                if (!Clients.Contains(newClient))
                {
                    Clients.Add(newClient);
                    i++;
                }
                    
                else Console.WriteLine("Клиент с таким номером пасспорта уже существует!");
            }
            */

            //Генерация клиентов случайным образом
            for (int i = 0; i < 10; )
            {
                FullName = Path.GetRandomFileName().Replace(".", ""); 
                PassportNumber = Convert.ToString(random.Next(10000));
                Money = random.Next(100000);
                newClient = new Client { PassportNumber = PassportNumber, FullName = FullName, Money = Money };
                if (!Clients.Contains(newClient))
                {
                    Clients.Add(newClient);
                    i++;
                }
            }

            Clients.Add(new Client {PassportNumber = "123", FullName = "Искомый клиент", Money = 6666 });

            for (int i = 0; i < 5;)
            {
                FullName = Path.GetRandomFileName().Replace(".", "");
                PassportNumber = Convert.ToString(random.Next(10000));
                Money = random.Next(100000);
                newClient = new Client { PassportNumber = PassportNumber, FullName = FullName, Money = Money };
                if (!Clients.Contains(newClient))
                {
                    Clients.Add(newClient);
                    i++;
                }
            }


            foreach (var client in Clients)
            {
                Console.WriteLine(client.PassportNumber + ", " + client.FullName + ", " + client.Money);
            }

            Console.WriteLine("________________________");

            Client c;
            
            //поиск клиента по номеру паспорта (точное значение, возвращается один клиент)
            Console.WriteLine("Ищем клиента с номером 123");
            c = FindClientByPassportNumber("123", Clients);
            Console.WriteLine(c.PassportNumber + ", " + c.FullName);

            //поиск клиентов по имени (вхождение подстроки, возвращается список клиентов)
            Console.WriteLine("Ищем клиентов где в имени есть \"w\"");
            List<Client> cc= FindClientsByName("w", Clients);
            foreach (var item in cc)
            {
                Console.WriteLine(item.PassportNumber + ", " + item.FullName);
            }

            //поиск клиентов с суммой на счету менее чем заданное значение
            int money = 33333;
            Console.WriteLine("Ищем клиентов с суммой на счету менее " + money);
            cc = FindClientsMoneyLess(money, Clients);
            foreach (var item in cc)
            {
                Console.WriteLine(item.PassportNumber + ", " + item.FullName + ", " + item.Money);
            }

            //поиск клиента с минимальной суммой
            Console.WriteLine("Ищем клиентов с минимальной суммой на счету");
            cc = FindClientsMinMoney(Clients);
            foreach (var item in cc)
            {
                Console.WriteLine(item.PassportNumber + ", " + item.FullName + ", " + item.Money);
            }


            //Сумма всех денег
            int TotalSum;
            TotalSum = TotalClientsSum(Clients);
            Console.WriteLine("Всего денег: " + TotalSum);
            
        }

        //поиск клиента по номеру паспорта (точное значение, возвращается один клиент)
        public static Client FindClientByPassportNumber(string PassportNumber, List<Client> clients)
        {
            var query2 = from client in clients
                         where client.PassportNumber == PassportNumber
                         select client;

            return query2.First();
        }

        //поиск клиентов по имени (вхождение подстроки, возвращается список клиентов)
        public static List<Client> FindClientsByName(string Name, List<Client> clients)
        {
            var query = from client in clients
                         where client.FullName.Contains(Name)
                         select client;

            return query.ToList();
        }

        //поиск клиентов с суммой на счету менее чем заданное значение
        public static List<Client> FindClientsMoneyLess(int Money, List<Client> clients)
        {
            var query = from client in clients
                        where client.Money < Money
                        select client;

            return query.ToList();
        }

        //поиск клиентов с минимальной суммой на счету
        public static List<Client> FindClientsMinMoney(List<Client> clients)
        {
            int minmoney;
            var query1 = from client in clients
                        select client.Money;
            minmoney = query1.Min();

            var query2 = from client in clients
                         where client.Money == minmoney
                         select client;

            return query2.ToList();
        }

        //Сумма денег всех клиентов
        public static int TotalClientsSum(List<Client> clients)
        {

            var query = from client in clients
                        select client;

            return clients.Sum(t=>t.Money);
        }

    }
}
