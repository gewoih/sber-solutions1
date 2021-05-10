using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1
{
	class Worker
	{
		public DateTime StartOfWork { get; } = DateTime.Now;

		public Worker(string startDate)
		{
			this.StartOfWork = DateTime.Parse(startDate);
		}
		private static UInt16 CalculateBusinessDays(DateTime startDate, DateTime endDate, IEnumerable<DateTime> missingDays)
		{
			UInt16 calcBusinessDays = (UInt16)(1 + ((endDate - startDate).TotalDays * 5 -
									(startDate.DayOfWeek - endDate.DayOfWeek) * 2) / 7);

			if (endDate.DayOfWeek == DayOfWeek.Saturday)
				calcBusinessDays--;
			if (startDate.DayOfWeek == DayOfWeek.Sunday)
				calcBusinessDays--;
			calcBusinessDays -= (UInt16)missingDays.Count(day => day > startDate && day < endDate);

			return (calcBusinessDays);
		}
		private (UInt16, UInt16) CalculateDays(DateTime billingDate, IEnumerable<DateTime> missingDays)
		{
			DateTime regularBound = new DateTime(billingDate.Year, billingDate.Month, 19);
			DateTime increasedBound = new DateTime(billingDate.Year, billingDate.Month, DateTime.DaysInMonth(billingDate.Year, billingDate.Month));
			UInt16 regularDays = 0, increasedDays;

			if (billingDate.Month == StartOfWork.Month && billingDate.Year == StartOfWork.Year)
				regularDays = CalculateBusinessDays(StartOfWork, regularBound, missingDays);
			else if (billingDate.Month > StartOfWork.Month || billingDate.Year > StartOfWork.Year)
				regularDays = CalculateBusinessDays(new DateTime(billingDate.Year, billingDate.Month, 1), regularBound, missingDays);
			else
				throw new Exception("В этом месяце сотрудник еще не был устроен на работу.");

			increasedDays = CalculateBusinessDays(regularBound.AddDays(1), increasedBound, missingDays);

			return (regularDays, increasedDays);
		}
		public UInt16 CalculateFoodMoney(DateTime billingDate, List<DateTime> missingDays)
		{
			UInt16 money, regularDays, increasedDays;

			billingDate = billingDate.AddDays(DateTime.DaysInMonth(billingDate.Year, billingDate.Month) - 1);
			(regularDays, increasedDays) = CalculateDays(billingDate, missingDays);

			money = (UInt16)((regularDays * 200) + (increasedDays * 300));
			return (money);
		}
	}
}
