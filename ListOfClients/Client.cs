using System;
using System.Collections.Generic;
using System.Text;

namespace ListOfClients
{
    class Client
    {
        public string FullName { get; set; }
        public int Money { get; set; }
        public string PassportNumber { get; set; }

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is Client))
                return false;

            Client c = (Client)obj;
            //По логике, номер паспорта - это уникальный идентификатор,
            return PassportNumber == c.PassportNumber;

        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FullName, Money, PassportNumber);
        }
    }
}
