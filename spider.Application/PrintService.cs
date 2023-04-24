using GemBox.Spreadsheet;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using spider.AdvantageModels;
using spider.YandexApi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StranglerUtilityLib.Print
{

	public class YandexRouteListPrintForm
	{

		string path;
		private ExcelFile _book;
		private YandexRoutingResult _result;
		private readonly IEnumerable<Counterparty> _clients;
        private readonly string _dir = Directory.GetCurrentDirectory();
        public YandexRouteListPrintForm(YandexRoutingResult result, IEnumerable<Counterparty> clients)
		{
			_clients= clients;
			if (result is null) throw new Exception("Результат работы Я.АПИ пришел пустой");
			if (result.result is null) throw new Exception("Задача еще не обработана");
			_result = result;
			SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
			_book = new ExcelFile();
			_book.AutomaticFormulaUpdate = false;
		}

        private void MergeCells(ExcelWorksheet worksheet, string cells)
		{
			CellRange range = worksheet.Cells.GetSubrange(cells);
			range.Merged = true;
		}

		public void Proccesing()
		{
			_book.Worksheets.Add("0");
			ExcelWorksheet form = _book.Worksheets[0];
			FillingForm(form);
			ExcelPrintOptions sheetPrintOptions = form.PrintOptions;
			sheetPrintOptions.Portrait = false;
			sheetPrintOptions.FitToPage = true;
			sheetPrintOptions.PrintBlackWhite = true;
			sheetPrintOptions.PrintHeadings = false;
			sheetPrintOptions.PrintGridlines = false;
		}

		public void FillingForm(ExcelWorksheet worksheet)
		{
			var res = _result.result;
			for (int i = 0; i < res.vehicles.Count; i++)
			{
                int j = i;
                worksheet.Rows[i+j+1].AllocatedCells[0].SetValue(res.vehicles[i].id);
				worksheet.Rows[i + j].AllocatedCells[0].SetValue(res.vehicles[i].@ref);
                worksheet.Rows[i + j + 2].AllocatedCells[0].SetValue(res.vehicles[i].capacity.weight_kg + " кг");
                //worksheet.Rows[i + j + 3].AllocatedCells[0].SetValue(res.vehicles[i].cost.hour+" час");
                //worksheet.Rows[i + j + 4].AllocatedCells[0].SetValue(res.vehicles[i].cost.km + " км");
                foreach (var item in res.routes[i].route)
				{
					if (item.node.value.id == "1") continue;
                    var client = _clients.Where(x => x.codeFromBase == item.node.value.id).FirstOrDefault();
                    worksheet.Rows[i + j].AllocatedCells[2].SetValue(client.name); 
                    if (worksheet.Rows.Count > 120) break;
                    j++;
                }
                if (worksheet.Rows.Count > 120) break;
            }
			/*
			summaryRow.Cells["I"].Style.Borders.SetBorders(MultipleBorders.Bottom, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Double);
			summaryRow.Cells["J"].SetValue(packs);
			summaryRow.Cells["J"].Style.Borders.SetBorders(MultipleBorders.Bottom, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Double);
			worksheet.Rows[50].AllocatedCells[3].SetValue(ves);
			*/
		}
		public void RealPrint()
		{
			throw new NotImplementedException();
			/*_book.Save(
                path,
                new PdfSaveOptions() 
                { 
                    SelectionType = SelectionType.ActiveSheet 
                });
			_book.Print(
                null,
                new PrintOptions()
                {
                    SelectionType = SelectionType.ActiveSheet,
                });
			*/
		}
		public byte[] VirtualPrint()
		{
			Proccesing();
			path = Path.Combine(_dir,$"{_result.id}.pdf");
			_book.Save(
                path,
                new PdfSaveOptions() 
                { 
                    SelectionType = SelectionType.ActiveSheet 
                });
			byte[] res = File.ReadAllBytes(path);
			return res;
		}
	}
}
