namespace Барс.Своды.БраузерОтчетныхФорм
{
    using System;
    using Барс.Своды.ОтчетнаяФорма;

    public class РедактируемаяФорма
    {
        private string группа = string.Empty;
        private string идентификатор;
        private string кодФормы = string.Empty;
        private string наименование = string.Empty;
        private DateTime началоДействия = DateTime.MinValue;
        private DateTime окончаниеДействия = DateTime.MaxValue;
        private string путьКФайлуМетаструктуры = string.Empty;

        public override string ToString()
        {
            return this.КодФормы;
        }

        public bool ПолучитьОписаниеФормы(string ИмяФайла)
        {
            try
            {
                МетаструктураФормы формы = new МетаструктураФормы();
                формы.ЗагрузитьОписаниеИзФайла(ИмяФайла);
                this.Идентификатор = формы.Идентификатор.ИдентификаторМетаописания;
                this.КодФормы = формы.Код;
                this.Наименование = формы.Наименование;
                this.Группа = формы.Группа;
                this.НачалоДействия = формы.ДатаНачалаДействия;
                this.ОкончаниеДействия = формы.ДатаОкончанияДействия;
                this.ПутьКФайлуМетаструктуры = ИмяФайла;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string Группа
        {
            get
            {
                return this.группа;
            }
            set
            {
                this.группа = value;
            }
        }

        public string Идентификатор
        {
            get
            {
                return this.идентификатор;
            }
            set
            {
                this.идентификатор = value;
            }
        }

        public string КодФормы
        {
            get
            {
                return this.кодФормы;
            }
            set
            {
                this.кодФормы = value;
            }
        }

        public string Наименование
        {
            get
            {
                return this.наименование;
            }
            set
            {
                this.наименование = value;
            }
        }

        public DateTime НачалоДействия
        {
            get
            {
                return this.началоДействия;
            }
            set
            {
                this.началоДействия = value;
            }
        }

        public DateTime ОкончаниеДействия
        {
            get
            {
                return this.окончаниеДействия;
            }
            set
            {
                this.окончаниеДействия = value;
            }
        }

        public string ПутьКФайлуМетаструктуры
        {
            get
            {
                return this.путьКФайлуМетаструктуры;
            }
            set
            {
                this.путьКФайлуМетаструктуры = value;
            }
        }
    }
}

