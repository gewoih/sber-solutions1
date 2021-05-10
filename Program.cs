using System;
using System.Collections.Generic;

namespace Task_1
{
	class Program
	{
		static void Main(string[] args)
		{
			string start_of_work, billing_date;
			List<DateTime> missing_days = new List<DateTime>();
			UInt16 N;

			Console.Write("Введите дату трудоустройства: ");
			start_of_work = Console.ReadLine();

			Worker _worker = new Worker(start_of_work);

			Console.Write("Введите месяц и год для рассчета компенсации за питание: ");
			billing_date = Console.ReadLine();

			Console.Write("Количество рабочих дней, пропущенных сотрудником: ");
			N = Convert.ToUInt16(Console.ReadLine());
			if (N > 0)
			{
				Console.WriteLine("Введите эти дни:");
				for (UInt16 i = 0; i < N; i++)
				{
					DateTime date = DateTime.Parse(Console.ReadLine());
					if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
						missing_days.Add(date);
				}
			}

			Console.Write("Компенсация для сотрудника составляет " + _worker.CalculateFoodMoney(DateTime.Parse(billing_date), missing_days) + "р.");
		}
	}
}
