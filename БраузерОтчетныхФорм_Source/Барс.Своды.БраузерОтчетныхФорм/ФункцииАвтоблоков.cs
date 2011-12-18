namespace Барс.Своды.БраузерОтчетныхФорм
{
    using Syncfusion.Calculate;
    using System;
    using Барс.Своды.ОтчетнаяФорма;

    public class ФункцииАвтоблоков : IDisposable
    {
        private ОтчетнаяФормаДанных отчетнаяФорма;

        public ФункцииАвтоблоков(ОтчетнаяФормаДанных ОтчетнаяФорма)
        {
            this.отчетнаяФорма = ОтчетнаяФорма;
        }

        public void Dispose()
        {
            this.отчетнаяФорма = null;
        }

        public string КоличествоСтрок(string args)
        {
            if (this.отчетнаяФорма != null)
            {
                string[] strArray = args.Split(new char[] { CalcEngine.ParseArgumentSeparator });
                int length = strArray.Length;
                if (length > 1)
                {
                    string str = this.ПолучитьКодТаблицы(strArray[0]);
                    if (string.IsNullOrEmpty(str))
                    {
                        return string.Empty;
                    }
                    string str2 = strArray[1];
                    str2 = str2.Replace("\"", "");
                    if (length > 2)
                    {
                        string str3 = strArray[2];
                        str3 = str3.Replace("\"", "");
                        if (length > 3)
                        {
                            string str4 = strArray[3];
                            return this.ПолучитьКоличествоСтрок(str, str2, str3, str4).ToString();
                        }
                        return this.ПолучитьКоличествоСтрок(str, str2, str3, string.Empty).ToString();
                    }
                    return this.ПолучитьКоличествоСтрок(str, str2, string.Empty, string.Empty).ToString();
                }
            }
            return string.Empty;
        }

        private string ПолучитьКодСтолбца(ТаблицаДанных таблицаДанных, string КодСтолбца)
        {
            return КодСтолбца.ToUpper();
        }

        private string ПолучитьКодТаблицы(string Код)
        {
            string str = Код.ToUpper();
            if (this.отчетнаяФорма.Метаструктура == null)
            {
                return string.Empty;
            }
            string str2 = string.Empty;
            foreach (string str3 in this.отчетнаяФорма.Метаструктура.Таблицы.Keys)
            {
                if (str3.ToUpper() == str)
                {
                    return str3;
                }
            }
            return str2;
        }

        private decimal ПолучитьКоличествоСтрок(string КодТаблицы, string КодСтолбцаОтбора, string ЗначениеОтбора, string Операция)
        {
            ТаблицаДанных данных = this.отчетнаяФорма.Данные[КодТаблицы];
            return данных.ПолучитьКоличествоСтрок(КодСтолбцаОтбора, ЗначениеОтбора, Операция);
        }

        private decimal ПолучитьСуммуПоСтолбцу(string КодТаблицы, string КодСтолбца)
        {
            return this.ПолучитьСуммуПоСтолбцу(КодТаблицы, КодСтолбца, string.Empty);
        }

        private decimal ПолучитьСуммуПоСтолбцу(string КодТаблицы, string КодСтолбца, string КодСтолбцаОтбора)
        {
            return this.ПолучитьСуммуПоСтолбцу(КодТаблицы, КодСтолбца, КодСтолбцаОтбора, string.Empty);
        }

        private decimal ПолучитьСуммуПоСтолбцу(string КодТаблицы, string КодСтолбца, string КодСтолбцаОтбора, string ЗначениеОтбора)
        {
            return this.ПолучитьСуммуПоСтолбцу(КодТаблицы, КодСтолбца, КодСтолбцаОтбора, ЗначениеОтбора, "Содержит");
        }

        private decimal ПолучитьСуммуПоСтолбцу(string КодТаблицы, string КодСтолбца, string КодСтолбцаОтбора, string ЗначениеОтбора, string Операция)
        {
            ТаблицаДанных данных = this.отчетнаяФорма.Данные[КодТаблицы];
            return данных.ПолучитьСуммуПоСтолбцу(КодСтолбца, КодСтолбцаОтбора, ЗначениеОтбора, Операция);
        }

        public string СуммаПоСтолбцу(string args)
        {
            if (this.отчетнаяФорма != null)
            {
                string[] strArray = args.Split(new char[] { CalcEngine.ParseArgumentSeparator });
                int length = strArray.Length;
                if (length >= 2)
                {
                    string str = this.ПолучитьКодТаблицы(strArray[0]);
                    if (string.IsNullOrEmpty(str))
                    {
                        return string.Empty;
                    }
                    string str2 = strArray[1];
                    if (length < 3)
                    {
                        return this.ПолучитьСуммуПоСтолбцу(str, str2).ToString();
                    }
                    string str3 = strArray[2];
                    str3 = str3.Replace("\"", "");
                    if (length >= 4)
                    {
                        string str4 = strArray[3];
                        str4 = str4.Replace("\"", "");
                        if (length >= 5)
                        {
                            string str5 = strArray[4];
                            return this.ПолучитьСуммуПоСтолбцу(str, str2, str3, str4, str5).ToString();
                        }
                        return this.ПолучитьСуммуПоСтолбцу(str, str2, str3, str4).ToString();
                    }
                    return this.ПолучитьСуммуПоСтолбцу(str, str2, str3).ToString();
                }
            }
            return string.Empty;
        }

        public ОтчетнаяФормаДанных ОтчетнаяФорма
        {
            get
            {
                return this.отчетнаяФорма;
            }
            set
            {
                this.отчетнаяФорма = value;
            }
        }
    }
}

