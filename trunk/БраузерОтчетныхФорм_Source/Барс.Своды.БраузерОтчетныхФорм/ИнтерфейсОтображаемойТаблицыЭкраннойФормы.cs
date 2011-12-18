namespace Барс.Своды.БраузерОтчетныхФорм
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using System.Xml;
    using Барс.Своды.ОтчетнаяФорма;

    public interface ИнтерфейсОтображаемойТаблицыЭкраннойФормы : IDisposable
    {
        event СобытиеПослеУстановкиЗначенияЯчейки ПослеУстановкиЗначенияЯчейкиСправочника;

        event СобытиеИзмененияТаблицыДанных ПриИзмененииДанных;

        void ОбновитьЗначения();
        void ПересчитатьАвтоблоки();
        bool ПроверитьЗаполненностьДанных();
        bool ПроверитьЗаполненностьДанных(out Dictionary<string, List<string>> КоординатыНезаполненныхЯчеек);
        bool ПроверитьЗаполненностьДанных(out string Сообщение);
        XmlDocument СериализоватьВXML();

        ВариантОткрытияФормы ВариантОткрытия { get; }

        string Заголовок { get; }

        string ИмяЛиста { get; set; }

        string КодТаблицы { get; set; }

        string Наименование { get; set; }

        bool РазмещатьНаЗакладке { get; set; }

        Барс.Своды.ОтчетнаяФорма.ТаблицаДанных ТаблицаДанных { get; }

        Барс.Своды.ОтчетнаяФорма.ТаблицаМетаструктуры ТаблицаМетаструктуры { get; }

        Барс.Своды.БраузерОтчетныхФорм.ЭкраннаяФорма ЭкраннаяФорма { get; set; }

        Control ЭлементУправления { get; }
    }
}

