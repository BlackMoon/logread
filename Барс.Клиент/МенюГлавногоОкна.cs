using System;
using System.Collections.Generic;
using System.Collections;
using System.Xml;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using System.IO;
using System.Drawing;
using DevExpress.XtraTreeList.Nodes;

namespace Барс.Клиент
{

	class МенюГлавногоОкна
	{
		private RibbonControl ПанельМеню;
		private Dictionary<Guid, ИсполняемыйПунктМеню> НаборИсполняемыхПунктовМеню;

		private ImageList маленькиеИконки = new ImageList();
		private ImageList большиеИконки = new ImageList();
		
		private ImageList imageList_ДеревоМеню = new ImageList();
		public ImageList ImageList_ДеревоМеню
		{
			get { return imageList_ДеревоМеню; }
			set { imageList_ДеревоМеню = value; }
		}

		private Dictionary<string, int> именаВсехКартинок = new Dictionary<string, int>();

		private List<ЭлементВыпадающегоМеню> списокЭлементовВыпадающегоМеню = new List<ЭлементВыпадающегоМеню>();

		public List<ЭлементВыпадающегоМеню> СписокЭлементовВыпадающегоМеню
		{
			get { return списокЭлементовВыпадающегоМеню; }
			set { списокЭлементовВыпадающегоМеню = value; }
		}
		
		private List<ЭлементыМеню> списокЭлементовМеню = new List<ЭлементыМеню>();

		public List<ЭлементыМеню> СписокЭлементовМеню
		{
			get { return списокЭлементовМеню; }
			set { списокЭлементовМеню = value; }
		}

		private СловарьНазваний словарьНазванийЭлементов = new СловарьНазваний();

		public СловарьНазваний СловарьНазванийЭлементов
		{
			get { return словарьНазванийЭлементов; }
			set { словарьНазванийЭлементов = value; }
		}

		private string БазаИконок = string.Empty;


		public МенюГлавногоОкна(RibbonControl панельМеню)
		{
			БазаИконок = "БарсРесурсы.dll";

			ПанельМеню = панельМеню;
			НаборИсполняемыхПунктовМеню = new Dictionary<Guid, ИсполняемыйПунктМеню>();

			try
			{
				Type type = Type.GetType("Барс.Интерфейс.ГрафическиеРесурсы,Ядро");
				List<string> всеКартинкиВРесурсах = (List<string>)type.InvokeMember("ПолучитьИменаКартинок", BindingFlags.InvokeMethod, null, null, new object[] { "Барс.Ресурсы", БазаИконок });

				int счетчикМаленькихИконок = -1, счетчикБольшихИконок = -1;

				большиеИконки.ImageSize = new Size(32, 32);
				большиеИконки.ColorDepth = ColorDepth.Depth16Bit;
				маленькиеИконки.ImageSize = new Size(16, 16);
				маленькиеИконки.ColorDepth = ColorDepth.Depth16Bit;
				imageList_ДеревоМеню.ImageSize = new Size(16, 16);
				imageList_ДеревоМеню.ColorDepth = ColorDepth.Depth32Bit;

				foreach (string имяКартинки in всеКартинкиВРесурсах)
				{
					Image текущаяКартинка = (Image)type.InvokeMember("ПолучитьКартинку", BindingFlags.InvokeMethod, null, null, new object[] { имяКартинки, "Барс.Ресурсы", БазаИконок });
					if (имяКартинки.LastIndexOf("_16") != -1)
					{
						маленькиеИконки.Images.Add(текущаяКартинка);
						счетчикМаленькихИконок++;
						именаВсехКартинок.Add(имяКартинки, счетчикМаленькихИконок);
					}
					else if (имяКартинки.LastIndexOf("_32") != -1)
					{
						большиеИконки.Images.Add(текущаяКартинка);
						счетчикБольшихИконок++;
						именаВсехКартинок.Add(имяКартинки, счетчикБольшихИконок);
					}
				}
				imageList_ДеревоМеню.Images.Add("Пусто", System.Drawing.Image.FromStream(new System.IO.MemoryStream(Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAABVJREFUOE9jYBgFoyEwGgKjIQAJAQAEEAABcqUTdgAAAABJRU5ErkJggg=="))));
				imageList_ДеревоМеню.Images.Add("Меню-приложение", Image.FromStream( new MemoryStream(Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAm9JREFUOE+tketLk1Ecx5+/oZtWVkZS0T3wRb0IRGJIFzGJXvSiICRiTJ0NdnPq5uYya6hliVFWiiUWJF0UTSsSL2gK3dQ55+a8zXnb5tQ9G+f5dp4j+EYSob7wgcNzvr/POTyH4/5HSjoDUDTzuNnEQ/6RR1ojD1kDD2k9jxv1QVyvCyLlQxDX3gVx9e0SrtQuwfDFD3Z2YXsAlT/D6J8lGA8IGKOMzAtwUYb9ApwUh0/AEMVOGfQK6HITWDqCUDV6waW8D+LHFPlr2UYHrHMC+il9swJ6Kb9nBLSMEkgq6C0uv1lk5kuvF3Dx1QKSawJIqp5H4st5nH/hx5kqPxIq/ZBU+nD6uQ/xz3yIK/eixyPg1OM5cEnVAWY+R8vrzYmyGbRPEMSWToOTVPjwfVpA/FMvmzebzTCZTDAajTAYDNDr9au8x0s8+DpGcPTeJDjxGl2TBCepda2o1eqV7cNFbjSNEBwsnAAX+3AKreMEx+57WEGn00Gr1UKj0UClUkGpVLLvCoUCGRkZbL3/7hjqnAT77oyCO1Lsxmf6Rw9R61pJTU1d2Y657ULNAMGeW8PgDljG0egizCpGLpcjPT0d4oBMJoNUKl3ljc5zovxXGLtMDnB7C0ZQayeIyXet9xEQZbCjuCeM7fpBcLvNTpR084jOczDjTuMQY0fuEKJy7awsFreJ5NiwNduGyOwBqD4tIiJrANyFJw4kV3mQ17bErEXdYVi+hVHQGUY+xdwRgrE9BENbCDmtIehaQkhrWERc2QTOllKBmMRHdmaNpMaILCu26CiZVmzO7McmrUgfNmqW2aDuZSQ8sC4P/2v+AJU9w+QNtCLQAAAAAElFTkSuQmCC"))));
				imageList_ДеревоМеню.Images.Add("Раздел", Image.FromStream( new MemoryStream(Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAbZJREFUOE+1k9tLG1EQxv1jfPI57/4Bvkt90GCFFi9BQU1tMNiaaEBrK6Ugai2idVEhsV6Szc0LeVeUPorSQqHWbHf3nJNiBMnXmYi7oASjtAs/5pyZb74z87A1Nf/iy+VyqERV/txsmiby+byDYRgVTW8ec8wTiSSuigrFglXmQl1HYf6Clf95B2WdI6HrcAy0pZUHG2jasmvwbEnAt2qhN2YjsCkxpEuEUgqjGYVItkAohNMKr5MSg1sCAxsC3ONM4J238GLNRGDDwrBuI5IWeLMt8XZXYnJP4d2exMSOxFhWYCQlEE4KeOdN16BxxoBv2YR/zaYXbLxKCIRINJqWiGRkOY6kaCrKDcXtsq5x1nANGt6f4cncb7R9ttATtfFyk4S0xnBSlVfh0YNxib4vNLpmoemTCe5xVqgf/4FW2qll0YaXaNMEnq8ItK9ew2fOcb15wcZTOnOPY+AJfUP3egFdsQI6mahCxy04x3WmZ/0PPKHvrkFd8ASD2Uv06xf34idNkLR1wVPXoHbgGHNfgamDEqYPS5hljkr4SHCcoTvnuf5hn3Kk5R5nBb48hqr+k/8u+gs49inQgJpNnwAAAABJRU5ErkJggg=="))));
               
				панельМеню.Images = маленькиеИконки;
				панельМеню.LargeImages = большиеИконки;
			}
			catch (Exception)
			{
				маленькиеИконки.Images.Clear();
				большиеИконки.Images.Clear();

				панельМеню.Images = маленькиеИконки;
				панельМеню.LargeImages = большиеИконки;

				именаВсехКартинок.Clear();
			}
		}
		
		public void ЗаполнитьПункт(XmlNode узел, ЭлементыМеню Родитель, string Путь, ref bool НоваяГруппа, bool ДляВыпадающегоМеню, ref BarButtonItem Пункт)
		{
			Пункт = ПанельМеню.Items.CreateButton(узел.Attributes["Наименование"].InnerText.Trim());

			string key = Путь + "." + узел.Attributes["Наименование"].InnerText.Trim();

			// Загрузка картинки
			bool битоваяИконка = false;

			if (узел.Attributes["БитоваяИконка"] != null)
			{
				try
				{
					string иконка = узел.Attributes["БитоваяИконка"].Value;
					if (иконка != string.Empty)
					{
						byte[] биты = Convert.FromBase64String(иконка);
						MemoryStream stream = new MemoryStream();
						stream.Write(биты, 0, биты.Length);

						if (Image.FromStream(stream).Height <= 16)
							Пункт.Glyph = Image.FromStream(stream);
						else
							Пункт.LargeGlyph = Image.FromStream(stream);

						imageList_ДеревоМеню.Images.Add(key, Image.FromStream(stream));

						битоваяИконка = true;
					}
				}
				catch
				{
					битоваяИконка = false;
				}
			}

			if (!битоваяИконка)
			{
				if (узел.Attributes["Иконка"] != null)
				{
					string имяИконки = узел.Attributes["Иконка"].Value + "_16";
					if (именаВсехКартинок.ContainsKey(имяИконки))
					{
						Пункт.LargeImageIndex = именаВсехКартинок[имяИконки];
						imageList_ДеревоМеню.Images.Add(key, маленькиеИконки.Images[именаВсехКартинок[имяИконки]]);
					}

					имяИконки = узел.Attributes["Иконка"].Value + "_32";
					if (именаВсехКартинок.ContainsKey(имяИконки))
					{
						Пункт.ImageIndex = именаВсехКартинок[имяИконки];
					}
				}
			}

			ИсполняемыйПунктМеню исполняемыйПунктМеню = new ИсполняемыйПунктМеню();
			исполняемыйПунктМеню.КвалифицирующееИмяФормы = узел.InnerText.Trim();
			исполняемыйПунктМеню.НаименованиеМодуля = узел.Attributes["Наименование"].InnerText.Trim();

			Guid идентификатор = Guid.NewGuid();
			Пункт.Tag = идентификатор;
			НаборИсполняемыхПунктовМеню.Add(идентификатор, исполняемыйПунктМеню);

			Пункт.ItemClick += new ItemClickEventHandler(ИсполнитьПунктМеню);

			ЭлементыМеню узелДерева = new ЭлементыМеню(узел.Attributes["Наименование"].InnerText.Trim(), false, Путь, Родитель, Пункт, imageList_ДеревоМеню.Images.IndexOfKey(key));

			СписокЭлементовМеню.Add(узелДерева);

			if (!string.IsNullOrEmpty(узел.Attributes["ВсплывающаяПодсказка"].InnerText.Trim()))
			{
				Пункт.Description = узел.Attributes["ВсплывающаяПодсказка"].InnerText.Trim();
				Пункт.Hint = узел.Attributes["ВсплывающаяПодсказка"].InnerText.Trim();
			}
			else
			{
				Пункт.Hint = узел.Attributes["Наименование"].InnerText.Trim();
			}

			if (ДляВыпадающегоМеню)
			{
				ЭлементВыпадающегоМеню элемент = new ЭлементВыпадающегоМеню(Пункт, НоваяГруппа);

				if (НоваяГруппа)
					НоваяГруппа = false;

				СписокЭлементовВыпадающегоМеню.Add(элемент);
			}

			if (!СловарьНазванийЭлементов.ContainsKey(узелДерева.Название))
			{
				СловарьЭлементов сз = new СловарьЭлементов(узелДерева.Название);

				сз.Add(узелДерева.Путь, узелДерева);

				СловарьНазванийЭлементов.Add(узелДерева.Название, сз);
			}
			else
			{
				if (!СловарьНазванийЭлементов[узелДерева.Название].ContainsKey(узелДерева.Путь))
				{
					СловарьНазванийЭлементов[узелДерева.Название].Add(узелДерева.Путь, узелДерева);
				}
			}
		}

		public void ЗаполнитьМеню(XmlNode узел, ЭлементыМеню Родитель, string Путь, ref bool НоваяГруппа, bool ДляВыпадающегоМеню, ref BarSubItem Пункт)
		{
			//Создаем объект меню
			Пункт = ПанельМеню.Items.CreateMenu(узел.Attributes["Наименование"].InnerText.Trim(), new BarItem[] { });

			string key = Путь + "." + узел.Attributes["Наименование"].InnerText.Trim();

			//Загружаю иконку
			bool битоваяИконка = false;
			
			if (узел.Attributes["БитоваяИконка"] != null)
			{
				try
				{
					string иконка = узел.Attributes["БитоваяИконка"].Value;

					if (иконка != string.Empty)
					{
						byte[] биты = Convert.FromBase64String(иконка);
						MemoryStream stream = new MemoryStream();
						stream.Write(биты, 0, биты.Length);

						if (Image.FromStream(stream).Height <= 16)
							Пункт.Glyph = Image.FromStream(stream);
						else
							Пункт.LargeGlyph = Image.FromStream(stream);

						imageList_ДеревоМеню.Images.Add(key, Image.FromStream(stream)); 

						битоваяИконка = true;
					}
				}
				catch
				{
					битоваяИконка = false;
				}
			}

			if (!битоваяИконка)
			{
				if (узел.Attributes["Иконка"] != null)
				{
					string имяИконки = узел.Attributes["Иконка"].Value + "_16";
					if (именаВсехКартинок.ContainsKey(имяИконки))
					{
						Пункт.ImageIndex = именаВсехКартинок[имяИконки];
						imageList_ДеревоМеню.Images.Add(key, маленькиеИконки.Images[именаВсехКартинок[имяИконки]]);
					}

					имяИконки = узел.Attributes["Иконка"].Value + "_32";
					if (именаВсехКартинок.ContainsKey(имяИконки))
					{
						Пункт.LargeImageIndex = именаВсехКартинок[имяИконки];
					}
				}
			}

			ЭлементыМеню узелДерева = new ЭлементыМеню(узел.Attributes["Наименование"].InnerText.Trim(), false, Путь, Родитель, null, imageList_ДеревоМеню.Images.IndexOfKey(key));

			ЗагрузитьПунктыМеню(узел, ref Пункт, узел.Attributes["Наименование"].InnerText.Trim(), ref узелДерева, Путь);

			Пункт.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;

			if (!string.IsNullOrEmpty(узел.Attributes["ВсплывающаяПодсказка"].InnerText.Trim()))
			{
				Пункт.Description = узел.Attributes["ВсплывающаяПодсказка"].InnerText.Trim();
				Пункт.Hint = узел.Attributes["ВсплывающаяПодсказка"].InnerText.Trim();
			}
			else
			{
				Пункт.Hint = узел.Attributes["Наименование"].InnerText.Trim();
			}

			if (ДляВыпадающегоМеню)
			{
				ЭлементВыпадающегоМеню элемент = new ЭлементВыпадающегоМеню(Пункт, НоваяГруппа);

				СписокЭлементовВыпадающегоМеню.Add(элемент);

				if (НоваяГруппа)
					НоваяГруппа = false;
			}

			//Добавляем сформированное меню в список элементов главного меню
			узелДерева.Элемент = Пункт;

			СписокЭлементовМеню.Add(узелДерева);

			if (!СловарьНазванийЭлементов.ContainsKey(узелДерева.Название))
			{
				СловарьЭлементов сз = new СловарьЭлементов(узелДерева.Название);

				сз.Add(узелДерева.Путь, узелДерева);

				СловарьНазванийЭлементов.Add(узелДерева.Название, сз);
			}
			else
			{
				if (!СловарьНазванийЭлементов[узелДерева.Название].ContainsKey(узелДерева.Путь))
				{
					СловарьНазванийЭлементов[узелДерева.Название].Add(узелДерева.Путь, узелДерева);
				}
			}

		}
		
		public bool ЗагрузитьXML(string XMLФайлГлавногоМеню)
		{
			XmlDocument XMLДокумент = new XmlDocument();
			XmlNodeList списокРазделов;

			RibbonPage РазделМеню;
			RibbonPageGroup ГруппаРазделаМеню;
			try
			{
				// очищаем существующую панель меню
				ПанельМеню.Pages.Clear();
				XMLДокумент.Load(XMLФайлГлавногоМеню);

				ЭлементыМеню кореньДерева = new ЭлементыМеню("Элементы главного меню", false, string.Empty, null, null, -1);
				СписокЭлементовМеню.Add(кореньДерева);

				#region Загружаем заголовок главного окна
				XmlNode узелЗаголовка = XMLДокумент.SelectSingleNode( "/СписокМеню/Метаописание/ЗаголовокКлиента" );
				if( узелЗаголовка != null )
				{
					НастольноеПриложение.ГлавноеОкно.Text = узелЗаголовка.InnerText;
				}
				#endregion

				XmlNode узелВыпадающееМеню = XMLДокумент.SelectNodes("/СписокМеню/ПанельМеню[@Наименование='Главная']/Меню-приложение")[0];

				ЭлементыМеню узелДереваСервис = new ЭлементыМеню();
				bool НоваяГруппаСервис = false;
				string NamespaceService = "Меню-приложение";

				СписокЭлементовМеню.Add(узелДереваСервис);
     
				try
				{
					XmlElement element = XMLДокумент.CreateElement("zzz");
					element.InnerXml = "<Меню Наименование=\"Сервис\" ВсплывающаяПодсказка=\"Администрирование, работа с БД и настройки пользователя\" БитоваяИконка=\"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\">"
 + "<Меню Наименование=\"Администрирование\" ВсплывающаяПодсказка=\"Управление пользователями и их ролями\" Иконка=\"Администрирование\" БитоваяИконка=\"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\">"
 + "<Пункт Наименование=\"Пользователи системы\" ВсплывающаяПодсказка=\"Список пользователей системы\" БитоваяИконка=\"iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAvdJREFUOE9dkmlIVFEYhr+5izPjNuZcmyyjElLa7IfZj2wR03bJgjaMVpgoSpMUR6NSlMrsR1pBi01BK8zQYCoKkVki9sM0DCkqUkvTssUSXMbl7bvjiE0XHu4993zve855z0fk8WwRiVQ8H9yJz0FX8Q98SOruO+WV9v+8a5yYXp1ypryzec3F1sZJiWU7xotQRHEY3A7ACgyeBV6Z8NtMCR4mfhvK1+57OgJHO1DQOAoltx1SbOlitQi36RC65wCfA4Fmf6BGA6eFMjwMfJOeZwUXDyH4Ui9MBd2gjBbI62vMLoN8Ch+pUAZQKwOPCcNXqfdPEs31MDCstsUYs1shWtpAqe+gNTc55aXJEWrR9f0+MXgU4EQJAXbC4E2x50MiRXoYhG22R6/M/zgckv4WyuE3MOyq6Thw48jdrKbMtm3PYnvqP4UCnVrgPdOiQ/8deskGmjGTFaek0Pz2ppiHwMwLA1ByBuFr3YvMn8mw/7ZjZ/NBzHMIGPoislgCGgSgSoM2My1y6Y0JJbHEQip0Qirsh3S6F7J9AabWGRFUFQSfUj/orYSOF3yEeuaphrOQkRhJG10GAVurD9H5X6BcDi/7G8jyE2JeCiY5tKB7BLJqEH/Dm9MPYDFTaQAcuuGIEJrvMvBfdm6VJo+FOR2gExxi2icsvHwFSxyRUKwKdFcCsekmGzh0QyjR4+s16fuxBMpkqdpwYznIy05eFY42Q5PaDjpuRcTr2Yh6tQimJyYYKxV4Vegwfx/VRYWSxc+b1rNkFhM4ESSRjrxnrBMMS1PJMv0NvRRB3DBStReEShlUIvAOqYsFu5k4ZiGjMML4der5YwoTTnliA1UbQGV8fr57sjH32KBI7CMt7eeaaCaMMfy7A0nNkzHRHsFGVZNBDjax+YPuMw+YLKmF55czs92ra8dXV99qGDKjJx9aQMeEWirWjdItb9A1Pbe2+J6CXdc22b2yKnY30oSN+kNNVj2XlozcKKEc2DSKca+ozqnCifR58BcvGV5P7Gay4wAAAABJRU5ErkJggg==\">Барс.ФормаСпискаПользователей,Ядро</Пункт>" 
 + "<Пункт Наименование=\"Роли пользователей\" ВсплывающаяПодсказка=\"Роли пользователей системы\" БитоваяИконка=\"iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAl1JREFUOE+NkV1I02EUh1/n0DHaRRRCzG5KWCDrZoEXXngRBoFQgbUuIhDywi5sUSvyQmK1C9P5MWrNCBsSczYnMjAnfUHl2MaUMQRjY+hGH5MycMW0/vufX2dSuylzLzzw8nLOc855jxDbnEKv/rocv7EsLzZnN6zVrtUmsWu72L/eJYtoJekciDwg+QHo42F8vyTsZQvkUeGk3AHQp32ghBYUrILUJxbLFhT6xTWKcnJIAZoVoAmBwhUxU7bg3TFx6Oc9hURjFaBHLBgQeN8szpYtWL5d66WFPSA/J48zfgW+2MTCW53Q7Cj5cFKckL9VgX5oQF8rQRkeY4kJC2x0iFs7CqQh4abPSlCaK6eYODPHBATkQbGwo0AeFs8prgJFOCnIvGSmGQ8L7orlJ6dF5X8lG/d3D9Az7mCWJTPVPD8zzveHCuT6FP/eRDQaVYfD4Y7pVyG/zzOaSY4cBLm56uPiFphh3sLQXthuXlg9337Zf6r1zEWDwaAudRKJRLpjsRiSyQSCoSjcHi9cg+0Y6znKNGGkxwhbfy8cwy44HA7Y7XYYjcbukmB+fn6QiJDP57G2toaVlTRCkTgCL0LwB95gOvAaT2dmMeH1bgm6urrQ0tIyUBI4nc7eomB9fR3ZbBbpdBqZTAa5XA6FQoGFK5iamoLVaoXZbIbJZEJjY+OdkkCtVhs6OzvnfD7fJv8FiuMkEomtbiRJQiqV2hJYLBa0tbVtNjQ0BJVK5ZE/ggq+qJha5rhGo7mq1WrtOp1uTK/XT9bX10/W1dW5a2pq7CqVylyMYfb/zlH8AkfYey3FwcuCAAAAAElFTkSuQmCC\">Барс.ФормаСпискаРолей,Ядро</Пункт>"
 + "<Разделитель/>"
 + "<Пункт Наименование=\"Работающие пользователи\" ВсплывающаяПодсказка=\"Пользователи, работающие в системе в данный момент\" Иконка=\"Пользователи\" БитоваяИконка=\"\">Барс.ФормаПросмотраЗарегистрированныхПользователей,Ядро</Пункт>"
 + "</Меню>"
 + "<Меню Наименование=\"Работа с базой данных\" ВсплывающаяПодсказка=\"Просмотр, загрузка и выгрузка хранимых объектов\" БитоваяИконка=\"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\">"
 + "<Пункт Наименование=\"Просмотр хранимых объектов\" ВсплывающаяПодсказка=\"Вывести на просмотр хранимые объекты\" Иконка=\"ПросмотрХО\" БитоваяИконка=\"\">Барс.ПросмотрТаблицХранимыхОбъектов,Ядро</Пункт>" 
 + "<Разделитель />" 
 + "<Пункт Наименование=\"Выгрузка данных в XML формат\" ВсплывающаяПодсказка=\"\" Иконка=\"\" БитоваяИконка=\"iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAl5JREFUOE+tk21P2lAUx321F/sq+zZL9nl8s1czkiUsUwRUWmjppZtSURYSME5XscIqzw/FYkW0kqkIe8At7r9zzQK6vbXJL70n957/Peffnqmpx3jy+fzzSsVipdwK61VniVesV/mH+izrFl+znLHNyuWawHPGdx8eHsZt24GZ1YCfFjDSgR/3+QT8OsDwSx6p9RhOT1202+34WMBxHFavt7C/HcPv7wYwXAcG90mQ4Cau3U2svY/g+PgEPGcsYNs2q1Sb0NMR3A7TwLUM9KP3oPhbHFcnCaywEI6cDq9gImBZ1H+pho8fFnF7HQcufcTChAuKhyIujhSwSBB2+wg8Z1xBtVplB4UKMto88PUdHZ6jFmg9huKRgAtHhLQ8j1bLRr1enwgUi0WWN0tIMg/cigc9awG95jzh+wutWwFY+kuIwbdoNC3wnHEF9ElYLpeDymSoUhAxKpOFA1BEP6LCAuSQj26eg0Tv5aUl1Go1mKY5Edjb22PZbBayLENRYmAxFdEoQzgiQwxLEMQIQoKI5ZAAv9+PcrkMwzAmAvru7sq+YWAxGMRn0wR5wk2C67o4Ozu74/z8HBsbG3jj9aJYKEDX9YlAIpGYvbq8gqbFuTlY0zTs7OxgNBphMBig3+/j5uYGqVQKMzMzoJaxtbU1EfD5fE8zmYxy2u2iSQaFw2Gsrq7yn4Ucb6HRaNxVwwUCgcBdnE6n1QdjND09/URVVYVvFqhEchldEux0OnCOHbjUBjeaC/EziiLL/82hx+N5JklSJJlMMmqLaZr2gCg5y/cEQRC9Xu+LxxjkqT8KnnMQ6BN9bAAAAABJRU5ErkJggg==\">Барс.ФормаВыгрузкиОбъектовВXML,Ядро</Пункт>"
 + "<Пункт Наименование=\"Загрузка данных из XML формата\" ВсплывающаяПодсказка=\"\" Иконка=\"\" БитоваяИконка=\"iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAupJREFUOE+VU20slWEYvs95X85xTuQzb2eHtlq1tHZQGpWWKUaNpX6VmCFDokzyWRpbYzUpqlP62iwTMqcWpz8JM5aPCKVQobSOVooV4+p937MVLVtd273de57nvu6v65HQf0AikRDDMiRlpMSasjQ5Pvnv0TZ2NsJjGW/mDDEy9RE1SbXS+QQKpYIUi3izUBArY0nIKEbJhTji3Nzdinve97z31fkO01WyVxYrfhNIpSKbpaXS0pEl1pb3hVuWN+FCqTRT5gxMDH5Pa0+H/l01GK00Y1mJw7wKhHQHbwzd+MaVcRMON9UfzM9a9LHH2TpaQYX13Q1j5UMVyG7LhGGqH2GPQkbogtjSLwgER+PbE6YTmg/BMNOHoeku9E41I60+/UfPeC/CHgejwVCJls86tHx9ALpIAX8SpMW0x84kNsehcawSdR/v4OGnMuQ9PQPvGm+UjxSi6l0B7hmK8BQ6+Om3N5HQu6nMVCBieOREth6cTX0Sj7vD51A9WohSQwFoLTVqyjSz9tc5aPsTUfIxEz4VXiMUTvniXnk4LLZY7MGpuGvBjSE40XEYVweSUTJ6EgUjGTBxlNUmtR2DZ/UWJHR44NTwTrhWrSvRXF8jdiDhhbG/c6ZrUvv2ymxoXThimwOQ/iwQuUPBiO3eiwDdnhn/2l3Ycd8TUa0aZAz6Yvmt5SlOt1fydRsrUNJqurJMp55N7Q1FWlcAIludkTzghUsTUUh9GY0g/V7sa/BDXKcLMl75wk5rH6i6zBlnKLUSNaAif6pLeh6K6A5XnsAFKYPeyBuLAPlIijSpzk2b9RvHt5ZvGNVcduplstlV1uet5uzQTlSdKyWbDGS+OICYDhdkvd6NpUWqGvIUBWVCaqO4HJI4UudzJD8tn7NFo6wFlqAlWvsvuW+Csali/TBFkI18D68Xvl2yFuskspi7/bm+OA4+k5ISVaWqzxRF7rZZlkSxCwX87dxMPGRoG1mbxZmRJM74oRbCTxATK6SniXzbAAAAAElFTkSuQmCC\">Барс.ФормаЗагрузкиОбъектовИзXML,Ядро</Пункт>"
 + "</Меню>"
 //+ "<Меню Наименование=\"Разное\" ВсплывающаяПодсказка=\"\" Иконка=\"Разное\" БитоваяИконка=\"\">"
 + "<Пункт Наименование=\"Настройки пользователя\" ВсплывающаяПодсказка=\"Настройка элементов интерфейса под Вас!\" БитоваяИконка=\"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\">Барс.ФормаОтображенияНастроекПользователя,Ядро</Пункт>" 
 //+ "</Меню>"
 + "</Меню>";

					foreach (XmlNode node in element.ChildNodes)
					{
						if (node.Name.ToString() == "Меню")
						{
							BarSubItem ПунктМеню = ПанельМеню.Items.CreateMenu(node.Attributes["Наименование"].InnerText.Trim(), new BarItem[] { });

							ЗаполнитьМеню(node, узелДереваСервис, NamespaceService, ref НоваяГруппаСервис, true, ref ПунктМеню);
						}
						else if (node.Name.ToString() == "Пункт")
						{
							BarButtonItem Пункт = ПанельМеню.Items.CreateButton(node.Attributes["Наименование"].InnerText.Trim());

							ЗаполнитьПункт(node, узелДереваСервис, NamespaceService, ref НоваяГруппаСервис, true, ref Пункт);
                        }
						else if (node.Name.ToString() == "Разделитель")
						{
							НоваяГруппаСервис = true;
						}
					}
				}
				catch( Exception )
				{
				}

				if (узелВыпадающееМеню != null)
				{
                
					bool НоваяГруппа = false;
					
					string Namespace = "Меню-приложение";

					ЭлементыМеню узелДерева = new ЭлементыМеню(узелВыпадающееМеню.Attributes["Наименование"].InnerText.Trim(), false, string.Empty, кореньДерева, null, imageList_ДеревоМеню.Images.IndexOfKey("Меню-приложение"));
					
					СписокЭлементовМеню.Add(узелДерева);
     
                    
                    foreach (XmlNode узел in узелВыпадающееМеню.ChildNodes)
					{
						if (узел.Name.ToString() == "Меню")
						{
							BarSubItem ПунктМеню = ПанельМеню.Items.CreateMenu(узел.Attributes["Наименование"].InnerText.Trim(), new BarItem[] { });

							ЗаполнитьМеню(узел, узелДерева, Namespace, ref НоваяГруппа, true, ref ПунктМеню);
						}
						else if (узел.Name.ToString() == "Пункт")
						{
							BarButtonItem Пункт = ПанельМеню.Items.CreateButton(узел.Attributes["Наименование"].InnerText.Trim());

							ЗаполнитьПункт(узел, узелДерева, Namespace, ref НоваяГруппа, true, ref Пункт);
                        }
						else if (узел.Name.ToString() == "Разделитель")
						{
							НоваяГруппа = true;
						}
					}
				}

				// загружаем вкладки разделов
				списокРазделов = XMLДокумент.SelectNodes("/СписокМеню/ПанельМеню[@Наименование='Главная']/Раздел");
				
				foreach (XmlNode раздел in списокРазделов)
				{
					РазделМеню = new RibbonPage(раздел.Attributes["Наименование"].InnerText.Trim());
					ПанельМеню.Pages.Add(РазделМеню);

					ЭлементыМеню узелДерева = new ЭлементыМеню(раздел.Attributes["Наименование"].InnerText.Trim(), false, string.Empty, кореньДерева, null, imageList_ДеревоМеню.Images.IndexOfKey("Раздел"));

					СписокЭлементовМеню.Add(узелДерева);

					string Namespace = раздел.Attributes["Наименование"].InnerText.Trim();

					bool НоваяГруппа = false;

					foreach (XmlNode узел in раздел.ChildNodes)
					{
						switch (узел.Name.ToString())
						{
							case "Меню":
								{
									ГруппаРазделаМеню = new RibbonPageGroup(узел.Attributes["Наименование"].InnerText.Trim());
									ГруппаРазделаМеню.Text = "";
									ГруппаРазделаМеню.ShowCaptionButton = false;
									РазделМеню.Groups.Add(ГруппаРазделаМеню);

									BarSubItem ПунктМеню = ПанельМеню.Items.CreateMenu(узел.Attributes["Наименование"].InnerText.Trim(), new BarItem[] { });

									ЗаполнитьМеню(узел, узелДерева, Namespace, ref НоваяГруппа, false, ref ПунктМеню);

									ГруппаРазделаМеню.ItemLinks.Add(ПунктМеню);
								}
								break;

							case "ГруппаПунктов":
								{
									ГруппаРазделаМеню = new RibbonPageGroup(узел.Attributes["Наименование"].InnerText.Trim());
									ГруппаРазделаМеню.Text = узел.Attributes["Наименование"].InnerText.Trim();
									ГруппаРазделаМеню.ShowCaptionButton = false;
									РазделМеню.Groups.Add(ГруппаРазделаМеню);

									foreach (XmlNode подузел in узел.ChildNodes)
									{
										if (подузел.Name.ToString() == "Меню")
										{
											BarSubItem ПунктМеню = ПанельМеню.Items.CreateMenu(подузел.Attributes["Наименование"].InnerText.Trim(), new BarItem[] { });

											ЗаполнитьМеню(подузел, узелДерева, Namespace, ref НоваяГруппа, false, ref ПунктМеню);

											ГруппаРазделаМеню.ItemLinks.Add(ПунктМеню);
										}
										else if (подузел.Name.ToString() == "Пункт")
										{
											BarButtonItem Пункт = ПанельМеню.Items.CreateButton(подузел.Attributes["Наименование"].InnerText.Trim());

											ЗаполнитьПункт(подузел, узелДерева, Namespace, ref НоваяГруппа, false, ref Пункт);

											ГруппаРазделаМеню.ItemLinks.Add(Пункт);

										}
									}
								}
								break;
						}
					}
				}
            }
			catch (Exception exc)
			{
				ГлавноеОкно.ПоказатьОкноИсключения(exc);
				return false;
			}
			return true;
		}
        
        public void ЗагрузитьПунктыМеню(XmlNode XMLМеню, ref BarSubItem меню, string НазваниеМодуля, ref ЭлементыМеню УзелРодитель, string Путь)
		{
			List<BarItem> списокПунктовМеню = new List<BarItem>();
			BarButtonItem пункт;
			BarSubItem подменю;
			bool началоГруппы = false;
			ИсполняемыйПунктМеню исполняемыйПунктМеню;
			Guid идентификатор;

			Путь += "." + УзелРодитель.Название;

			foreach (XmlNode узел in XMLМеню.ChildNodes)
			{

				if (узел.Name.Trim().ToLower() == "меню")
				{
					string key = Путь + "." + узел.Attributes["Наименование"].InnerText.Trim();

					// это подменю, надо раскрывать еще уровень
					подменю = ПанельМеню.Items.CreateMenu(узел.Attributes["Наименование"].InnerText.Trim(), new BarItem[] { });

					// Загрузка картинки
					bool битоваяИконка = false;
					if (узел.Attributes["БитоваяИконка"] != null)
					{
						try
						{
							string иконка = узел.Attributes["БитоваяИконка"].Value;

							if (иконка != string.Empty)
							{
								byte[] биты = Convert.FromBase64String(иконка);
								MemoryStream stream = new MemoryStream();
								stream.Write(биты, 0, биты.Length);

								if (Image.FromStream(stream).Height <= 16)
									подменю.Glyph = Image.FromStream(stream);
								else
									подменю.LargeGlyph = Image.FromStream(stream);

								imageList_ДеревоМеню.Images.Add(key, Image.FromStream(stream));

								битоваяИконка = true;
							}
						}
						catch
						{
							битоваяИконка = false;
						}
					}

					if (!битоваяИконка)
					{
						if (узел.Attributes["Иконка"] != null)
						{
							string имяИконки = узел.Attributes["Иконка"].Value + "_32";
							if (именаВсехКартинок.ContainsKey(имяИконки))
							{
								подменю.LargeImageIndex = именаВсехКартинок[имяИконки];
								imageList_ДеревоМеню.Images.Add(key, маленькиеИконки.Images[именаВсехКартинок[имяИконки]]);
							}

							имяИконки = узел.Attributes["Иконка"].Value + "_16";
							if (именаВсехКартинок.ContainsKey(имяИконки))
							{
								подменю.ImageIndex = именаВсехКартинок[имяИконки];
							}
						}
					}

					ЭлементыМеню узелДерева = new ЭлементыМеню(узел.Attributes["Наименование"].InnerText.Trim(), false, Путь, УзелРодитель, null, imageList_ДеревоМеню.Images.IndexOfKey(key));

					ЗагрузитьПунктыМеню(узел, ref подменю, НазваниеМодуля, ref узелДерева, Путь);

					узелДерева.Элемент = подменю;

					if (!string.IsNullOrEmpty(узел.Attributes["ВсплывающаяПодсказка"].InnerText.Trim()))
					{
						подменю.Description = узел.Attributes["ВсплывающаяПодсказка"].InnerText.Trim();
						подменю.Hint = узел.Attributes["ВсплывающаяПодсказка"].InnerText.Trim();
					}
					else
					{
						подменю.Hint = узел.Attributes["Наименование"].InnerText.Trim();
					}

					СписокЭлементовМеню.Add(узелДерева);

					if (!СловарьНазванийЭлементов.ContainsKey(узелДерева.Название))
					{
						СловарьЭлементов сз = new СловарьЭлементов(узелДерева.Название);

						сз.Add(узелДерева.Путь, узелДерева);

						СловарьНазванийЭлементов.Add(узелДерева.Название, сз);
					}
					else
					{
						if (!СловарьНазванийЭлементов[узелДерева.Название].ContainsKey(узелДерева.Путь))
						{
							СловарьНазванийЭлементов[узелДерева.Название].Add(узелДерева.Путь, узелДерева);
						}
					}

					меню.ItemLinks.Add(подменю, началоГруппы);
					началоГруппы = false;

				}
				else if (узел.Name.Trim().ToLower() == "пункт")
				{

					string key = Путь + "." + узел.Attributes["Наименование"].InnerText.Trim();

					// это окончательный пункт меню
					пункт = ПанельМеню.Items.CreateButton(узел.Attributes["Наименование"].InnerText.Trim());
					
					// Загрузка картинки
					bool битоваяИконка = false;
					if (узел.Attributes["БитоваяИконка"] != null)
					{
						try
						{
							string иконка = узел.Attributes["БитоваяИконка"].Value;
							if (иконка != string.Empty)
							{
								byte[] биты = Convert.FromBase64String(иконка);
								MemoryStream stream = new MemoryStream();
								stream.Write(биты, 0, биты.Length);

								if (Image.FromStream(stream).Height <= 16)
									пункт.Glyph = Image.FromStream(stream);
								else
									пункт.LargeGlyph = Image.FromStream(stream);

								imageList_ДеревоМеню.Images.Add(key, Image.FromStream(stream));

								битоваяИконка = true;
							}
						}
						catch
						{
							битоваяИконка = false;
						}
					}

					if (!битоваяИконка)
					{
						if (узел.Attributes["Иконка"] != null)
						{
							string имяИконки = узел.Attributes["Иконка"].Value + "_32";
							if (именаВсехКартинок.ContainsKey(имяИконки))
							{
								пункт.LargeImageIndex = именаВсехКартинок[имяИконки];
								imageList_ДеревоМеню.Images.Add(key, маленькиеИконки.Images[именаВсехКартинок[имяИконки]]);
							}

							имяИконки = узел.Attributes["Иконка"].Value + "_32";
							if (именаВсехКартинок.ContainsKey(имяИконки))
							{
								пункт.ImageIndex = именаВсехКартинок[имяИконки];
							}
						}
					}

					меню.ItemLinks.Add(пункт, началоГруппы);
					//меню.ItemLinks[ меню.ItemLinks.Count - 1 ].
					//пункт.Hint = узел.Attributes[ "ВсплывающаяПодсказка" ].InnerText.Trim();

					исполняемыйПунктМеню = new ИсполняемыйПунктМеню();
					исполняемыйПунктМеню.КвалифицирующееИмяФормы = узел.InnerText.Trim();
					исполняемыйПунктМеню.НаименованиеМодуля = НазваниеМодуля;

					идентификатор = Guid.NewGuid();
					пункт.Tag = идентификатор;
					НаборИсполняемыхПунктовМеню.Add(идентификатор, исполняемыйПунктМеню);

					пункт.ItemClick += new ItemClickEventHandler(ИсполнитьПунктМеню);

					ЭлементыМеню узелДерева = new ЭлементыМеню(узел.Attributes["Наименование"].InnerText.Trim(), false, Путь, УзелРодитель, пункт, imageList_ДеревоМеню.Images.IndexOfKey(key));

					if (!string.IsNullOrEmpty(узел.Attributes["ВсплывающаяПодсказка"].InnerText.Trim()))
					{
						пункт.Description = узел.Attributes["ВсплывающаяПодсказка"].InnerText.Trim();
						пункт.Hint = узел.Attributes["ВсплывающаяПодсказка"].InnerText.Trim();
					}
					else
					{
						пункт.Hint = узел.Attributes["Наименование"].InnerText.Trim();
					}
					
					СписокЭлементовМеню.Add(узелДерева);

					if (!СловарьНазванийЭлементов.ContainsKey(узелДерева.Название))
					{
						СловарьЭлементов сз = new СловарьЭлементов(узелДерева.Название);

						сз.Add(узелДерева.Путь, узелДерева);

						СловарьНазванийЭлементов.Add(узелДерева.Название, сз);
					}
					else
					{
						if (!СловарьНазванийЭлементов[узелДерева.Название].ContainsKey(узелДерева.Путь))
						{
							СловарьНазванийЭлементов[узелДерева.Название].Add(узелДерева.Путь, узелДерева);
						}
					}

				}
				else if (узел.Name.Trim().ToLower() == "разделитель")
				{
					началоГруппы = true;
				}
			}
		}

		private void ИсполнитьПунктМеню(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			Guid идПунктаМеню = (Guid)e.Item.Tag;
			ИсполняемыйПунктМеню исполняемыйПунктМеню;
			if (НаборИсполняемыхПунктовМеню.TryGetValue(идПунктаМеню, out исполняемыйПунктМеню))
			{
				try
				{

					if( !string.IsNullOrEmpty( исполняемыйПунктМеню.КвалифицирующееИмяФормы.Trim() ) )
					{
						Type типТочкиВходаПотока = Type.GetType( исполняемыйПунктМеню.КвалифицирующееИмяФормы.Trim() );

						if( типТочкиВходаПотока != null )
						{
							ПотокиПриложения.ЗапуститьНовыйПоток( типТочкиВходаПотока );
						}
					}
				}
				catch( Exception exc )
				{
					ФормаПоказаИсключительнойСитуации.Показать( "Не удалось выполнить пункт меню.", exc );
				}
			}
		}

	}

	public class СловарьНазваний : Dictionary<string, СловарьЭлементов>
	{
		public СловарьНазваний(): base()
		{
		}
	}

	public class СловарьЭлементов : Dictionary<string, ЭлементыМеню>
	{
		private string Namespace = string.Empty;

		public СловарьЭлементов(string _Namespace)
			: base()
		{
			Namespace = _Namespace;
		}
	}

	public class ЭлементВыпадающегоМеню
	{
		private BarItem элемент = null;

		public BarItem Элемент
		{
			get { return элемент; }
			set { элемент = value; }
		}

		private bool новаяГруппа = false;

		public bool НоваяГруппа
		{
			get { return новаяГруппа; }
			set { новаяГруппа = value; }
		}

		public ЭлементВыпадающегоМеню()
		{
		}

		public ЭлементВыпадающегоМеню(BarItem _Элемент, bool _НоваяГруппа)
		{
			Элемент = _Элемент;
			НоваяГруппа = _НоваяГруппа;
		}
	}

	public class ЭлементыМеню
	{
		private string название = string.Empty;

		public string Название
		{
			get { return название; }
			set { название = value; }
		}

		private string путь = string.Empty;

		public string Путь
		{
			get { return путь; }
			set { путь = value; }
		}

		private bool переключатель = false;

		public bool Переключатель
		{
			get { return переключатель; }
			set { переключатель = value; }
		}
		private ЭлементыМеню родитель = null;

		public ЭлементыМеню Родитель
		{
			get { return родитель; }
			set { родитель = value; }
		}

		public ЭлементыМеню ID
		{
			get { return this; }
		}

		private BarItem элемент = null;

		public BarItem Элемент
		{
			get { return элемент; }
			set { элемент = value; }
		}

		private int индексИконки = 0;

		public int ИндексИконки
		{
			get { return индексИконки; }
			set { индексИконки = value; }
		}

		public ЭлементыМеню()
		{
		}

		public ЭлементыМеню(string _Название, bool _Переключатель, string _Путь, ЭлементыМеню _Родитель, BarItem _Элемент, int _ИндексИконки)
		{
			Название = _Название;
			Переключатель = _Переключатель;
			Родитель = _Родитель;
			Элемент = _Элемент;
			Путь = _Путь;
			ИндексИконки = _ИндексИконки;
		}
	}


	/// <summary>
	/// Пункт меню, который вызывает исполнение некоторого кода в системе
	/// </summary>
	class ИсполняемыйПунктМеню
	{
		public string КвалифицирующееИмяФормы; // "Барс.СправочникУчреждений,Платформа"
		public string НаименованиеМодуля;
	}
}
