namespace Барс.Своды.БраузерОтчетныхФорм
{
    using DevExpress.XtraTab;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;
    using Барс;
    using Барс.Своды.ОтчетнаяФорма;

    public class НастройкиГруппыВкладок : List<НастройкиВкладки>
    {
        private string идентификаторМетаОписания = "";
        private string кодКомпонентаОтчетногоПериода = "";
        private string кодОтчетногоПериода = "";
        private string кодУчреждения = "";
        private string путьКФайлуНастроек = "";
        private Type типГоловногоОбъекта = null;

        public НастройкиГруппыВкладок(XtraTabControl группаЗакладок, Type типГоловногоОбъекта, ОтчетнаяФормаДанных отчетнаяФорма)
        {
            this.типГоловногоОбъекта = типГоловногоОбъекта;
            this.идентификаторМетаОписания = отчетнаяФорма.Метаструктура.Идентификатор.ИдентификаторМетаописания;
            this.кодУчреждения = отчетнаяФорма.Данные.Идентификатор.Учреждение.КороткийКод;
            this.кодКомпонентаОтчетногоПериода = отчетнаяФорма.КомпонентОтчетногоПериода.Код;
            this.кодОтчетногоПериода = отчетнаяФорма.КомпонентОтчетногоПериода.ОтчетныйПериод.Код;
            for (int i = 0; i < группаЗакладок.TabPages.Count; i++)
            {
                НастройкиВкладки вкладки = new НастройкиВкладки(группаЗакладок.TabPages[i], i);
                this.Add(вкладки);
            }
        }

        public void Add(НастройкиВкладки Вкладка)
        {
            if (Вкладка == null)
            {
                throw new ArgumentNullException("Задан пустой объект для добавления в список вкладок.");
            }
            base.Add(Вкладка);
        }

        public void ДесериализоватьНастройки(XtraTabControl группаЗакладок)
        {
            this.ПолучитьПолныйПутьКФайлу();
            if (File.Exists(this.путьКФайлуНастроек))
            {
                List<string> list = new List<string>();
                XmlDocument document = new XmlDocument();
                document.Load(this.путьКФайлуНастроек);
                XmlNodeList list2 = document.SelectNodes("ГруппаВкладок");
                if (list2.Count > 0)
                {
                    foreach (XmlNode node in list2[0].ChildNodes)
                    {
                        if (node.Name == "Вкладка")
                        {
                            НастройкиВкладки вкладки = new НастройкиВкладки();
                            вкладки.Десериализовать(node);
                            for (int i = 0; i < группаЗакладок.TabPages.Count; i++)
                            {
                                XtraTabPage page = группаЗакладок.TabPages[i];
                                if (page.Text == вкладки.НаименованиеВкладки)
                                {
                                    if (i != вкладки.ИндексРазмещения)
                                    {
                                        группаЗакладок.TabPages.Move(вкладки.ИндексРазмещения, page);
                                    }
                                    page.ImageIndex = вкладки.ImageIndex;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void ПолучитьПолныйПутьКФайлу()
        {
            string str = string.Format("{0}_{1}.Расположение", this.идентификаторМетаОписания, this.кодУчреждения);
            ПутьФайлаНастроек настроек = new ПутьФайлаНастроек(this.типГоловногоОбъекта, str);
            string path = Path.Combine(Path.Combine(Path.Combine(настроек.ПолучитьПутьКФайлуНастроек(Метод.Запись), "НастройкиВкладок"), this.кодОтчетногоПериода), this.кодКомпонентаОтчетногоПериода);
            this.путьКФайлуНастроек = path + @"\" + str + ".xml";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public void СброситьНастройки()
        {
            this.ПолучитьПолныйПутьКФайлу();
            if (File.Exists(this.путьКФайлуНастроек))
            {
                File.SetAttributes(this.путьКФайлуНастроек, FileAttributes.Normal);
                File.Delete(this.путьКФайлуНастроек);
            }
        }

        public void СериализоватьНастройки()
        {
            this.СброситьНастройки();
            List<string> list = new List<string>();
            XmlElement newChild = null;
            try
            {
                XmlDocument document = new XmlDocument();
                newChild = document.CreateElement("ГруппаВкладок");
                foreach (НастройкиВкладки вкладки in this)
                {
                    XmlElement element2 = вкладки.Сериализовать(document);
                    if (element2 != null)
                    {
                        newChild.AppendChild(element2);
                    }
                }
                document.AppendChild(newChild);
                document.PreserveWhitespace = true;
                document.Save(this.путьКФайлуНастроек);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}

