using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.Win32;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.Nodes.Operations;

namespace Барс.Интерфейс
{
    public delegate void ИзменениеВыбранногоЭлемента( ЭлементДереваКаталогов Элемент );
    public delegate void ЭлементМожетБытьВыбран( ЭлементДереваКаталогов Элемент, out bool МожетБытьВыбран );


    /// <summary>
    /// Диалог выбора каталога
    /// </summary>
	public partial class ДиалогВыбораКаталога : DevExpress.XtraEditors.XtraForm
	{
        #region Конструкторы

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public ДиалогВыбораКаталога()
        {
            InitializeComponent();
        }

        #endregion

        #region Поля

        #region Выбирать файлы
        protected bool выбиратьФайлы = false;
        #endregion

        #region Показывать справку

        private bool показыватьСправку = false;
        //private bool ужеОткрыто = false;

        #endregion

        #endregion

        #region Свойства

        #region Выбранный каталог
        private string выбранныйКаталог = string.Empty;
		public string ВыбранныйКаталог
		{
			get { return выбранныйКаталог; }
			set { выбранныйКаталог = value; }
        }

        public virtual string Путь
        {
            get { return ВыбранныйКаталог; }
            set { ВыбранныйКаталог = value; }
        }

        #endregion

        #region КорневойКаталог
        private string корневойКаталог = string.Empty;
		public string КорневойКаталог
		{
			get { return корневойКаталог; }
			set { корневойКаталог = value; }
        }
        #endregion

        #region Заголовок окна
        private string заголовок = string.Empty;
		public string Заголовок
		{
			get { return заголовок; }
			set { заголовок = value; }
        }
        #endregion

        #region Текущий выбранный элемент

        protected ЭлементДереваКаталогов ТекущийЭлемент
        {
            get
            {
                if ((деревоКаталогов.FocusedNode == null) || (деревоКаталогов.FocusedNode.Tag == null))
                {
                    return null;
                }
                else
                {
                    return (ЭлементДереваКаталогов)деревоКаталогов.FocusedNode.Tag;
                }

            }
        }

        #endregion

        #region Видимость кнопки создания каталога

        public bool КнопкаСозданияКаталога
        {
            get { return кнопкаСоздать.Visible; }
            set { кнопкаСоздать.Visible = value; }
        }

        #endregion

        #endregion

        #region События

        #region Событие изменения выбранного элемента

        public event ИзменениеВыбранногоЭлемента ИзменениеВыбранногоЭлемента = null;

        protected virtual void СобытиеИзмененияВыбранногоЭлемента(ЭлементДереваКаталогов Элемент){}

        #endregion

        #region Событие проверки на возможность выбора элемента

        public event ЭлементМожетБытьВыбран ПроверкаНаВозможностьВыбораЭлемента = null;

        protected virtual void СобытиеПроверкиНаВозможностьВыбораЭлемента(ЭлементДереваКаталогов Элемент, out bool ЭлементМожетБытьВыбран)
        {
            ЭлементМожетБытьВыбран = ( ( Элемент != null ) && Элемент.ЯвляетсяКаталогом );
        }

        #endregion

        #region Событие при нажатии на кнопку ОК

        public event ЭлементМожетБытьВыбран НажатиеНаКнопкуОК = null;

        protected virtual void СобытиеНажатияНаКнопкуОК(ЭлементДереваКаталогов Элемент, out bool ПродолжениеВозможно)
        {
            выбранныйКаталог = "";
            ПродолжениеВозможно = false;
            if ((Элемент != null) && Элемент.ЯвляетсяКаталогом)
            {
                выбранныйКаталог = Элемент.ПолноеНаименование;
                ПродолжениеВозможно = true;
            }
        }

        #endregion

        #endregion

        #region Константы

        private const string СООБЩЕНИЕ_ДЕРЕВО = "Чтобы развернуть структуру элементы, щелкните\nрасположенный слева от него знак плюс(+).";
		private const string СООБЩЕНИЕ_OK = "Закрытие диалогового окна с сохранением всех\nвнесенных изменений.";
		private const string СООБЩЕНИЕ_ОТМЕНА = "Закрытие диалогового окна без сохранения всех\nвнесенных изменений.";

		private const int МОЙ_КОМПЬЮТЕР = 0;
		private const int ДИСК = 1;
		private const int CD = 2;
		private const int ПАПКА = 3;
		private const int ОТКРЫТАЯ_ПАПКА = 4;
        private const int ФЛОПИ = 5;
        private const int ФАЙЛ = 6;

        #endregion

        #region Методы

        private void УстановитьИконкуЭлемента(ЭлементДереваКаталогов Элемент, TreeListNode Узел)
        {
            switch (Элемент.ТипЭлемента)
            {
                case ТипЭлементаДереваКаталогов.МойКомпьютер :
                    Узел.ImageIndex = МОЙ_КОМПЬЮТЕР;
                    break;

                case ТипЭлементаДереваКаталогов.Диск :
                    Узел.ImageIndex = ДИСК;
                    break;

                case ТипЭлементаДереваКаталогов.CDДиск :
                    Узел.ImageIndex = CD;
                    break;

                case ТипЭлементаДереваКаталогов.Флопи :
                    Узел.ImageIndex = ФЛОПИ;
                    break;

                case ТипЭлементаДереваКаталогов.Каталог :
                    Узел.ImageIndex = ПАПКА;
                    break;

                case ТипЭлементаДереваКаталогов.Файл :
                    Узел.ImageIndex = ФАЙЛ;
                    break;
            }

            Узел.SelectImageIndex = Узел.ImageIndex;
            
        }

        protected virtual bool ДобавлятьЭлементВДерево(ЭлементДереваКаталогов Элемент, TreeListNode Родитель)
        {
            return true;
        }

        protected virtual TreeListNode ДобавитьЭлементВДерево(ЭлементДереваКаталогов Элемент, TreeListNode Родитель)
        {
            if (!ДобавлятьЭлементВДерево(Элемент, Родитель))
            {
                return null;
            }

            TreeListNode результат = деревоКаталогов.AppendNode(new string[] { Элемент.Наименование }, Родитель);
            результат.Tag = Элемент;

            УстановитьИконкуЭлемента(Элемент, результат);

            if (Элемент.ПодчиненныеЭлементыЗагружены)
            {

                foreach (ЭлементДереваКаталогов подЭлемент in Элемент.ПолучитьСписокПодчиненныхЭлементов(выбиратьФайлы))
                {
                    ДобавитьЭлементВДерево(подЭлемент, результат);
                }
            }
            else if ( (выбиратьФайлы && Элемент.СодержитФайлыИлиКаталоги ) || Элемент.СодержитКаталоги)
            {
                деревоКаталогов.AppendNode(new object[] { new object() }, результат);
            }

            return результат;
        }

        protected virtual TreeListNode НайтиУзелПоПути(string Путь, TreeListNode Корень)
        {
            ЭлементДереваКаталогов Элемент = (ЭлементДереваКаталогов)Корень.Tag;

            if (!Путь.Trim().ToUpper().StartsWith(Элемент.ПолноеНаименование.Trim().ToUpper()))
            {
                return null;
            }

            if (Элемент.ПолноеНаименование.Trim().ToUpper() == Путь.Trim().ToUpper())
            {
                return Корень;
            }

            if( Элемент.ПодчиненныеЭлементыЗагружены )
            {
                foreach (TreeListNode подУзел in Корень.Nodes)
                {
                    TreeListNode node = НайтиУзелПоПути(Путь, подУзел);
                    if (node != null)
                    {
                        return node;
                    }
                }
                return null;
            }
            else
            {
                return null;
            }
        }

        protected virtual void УстановитьТекущийЭлемент( string Путь )
        {
            TreeListNode узел = null;

            if( Directory.Exists( КорневойКаталог ) )
            {
                if( КорневойКаталог.Trim().ToUpper() == Путь.Trim().ToUpper() )
                {
                    деревоКаталогов.FocusedNode = деревоКаталогов.Nodes[0];
                    деревоКаталогов.FocusedNode.Expanded = true;
                    return;
                }

                if( !Путь.Trim().ToUpper().StartsWith( КорневойКаталог.Trim().ToUpper() ) )
                {
                    return;
                }
                
                узел = деревоКаталогов.Nodes[0];
            }
            else
            {
                foreach (TreeListNode node in деревоКаталогов.Nodes[0].Nodes)
                {
                    ЭлементДереваКаталогов элемент = (ЭлементДереваКаталогов)node.Tag;
                    if (элемент.ПолноеНаименование.Trim().ToUpper() == Путь.Substring(0, 3).Trim().ToUpper())
                    {
                        узел = node;
                        break;
                    }
                }
            }

            if( узел != null )
            {
                TreeListNode узелПути = null;
                ЭлементДереваКаталогов элемент = (ЭлементДереваКаталогов)узел.Tag;
                элемент.ЗагрузитьПодчиненныйПуть( Путь, выбиратьФайлы );

                узел.Nodes.Clear();
                foreach (ЭлементДереваКаталогов подЭлемент in элемент.ПолучитьСписокПодчиненныхЭлементов(выбиратьФайлы))
                {
                    ДобавитьЭлементВДерево(подЭлемент, узел);
                }

                узелПути = НайтиУзелПоПути(Путь, узел);

                if (узелПути != null)
                {
                    деревоКаталогов.FocusedNode = узелПути;
                    узелПути.Expanded = true;
                }
            }
        }

		private void ДиалогВыбораКаталога_HelpRequested(object sender, HelpEventArgs hlpevent)
		{
			Point point = this.PointToClient(hlpevent.MousePos);
			Control control = this.GetChildAtPoint(point);
			if (control != null)
			{
				if (control is DevExpress.XtraTreeList.TreeList)
				{
					показыватьСправку = true;
					toolTip.ShowAlways = true;
					toolTip.Show(СООБЩЕНИЕ_ДЕРЕВО, control);
				}
				else if (control is DevExpress.XtraEditors.SimpleButton)
				{
					string текст = (control as DevExpress.XtraEditors.SimpleButton).Text;
					if (текст == "OK")
					{
						показыватьСправку = true;
						toolTip.ShowAlways = true;
						toolTip.Show(СООБЩЕНИЕ_OK, control);
					}
					else if (текст == "Отмена")
					{
						показыватьСправку = true;
						toolTip.ShowAlways = true;
						toolTip.Show(СООБЩЕНИЕ_ОТМЕНА, control);
					}
				}
			}
		}

		private void toolTip_Popup(object sender, PopupEventArgs e)
		{
			if (показыватьСправку)
				показыватьСправку = false;
			else
				e.Cancel = true;
		}

		private void ДиалогВыбораКаталога_Load(object sender, EventArgs e)
		{
			try
			{
				if (!string.IsNullOrEmpty(Заголовок))
					this.Text = Заголовок;

                #region Устанавливаем базовые обработчики событий

                ИзменениеВыбранногоЭлемента += new ИзменениеВыбранногоЭлемента( СобытиеИзмененияВыбранногоЭлемента );
                ПроверкаНаВозможностьВыбораЭлемента += new ЭлементМожетБытьВыбран( СобытиеПроверкиНаВозможностьВыбораЭлемента );
                НажатиеНаКнопкуОК += new ЭлементМожетБытьВыбран(СобытиеНажатияНаКнопкуОК);

                #endregion

                // Строю дерево каталогов
                деревоКаталогов.ClearNodes();
                деревоКаталогов.SelectImageList = imageList;

                ЭлементДереваКаталогов корневойЭлемент;
                TreeListNode корень;

                if (Directory.Exists(КорневойКаталог))
                {
                    корневойЭлемент = new ЭлементДереваКаталогов(КорневойКаталог);
                }
                else
                {
                    корневойЭлемент = new ЭлементДереваКаталогов("Мой компьютер");
                }

                корень = ДобавитьЭлементВДерево(корневойЭлемент, null);

                if (корень == null)
                {
                    return;
                }

                if (корневойЭлемент.ТипЭлемента == ТипЭлементаДереваКаталогов.Каталог)
                {
                    корень.ImageIndex = ОТКРЫТАЯ_ПАПКА;
                    корень.SelectImageIndex = ОТКРЫТАЯ_ПАПКА;
                }

                if (корневойЭлемент.СодержитФайлыИлиКаталоги)
                {
                    деревоКаталогов.AppendNode(new object[] { new object() }, корень);                    
                }

                деревоКаталогов.FocusedNode = корень;
                корень.Expanded = true;

                if( !string.IsNullOrEmpty( выбранныйКаталог ) )
                {
                    if (File.Exists(выбранныйКаталог))
                    {
                        выбранныйКаталог = Path.GetDirectoryName(выбранныйКаталог);
                    }

                    try
                    {
                        УстановитьТекущийЭлемент(выбранныйКаталог);
                    }
                    finally
                    {
                        выбранныйКаталог = string.Empty;
                    }
                }

			}
			catch(Exception err)
			{
				MessageBox.Show(string.Format("Во время построения дерева каталогов произошла ошибка. Ошибка: {0}.", err.Message), "БАРС II", MessageBoxButtons.OK,
					MessageBoxIcon.Error);
			}
		}

		private void деревоКаталогов_BeforeExpand(object sender, DevExpress.XtraTreeList.BeforeExpandEventArgs e)
		{
		    this.Cursor = Cursors.WaitCursor;

            try
            {
                TreeListNode текущийУзел = e.Node;
                ЭлементДереваКаталогов элемент = (ЭлементДереваКаталогов)текущийУзел.Tag;

                try
                {
                    if ( элемент.МожетСодержатьЭлементы && !элемент.ПодчиненныеЭлементыЗагружены )
                    {
                        текущийУзел.Nodes.Clear();

                        try
                        {
                            if (элемент.ТипЭлемента == ТипЭлементаДереваКаталогов.Каталог)
                            {
                                    текущийУзел.ImageIndex = ОТКРЫТАЯ_ПАПКА;
                                    текущийУзел.SelectImageIndex = ОТКРЫТАЯ_ПАПКА;
                            }

                            foreach (ЭлементДереваКаталогов подЭлемент in элемент.ПолучитьСписокПодчиненныхЭлементов(выбиратьФайлы))
                            {
                                TreeListNode узел = ДобавитьЭлементВДерево(подЭлемент, текущийУзел);

                                if ( ( узел != null ) && ( ( выбиратьФайлы && подЭлемент.СодержитФайлыИлиКаталоги ) || подЭлемент.СодержитКаталоги) )
                                {
                                    деревоКаталогов.AppendNode(new string[] { "" }, узел);
                                }
                            }
                        }
                        catch
                        {
                        }
                    }
                }
                catch
                {
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(string.Format("Во время построения дерева каталогов произошла ошибка. Ошибка: {0}.", err.Message), "БАРС II", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
		}

		private void кнопкаOK_Click(object sender, EventArgs e)
		{
			try
			{
                ВыбранныйКаталог = string.Empty;

                if (ТекущийЭлемент != null)
                {
                    ВыбранныйКаталог = ТекущийЭлемент.ПолноеНаименование;
                }

                if (НажатиеНаКнопкуОК != null)
                {
                    bool ПродолжениеВозможно = true;
                    НажатиеНаКнопкуОК(ТекущийЭлемент, out ПродолжениеВозможно);

                    if (!ПродолжениеВозможно)
                    {
                        return;
                    }

                }
			}
			catch
			{
				ВыбранныйКаталог = string.Empty;
			}
		}

		private void деревоКаталогов_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
		{
			деревоКаталогов.OptionsBehavior.Editable = false;

            ЭлементДереваКаталогов текущийЭлемент = ТекущийЭлемент;
            if (ИзменениеВыбранногоЭлемента != null)
            {
                ИзменениеВыбранногоЭлемента(текущийЭлемент);
            }

            bool ЭлементМожетБытьВыбран = true;
            if (ПроверкаНаВозможностьВыбораЭлемента != null)
            {
                ПроверкаНаВозможностьВыбораЭлемента(текущийЭлемент, out ЭлементМожетБытьВыбран);
            }
            кнопкаOK.Enabled = ЭлементМожетБытьВыбран;

		}

		private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
		{
			try
			{
				if (деревоКаталогов.FocusedNode[0] is ЭлементДереваКаталогов)
				{
					if(string.IsNullOrEmpty((деревоКаталогов.FocusedNode.Tag as ЭлементДереваКаталогов).Наименование))
						return;

					if ((деревоКаталогов.FocusedNode.Tag as ЭлементДереваКаталогов).ТипЭлемента == ТипЭлементаДереваКаталогов.МойКомпьютер)
						e.Cancel = true;
				}
			}
			catch
			{
			}
		}

		private void деревоКаталогов_BeforeCollapse(object sender, DevExpress.XtraTreeList.BeforeCollapseEventArgs e)
		{
			if (e.Node.ImageIndex == ОТКРЫТАЯ_ПАПКА)
			{
				e.Node.ImageIndex = ПАПКА;
				e.Node.SelectImageIndex = ПАПКА;
			}
        }

        #endregion

        protected void ОткрытьЭлементВWin(ЭлементДереваКаталогов Элемент)
        {
            try
            {
                if (Элемент.ЯвляетсяКаталогом)
                {
                    Process.Start("explorer.exe", @Элемент.ПолноеНаименование);
                    return;
                }
                else if (Элемент.ЯвляетсяФайлом)
                {

                    string ext = Path.GetExtension(Элемент.ПолноеНаименование);

                    string openCommand = string.Empty;

                    using (RegistryKey classes_root = Registry.ClassesRoot)
                    {
                        string app_name = string.Empty;

                        RegistryKey ext_key = classes_root.OpenSubKey(ext);
                        app_name = ext_key.GetValue("").ToString();

                        if (!string.IsNullOrEmpty(app_name))
                        {
                            RegistryKey open_key = classes_root.OpenSubKey(app_name + @"\shell\open\command\");

                            openCommand = open_key.GetValue("").ToString();
                        }
                    }

                    if (!string.IsNullOrEmpty(openCommand))
                    {
                        string[] pars = openCommand.Split('\"');

                        string app = pars[1];
                        string arguments = string.Format("{0} \"{1}\" ", pars[2], Элемент.ПолноеНаименование);

                        Process.Start(app, arguments);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void кнопкаСоздать_Click(object sender, EventArgs e)
        {
            ЭлементДереваКаталогов элемент = ТекущийЭлемент;
            TreeListNode узел = деревоКаталогов.FocusedNode;

            if ((элемент == null) || !элемент.ЯвляетсяКаталогом)
            {
                return;
            }

            string новоеИмя = "";
            int индексПапки = 0;
            string новыйПуть = "";

            новоеИмя = ДиалогВводаСтроки.Показать("", "Введите имя нового каталога", "Новая папка");

            try
            {
                if (Directory.Exists(новыйПуть = Path.Combine(элемент.ПолноеНаименование, новоеИмя)))
                {
                    while (Directory.Exists(новыйПуть = Path.Combine(элемент.ПолноеНаименование, новоеИмя + "_" + индексПапки.ToString())))
                    { индексПапки++; }
                }

                Directory.CreateDirectory(новыйПуть);
                УстановитьТекущийЭлемент(новыйПуть);
            }
            catch( Exception ex )
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Ошибка создания каталога", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

        }

        private void переименоватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ЭлементДереваКаталогов элемент = ТекущийЭлемент;
            TreeListNode узел = деревоКаталогов.FocusedNode;

            if ((элемент == null) || ( элемент.Родитель == null ) || элемент.ЯвляетсяДиском || ( элемент.ТипЭлемента == ТипЭлементаДереваКаталогов.МойКомпьютер ))
            {
                System.Windows.Forms.MessageBox.Show("Изменение имени данного элемента запрещено.", "Изменение имени элемента", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return;
            }

            string выбранныйПуть = элемент.ПолноеНаименование;
            string наименование = элемент.Наименование;
            string новоеИмя = ДиалогВводаСтроки.Показать("Изменение имени элемента", "Введите новое имя элемента", наименование);
            if (string.IsNullOrEmpty(новоеИмя))
            {
                return;
            }

            элемент.Наименование = новоеИмя;

            УстановитьТекущийЭлемент(Path.Combine(элемент.Родитель.ПолноеНаименование, новоеИмя));
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ЭлементДереваКаталогов элемент = ТекущийЭлемент;
            TreeListNode узел = деревоКаталогов.FocusedNode;

            if ((элемент == null) || (элемент.Родитель == null) || элемент.ЯвляетсяДиском || (элемент.ТипЭлемента == ТипЭлементаДереваКаталогов.МойКомпьютер))
            {
                System.Windows.Forms.MessageBox.Show("Удаление данного элемента запрещено.", "Удаление элемента", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if( System.Windows.Forms.MessageBox.Show("Удалить файл или каталог\n" + элемент.ПолноеНаименование, "Удаление элемента", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) != DialogResult.Yes )
                {
                    return;
                }

                if (элемент.ЯвляетсяКаталогом)
                {
                    Directory.Delete(элемент.ПолноеНаименование, true);
                }
                else if (элемент.ЯвляетсяФайлом)
                {
                    File.Delete(элемент.ПолноеНаименование);
                }

                элемент.Родитель.ОбновитьСписокПодчиненныхЭлементов(выбиратьФайлы);
                УстановитьТекущийЭлемент(элемент.Родитель.ПолноеНаименование);
            }
            catch( Exception ex )
            {
                System.Windows.Forms.MessageBox.Show("Ошибка удаления элемента.\n" + ex.Message, "Удаление элемента", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ЭлементДереваКаталогов элемент = ТекущийЭлемент;
            if ((элемент == null) || (!элемент.ЯвляетсяКаталогом && !элемент.ЯвляетсяФайлом))
            {
                return;
            }
            ОткрытьЭлементВWin(элемент);

        }
    }

    public class ДиалогВыбораФайла : ДиалогВыбораКаталога
    {
        #region Конструктор
        public ДиалогВыбораФайла():base()
        {
            выбиратьФайлы = true;
            КнопкаСозданияКаталога = false;
        }
        #endregion

        #region Свойства

        #region Фильтр файлов

        private string допустимыеРасширенияФайлов = "";
        public string ДопустимыеРасширенияФайлов
        {
            get { return допустимыеРасширенияФайлов; }
            set { допустимыеРасширенияФайлов = value; }
        }

        #endregion

        #region Выбранный файл

        private string выбранныйФайл = string.Empty;
        public string ВыбранныйФайл
        {
            get { return выбранныйФайл; }
        }

        public override string Путь
        {
            get { return ВыбранныйФайл; }
            set { выбранныйФайл = value; }
        }


        #endregion

        #endregion

        #region События

        #region Событие при нажатии на кнопку ОК

        protected override void СобытиеНажатияНаКнопкуОК(ЭлементДереваКаталогов Элемент, out bool ПродолжениеВозможно)
        {
            ВыбранныйКаталог = "";
            выбранныйФайл = "";
            ПродолжениеВозможно = false;
            if ((Элемент != null) && Элемент.ЯвляетсяФайлом)
            {
                выбранныйФайл = Элемент.ПолноеНаименование;
                ВыбранныйКаталог = Path.GetDirectoryName(выбранныйФайл);
                ПродолжениеВозможно = true;
            }
        }

        #endregion

        #region Событие проверки на возможность выбора элемента

        protected override void СобытиеПроверкиНаВозможностьВыбораЭлемента(ЭлементДереваКаталогов Элемент, out bool ЭлементМожетБытьВыбран)
        {
            ЭлементМожетБытьВыбран = ((Элемент != null) && Элемент.ЯвляетсяФайлом);
        }

        #endregion

        #endregion

        protected override bool ДобавлятьЭлементВДерево(ЭлементДереваКаталогов Элемент, TreeListNode Родитель)
        {
            if (!Элемент.ЯвляетсяФайлом)
            {
                return true;
            }
            else
            {
                if (string.IsNullOrEmpty(допустимыеРасширенияФайлов))
                {
                    return true;
                }

                string[] расширения = допустимыеРасширенияФайлов.Trim().ToUpper().Split('|');
                foreach( string расширение in расширения )
                {
                    if (расширение == Элемент.КакФайл.Extension.Trim().ToUpper())
                    {
                        return true;
                    }
                }
                return false;
            }
        }
    }

    class ОперацияПоискаЭлементаФайловойСистемы : TreeListOperation
    {
        private string путьПоиска = string.Empty;

        private TreeListNode узел = null;
        public TreeListNode Узел
        {
            get { return узел; }
        }

        public ОперацияПоискаЭлементаФайловойСистемы(string Путь)
        {
            if (!string.IsNullOrEmpty(Путь))
            {
                if (Directory.Exists(Путь))
                {
                    путьПоиска = Путь.Trim();
                }
                else if (File.Exists(Путь))
                {
                    путьПоиска = Path.GetDirectoryName(Путь);
                }
            }
        }

        public override void Execute(TreeListNode ТекущийУзел)
        {
            if (string.IsNullOrEmpty(путьПоиска))
            {
                return;
            }

            if (узел != null)
            {
                return;
            }

            if ( ( ТекущийУзел == null ) || (ТекущийУзел.Tag == null) || !(ТекущийУзел.Tag is ЭлементДереваКаталогов))
            {
                return;
            }

            ЭлементДереваКаталогов элемент = (ЭлементДереваКаталогов)ТекущийУзел.Tag;
            if (элемент.ПолноеНаименование.Trim().ToUpper() == путьПоиска.Trim().ToUpper())
            {
                узел = ТекущийУзел;
                return;
            }
        }
    }
}