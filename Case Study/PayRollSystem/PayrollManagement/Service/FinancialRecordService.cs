using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayrollManagement.Exceptions;
using PayrollManagement.model;
using PayrollManagement.Repository;

namespace PayrollManagement.Service
{
    internal class FinancialRecordService : IFinancialRecordService
    {
        readonly IFinancialRecordServiceRepository _financialRecordServiceRepository;
        public FinancialRecordService()
        {
            _financialRecordServiceRepository = new FinancialRecordServiceRepository();
        }
        public void GetFinancialRecordById()
        {
            Console.WriteLine("Enter the RecordID:");
            int recordId = int.Parse(Console.ReadLine());
            List<FinancialRecord> FinancialRecordlist = _financialRecordServiceRepository.GetFinancialRecordById(recordId);
            foreach (FinancialRecord record in FinancialRecordlist)
            {
                Console.WriteLine(record);
            }
        }
        public void GetFinancialRecordsForEmployee()
        {
            Console.WriteLine("Enter  EmployeeID::");
            int EmployeeID = int.Parse(Console.ReadLine());
            List<FinancialRecord> FinancialRecordlist = _financialRecordServiceRepository.GetFinancialRecordsForEmployee(EmployeeID);
            foreach (FinancialRecord record in FinancialRecordlist)
            {
                Console.WriteLine(record);
            }
        }

        public void GetFinancialRecordsForDate()
        {
            Console.WriteLine("Enter  RecordDate::");
            DateTime Recorddate = DateTime.Parse(Console.ReadLine());
            List<FinancialRecord> FinancialRecordlist = _financialRecordServiceRepository.GetFinancialRecordsForDate(Recorddate);
            foreach (FinancialRecord record in FinancialRecordlist)
            {
                Console.WriteLine(record);
            }
        }
        public void AddFinancialRecord()
        {
            try
            {
                FinancialRecord financialRecord = new FinancialRecord();
                
                Console.WriteLine("Enter EmployeeID:");
                financialRecord.EmployeeID = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter RecordDate (yyyy-MM-dd):");
                financialRecord.RecordDate = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Enter Description:");
                financialRecord.Description = Console.ReadLine();
                Console.WriteLine("Enter Amount:");
                financialRecord.Amount = decimal.Parse(Console.ReadLine());
                if (financialRecord.Amount < 0)
                {
                    throw new InvalidInputException("Enter a valid Amount");
                }
                Console.WriteLine("Enter RecordType:");
                financialRecord.RecordType = Console.ReadLine();
                int AddStatus = _financialRecordServiceRepository.AddFinancialRecord(financialRecord);
                if (AddStatus > 0)
                {
                    Console.WriteLine("FinancialRecord Added Successfully");
                }
                else
                {
                    throw new FinancialRecordException("Record Already Exists");
                }
            }
            catch (FinancialRecordException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (InvalidInputException ex)
            {
                Console.WriteLine("Wrong Format" + ex.Message);
            }
        }
    }
}
