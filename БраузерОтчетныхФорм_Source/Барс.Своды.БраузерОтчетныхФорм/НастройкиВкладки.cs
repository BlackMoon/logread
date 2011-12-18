namespace Барс.Своды.БраузерОтчетныхФорм
{
    using DevExpress.XtraTab;
    using System;
    using System.Xml;

    public class НастройкиВкладки
    {
        private int imageIndex;
        private int индексРазмещения;
        private string наименованиеВкладки;

        public НастройкиВкладки()
        {
            this.imageIndex = -1;
            this.индексРазмещения = 0;
            this.наименованиеВкладки = "";
        }

        public НастройкиВкладки(XtraTabPage страница, int порядок)
        {
            this.imageIndex = -1;
            this.индексРазмещения = 0;
            this.наименованиеВкладки = "";
            this.imageIndex = страница.ImageIndex;
            this.индексРазмещения = порядок;
            this.наименованиеВкладки = страница.Text;
        }

        public void Десериализовать(XmlNode xmlСвойства)
        {
            foreach (XmlNode node in xmlСвойства.ChildNodes)
            {
                if (node.Name == "ImageIndex")
                {
                    this.imageIndex = int.Parse(node.InnerText);
                }
                else if (node.Name == "ИндексРазмещения")
                {
                    this.индексРазмещения = int.Parse(node.InnerText);
                }
                else if (node.Name == "НаименованиеВкладки")
                {
                    this.наименованиеВкладки = node.InnerText;
                }
            }
        }

        public XmlElement Сериализовать(XmlDocument xmlДокумент)
        {
            XmlElement element = xmlДокумент.CreateElement("Вкладка");
            XmlElement newChild = xmlДокумент.CreateElement("ImageIndex");
            newChild.InnerText = this.ImageIndex.ToString();
            element.AppendChild(newChild);
            newChild = xmlДокумент.CreateElement("ИндексРазмещения");
            newChild.InnerText = this.ИндексРазмещения.ToString();
            element.AppendChild(newChild);
            newChild = xmlДокумент.CreateElement("НаименованиеВкладки");
            newChild.InnerText = this.НаименованиеВкладки;
            element.AppendChild(newChild);
            return element;
        }

        public int ImageIndex
        {
            get
            {
                return this.imageIndex;
            }
        }

        public int ИндексРазмещения
        {
            get
            {
                return this.индексРазмещения;
            }
        }

        public string НаименованиеВкладки
        {
            get
            {
                return this.наименованиеВкладки;
            }
        }
    }
}

