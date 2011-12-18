namespace Барс.Своды.БраузерОтчетныхФорм
{
    using System;
    using Барс;

    public class ЭлементПросмотраЗаписиИсторииСборки
    {
        private string[] данныеСтроки = null;
        private bool загружен = true;
        private ЗаписьИсторииСборкиИтоговыхОтчетов запись = null;
        private string значение = "";
        private string кодУчрежденияИсточника;
        private string столбец = "";
        private string столбецПриемника = "";

        public ЭлементПросмотраЗаписиИсторииСборки(ЗаписьИсторииСборкиИтоговыхОтчетов запись, string СтолбецПриемника)
        {
            this.запись = запись;
            if (this.запись != null)
            {
                this.кодУчрежденияИсточника = this.запись.КодУчрежденияИсточника;
            }
            this.столбецПриемника = СтолбецПриемника;
            this.данныеСтроки = запись.ПолучитьДанныеСтрокиИстории().Split(new char[] { '|' });
            string str2 = "";
            if (this.данныеСтроки.Length == 0)
            {
                this.загружен = false;
            }
            else if (!string.IsNullOrEmpty(this.столбецПриемника))
            {
                foreach (string str3 in this.данныеСтроки)
                {
                    if (str3.StartsWith(СтолбецПриемника + ":"))
                    {
                        str2 = str3;
                        break;
                    }
                }
                if (!string.IsNullOrEmpty(str2))
                {
                    string[] strArray = str2.Split(new char[] { ':' });
                    if (strArray.Length == 3)
                    {
                        this.столбец = strArray[1];
                        this.значение = strArray[2];
                    }
                    else
                    {
                        this.загружен = false;
                    }
                }
                else
                {
                    this.загружен = false;
                }
            }
        }

        public string ПолучитьЗначениеПараметра(string имяПараметра)
        {
            string str = имяПараметра.Trim().ToUpper();
            string str2 = "";
            switch (str)
            {
                case "ФОРМА":
                    return this.Форма;

                case "УЧРЕЖДЕНИЕ":
                    return this.Учреждение;

                case "КОДУЧРЕЖДЕНИЯ":
                    return this.КодУчреждения;

                case "ТАБЛИЦА":
                    return this.Таблица;

                case "СТРОКА":
                    return this.Строка;

                case "СТОЛБЕЦ":
                    return this.Столбец;

                case "ЗНАЧЕНИЕ":
                    return this.Значение;
            }
            if (str.StartsWith("ЗНАЧЕНИЕ_СТОЛБЦА_"))
            {
                string str3 = str.Substring(0x11);
                str2 = this.ПолучитьЗначениеСтолбца(str3);
            }
            return str2;
        }

        private string ПолучитьЗначениеСтолбца(string кодСтолбца)
        {
            string str = "";
            foreach (string str2 in this.данныеСтроки)
            {
                if (str2.StartsWith(кодСтолбца + ":"))
                {
                    str = str2;
                    break;
                }
            }
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            string[] strArray = str.Split(new char[] { ':' });
            if (strArray.Length != 3)
            {
                return "";
            }
            return strArray[2];
        }

        public ЗаписьИсторииСборкиИтоговыхОтчетов ЗаписьИстории
        {
            get
            {
                return this.запись;
            }
        }

        public string Значение
        {
            get
            {
                return this.значение;
            }
        }

        public string КодСтрокиИсточника
        {
            get
            {
                return (((this.запись == null) || (this.запись.КодСтрокиИсточника == null)) ? "" : this.запись.КодСтрокиИсточника);
            }
        }

        public string КодСтрокиПриемника
        {
            get
            {
                return (((this.запись == null) || (this.запись.КодСтрокиПриемника == null)) ? "" : this.запись.КодСтрокиПриемника);
            }
        }

        public string КодУчреждения
        {
            get
            {
                return this.кодУчрежденияИсточника;
            }
        }

        public string Столбец
        {
            get
            {
                return this.столбец;
            }
        }

        public string Строка
        {
            get
            {
                return this.запись.КодСтрокиИсточника;
            }
        }

        public string Таблица
        {
            get
            {
                return this.запись.КодТаблицыИсточника;
            }
        }

        public string Учреждение
        {
            get
            {
                return this.запись.ЗначениеЭлементаЦепочкиИсточника;
            }
        }

        public string Форма
        {
            get
            {
                return this.запись.НаименованиеФормыИсточника;
            }
        }

        public bool ЭлементЗагружен
        {
            get
            {
                return this.загружен;
            }
        }
    }
}

