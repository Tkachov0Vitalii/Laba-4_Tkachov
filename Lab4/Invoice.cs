using System;
using System.Collections.Generic;
using System.Text;

namespace Laba4
{
    [Serializable]
   public struct Invoice : IComparable<Invoice>
    {
        public string accountOfnumber;
        public string data;
        public string name;
        public int ContractOfnumber;
        public int Amount;
        public Invoice(string LineWithInvoiceData)
        {
            string[] dataSplitted = LineWithInvoiceData.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            accountOfnumber = dataSplitted[0];
            data = dataSplitted[1];
            name = dataSplitted[2];
            ContractOfnumber = int.Parse(dataSplitted[3]);
            Amount = int.Parse(dataSplitted[4]);
        }
        public int CompareTo(Invoice other)
        {

            return this.accountOfnumber.CompareTo(other.accountOfnumber);
        }
        public override string ToString()
        { return accountOfnumber + ", " + data + ", " + name + ", "+ ContractOfnumber+", "+ Amount; }
    }
}

