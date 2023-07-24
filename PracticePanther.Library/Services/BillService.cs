using PracticePanther.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticePanther.Library.Services
{
    public class BillService
    {
        // List of bills
        public List<Bill> Bills
        {
            get
            {
                return bills;
            }
        }
        private List<Bill> bills;

        private static BillService? instance;

        // Singleton instance of BillService
        public static BillService Current
        {
            get
            {
                if (instance == null)
                {
                    instance = new BillService();
                }
                return instance;
            }
        }

        // Returns a bill by id
        public Bill? Get(int id)
        {
            return Bills.FirstOrDefault(b => b.Id == id);
        }

        private BillService()
        {
            bills = new List<Bill> 
    {
        new Bill { Id = 1, ProjectId = 1, TotalAmount = 100, DueDate = DateTime.Today.AddDays(7) }
    };
        }

        private int lastBillId = 1;

        // Method to generate a new unique bill ID
        private int GenerateNewBillId()
        {
            lastBillId++;
            return lastBillId;
        }

        public void AddBill(Bill b)
        {
            b.Id = GenerateNewBillId();
            Bills.Add (b);
        }
    }
}
