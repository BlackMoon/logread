namespace Барс.Своды.БраузерОтчетныхФорм
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct ПараметрыКнопкиТаблицы
    {
        private string операция;
        private string фильтр;
        private ТипОперацииКнопкиТаблицы типОперации;
        private List<string> утверждения;
        private List<object> параметрыВызова;
        private Dictionary<string, object> именованыеПараметры;
        public string Операция
        {
            get
            {
                return this.операция;
            }
        }
        public string Фильтр
        {
            get
            {
                return this.фильтр;
            }
            set
            {
                this.фильтр = value;
            }
        }
        public ТипОперацииКнопкиТаблицы ТипОперации
        {
            get
            {
                return this.типОперации;
            }
        }
        public bool СодержитУтверждение(string Утверждение)
        {
            if (string.IsNullOrEmpty(Утверждение))
            {
                return false;
            }
            return this.утверждения.Contains(Утверждение.Trim().ToLower());
        }

        public void ДобавитьУтверждение(string Утверждение)
        {
            if (!this.СодержитУтверждение(Утверждение))
            {
                this.утверждения.Add(Утверждение.Trim().ToLower());
            }
        }

        public void ДобавитьПараметр(object Параметр)
        {
            this.параметрыВызова.Add(Параметр);
        }

        public void ДобавитьПараметрыИзСтроки(string СтрокаПараметров)
        {
            if (!string.IsNullOrEmpty(СтрокаПараметров))
            {
                string[] strArray = СтрокаПараметров.Split(new char[] { ',' });
                foreach (string str in strArray)
                {
                    this.параметрыВызова.Add(str);
                }
            }
        }

        public object[] МассивПараметров()
        {
            if (this.параметрыВызова.Count == 0)
            {
                return null;
            }
            object[] objArray = new object[this.параметрыВызова.Count];
            for (int i = 0; i < objArray.Length; i++)
            {
                objArray[i] = this.параметрыВызова[i];
            }
            return objArray;
        }

        public void ДобавитьИменованыйПараметр(string Имя, object Значение)
        {
            if (!string.IsNullOrEmpty(Имя))
            {
                if (this.именованыеПараметры.ContainsKey(Имя.Trim().ToLower()))
                {
                    this.именованыеПараметры[Имя.Trim().ToLower()] = Значение;
                }
                else
                {
                    this.именованыеПараметры.Add(Имя.Trim().ToLower(), Значение);
                }
            }
        }

        public object ПолучитьИменованыйПараметр(string Имя)
        {
            if (!string.IsNullOrEmpty(Имя) && this.именованыеПараметры.ContainsKey(Имя.Trim().ToLower()))
            {
                return this.именованыеПараметры[Имя.Trim().ToLower()];
            }
            return null;
        }

        public ПараметрыКнопкиТаблицы(string Операция, ТипОперацииКнопкиТаблицы ТипОперации)
        {
            this.операция = Операция;
            this.типОперации = ТипОперации;
            this.фильтр = "";
            this.утверждения = new List<string>();
            this.параметрыВызова = new List<object>();
            this.именованыеПараметры = new Dictionary<string, object>();
        }
        public enum ТипОперацииКнопкиТаблицы
        {
            НеОпределено,
            ОткрытиеТаблицы,
            ВызовМакроса
        }
    }
}

