namespace Барс.Своды.БраузерОтчетныхФорм
{
    using DevExpress.Utils;
    using DevExpress.XtraBars;
    using DevExpress.XtraEditors;
    using DevExpress.XtraEditors.Controls;
    using DevExpress.XtraEditors.Repository;
    using DevExpress.XtraLayout;
    using DevExpress.XtraLayout.Utils;
    using DevExpress.XtraTab;
    using Syncfusion.Windows.Forms.Grid;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;
    using System.Xml;
    using System.Xml.XPath;
    using Барс;
    using Барс.БраузерОтчетныхФорм.Properties;
    using Барс.ИмпортЭкспорт.ИмпортДанныхОтчетныхФорм;
    using Барс.ИмпортЭкспорт.ЭкспортДанныхОтчетныхФорм;
    using Барс.Интерфейс;
    using Барс.Клиент;
    using Барс.Своды;
    using Барс.Своды.АргументыСобытийОтчетнойФормы;
    using Барс.Своды.ОтчетнаяФорма;
    using Барс.Своды.ТипыЯчеек;
    using Барс.Типы;
    using Барс.Ядро;

    public class ЭкраннаяФорма : XtraForm
    {
        private BarButtonItem barButtonItem_;
        private BarButtonItem barButtonItem_CloseFind;
        private BarButtonItem barButtonItem_FindNext;
        private BarButtonItem barButtonItem_FindPrev;
        private BarButtonItem barButtonItem_inner;
        private BarButtonItem barButtonItem_out;
        private BarButtonItem barButtonItem_АвтосохраненныеДанные;
        private BarButtonItem barButtonItem_Архив_Сохранить;
        private BarButtonItem barButtonItem_Архив_Список;
        private BarButtonItem barButtonItem_ВосстановитьИзЗеркала;
        private BarButtonItem barButtonItem_ОбновитьЗеркало;
        private BarButtonItem barButtonItem_сверитьСЗеркалом;
        private BarButtonItem barButtonItem_собрать;
        private BarButtonItem barButtonItem_Справка;
        private BarButtonItem barButtonItem_тестирование;
        private BarButtonItem barButtonItem1;
        private BarButtonItem barButtonItem2;
        private BarButtonItem barButtonItem3;
        private BarButtonItem barButtonItem4;
        private BarButtonItem barButtonItemСобратьПоКритериям;
        private BarDockControl barDockControlBottom;
        private BarDockControl barDockControlLeft;
        private BarDockControl barDockControlRight;
        private BarDockControl barDockControlTop;
        private BarEditItem barEditItem_FindColumnOnly;
        private BarEditItem barEditItem_FindSens;
        private BarEditItem barEditItem_FindText;
        private BarManager barManager_main;
        private BarStaticItem barStaticItem_Найти;
        private BarSubItem barSubItem_func;
        private BarSubItem barSubItem_Архив;
        private BarSubItem barSubItem_дополнительно;
        private BarSubItem barSubItem_Зеркало;
        private BarSubItem barSubItem_ИмпортДанных;
        private BarSubItem barSubItem_ПечатныеФормы;
        private BarSubItem barSubItem_Сводная;
        private BarSubItem barSubItem_Справка;
        private BarSubItem barSubItem_ЭкспортДанных;
        private BarSubItem barSubItem1;
        private BarSubItem barSubItem2;
        private BarStaticItem barToolTip;
        private ComboBoxEdit comboBoxEdit_Закладки;
        private IContainer components;
        private ContextMenuStrip contextMenuStrip_КонтекстноеМенюТабов;
        private EmptySpaceItem emptySpaceItem_bottom;
        private Bar FindTool;
        private ImageList imageList_bar;
        private ImageList imageList_наборРисунков;
        private LayoutControlItem layoutControlItem_закладки;
        private LayoutControlItem layoutControlItem_отмена;
        private LayoutControlItem layoutControlItem_применить;
        private LayoutControlItem layoutControlItem_сохранитьИЗакрыть;
        private LayoutControlItem layoutItem_выборВкладки;
        private LayoutControlGroup mainGroup;
        private LayoutControl mainLayout;
        private RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private RepositoryItemTextEdit repositoryItemTextEdit1;
        public SimpleButton simpleButton_отмена;
        private SimpleButton simpleButton_применить;
        private SimpleButton simpleButton_сохранитьИЗакрыть;
        private Bar StatusBar;
        private Timer timer_автосохранение;
        private ToolStripMenuItem toolStripMenuItem_вернутьНастройкиПоУмолчанию;
        private ToolStripMenuItem ToolStripMenuItem_выставитьЦвет;
        private ToolStripMenuItem toolStripMenuItem_СдвинутьВкладкуВлево;
        private ToolStripMenuItem toolStripMenuItem_СдвинутьВкладкуВправо;
        private XtraTabControl xtraTabControl_ГруппаЗакладок;
        private XtraTabPage xtraTabPage1;
        private ВариантОткрытияФормы вариантОткрытия = ВариантОткрытияФормы.ПолныйДоступ;
        private bool данныеИзменились = false;
        private Dictionary<string, ДинамическаяТаблица> динамическиеТаблицы = new Dictionary<string, ДинамическаяТаблица>();
        private int кодГруппыТаблиц = -1;
        private bool настройкиСброшены = false;
        private string ограниченияНаЭлементы = "";
        private List<ИнтерфейсОтображаемойТаблицыЭкраннойФормы> отображаемыеТаблицы = new List<ИнтерфейсОтображаемойТаблицыЭкраннойФормы>();
        private ОтчетнаяФормаДанных отчетнаяФорма = new ОтчетнаяФормаДанных();
        private Bar ПанельИнструментов;
        private Bar ПанельМеню;
        private bool разрешеноЗакрытьФорму = true;
        private РежимРаботыЭкраннойФормы режимРаботы = РежимРаботыЭкраннойФормы.Просмотр;
        private ToolStripMenuItem сдвинутьВКонецToolStripMenuItem;
        private ToolStripMenuItem сдвинутьВлевоНаОдинШагToolStripMenuItem;
        private ToolStripMenuItem сдвинутьВначалоToolStripMenuItem;
        private ToolStripMenuItem сдвинутьВправоНаОдинШагToolStripMenuItem;
        private Dictionary<string, Dictionary<string, string>> списокОграниченийНаЭлементы = new Dictionary<string, Dictionary<string, string>>();
        private Dictionary<string, object> списокПеременных = new Dictionary<string, object>();
        private List<ТаблицаОтчетнойФормы> таблицы = new List<ТаблицаОтчетнойФормы>();
        private ШапкаЭкраннойФормы шапка = null;

        public ЭкраннаяФорма()
        {
            this.InitializeComponent();
        }

        private void barButtonItem_CloseFind_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.СкрытьПанельПоиска();
        }

        private void barButtonItem_FindNext_ItemClick(object sender, ItemClickEventArgs e)
        {
            Control control = this.xtraTabControl_ГруппаЗакладок.SelectedTabPage.Controls[0];
            if (control is ТаблицаОтчетнойФормы)
            {
                ТаблицаОтчетнойФормы формы = control as ТаблицаОтчетнойФормы;
                new ФункцииПоиска(формы).Найти((string) this.barEditItem_FindText.EditValue, (this.barEditItem_FindSens.EditValue != null) && ((bool) this.barEditItem_FindSens.EditValue), false, (this.barEditItem_FindColumnOnly.EditValue != null) && ((bool) this.barEditItem_FindColumnOnly.EditValue));
            }
        }

        private void barButtonItem_FindPrev_ItemClick(object sender, ItemClickEventArgs e)
        {
            Control control = this.xtraTabControl_ГруппаЗакладок.SelectedTabPage.Controls[0];
            if (control is ТаблицаОтчетнойФормы)
            {
                ТаблицаОтчетнойФормы формы = control as ТаблицаОтчетнойФормы;
                new ФункцииПоиска(формы).Найти((string) this.barEditItem_FindText.EditValue, (this.barEditItem_FindSens.EditValue != null) && ((bool) this.barEditItem_FindSens.EditValue), true, (this.barEditItem_FindColumnOnly.EditValue != null) && ((bool) this.barEditItem_FindColumnOnly.EditValue));
            }
        }

        private void barButtonItem_inner_ItemClick(object sender, ItemClickEventArgs e)
        {
            string str = string.Empty;
            if (МенеджерБД.МенеджерИнициализирован)
            {
                ХранимыеДанныеФормы формы = ХранилищеДанныхФорм.ПолучитьДанныеФормы(this.ОтчетнаяФорма.Данные.Идентификатор);
                if (формы != null)
                {
                    str = формы.ДополнительныйСтатус;
                }
            }
            if (!ПроверкаДоступаКОтчетнойФорме.ПроверитьДоступКФорме(this.ОтчетнаяФорма.КомпонентОтчетногоПериода.ОтчетныйПериод, this.ОтчетнаяФорма.Метаструктура.Идентификатор.ИдентификаторМетаописания, this.ОтчетнаяФорма.Учреждение, ТипДоступаКОтчетнойФорме.ПроверкаУвязок, str))
            {
                Сообщение.Показать("Извините, у вас нет прав на проверку увязок этой формы.", "Предупреждение!");
            }
            else
            {
                try
                {
                    this.simpleButton_применить.Focus();
                    foreach (ТаблицаОтчетнойФормы формы2 in this.Таблицы)
                    {
                        формы2.ПересчитатьАвтоблоки();
                    }
                    ФормаРезультатовПроверкиУвязок увязок = new ФормаРезультатовПроверкиУвязок();
                    this.ОтчетнаяФорма.ПроверитьВнутриформенныеУвязки(увязок);
                }
                catch (ИсключениеБарсСводов сводов)
                {
                    Сообщение.ПоказатьИсключительнуюСитуацию(сводов.Message, сводов);
                }
                catch (Exception exception)
                {
                    Сообщение.ПоказатьИсключительнуюСитуацию("Не удалось проверить внутриформенные увязки.", exception);
                }
            }
        }

        private void barButtonItem_out_ItemClick(object sender, ItemClickEventArgs e)
        {
            ХранимыеДанныеФормы формы = ХранилищеДанныхФорм.ПолучитьДанныеФормы(this.ОтчетнаяФорма.Данные.Идентификатор);
            string str = string.Empty;
            if (формы != null)
            {
                str = формы.ДополнительныйСтатус;
            }
            if (!ПроверкаДоступаКОтчетнойФорме.ПроверитьДоступКФорме(this.ОтчетнаяФорма.КомпонентОтчетногоПериода.ОтчетныйПериод, this.ОтчетнаяФорма.Метаструктура.Идентификатор.ИдентификаторМетаописания, this.ОтчетнаяФорма.Учреждение, ТипДоступаКОтчетнойФорме.ПроверкаУвязок, str))
            {
                Сообщение.Показать("Извините, у вас нет прав на проверку увязок этой формы.", "Предупреждение!");
            }
            else
            {
                try
                {
                    this.simpleButton_применить.Focus();
                    foreach (ТаблицаОтчетнойФормы формы2 in this.Таблицы)
                    {
                        формы2.ПересчитатьАвтоблоки();
                    }
                    ФормаРезультатовПроверкиУвязок увязок = new ФормаРезультатовПроверкиУвязок();
                    this.ОтчетнаяФорма.ПроверитьМежформенныеУвязки(увязок);
                }
                catch (ИсключениеБарсСводов сводов)
                {
                    Сообщение.ПоказатьИсключительнуюСитуацию(сводов.Message, сводов);
                }
                catch (Exception exception)
                {
                    Сообщение.ПоказатьИсключительнуюСитуацию("Не удалось проверить межформенные увязки.", exception);
                }
            }
        }

        private void barButtonItem_АвтосохраненныеДанные_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                List<АвтосохранениеДанныхОтчетнойФормы.АвтосохраненныйФайл> list = new АвтосохранениеДанныхОтчетнойФормы(this.ОтчетнаяФорма).ПолучитьСписокАвтосохраненныхФайлов();
                if (list.Count == 0)
                {
                    Сообщение.Показать("Для данной формы автосохранение данных ни разу не выполнялось.");
                }
                else
                {
                    ФормаПоказаАвтосохраненныхФайлов файлов = new ФормаПоказаАвтосохраненныхФайлов();
                    файлов.СписокАвтосохраненныхФайлов = list;
                    if ((файлов.ShowDialog() == DialogResult.OK) && (файлов.ВыбранныйФайл != null))
                    {
                        this.ОтчетнаяФорма.Данные.ОчиститьДанные();
                        this.ОтчетнаяФорма.Данные.ЗагрузитьИзФайла(файлов.ВыбранныйФайл.ПолныйПутьФайла);
                        this.ОбновитьДанные();
                        this.ПересчитатьАвтоблоки();
                    }
                }
            }
            catch (Exception exception)
            {
                Сообщение.ПоказатьИсключительнуюСитуацию("Не удалось отобразить список файлов с автосохраненными данными формы.", exception);
            }
        }

        private void barButtonItem_Архив_Сохранить_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.ЗавершитьРедактированиеТекущейЯчейки();
            АрхивХранимыхДанныхФорм форм = new АрхивХранимыхДанныхФорм();
            if (форм.Редактировать())
            {
                try
                {
                    МенеджерБД.НачатьТранзакцию();
                    форм.ХранимыеДанные = ХранилищеДанныхФорм.ПолучитьДанныеФормы(this.ОтчетнаяФорма.Данные.Идентификатор);
                    if (форм.ХранимыеДанные != null)
                    {
                        форм.Сохранить();
                        форм.Заблокировать();
                        XmlDocument document = this.ОтчетнаяФорма.Данные.СериализоватьВXML(false);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            document.Save(stream);
                            stream.Seek(0L, SeekOrigin.Begin);
                            Файл.ЗаписатьПотокВБД(stream, форм, "ФайлАрхивДанных", true);
                        }
                    }
                    else
                    {
                        МенеджерБД.СохранитьТранзакцию();
                        Сообщение.Показать("Хранимые данные пустые. Необходимо сохранить форму.");
                        return;
                    }
                    МенеджерБД.СохранитьТранзакцию();
                    Сообщение.Показать("Сохранение данных прошло успешно.");
                }
                catch (Exception exception)
                {
                    МенеджерБД.ОткатитьТранзакцию();
                    Сообщение.ПоказатьИсключительнуюСитуацию("Не удалось сохранить данные в базу данных.", exception);
                }
            }
        }

        private void barButtonItem_Архив_Список_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.ЗавершитьРедактированиеТекущейЯчейки();
            ФормаСпискаАрхивовХранимыхДанных данных = new ФормаСпискаАрхивовХранимыхДанных();
            ХранимыеДанныеФормы формы = ХранилищеДанныхФорм.ПолучитьДанныеФормы(this.ОтчетнаяФорма.Данные.Идентификатор);
            if (формы != null)
            {
                данных.РедактируемыйОбъект = формы;
                if (данных.ПоказатьДиалог() == РезультатДиалога.OK)
                {
                    АрхивХранимыхДанныхФорм форм = данных.АрхивХранимыхДанных;
                    if (форм != null)
                    {
                        XPathDocument document = null;
                        using (MemoryStream stream = Файл.СчитатьПотокИзБД(форм, "ФайлАрхивДанных", true))
                        {
                            document = new XPathDocument(stream);
                        }
                        if (document != null)
                        {
                            this.ОтчетнаяФорма.Данные.ОчиститьДанные();
                            this.ОтчетнаяФорма.Данные.ЗагрузитьИзXPathDocument(document);
                            this.ОбновитьДанные();
                            this.ПересчитатьАвтоблоки();
                        }
                    }
                }
            }
        }

        private void barButtonItem_ВосстановитьИзЗеркала_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.ВосстановитьИзЗеркала();
        }

        private void barButtonItem_сверитьСЗеркалом_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.СверитьСЗеркалом();
        }

        private void barButtonItem_собрать_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.СобратьФорму();
        }

        private void barButtonItem_Справка_ItemClick(object sender, ItemClickEventArgs e)
        {
            if ((this.РежимРаботы == РежимРаботыЭкраннойФормы.Просмотр) || (Сообщение.ПоказатьВопрос("В Н И М А Н И Е!!!\n\nВыполнение дополнительных обработок в режиме предварительного просмотра чревато возникновением критических ошибок.\nРазработчики снимают с себя ответственность за Ваши дальнейшие действия!\n\nВы действительно хотите выполнить данную обработку ?") == РезультатДиалога.Да))
            {
                ИнтерфейсОтображаемойТаблицыЭкраннойФормы формы = this.ТаблицаТекущейЗакладки;
                this.ОткрытьМетодическийСправочник(формы);
            }
        }

        private void barButtonItem_тестирование_ItemClick(object sender, ItemClickEventArgs e)
        {
            ФормаВыбораТеста теста = new ФормаВыбораТеста();
            теста.ИдентификаторМетаописания = this.ОтчетнаяФорма.Метаструктура.Идентификатор;
            if (теста.ShowDialog() == DialogResult.OK)
            {
                List<ОписаниеТеста> list = теста.ВыбранныеТесты;
                if ((list != null) || (list.Count == 0))
                {
                    ФормаПроверкиТестов тестов = new ФормаПроверкиТестов();
                    тестов.ПоказатьФорму(list.Count);
                    foreach (ОписаниеТеста теста2 in list)
                    {
                        ПроверкаТестаФормы формы = new ПроверкаТестаФормы(теста2, this.ОтчетнаяФорма);
                        тестов.НачатьВыводТеста(формы);
                        формы.ПроверитьТест();
                    }
                    Сообщение.Показать("Тест завершен!");
                }
            }
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            КэшСправочников.ОчиститьКэшСправочников(this.ОтчетнаяФорма);
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            КэшСправочников.ОчиститьКэшСправочников();
        }

        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            ИдентификаторДанныхФормы формы = this.ОтчетнаяФорма.Данные.Идентификатор;
            if (формы != null)
            {
                string str = ПровайдерФайловФормы.ПолучитьПутьКПапкеДанных(формы);
                if (!string.IsNullOrEmpty(str))
                {
                    Process.Start("explorer.exe", str);
                }
                else
                {
                    Сообщение.Показать("Не удалось получить путь к папке данных.", "Папка данных...");
                }
            }
            else
            {
                Сообщение.Показать("Не удалось получить идентификатор данных.", "Папка данных...");
            }
        }

        private void barButtonItemСобратьПоКритериям_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                this.СобратьФорму(new ОбработчикСводаПоКритериям(this.отчетнаяФорма));
            }
            catch (Exception exception)
            {
                Сообщение.ПоказатьИсключительнуюСитуацию("Не удалось собрать свод по критериям", exception);
            }
        }

        private void comboBoxEdit_Закладки_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Keys.Return == e.KeyCode) && ((this.xtraTabControl_ГруппаЗакладок.SelectedTabPage != null) && (this.xtraTabControl_ГруппаЗакладок.SelectedTabPage.Controls.Count > 0)))
            {
                this.xtraTabControl_ГруппаЗакладок.SelectedTabPage.Controls[0].Focus();
            }
        }

        private void comboBoxEdit_Закладки_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((-1 != this.comboBoxEdit_Закладки.SelectedIndex) && (this.comboBoxEdit_Закладки.SelectedIndex < this.xtraTabControl_ГруппаЗакладок.TabPages.Count))
            {
                this.xtraTabControl_ГруппаЗакладок.Enabled = false;
                this.xtraTabControl_ГруппаЗакладок.SelectedTabPageIndex = this.comboBoxEdit_Закладки.SelectedIndex;
                this.xtraTabControl_ГруппаЗакладок.Enabled = true;
            }
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(ЭкраннаяФорма));
            this.mainLayout = new LayoutControl();
            this.comboBoxEdit_Закладки = new ComboBoxEdit();
            this.simpleButton_сохранитьИЗакрыть = new SimpleButton();
            this.xtraTabControl_ГруппаЗакладок = new XtraTabControl();
            this.contextMenuStrip_КонтекстноеМенюТабов = new ContextMenuStrip(this.components);
            this.ToolStripMenuItem_выставитьЦвет = new ToolStripMenuItem();
            this.toolStripMenuItem_СдвинутьВкладкуВправо = new ToolStripMenuItem();
            this.сдвинутьВправоНаОдинШагToolStripMenuItem = new ToolStripMenuItem();
            this.сдвинутьВКонецToolStripMenuItem = new ToolStripMenuItem();
            this.toolStripMenuItem_СдвинутьВкладкуВлево = new ToolStripMenuItem();
            this.сдвинутьВлевоНаОдинШагToolStripMenuItem = new ToolStripMenuItem();
            this.сдвинутьВначалоToolStripMenuItem = new ToolStripMenuItem();
            this.toolStripMenuItem_вернутьНастройкиПоУмолчанию = new ToolStripMenuItem();
            this.imageList_наборРисунков = new ImageList(this.components);
            this.xtraTabPage1 = new XtraTabPage();
            this.simpleButton_отмена = new SimpleButton();
            this.simpleButton_применить = new SimpleButton();
            this.mainGroup = new LayoutControlGroup();
            this.layoutControlItem_отмена = new LayoutControlItem();
            this.emptySpaceItem_bottom = new EmptySpaceItem();
            this.layoutControlItem_закладки = new LayoutControlItem();
            this.layoutControlItem_сохранитьИЗакрыть = new LayoutControlItem();
            this.layoutControlItem_применить = new LayoutControlItem();
            this.layoutItem_выборВкладки = new LayoutControlItem();
            this.barManager_main = new BarManager(this.components);
            this.ПанельМеню = new Bar();
            this.barSubItem_func = new BarSubItem();
            this.barButtonItem_inner = new BarButtonItem();
            this.barButtonItem_out = new BarButtonItem();
            this.barSubItem_Сводная = new BarSubItem();
            this.barButtonItem_собрать = new BarButtonItem();
            this.barButtonItemСобратьПоКритериям = new BarButtonItem();
            this.barButtonItem_сверитьСЗеркалом = new BarButtonItem();
            this.barButtonItem_ВосстановитьИзЗеркала = new BarButtonItem();
            this.barSubItem_Архив = new BarSubItem();
            this.barButtonItem_Архив_Сохранить = new BarButtonItem();
            this.barButtonItem_Архив_Список = new BarButtonItem();
            this.barSubItem_ИмпортДанных = new BarSubItem();
            this.barSubItem_ЭкспортДанных = new BarSubItem();
            this.barSubItem2 = new BarSubItem();
            this.barButtonItem_АвтосохраненныеДанные = new BarButtonItem();
            this.barButtonItem4 = new BarButtonItem();
            this.barButtonItem_тестирование = new BarButtonItem();
            this.barSubItem1 = new BarSubItem();
            this.barButtonItem2 = new BarButtonItem();
            this.barButtonItem3 = new BarButtonItem();
            this.barSubItem_ПечатныеФормы = new BarSubItem();
            this.barButtonItem_Справка = new BarButtonItem();
            this.FindTool = new Bar();
            this.barButtonItem_CloseFind = new BarButtonItem();
            this.barStaticItem_Найти = new BarStaticItem();
            this.barEditItem_FindText = new BarEditItem();
            this.repositoryItemTextEdit1 = new RepositoryItemTextEdit();
            this.barButtonItem_FindNext = new BarButtonItem();
            this.barButtonItem_FindPrev = new BarButtonItem();
            this.barEditItem_FindSens = new BarEditItem();
            this.repositoryItemCheckEdit1 = new RepositoryItemCheckEdit();
            this.barEditItem_FindColumnOnly = new BarEditItem();
            this.repositoryItemCheckEdit2 = new RepositoryItemCheckEdit();
            this.ПанельИнструментов = new Bar();
            this.StatusBar = new Bar();
            this.barToolTip = new BarStaticItem();
            this.barDockControlTop = new BarDockControl();
            this.barDockControlBottom = new BarDockControl();
            this.barDockControlLeft = new BarDockControl();
            this.barDockControlRight = new BarDockControl();
            this.imageList_bar = new ImageList(this.components);
            this.barSubItem_дополнительно = new BarSubItem();
            this.barButtonItem_ = new BarButtonItem();
            this.barSubItem_Зеркало = new BarSubItem();
            this.barButtonItem_ОбновитьЗеркало = new BarButtonItem();
            this.barSubItem_Справка = new BarSubItem();
            this.barButtonItem1 = new BarButtonItem();
            this.timer_автосохранение = new Timer(this.components);
            this.mainLayout.BeginInit();
            this.mainLayout.SuspendLayout();
            this.comboBoxEdit_Закладки.Properties.BeginInit();
            this.xtraTabControl_ГруппаЗакладок.BeginInit();
            this.xtraTabControl_ГруппаЗакладок.SuspendLayout();
            this.contextMenuStrip_КонтекстноеМенюТабов.SuspendLayout();
            this.mainGroup.BeginInit();
            this.layoutControlItem_отмена.BeginInit();
            this.emptySpaceItem_bottom.BeginInit();
            this.layoutControlItem_закладки.BeginInit();
            this.layoutControlItem_сохранитьИЗакрыть.BeginInit();
            this.layoutControlItem_применить.BeginInit();
            this.layoutItem_выборВкладки.BeginInit();
            this.barManager_main.BeginInit();
            this.repositoryItemTextEdit1.BeginInit();
            this.repositoryItemCheckEdit1.BeginInit();
            this.repositoryItemCheckEdit2.BeginInit();
            base.SuspendLayout();
            this.mainLayout.Appearance.DisabledLayoutGroupCaption.ForeColor = SystemColors.GrayText;
            this.mainLayout.Appearance.DisabledLayoutGroupCaption.Options.UseForeColor = true;
            this.mainLayout.Appearance.DisabledLayoutItem.ForeColor = SystemColors.GrayText;
            this.mainLayout.Appearance.DisabledLayoutItem.Options.UseForeColor = true;
            this.mainLayout.Controls.Add(this.comboBoxEdit_Закладки);
            this.mainLayout.Controls.Add(this.simpleButton_сохранитьИЗакрыть);
            this.mainLayout.Controls.Add(this.xtraTabControl_ГруппаЗакладок);
            this.mainLayout.Controls.Add(this.simpleButton_отмена);
            this.mainLayout.Controls.Add(this.simpleButton_применить);
            this.mainLayout.Dock = DockStyle.Fill;
            this.mainLayout.Location = new Point(0, 0x33);
            this.mainLayout.Name = "mainLayout";
            this.mainLayout.Root = this.mainGroup;
            this.mainLayout.Size = new Size(0x318, 0x1d8);
            this.mainLayout.TabIndex = 0;
            this.comboBoxEdit_Закладки.Location = new Point(0x5f, 0x1bc);
            this.comboBoxEdit_Закладки.Name = "comboBoxEdit_Закладки";
            this.comboBoxEdit_Закладки.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.comboBoxEdit_Закладки.Size = new Size(0xd5, 20);
            this.comboBoxEdit_Закладки.StyleController = this.mainLayout;
            this.comboBoxEdit_Закладки.TabIndex = 8;
            this.comboBoxEdit_Закладки.SelectedIndexChanged += new EventHandler(this.comboBoxEdit_Закладки_SelectedIndexChanged);
            this.comboBoxEdit_Закладки.KeyDown += new KeyEventHandler(this.comboBoxEdit_Закладки_KeyDown);
            this.simpleButton_сохранитьИЗакрыть.Location = new Point(0x227, 0x1bc);
            this.simpleButton_сохранитьИЗакрыть.Name = "simpleButton_сохранитьИЗакрыть";
            this.simpleButton_сохранитьИЗакрыть.Size = new Size(0x7b, 0x16);
            this.simpleButton_сохранитьИЗакрыть.StyleController = this.mainLayout;
            this.simpleButton_сохранитьИЗакрыть.TabIndex = 7;
            this.simpleButton_сохранитьИЗакрыть.Text = "Сохранить и закрыть";
            this.simpleButton_сохранитьИЗакрыть.Click += new EventHandler(this.simpleButton_сохранитьИЗакрыть_Click);
            this.xtraTabControl_ГруппаЗакладок.ContextMenuStrip = this.contextMenuStrip_КонтекстноеМенюТабов;
            this.xtraTabControl_ГруппаЗакладок.HeaderButtons = TabButtons.Default | TabButtons.Next | TabButtons.Prev;
            this.xtraTabControl_ГруппаЗакладок.Images = this.imageList_наборРисунков;
            this.xtraTabControl_ГруппаЗакладок.Location = new Point(7, 7);
            this.xtraTabControl_ГруппаЗакладок.Name = "xtraTabControl_ГруппаЗакладок";
            this.xtraTabControl_ГруппаЗакладок.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl_ГруппаЗакладок.Size = new Size(0x30b, 0x1aa);
            this.xtraTabControl_ГруппаЗакладок.TabIndex = 6;
            this.xtraTabControl_ГруппаЗакладок.TabPages.AddRange(new XtraTabPage[] { this.xtraTabPage1 });
            this.xtraTabControl_ГруппаЗакладок.SelectedPageChanging += new TabPageChangingEventHandler(this.xtraTabControl_ГруппаЗакладок_SelectedPageChanging);
            this.xtraTabControl_ГруппаЗакладок.SelectedPageChanged += new TabPageChangedEventHandler(this.xtraTabControl_ГруппаЗакладок_SelectedPageChanged);
            this.contextMenuStrip_КонтекстноеМенюТабов.ImageScalingSize = new Size(0x11, 0x11);
            this.contextMenuStrip_КонтекстноеМенюТабов.Items.AddRange(new ToolStripItem[] { this.ToolStripMenuItem_выставитьЦвет, this.toolStripMenuItem_СдвинутьВкладкуВправо, this.toolStripMenuItem_СдвинутьВкладкуВлево, this.toolStripMenuItem_вернутьНастройкиПоУмолчанию });
            this.contextMenuStrip_КонтекстноеМенюТабов.Name = "contextMenuStrip_КонтекстноеМенюТабов";
            this.contextMenuStrip_КонтекстноеМенюТабов.Size = new Size(0xed, 0x7a);
            this.ToolStripMenuItem_выставитьЦвет.Image = (Image) manager.GetObject("ToolStripMenuItem_выставитьЦвет.Image");
            this.ToolStripMenuItem_выставитьЦвет.Name = "ToolStripMenuItem_выставитьЦвет";
            this.ToolStripMenuItem_выставитьЦвет.Size = new Size(0xec, 0x18);
            this.ToolStripMenuItem_выставитьЦвет.Text = "Выставить цвет вкладки";
            this.toolStripMenuItem_СдвинутьВкладкуВправо.DropDownItems.AddRange(new ToolStripItem[] { this.сдвинутьВправоНаОдинШагToolStripMenuItem, this.сдвинутьВКонецToolStripMenuItem });
            this.toolStripMenuItem_СдвинутьВкладкуВправо.Image = (Image) manager.GetObject("toolStripMenuItem_СдвинутьВкладкуВправо.Image");
            this.toolStripMenuItem_СдвинутьВкладкуВправо.Name = "toolStripMenuItem_СдвинутьВкладкуВправо";
            this.toolStripMenuItem_СдвинутьВкладкуВправо.Size = new Size(0xec, 0x18);
            this.toolStripMenuItem_СдвинутьВкладкуВправо.Text = "Сдвинуть вкладку вправо";
            this.сдвинутьВправоНаОдинШагToolStripMenuItem.Image = (Image) manager.GetObject("сдвинутьВправоНаОдинШагToolStripMenuItem.Image");
            this.сдвинутьВправоНаОдинШагToolStripMenuItem.Name = "сдвинутьВправоНаОдинШагToolStripMenuItem";
            this.сдвинутьВправоНаОдинШагToolStripMenuItem.Size = new Size(0xef, 0x18);
            this.сдвинутьВправоНаОдинШагToolStripMenuItem.Text = "Сдвинуть вправо на один шаг";
            this.сдвинутьВправоНаОдинШагToolStripMenuItem.Click += new EventHandler(this.сдвинутьВправоНаОдинШагToolStripMenuItem_Click);
            this.сдвинутьВКонецToolStripMenuItem.Image = (Image) manager.GetObject("сдвинутьВКонецToolStripMenuItem.Image");
            this.сдвинутьВКонецToolStripMenuItem.Name = "сдвинутьВКонецToolStripMenuItem";
            this.сдвинутьВКонецToolStripMenuItem.Size = new Size(0xef, 0x18);
            this.сдвинутьВКонецToolStripMenuItem.Text = "Сдвинуть в конец";
            this.сдвинутьВКонецToolStripMenuItem.Click += new EventHandler(this.сдвинутьВКонецToolStripMenuItem_Click);
            this.toolStripMenuItem_СдвинутьВкладкуВлево.DropDownItems.AddRange(new ToolStripItem[] { this.сдвинутьВлевоНаОдинШагToolStripMenuItem, this.сдвинутьВначалоToolStripMenuItem });
            this.toolStripMenuItem_СдвинутьВкладкуВлево.Image = (Image) manager.GetObject("toolStripMenuItem_СдвинутьВкладкуВлево.Image");
            this.toolStripMenuItem_СдвинутьВкладкуВлево.Name = "toolStripMenuItem_СдвинутьВкладкуВлево";
            this.toolStripMenuItem_СдвинутьВкладкуВлево.Size = new Size(0xec, 0x18);
            this.toolStripMenuItem_СдвинутьВкладкуВлево.Text = "Сдвинуть вкладку влево";
            this.сдвинутьВлевоНаОдинШагToolStripMenuItem.Image = (Image) manager.GetObject("сдвинутьВлевоНаОдинШагToolStripMenuItem.Image");
            this.сдвинутьВлевоНаОдинШагToolStripMenuItem.Name = "сдвинутьВлевоНаОдинШагToolStripMenuItem";
            this.сдвинутьВлевоНаОдинШагToolStripMenuItem.Size = new Size(0xe7, 0x16);
            this.сдвинутьВлевоНаОдинШагToolStripMenuItem.Text = "Сдвинуть влево на один шаг";
            this.сдвинутьВлевоНаОдинШагToolStripMenuItem.Click += new EventHandler(this.сдвинутьВлевоНаОдинШагToolStripMenuItem_Click);
            this.сдвинутьВначалоToolStripMenuItem.Image = (Image) manager.GetObject("сдвинутьВначалоToolStripMenuItem.Image");
            this.сдвинутьВначалоToolStripMenuItem.Name = "сдвинутьВначалоToolStripMenuItem";
            this.сдвинутьВначалоToolStripMenuItem.Size = new Size(0xe7, 0x16);
            this.сдвинутьВначалоToolStripMenuItem.Text = "Сдвинуть вначало";
            this.сдвинутьВначалоToolStripMenuItem.Click += new EventHandler(this.сдвинутьВначалоToolStripMenuItem_Click);
            this.toolStripMenuItem_вернутьНастройкиПоУмолчанию.Image = (Image) manager.GetObject("toolStripMenuItem_вернутьНастройкиПоУмолчанию.Image");
            this.toolStripMenuItem_вернутьНастройкиПоУмолчанию.Name = "toolStripMenuItem_вернутьНастройкиПоУмолчанию";
            this.toolStripMenuItem_вернутьНастройкиПоУмолчанию.Size = new Size(0xec, 0x18);
            this.toolStripMenuItem_вернутьНастройкиПоУмолчанию.Text = "Сбросить настройки вкладок";
            this.toolStripMenuItem_вернутьНастройкиПоУмолчанию.Click += new EventHandler(this.toolStripMenuItem_вернутьНастройкиПоУмолчанию_Click);
            this.imageList_наборРисунков.ImageStream = (ImageListStreamer) manager.GetObject("imageList_наборРисунков.ImageStream");
            this.imageList_наборРисунков.TransparentColor = Color.Transparent;
            this.imageList_наборРисунков.Images.SetKeyName(0, "Черный.ico");
            this.imageList_наборРисунков.Images.SetKeyName(1, "Белый.ico");
            this.imageList_наборРисунков.Images.SetKeyName(2, "Голубой.ico");
            this.imageList_наборРисунков.Images.SetKeyName(3, "Желтый.ico");
            this.imageList_наборРисунков.Images.SetKeyName(4, "Зеленый.ico");
            this.imageList_наборРисунков.Images.SetKeyName(5, "Красный.ico");
            this.imageList_наборРисунков.Images.SetKeyName(6, "Синий.ico");
            this.imageList_наборРисунков.Images.SetKeyName(7, "Сиреневый.ico");
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new Size(770, 0x18b);
            this.xtraTabPage1.Text = "xtraTabPage1";
            this.simpleButton_отмена.DialogResult = DialogResult.Cancel;
            this.simpleButton_отмена.Location = new Point(0x2ad, 0x1bc);
            this.simpleButton_отмена.Name = "simpleButton_отмена";
            this.simpleButton_отмена.Size = new Size(0x65, 0x16);
            this.simpleButton_отмена.StyleController = this.mainLayout;
            this.simpleButton_отмена.TabIndex = 4;
            this.simpleButton_отмена.Text = "Закрыть";
            this.simpleButton_отмена.Click += new EventHandler(this.simpleButton_отмена_Click);
            this.simpleButton_применить.Location = new Point(0x1b7, 0x1bc);
            this.simpleButton_применить.Name = "simpleButton_применить";
            this.simpleButton_применить.Size = new Size(0x65, 0x16);
            this.simpleButton_применить.StyleController = this.mainLayout;
            this.simpleButton_применить.TabIndex = 5;
            this.simpleButton_применить.Text = "Сохранить";
            this.simpleButton_применить.Click += new EventHandler(this.simpleButton_применить_Click);
            this.mainGroup.CustomizationFormText = "mainGroup";
            this.mainGroup.Items.AddRange(new BaseLayoutItem[] { this.layoutControlItem_отмена, this.emptySpaceItem_bottom, this.layoutControlItem_закладки, this.layoutControlItem_сохранитьИЗакрыть, this.layoutControlItem_применить, this.layoutItem_выборВкладки });
            this.mainGroup.Location = new Point(0, 0);
            this.mainGroup.Name = "mainGroup";
            this.mainGroup.Size = new Size(0x318, 0x1d8);
            this.mainGroup.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.mainGroup.Text = "mainGroup";
            this.mainGroup.TextVisible = false;
            this.layoutControlItem_отмена.AllowHide = false;
            this.layoutControlItem_отмена.Control = this.simpleButton_отмена;
            this.layoutControlItem_отмена.CustomizationFormText = "layoutControlItem_отмена";
            this.layoutControlItem_отмена.Location = new Point(0x2a6, 0x1b5);
            this.layoutControlItem_отмена.MaxSize = new Size(0x70, 0x21);
            this.layoutControlItem_отмена.MinSize = new Size(0x70, 0x21);
            this.layoutControlItem_отмена.Name = "layoutControlItem_отмена";
            this.layoutControlItem_отмена.Size = new Size(0x70, 0x21);
            this.layoutControlItem_отмена.SizeConstraintsType = SizeConstraintsType.Custom;
            this.layoutControlItem_отмена.Text = "layoutControlItem_отмена";
            this.layoutControlItem_отмена.TextLocation = Locations.Left;
            this.layoutControlItem_отмена.TextSize = new Size(0, 0);
            this.layoutControlItem_отмена.TextToControlDistance = 0;
            this.layoutControlItem_отмена.TextVisible = false;
            this.emptySpaceItem_bottom.CustomizationFormText = "emptySpaceItem_bottom";
            this.emptySpaceItem_bottom.Location = new Point(0x138, 0x1b5);
            this.emptySpaceItem_bottom.Name = "emptySpaceItem_bottom";
            this.emptySpaceItem_bottom.Size = new Size(120, 0x21);
            this.emptySpaceItem_bottom.Tag = "";
            this.emptySpaceItem_bottom.Text = "emptySpaceItem_bottom";
            this.emptySpaceItem_bottom.TextSize = new Size(0, 0);
            this.layoutControlItem_закладки.Control = this.xtraTabControl_ГруппаЗакладок;
            this.layoutControlItem_закладки.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem_закладки.Location = new Point(0, 0);
            this.layoutControlItem_закладки.Name = "layoutControlItem_закладки";
            this.layoutControlItem_закладки.Size = new Size(790, 0x1b5);
            this.layoutControlItem_закладки.Text = "layoutControlItem_закладки";
            this.layoutControlItem_закладки.TextLocation = Locations.Left;
            this.layoutControlItem_закладки.TextSize = new Size(0, 0);
            this.layoutControlItem_закладки.TextToControlDistance = 0;
            this.layoutControlItem_закладки.TextVisible = false;
            this.layoutControlItem_сохранитьИЗакрыть.AllowHide = false;
            this.layoutControlItem_сохранитьИЗакрыть.Control = this.simpleButton_сохранитьИЗакрыть;
            this.layoutControlItem_сохранитьИЗакрыть.CustomizationFormText = "layoutControlItem_сохранитьИЗакрыть";
            this.layoutControlItem_сохранитьИЗакрыть.Location = new Point(0x220, 0x1b5);
            this.layoutControlItem_сохранитьИЗакрыть.MaxSize = new Size(0x86, 0x21);
            this.layoutControlItem_сохранитьИЗакрыть.MinSize = new Size(0x86, 0x21);
            this.layoutControlItem_сохранитьИЗакрыть.Name = "layoutControlItem_сохранитьИЗакрыть";
            this.layoutControlItem_сохранитьИЗакрыть.Size = new Size(0x86, 0x21);
            this.layoutControlItem_сохранитьИЗакрыть.SizeConstraintsType = SizeConstraintsType.Custom;
            this.layoutControlItem_сохранитьИЗакрыть.Text = "layoutControlItem_сохранитьИЗакрыть";
            this.layoutControlItem_сохранитьИЗакрыть.TextLocation = Locations.Left;
            this.layoutControlItem_сохранитьИЗакрыть.TextSize = new Size(0, 0);
            this.layoutControlItem_сохранитьИЗакрыть.TextToControlDistance = 0;
            this.layoutControlItem_сохранитьИЗакрыть.TextVisible = false;
            this.layoutControlItem_применить.AllowHide = false;
            this.layoutControlItem_применить.Control = this.simpleButton_применить;
            this.layoutControlItem_применить.CustomizationFormText = "layoutControlItem_применить";
            this.layoutControlItem_применить.Location = new Point(0x1b0, 0x1b5);
            this.layoutControlItem_применить.MaxSize = new Size(0x70, 0x21);
            this.layoutControlItem_применить.MinSize = new Size(0x70, 0x21);
            this.layoutControlItem_применить.Name = "layoutControlItem_применить";
            this.layoutControlItem_применить.Size = new Size(0x70, 0x21);
            this.layoutControlItem_применить.SizeConstraintsType = SizeConstraintsType.Custom;
            this.layoutControlItem_применить.Text = "layoutControlItem_применить";
            this.layoutControlItem_применить.TextLocation = Locations.Left;
            this.layoutControlItem_применить.TextSize = new Size(0, 0);
            this.layoutControlItem_применить.TextToControlDistance = 0;
            this.layoutControlItem_применить.TextVisible = false;
            this.layoutItem_выборВкладки.Control = this.comboBoxEdit_Закладки;
            this.layoutItem_выборВкладки.CustomizationFormText = "layoutItem_выборВкладки";
            this.layoutItem_выборВкладки.Location = new Point(0, 0x1b5);
            this.layoutItem_выборВкладки.Name = "layoutItem_выборВкладки";
            this.layoutItem_выборВкладки.Size = new Size(0x138, 0x21);
            this.layoutItem_выборВкладки.Text = "Выбор закладки";
            this.layoutItem_выборВкладки.TextLocation = Locations.Left;
            this.layoutItem_выборВкладки.TextSize = new Size(0x53, 20);
            this.barManager_main.AllowShowToolbarsPopup = false;
            this.barManager_main.Bars.AddRange(new Bar[] { this.ПанельМеню, this.FindTool, this.ПанельИнструментов, this.StatusBar });
            this.barManager_main.DockControls.Add(this.barDockControlTop);
            this.barManager_main.DockControls.Add(this.barDockControlBottom);
            this.barManager_main.DockControls.Add(this.barDockControlLeft);
            this.barManager_main.DockControls.Add(this.barDockControlRight);
            this.barManager_main.Form = this;
            this.barManager_main.Images = this.imageList_bar;
            this.barManager_main.Items.AddRange(new BarItem[] { 
                this.barSubItem_func, this.barButtonItem_inner, this.barButtonItem_out, this.barSubItem_Сводная, this.barButtonItem_собрать, this.barSubItem_ПечатныеФормы, this.barSubItem_дополнительно, this.barSubItem_ИмпортДанных, this.barButtonItem_, this.barSubItem_ЭкспортДанных, this.barToolTip, this.barButtonItem_АвтосохраненныеДанные, this.barButtonItem_CloseFind, this.barEditItem_FindText, this.barStaticItem_Найти, this.barButtonItem_FindNext, 
                this.barButtonItem_FindPrev, this.barEditItem_FindSens, this.barEditItem_FindColumnOnly, this.barSubItem_Архив, this.barButtonItem_Архив_Сохранить, this.barButtonItem_Архив_Список, this.barButtonItem_сверитьСЗеркалом, this.barSubItem_Зеркало, this.barButtonItem_ОбновитьЗеркало, this.barButtonItem_ВосстановитьИзЗеркала, this.barButtonItem_тестирование, this.barSubItem_Справка, this.barButtonItem_Справка, this.barButtonItem1, this.barSubItem1, this.barButtonItem2, 
                this.barButtonItem3, this.barButtonItemСобратьПоКритериям, this.barButtonItem4, this.barSubItem2
             });
            this.barManager_main.MaxItemId = 40;
            this.barManager_main.RepositoryItems.AddRange(new RepositoryItem[] { this.repositoryItemTextEdit1, this.repositoryItemCheckEdit1, this.repositoryItemCheckEdit2 });
            this.barManager_main.ShowScreenTipsInToolbars = false;
            this.barManager_main.StatusBar = this.StatusBar;
            this.ПанельМеню.BarName = "MainMenu";
            this.ПанельМеню.DockCol = 0;
            this.ПанельМеню.DockRow = 0;
            this.ПанельМеню.DockStyle = BarDockStyle.Top;
            this.ПанельМеню.LinksPersistInfo.AddRange(new LinkPersistInfo[] { new LinkPersistInfo(BarLinkUserDefines.PaintStyle, this.barSubItem_func, "", true, true, true, 0, null, BarItemPaintStyle.CaptionGlyph), new LinkPersistInfo(BarLinkUserDefines.PaintStyle, this.barSubItem_ПечатныеФормы, BarItemPaintStyle.CaptionGlyph), new LinkPersistInfo(BarLinkUserDefines.PaintStyle, this.barSubItem_дополнительно, BarItemPaintStyle.CaptionGlyph), new LinkPersistInfo(this.barButtonItem_Справка) });
            this.ПанельМеню.OptionsBar.AllowQuickCustomization = false;
            this.ПанельМеню.OptionsBar.DisableCustomization = true;
            this.ПанельМеню.OptionsBar.DrawDragBorder = false;
            this.ПанельМеню.OptionsBar.UseWholeRow = true;
            this.ПанельМеню.Text = "MainMenu";
            this.barSubItem_func.Caption = "Функции";
            this.barSubItem_func.Glyph = (Image) manager.GetObject("barSubItem_func.Glyph");
            this.barSubItem_func.Id = 0;
            this.barSubItem_func.LinksPersistInfo.AddRange(new LinkPersistInfo[] { new LinkPersistInfo(BarLinkUserDefines.PaintStyle, this.barButtonItem_inner, BarItemPaintStyle.CaptionGlyph), new LinkPersistInfo(BarLinkUserDefines.PaintStyle, this.barButtonItem_out, BarItemPaintStyle.CaptionGlyph), new LinkPersistInfo(BarLinkUserDefines.PaintStyle, this.barSubItem_Сводная, "", true, true, true, 0, null, BarItemPaintStyle.CaptionGlyph), new LinkPersistInfo(this.barSubItem_Архив, true), new LinkPersistInfo(this.barSubItem_ИмпортДанных, true), new LinkPersistInfo(this.barSubItem_ЭкспортДанных), new LinkPersistInfo(this.barSubItem2), new LinkPersistInfo(this.barButtonItem_тестирование), new LinkPersistInfo(this.barSubItem1) });
            this.barSubItem_func.Name = "barSubItem_func";
            this.barSubItem_func.PaintStyle = BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem_inner.Caption = "Проверить внутриформенные увязки";
            this.barButtonItem_inner.Glyph = (Image) manager.GetObject("barButtonItem_inner.Glyph");
            this.barButtonItem_inner.Id = 1;
            this.barButtonItem_inner.Name = "barButtonItem_inner";
            this.barButtonItem_inner.ItemClick += new ItemClickEventHandler(this.barButtonItem_inner_ItemClick);
            this.barButtonItem_out.Caption = "Проверить межформенные увязки";
            this.barButtonItem_out.Glyph = (Image) manager.GetObject("barButtonItem_out.Glyph");
            this.barButtonItem_out.Id = 2;
            this.barButtonItem_out.Name = "barButtonItem_out";
            this.barButtonItem_out.ItemClick += new ItemClickEventHandler(this.barButtonItem_out_ItemClick);
            this.barSubItem_Сводная.Caption = "Сводная форма";
            this.barSubItem_Сводная.Glyph = (Image) manager.GetObject("barSubItem_Сводная.Glyph");
            this.barSubItem_Сводная.Id = 3;
            this.barSubItem_Сводная.LinksPersistInfo.AddRange(new LinkPersistInfo[] { new LinkPersistInfo(BarLinkUserDefines.PaintStyle, this.barButtonItem_собрать, BarItemPaintStyle.CaptionGlyph), new LinkPersistInfo(this.barButtonItemСобратьПоКритериям), new LinkPersistInfo(this.barButtonItem_сверитьСЗеркалом), new LinkPersistInfo(this.barButtonItem_ВосстановитьИзЗеркала) });
            this.barSubItem_Сводная.Name = "barSubItem_Сводная";
            this.barButtonItem_собрать.Caption = "Собрать сводную форму";
            this.barButtonItem_собрать.Glyph = (Image) manager.GetObject("barButtonItem_собрать.Glyph");
            this.barButtonItem_собрать.Id = 4;
            this.barButtonItem_собрать.Name = "barButtonItem_собрать";
            this.barButtonItem_собрать.ItemClick += new ItemClickEventHandler(this.barButtonItem_собрать_ItemClick);
            this.barButtonItemСобратьПоКритериям.Caption = "Собрать сводную по критериям";
            this.barButtonItemСобратьПоКритериям.Id = 0x25;
            this.barButtonItemСобратьПоКритериям.Name = "barButtonItemСобратьПоКритериям";
            this.barButtonItemСобратьПоКритериям.ItemClick += new ItemClickEventHandler(this.barButtonItemСобратьПоКритериям_ItemClick);
            this.barButtonItem_сверитьСЗеркалом.Caption = "Сверить с данными предыдущей сборки";
            this.barButtonItem_сверитьСЗеркалом.Id = 0x1a;
            this.barButtonItem_сверитьСЗеркалом.Name = "barButtonItem_сверитьСЗеркалом";
            this.barButtonItem_сверитьСЗеркалом.ItemClick += new ItemClickEventHandler(this.barButtonItem_сверитьСЗеркалом_ItemClick);
            this.barButtonItem_ВосстановитьИзЗеркала.Caption = "Восстановить из данных предыдущей сборки";
            this.barButtonItem_ВосстановитьИзЗеркала.Id = 0x1d;
            this.barButtonItem_ВосстановитьИзЗеркала.Name = "barButtonItem_ВосстановитьИзЗеркала";
            this.barButtonItem_ВосстановитьИзЗеркала.ItemClick += new ItemClickEventHandler(this.barButtonItem_ВосстановитьИзЗеркала_ItemClick);
            this.barSubItem_Архив.Caption = "Архив данных";
            this.barSubItem_Архив.Glyph = Resources.pack;
            this.barSubItem_Архив.Id = 0x17;
            this.barSubItem_Архив.LinksPersistInfo.AddRange(new LinkPersistInfo[] { new LinkPersistInfo(this.barButtonItem_Архив_Сохранить), new LinkPersistInfo(this.barButtonItem_Архив_Список) });
            this.barSubItem_Архив.Name = "barSubItem_Архив";
            this.barButtonItem_Архив_Сохранить.Caption = "Сохранить данные";
            this.barButtonItem_Архив_Сохранить.Glyph = Resources.save;
            this.barButtonItem_Архив_Сохранить.Id = 0x18;
            this.barButtonItem_Архив_Сохранить.Name = "barButtonItem_Архив_Сохранить";
            this.barButtonItem_Архив_Сохранить.ItemClick += new ItemClickEventHandler(this.barButtonItem_Архив_Сохранить_ItemClick);
            this.barButtonItem_Архив_Список.Caption = "Загрузить из архива";
            this.barButtonItem_Архив_Список.Glyph = Resources.unpack;
            this.barButtonItem_Архив_Список.Id = 0x19;
            this.barButtonItem_Архив_Список.Name = "barButtonItem_Архив_Список";
            this.barButtonItem_Архив_Список.ItemClick += new ItemClickEventHandler(this.barButtonItem_Архив_Список_ItemClick);
            this.barSubItem_ИмпортДанных.Caption = "Импорт данных";
            this.barSubItem_ИмпортДанных.Glyph = (Image) manager.GetObject("barSubItem_ИмпортДанных.Glyph");
            this.barSubItem_ИмпортДанных.Id = 9;
            this.barSubItem_ИмпортДанных.Name = "barSubItem_ИмпортДанных";
            this.barSubItem_ЭкспортДанных.Caption = "Экспорт данных";
            this.barSubItem_ЭкспортДанных.Enabled = false;
            this.barSubItem_ЭкспортДанных.Glyph = (Image) manager.GetObject("barSubItem_ЭкспортДанных.Glyph");
            this.barSubItem_ЭкспортДанных.Id = 12;
            this.barSubItem_ЭкспортДанных.Name = "barSubItem_ЭкспортДанных";
            this.barSubItem2.Caption = "Данные";
            this.barSubItem2.Glyph = Resources.TableHS;
            this.barSubItem2.Id = 0x27;
            this.barSubItem2.LinksPersistInfo.AddRange(new LinkPersistInfo[] { new LinkPersistInfo(this.barButtonItem_АвтосохраненныеДанные), new LinkPersistInfo(this.barButtonItem4) });
            this.barSubItem2.Name = "barSubItem2";
            this.barButtonItem_АвтосохраненныеДанные.Caption = "Восстановление данных";
            this.barButtonItem_АвтосохраненныеДанные.Glyph = (Image) manager.GetObject("barButtonItem_АвтосохраненныеДанные.Glyph");
            this.barButtonItem_АвтосохраненныеДанные.Id = 15;
            this.barButtonItem_АвтосохраненныеДанные.Name = "barButtonItem_АвтосохраненныеДанные";
            this.barButtonItem_АвтосохраненныеДанные.ItemClick += new ItemClickEventHandler(this.barButtonItem_АвтосохраненныеДанные_ItemClick);
            this.barButtonItem4.Caption = "Папка данных";
            this.barButtonItem4.Glyph = Resources.folder;
            this.barButtonItem4.Id = 0x26;
            this.barButtonItem4.Name = "barButtonItem4";
            this.barButtonItem4.ItemClick += new ItemClickEventHandler(this.barButtonItem4_ItemClick);
            this.barButtonItem_тестирование.Caption = "Тестирование формы";
            this.barButtonItem_тестирование.Glyph = Resources.settings;
            this.barButtonItem_тестирование.Id = 30;
            this.barButtonItem_тестирование.Name = "barButtonItem_тестирование";
            this.barButtonItem_тестирование.Visibility = BarItemVisibility.Never;
            this.barButtonItem_тестирование.ItemClick += new ItemClickEventHandler(this.barButtonItem_тестирование_ItemClick);
            this.barSubItem1.Caption = "Очистить кеш справочников";
            this.barSubItem1.Glyph = Resources.trash;
            this.barSubItem1.Id = 0x22;
            this.barSubItem1.LinksPersistInfo.AddRange(new LinkPersistInfo[] { new LinkPersistInfo(BarLinkUserDefines.PaintStyle, this.barButtonItem2, BarItemPaintStyle.Standard), new LinkPersistInfo(this.barButtonItem3) });
            this.barSubItem1.Name = "barSubItem1";
            this.barButtonItem2.Caption = "Очистить кэш справочников текущей формы";
            this.barButtonItem2.Glyph = Resources.trash;
            this.barButtonItem2.Id = 0x23;
            this.barButtonItem2.Name = "barButtonItem2";
            this.barButtonItem2.ItemClick += new ItemClickEventHandler(this.barButtonItem2_ItemClick);
            this.barButtonItem3.Caption = "Очистить весь кэш справочников";
            this.barButtonItem3.Glyph = Resources.trash;
            this.barButtonItem3.Id = 0x24;
            this.barButtonItem3.Name = "barButtonItem3";
            this.barButtonItem3.ItemClick += new ItemClickEventHandler(this.barButtonItem3_ItemClick);
            this.barSubItem_ПечатныеФормы.Caption = "Печатные формы";
            this.barSubItem_ПечатныеФормы.Glyph = (Image) manager.GetObject("barSubItem_ПечатныеФормы.Glyph");
            this.barSubItem_ПечатныеФормы.Id = 5;
            this.barSubItem_ПечатныеФормы.Name = "barSubItem_ПечатныеФормы";
            this.barSubItem_ПечатныеФормы.PaintStyle = BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem_Справка.Caption = "Справка";
            this.barButtonItem_Справка.Glyph = (Image) manager.GetObject("barButtonItem_Справка.Glyph");
            this.barButtonItem_Справка.Id = 0x20;
            this.barButtonItem_Справка.ItemShortcut = new BarShortcut(Keys.F1);
            this.barButtonItem_Справка.Name = "barButtonItem_Справка";
            this.barButtonItem_Справка.PaintStyle = BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem_Справка.ItemClick += new ItemClickEventHandler(this.barButtonItem_Справка_ItemClick);
            this.FindTool.BarName = "FindTool";
            this.FindTool.DockCol = 0;
            this.FindTool.DockRow = 0;
            this.FindTool.DockStyle = BarDockStyle.Bottom;
            this.FindTool.LinksPersistInfo.AddRange(new LinkPersistInfo[] { new LinkPersistInfo(BarLinkUserDefines.PaintStyle, this.barButtonItem_CloseFind, BarItemPaintStyle.CaptionGlyph), new LinkPersistInfo(this.barStaticItem_Найти), new LinkPersistInfo(BarLinkUserDefines.Width, this.barEditItem_FindText, "", false, true, true, 0x8d), new LinkPersistInfo(BarLinkUserDefines.PaintStyle, this.barButtonItem_FindNext, BarItemPaintStyle.CaptionGlyph), new LinkPersistInfo(BarLinkUserDefines.PaintStyle, this.barButtonItem_FindPrev, BarItemPaintStyle.CaptionGlyph), new LinkPersistInfo(BarLinkUserDefines.Width | BarLinkUserDefines.PaintStyle, this.barEditItem_FindSens, "", false, true, true, 0x7e, null, BarItemPaintStyle.Caption), new LinkPersistInfo(BarLinkUserDefines.Width | BarLinkUserDefines.PaintStyle, this.barEditItem_FindColumnOnly, "", false, true, true, 0x7f, null, BarItemPaintStyle.Standard) });
            this.FindTool.OptionsBar.AllowQuickCustomization = false;
            this.FindTool.OptionsBar.DrawDragBorder = false;
            this.FindTool.OptionsBar.UseWholeRow = true;
            this.FindTool.Text = "FindTool";
            this.barButtonItem_CloseFind.Id = 0x10;
            this.barButtonItem_CloseFind.ImageIndex = 2;
            this.barButtonItem_CloseFind.Name = "barButtonItem_CloseFind";
            this.barButtonItem_CloseFind.ItemClick += new ItemClickEventHandler(this.barButtonItem_CloseFind_ItemClick);
            this.barStaticItem_Найти.Border = BorderStyles.NoBorder;
            this.barStaticItem_Найти.Caption = "Найти:";
            this.barStaticItem_Найти.Id = 0x12;
            this.barStaticItem_Найти.Name = "barStaticItem_Найти";
            this.barStaticItem_Найти.TextAlignment = StringAlignment.Near;
            this.barEditItem_FindText.Edit = this.repositoryItemTextEdit1;
            this.barEditItem_FindText.Id = 0x11;
            this.barEditItem_FindText.Name = "barEditItem_FindText";
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            this.barButtonItem_FindNext.Caption = "Следующее";
            this.barButtonItem_FindNext.Id = 0x13;
            this.barButtonItem_FindNext.ImageIndex = 0;
            this.barButtonItem_FindNext.Name = "barButtonItem_FindNext";
            this.barButtonItem_FindNext.ItemClick += new ItemClickEventHandler(this.barButtonItem_FindNext_ItemClick);
            this.barButtonItem_FindPrev.Caption = "Предыдущее";
            this.barButtonItem_FindPrev.Id = 20;
            this.barButtonItem_FindPrev.ImageIndex = 1;
            this.barButtonItem_FindPrev.Name = "barButtonItem_FindPrev";
            this.barButtonItem_FindPrev.ItemClick += new ItemClickEventHandler(this.barButtonItem_FindPrev_ItemClick);
            this.barEditItem_FindSens.Edit = this.repositoryItemCheckEdit1;
            this.barEditItem_FindSens.Id = 0x15;
            this.barEditItem_FindSens.Name = "barEditItem_FindSens";
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Caption = "С учетом регистра";
            this.repositoryItemCheckEdit1.GlyphAlignment = HorzAlignment.Near;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.NullStyle = StyleIndeterminate.Unchecked;
            this.repositoryItemCheckEdit1.ValueGrayed = false;
            this.barEditItem_FindColumnOnly.Edit = this.repositoryItemCheckEdit2;
            this.barEditItem_FindColumnOnly.Id = 0x16;
            this.barEditItem_FindColumnOnly.Name = "barEditItem_FindColumnOnly";
            this.repositoryItemCheckEdit2.AutoHeight = false;
            this.repositoryItemCheckEdit2.Caption = "Только по столбцу";
            this.repositoryItemCheckEdit2.GlyphAlignment = HorzAlignment.Near;
            this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            this.repositoryItemCheckEdit2.NullStyle = StyleIndeterminate.Unchecked;
            this.ПанельИнструментов.BarName = "Custom 5";
            this.ПанельИнструментов.DockCol = 0;
            this.ПанельИнструментов.DockRow = 1;
            this.ПанельИнструментов.DockStyle = BarDockStyle.Top;
            this.ПанельИнструментов.OptionsBar.AllowQuickCustomization = false;
            this.ПанельИнструментов.OptionsBar.DisableClose = true;
            this.ПанельИнструментов.OptionsBar.DisableCustomization = true;
            this.ПанельИнструментов.OptionsBar.DrawDragBorder = false;
            this.ПанельИнструментов.OptionsBar.UseWholeRow = true;
            this.ПанельИнструментов.Text = "Custom 5";
            this.StatusBar.BarName = "StatusBar";
            this.StatusBar.CanDockStyle = BarCanDockStyle.Bottom;
            this.StatusBar.DockCol = 0;
            this.StatusBar.DockRow = 1;
            this.StatusBar.DockStyle = BarDockStyle.Bottom;
            this.StatusBar.LinksPersistInfo.AddRange(new LinkPersistInfo[] { new LinkPersistInfo(this.barToolTip) });
            this.StatusBar.OptionsBar.AllowQuickCustomization = false;
            this.StatusBar.OptionsBar.DrawDragBorder = false;
            this.StatusBar.OptionsBar.UseWholeRow = true;
            this.StatusBar.Text = "Custom 6";
            this.barToolTip.AutoSize = BarStaticItemSize.Spring;
            this.barToolTip.Id = 13;
            this.barToolTip.Name = "barToolTip";
            this.barToolTip.OwnFont = new Font("Tahoma", 11f, FontStyle.Bold, GraphicsUnit.World);
            this.barToolTip.ShowImageInToolbar = false;
            this.barToolTip.TextAlignment = StringAlignment.Near;
            this.barToolTip.UseOwnFont = true;
            this.barToolTip.Width = 0x20;
            this.imageList_bar.ImageStream = (ImageListStreamer) manager.GetObject("imageList_bar.ImageStream");
            this.imageList_bar.TransparentColor = Color.Transparent;
            this.imageList_bar.Images.SetKeyName(0, "down.png");
            this.imageList_bar.Images.SetKeyName(1, "up.png");
            this.imageList_bar.Images.SetKeyName(2, "remove.png");
            this.barSubItem_дополнительно.Caption = "Обработки";
            this.barSubItem_дополнительно.Glyph = (Image) manager.GetObject("barSubItem_дополнительно.Glyph");
            this.barSubItem_дополнительно.Id = 6;
            this.barSubItem_дополнительно.Name = "barSubItem_дополнительно";
            this.barSubItem_дополнительно.PaintStyle = BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem_.Caption = "barButtonItem1";
            this.barButtonItem_.Id = 11;
            this.barButtonItem_.Name = "barButtonItem_";
            this.barSubItem_Зеркало.Caption = "Зеркало данных";
            this.barSubItem_Зеркало.Id = 0x1b;
            this.barSubItem_Зеркало.LinksPersistInfo.AddRange(new LinkPersistInfo[] { new LinkPersistInfo(this.barButtonItem_ОбновитьЗеркало) });
            this.barSubItem_Зеркало.Name = "barSubItem_Зеркало";
            this.barButtonItem_ОбновитьЗеркало.Caption = "Обновить зеркало";
            this.barButtonItem_ОбновитьЗеркало.Id = 0x1c;
            this.barButtonItem_ОбновитьЗеркало.Name = "barButtonItem_ОбновитьЗеркало";
            this.barSubItem_Справка.Caption = "Справка";
            this.barSubItem_Справка.Glyph = (Image) manager.GetObject("barSubItem_Справка.Glyph");
            this.barSubItem_Справка.Id = 0x1f;
            this.barSubItem_Справка.ItemShortcut = new BarShortcut(Keys.F1);
            this.barSubItem_Справка.Name = "barSubItem_Справка";
            this.barSubItem_Справка.PaintStyle = BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem1.Caption = "Очистить кеш справочников";
            this.barButtonItem1.Id = 0x21;
            this.barButtonItem1.Name = "barButtonItem1";
            this.timer_автосохранение.Interval = 0x493e0;
            this.timer_автосохранение.Tick += new EventHandler(this.timer1_Tick);
            base.CancelButton = this.simpleButton_отмена;
            base.ClientSize = new Size(0x318, 0x23d);
            base.Controls.Add(this.mainLayout);
            base.Controls.Add(this.barDockControlLeft);
            base.Controls.Add(this.barDockControlRight);
            base.Controls.Add(this.barDockControlBottom);
            base.Controls.Add(this.barDockControlTop);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.KeyPreview = true;
            base.Name = "ЭкраннаяФорма";
            base.StartPosition = FormStartPosition.CenterScreen;
            base.WindowState = FormWindowState.Maximized;
            base.Load += new EventHandler(this.ЭкраннаяФорма_Load);
            base.KeyUp += new KeyEventHandler(this.ЭкраннаяФорма_KeyUp);
            base.FormClosing += new FormClosingEventHandler(this.ЭкраннаяФорма_FormClosing);
            this.mainLayout.EndInit();
            this.mainLayout.ResumeLayout(false);
            this.comboBoxEdit_Закладки.Properties.EndInit();
            this.xtraTabControl_ГруппаЗакладок.EndInit();
            this.xtraTabControl_ГруппаЗакладок.ResumeLayout(false);
            this.contextMenuStrip_КонтекстноеМенюТабов.ResumeLayout(false);
            this.mainGroup.EndInit();
            this.layoutControlItem_отмена.EndInit();
            this.emptySpaceItem_bottom.EndInit();
            this.layoutControlItem_закладки.EndInit();
            this.layoutControlItem_сохранитьИЗакрыть.EndInit();
            this.layoutControlItem_применить.EndInit();
            this.layoutItem_выборВкладки.EndInit();
            this.barManager_main.EndInit();
            this.repositoryItemTextEdit1.EndInit();
            this.repositoryItemCheckEdit1.EndInit();
            this.repositoryItemCheckEdit2.EndInit();
            base.ResumeLayout(false);
        }

        private void simpleButton_отмена_Click(object sender, EventArgs e)
        {
            if (this.ДанныеИзменились)
            {
                if (!this.ОтчетнаяФорма.ОбработатьСобытие_ЗакрытиеФормыСОтказомОтСохранения())
                {
                    base.Close();
                }
            }
            else
            {
                base.Close();
            }
        }

        public void simpleButton_применить_Click(object sender, EventArgs e)
        {
            РезультатыВыполненияСверкиДанных данных;
            if (!(sender is ФормаДинамическойТаблицы))
            {
                this.simpleButton_применить.Focus();
            }
            List<СтрокаОтчетаСверкиДанных> list = new List<СтрокаОтчетаСверкиДанных>();
            int num = 0;
            int num2 = 0;
            if (!this.ПроверитьКорректностьДанных(ref list, ref num, ref num2))
            {
                данных = new РезультатыВыполненияСверкиДанных();
                данных.Заголовок = string.Format("Во время проверки данных были обнаружены ошибки и предупреждения (количество ошибок: {0}, количество предупреждений: {1})", num, num2);
                данных.Подзаголовок = "Так как форма содержит ошибки, форма сохранена не будет.";
                данных.СтрокиСверки.AddRange(list);
                данных.СброситьСписокСтолбцов();
                данных.ДобавитьОтображаемыйСтолбец("Таблица", "Субтаблица");
                данных.ДобавитьОтображаемыйСтолбец("Столбец", "Столбец");
                данных.ДобавитьОтображаемыйСтолбец("Строка", "Строка");
                данных.ДобавитьОтображаемыйСтолбец("Условие", "Условие");
                данных.ДобавитьОтображаемыйСтолбец("Ошибка", "ТипОшибки");
                данных.ДобавитьОтображаемыйСтолбец("Сохранение разрешено", "СохранениеРазрешено");
                ФормаОтчетаСверкиДанных данных2 = new ФормаОтчетаСверкиДанных(данных);
                данных2.ПоказатьДиалог();
            }
            else
            {
                Exception exception;
                if ((list != null) && (list.Count > 0))
                {
                    данных = new РезультатыВыполненияСверкиДанных();
                    данных.Заголовок = string.Format("Во время проверки данных были обнаружены предупреждения (количество предупреждений: {0})", num2);
                    данных.Подзаголовок = "Так как форма содержит только предупреждения, форма будет сохранена.";
                    данных.СтрокиСверки.AddRange(list);
                    данных.СброситьСписокСтолбцов();
                    данных.ДобавитьОтображаемыйСтолбец("Таблица", "Субтаблица");
                    данных.ДобавитьОтображаемыйСтолбец("Столбец", "Столбец");
                    данных.ДобавитьОтображаемыйСтолбец("Строка", "Строка");
                    данных.ДобавитьОтображаемыйСтолбец("Условие", "Условие");
                    данных.ДобавитьОтображаемыйСтолбец("Ошибка", "ТипОшибки");
                    данных.ДобавитьОтображаемыйСтолбец("Сохранение разрешено", "СохранениеРазрешено");
                    new ФормаОтчетаСверкиДанных(данных).ПоказатьДиалог();
                }
                try
                {
                    new АвтосохранениеДанныхОтчетнойФормы(this.ОтчетнаяФорма).СохранитьДанныеФормыАвтоматически(true);
                }
                catch (Exception)
                {
                }
                bool flag = true;
                try
                {
                    flag = !this.ОтчетнаяФорма.ОбработатьСобытие_ДоСохраненияФормы();
                }
                catch (Exception exception2)
                {
                    exception = exception2;
                    Сообщение.ПоказатьИсключительнуюСитуацию("В методе 'Обработать событие до сохранения формы' возникла исключительная ситуация", exception);
                    flag = false;
                }
                if (flag)
                {
                    this.ОбновитьДанные();
                    Application.DoEvents();
                    this.ПересчитатьАвтоблоки();
                    Application.DoEvents();
                    ФормаИндикаторПроцесса процесса = new ФормаИндикаторПроцесса();
                    процесса.TopMost = false;
                    процесса.УстановитьЗначениеИндикатора(0, "Сохранение данных...");
                    процесса.Show(this);
                    try
                    {
                        try
                        {
                            МенеджерБД.ПроверитьСоединение();
                            this.СохранитьДанные(new КонтекстСохраненияОтчетнойФормы(false, true));
                            ХранилищеДанныхФорм.ПроверитьСохранениеДанных(this.ОтчетнаяФорма.Данные.Идентификатор);
                            процесса.Закрыть();
                            this.ОбновитьДанные();
                            Сообщение.Показать("Форма успешно сохранена!", "Сохранение...");
                        }
                        catch (Exception exception3)
                        {
                            exception = exception3;
                            процесса.Закрыть();
                            if ((exception.Message.Contains("ORA-03135") || exception.Message.Contains("ORA-03114")) || exception.Message.Contains("ORA-12571"))
                            {
                                if (МенеджерБД.ПроверитьСоединение())
                                {
                                    Сообщение.ПоказатьОшибку("Во время выполения операции произошел обрыв связи.\r\nВ данный момент соединение восстановленно.\r\nДля сохранения данных формы в базу данных повторите операцию.");
                                }
                                else
                                {
                                    Сообщение.ПоказатьОшибку("Во время выполения операции произошел обрыв связи и восстановить соединение не удалось.\r\nВсе ваши данные были сохранены, и могут быть восстановлены через пункт \"Функции-Восстановление данных\".");
                                }
                            }
                            else
                            {
                                Сообщение.ПоказатьИсключительнуюСитуацию("Во время сохранения данных произошла ошибка.\r\nВсе ваши данные были сохранены, и могут быть восстановлены через пункт \"Функции-Восстановление данных\".", exception);
                            }
                        }
                    }
                    finally
                    {
                        процесса.УстановитьЗначениеИндикатора(100, "Сохранение данных...");
                        процесса.Закрыть();
                        this.Cursor = Cursors.Default;
                    }
                }
            }
        }

        private void simpleButton_сохранитьИЗакрыть_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.OK;
            base.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.ВариантОткрытия != ВариантОткрытияФормы.Чтение)
            {
                try
                {
                    new АвтосохранениеДанныхОтчетнойФормы(this.ОтчетнаяФорма).СохранитьДанныеФормыАвтоматически(false);
                }
                catch (Exception exception)
                {
                    Сообщение.ПоказатьИсключительнуюСитуацию("Не удалось автоматически сохранить форму...", exception);
                }
            }
        }

        private void toolStripMenuItem_вернутьНастройкиПоУмолчанию_Click(object sender, EventArgs e)
        {
            if (Сообщение.ПоказатьПредупреждение("Вы действительно желаете сбросить настройки?", "Сброс настроек", КнопкиСообщения.ДаНетОтмена) == РезультатДиалога.Да)
            {
                this.СброситьНастройкиВкладок(base.GetType());
            }
        }

        private void toolStripMenuItem_наименованиеЦвета_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item != null)
            {
                int selectedTabPageIndex = this.xtraTabControl_ГруппаЗакладок.SelectedTabPageIndex;
                if (item.Text == "Нет цвета")
                {
                    this.xtraTabControl_ГруппаЗакладок.TabPages[selectedTabPageIndex].Image = null;
                    this.xtraTabControl_ГруппаЗакладок.TabPages[selectedTabPageIndex].ImageIndex = -1;
                }
                else
                {
                    this.xtraTabControl_ГруппаЗакладок.TabPages[selectedTabPageIndex].Image = this.imageList_наборРисунков.Images[item.Text.Trim() + ".ico"];
                    this.xtraTabControl_ГруппаЗакладок.TabPages[selectedTabPageIndex].ImageIndex = this.imageList_наборРисунков.Images.IndexOfKey(item.Text.Trim() + ".ico");
                }
            }
        }

        private void xtraTabControl_ГруппаЗакладок_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            this.comboBoxEdit_Закладки.SelectedIndex = this.xtraTabControl_ГруппаЗакладок.SelectedTabPageIndex;
            if ((e.Page != null) && (0 != e.Page.Controls.Count))
            {
                e.Page.Controls[0].Focus();
            }
            if (this.ТаблицаТекущейЗакладки != null)
            {
            }
        }

        private void xtraTabControl_ГруппаЗакладок_SelectedPageChanging(object sender, TabPageChangingEventArgs e)
        {
            ИнтерфейсОтображаемойТаблицыЭкраннойФормы формы = this.ТаблицаТекущейЗакладки;
            if ((((формы != null) && (this.РежимРаботы == РежимРаботыЭкраннойФормы.Просмотр)) && (this.ВариантОткрытия != ВариантОткрытияФормы.Чтение)) && ((формы is ДинамическаяТаблица) && (формы as ДинамическаяТаблица).ЗначенияИзменились))
            {
                (формы as ДинамическаяТаблица).СохранитьЗеркалоДанных();
                this.ПересчитатьАвтоблоки();
            }
        }

        public void ВосстановитьИзЗеркала()
        {
            if (!this.РедактированиеРазрешено)
            {
                Сообщение.ПоказатьПредупреждение("Восстановление данных запрещено ( возможно вы в режиме \"Только для чтения\" )");
            }
            else if (Сообщение.ПоказатьВопрос("Внимание! Выполнение операции связано с изменением ВСЕХ данных формы. Вы уверены, что хотите продолжить ?", "Восстановление данных") == РезультатДиалога.Да)
            {
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    if (this.ОтчетнаяФорма.ВосстановитьИзЗеркалаДанных())
                    {
                        this.ОбновитьДанные();
                        this.ПересчитатьАвтоблоки();
                    }
                    else
                    {
                        Сообщение.ПоказатьПредупреждение("Восстановление на основе данных предыдущей сборки невозможно.");
                    }
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
            }
        }

        public void ДобавитьДинамическуюТаблицу(ДинамическаяТаблица ДинамическаяТаблица)
        {
            if ((this.ОтчетнаяФорма.Метаструктура == null) || this.ОтчетнаяФорма.Метаструктура.Таблицы.ContainsKey(ДинамическаяТаблица.КодТаблицы))
            {
                this.ДинамическиеТаблицы.Add(ДинамическаяТаблица.КодТаблицы, ДинамическаяТаблица);
                if (ДинамическаяТаблица.РазмещатьНаЗакладке)
                {
                    this.отображаемыеТаблицы.Add(ДинамическаяТаблица);
                }
            }
        }

        public void ДобавитьТаблицу(ТаблицаОтчетнойФормы Таблица)
        {
            if ((this.ОтчетнаяФорма.Метаструктура == null) || this.ОтчетнаяФорма.Метаструктура.Таблицы.ContainsKey(Таблица.КодТаблицы))
            {
                this.Таблицы.Add(Таблица);
                this.отображаемыеТаблицы.Add(Таблица);
            }
        }

        public void ДобавитьТаблицы(IEnumerable<ТаблицаОтчетнойФормы> Таблицы)
        {
            foreach (ТаблицаОтчетнойФормы формы in Таблицы)
            {
                this.ДобавитьТаблицу(формы);
            }
        }

        private void ДобавитьЭлементМенюМетаструктуры(ЭлементМенюМетаструктуры Элемент, BarSubItem РодительскийЭлемент, bool НачалоГруппы)
        {
            BarItem item = null;
            BarSubItem item2 = РодительскийЭлемент;
            РасположениеЭлементаМеню tag = Элемент.Расположение;
            РасположениеЭлементаМеню меню2 = Элемент.Расположение;
            if ((item2 != null) && (item2.Tag is РасположениеЭлементаМеню))
            {
                tag = (РасположениеЭлементаМеню) item2.Tag;
            }
            Image image = null;
            if (string.IsNullOrEmpty(Элемент.Иконка))
            {
                image = Image.FromStream(new MemoryStream(Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAAlwSFlzAAALEQAACxEBf2RfkQAAASZJREFUOE9j/P//PwNFAGQANlzWPjcZiP8Xtcz6n9cwDYSjsKnDqhmksLRtzn8gTgGxgZpjsmunAJmYluE0IL9xOoqGmLxW4gwoap4JVphZPQlMxxd2gOmQtHriDMiBOjWppBusITyrEUz7xFcQZ0BqeR9YYVRuC5j2T6oG064RhcQZEFfQDlYYnFoHpj2iS8C0U2gecQZE5aDa7BgC0WgXlE2cAWGZED97x5VDNAZCNFr6phNngF9iFVihS3gBmLb2zwDTpp7JxBngHlUMt9naLyPLwjuVNAMcQnL/A7EB0Onhlj5p/4HY2No/08zMC5QoiUiJQM3hNgGZIEPsQRqAtBzQgP9A7EmUAVBN9ubeKf9B/rYAugJoSBo2zSAxnHkBlwZ0cQAwd5JTiiFelgAAAABJRU5ErkJggg==")));
            }
            else
            {
                image = Image.FromStream(new MemoryStream(Convert.FromBase64String(Элемент.Иконка)));
            }
            if (Элемент is ПунктМенюМетаструктуры)
            {
                ПунктМенюМетаструктуры метаструктуры = Элемент as ПунктМенюМетаструктуры;
                BarButtonItem item3 = new BarButtonItem(this.barManager_main, Элемент.Заголовок);
                item3.Glyph = image;
                item3.Tag = метаструктуры;
                if ((this.РежимРаботы == РежимРаботыЭкраннойФормы.Просмотр) || this.ПеременнаяОбъявлена("Предпросмотр_ПараметрыЗаданы"))
                {
                    if ((this.ВариантОткрытия == ВариантОткрытияФормы.Чтение) && метаструктуры.НеЗапускатьВРежимеТолькоЧтение)
                    {
                        item3.Enabled = false;
                    }
                    else
                    {
                        item3.Enabled = true;
                        item3.ItemClick += new ItemClickEventHandler(this.ОбработчикЗапускаДополнительнойОбработки);
                    }
                }
                item = item3;
            }
            else if (Элемент is ПодменюМетаструктуры)
            {
                BarSubItem item4 = new BarSubItem(this.barManager_main, Элемент.Заголовок);
                item4.Glyph = image;
                item4.Tag = Элемент.Расположение;
                bool flag = false;
                foreach (ЭлементМенюМетаструктуры метаструктуры2 in (Элемент as ПодменюМетаструктуры).СписокЭлементов)
                {
                    if (метаструктуры2 is РазделительМенюМетаструктуры)
                    {
                        flag = true;
                    }
                    else
                    {
                        this.ДобавитьЭлементМенюМетаструктуры(метаструктуры2, item4, flag);
                        flag = false;
                    }
                }
                item = item4;
            }
            if (item != null)
            {
                item.PaintStyle = BarItemPaintStyle.CaptionGlyph;
                bool flag2 = true;
                if ((item2 != null) && (((Элемент.Расположение == tag) || ((меню2 == РасположениеЭлементаМеню.ТолькоМеню) && (tag == РасположениеЭлементаМеню.МенюИПанельИнструментов))) || ((меню2 == РасположениеЭлементаМеню.ТолькоПанельИнструментов) && (tag == РасположениеЭлементаМеню.МенюИПанельИнструментов))))
                {
                    item2.AddItem(item).BeginGroup = НачалоГруппы;
                    flag2 = false;
                }
                if (flag2)
                {
                    if (Элемент.Расположение == РасположениеЭлементаМеню.ТолькоМеню)
                    {
                        this.barSubItem_дополнительно.AddItem(item).BeginGroup = НачалоГруппы;
                    }
                    else if (Элемент.Расположение == РасположениеЭлементаМеню.ТолькоПанельИнструментов)
                    {
                        this.ПанельИнструментов.AddItem(item).BeginGroup = НачалоГруппы;
                    }
                    else if (Элемент.Расположение == РасположениеЭлементаМеню.МенюИПанельИнструментов)
                    {
                        this.barSubItem_дополнительно.AddItem(item).BeginGroup = НачалоГруппы;
                        this.ПанельИнструментов.AddItem(item).BeginGroup = НачалоГруппы;
                    }
                }
            }
        }

        private void ЗавершитьРедактированиеТекущейЯчейки()
        {
            if (this.ТаблицаТекущейЗакладки.ЭлементУправления is ТаблицаОтчетнойФормы)
            {
                ТаблицаОтчетнойФормы формы = this.ТаблицаТекущейЗакладки.ЭлементУправления as ТаблицаОтчетнойФормы;
                if (((формы != null) && (формы.CurrentCell != null)) && формы.CurrentCell.IsEditing)
                {
                    формы.CurrentCell.EndEdit();
                    формы.CurrentCell.Deactivate(false);
                }
            }
        }

        public void ЗагрузитьФорму(XPathDocument ЭкраннаяФорма)
        {
            XPathNavigator navigator = ЭкраннаяФорма.CreateNavigator();
            this.ЗагрузитьФорму(navigator);
        }

        public void ЗагрузитьФорму(XPathNavigator Навигатор)
        {
            string str8;
            this.Таблицы = new List<ТаблицаОтчетнойФормы>();
            this.СформироватьСловарьОграниченийНаЭлементы();
            if (!Навигатор.MoveToFirstChild())
            {
                throw new Exception("Загружаемый файл экранной формы пуст!");
            }
            if (!Навигатор.MoveToFirstChild())
            {
                throw new Exception("Загружаемый файл экранной формы пуст!");
            }
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
        Label_004A:
            str8 = Навигатор.Name;
            if (str8 != null)
            {
                if (!(str8 == "Шапка"))
                {
                    ТаблицаМетаструктуры метаструктуры;
                    ТаблицаДанных данных;
                    string str4;
                    if (str8 == "Закладка")
                    {
                        string attribute = Навигатор.GetAttribute("Код", "");
                        string str2 = Навигатор.GetAttribute("Наименование", "");
                        string str3 = Навигатор.GetAttribute("ИмяЛиста", "");
                        ТаблицаОтчетнойФормы формы = null;
                        if ((this.ОтчетнаяФорма.Метаструктура != null) && this.ОтчетнаяФорма.Метаструктура.Таблицы.ContainsKey(attribute))
                        {
                            метаструктуры = this.ОтчетнаяФорма.Метаструктура.Таблицы[attribute];
                            данных = this.ОтчетнаяФорма.Данные[attribute];
                            формы = new ТаблицаОтчетнойФормы(метаструктуры, данных);
                        }
                        else
                        {
                            формы = new ТаблицаОтчетнойФормы();
                        }
                        формы.КодТаблицы = attribute;
                        формы.Наименование = str2;
                        формы.ИмяЛиста = string.IsNullOrEmpty(str3) ? attribute : str3;
                        if (((string.IsNullOrEmpty(формы.Наименование) || (формы.Наименование == формы.КодТаблицы)) || (формы.Наименование == формы.ИмяЛиста)) && (((this.ОтчетнаяФорма != null) && (this.ОтчетнаяФорма.Метаструктура != null)) && this.ОтчетнаяФорма.Метаструктура.Таблицы.ContainsKey(формы.КодТаблицы)))
                        {
                            str4 = this.ОтчетнаяФорма.Метаструктура.Таблицы[формы.КодТаблицы].Наименование;
                            if (str4.Trim().ToLower() != "таблица")
                            {
                                формы.Наименование = str4;
                            }
                        }
                        формы.ЭкраннаяФорма = this;
                        GridFormulaEngine.RegisterGridAsSheet(формы.ИмяЛиста, формы.Представление, this.КодГруппыТаблиц);
                        формы.ЗагрузитьИзXML(Навигатор.CreateNavigator());
                        if (this.СписокОграниченийНаЭлементы.ContainsKey(attribute))
                        {
                            формы.ПроанализироватьЗапретыДоступаКЭлементамФормы(this.СписокОграниченийНаЭлементы[attribute]);
                        }
                        формы.ПослеУстановкиЗначенияЯчейкиСправочника += new СобытиеПослеУстановкиЗначенияЯчейки(this.ОбработчикПослеУстановкиЗначенияЯчейкиСправочника);
                        this.ДобавитьТаблицу(формы);
                        goto Label_0606;
                    }
                    if (str8 == "ДинамическаяТаблица")
                    {
                        string key = Навигатор.GetAttribute("Код", "");
                        string str6 = Навигатор.GetAttribute("Наименование", "");
                        string str7 = Навигатор.GetAttribute("ИмяЛиста", "");
                        bool flag = Навигатор.GetAttribute("РазмещатьНаЗакладке", "").Trim().ToLower() == "да";
                        ДинамическаяТаблица таблица = null;
                        if ((this.ОтчетнаяФорма.Метаструктура != null) && this.ОтчетнаяФорма.Метаструктура.Таблицы.ContainsKey(key))
                        {
                            метаструктуры = this.ОтчетнаяФорма.Метаструктура.Таблицы[key];
                            данных = this.ОтчетнаяФорма.Данные[key];
                            таблица = new ДинамическаяТаблица(метаструктуры, данных);
                        }
                        else
                        {
                            таблица = new ДинамическаяТаблица();
                        }
                        таблица.РазмещатьНаЗакладке = flag;
                        таблица.ЭкраннаяФорма = this;
                        таблица.КодТаблицы = key;
                        таблица.ИмяЛиста = !string.IsNullOrEmpty(str7) ? str7 : (string.IsNullOrEmpty(str6) ? key : str6);
                        таблица.ЗагрузитьИзXML(Навигатор.CreateNavigator());
                        if (((string.IsNullOrEmpty(таблица.Наименование) || (таблица.Наименование == таблица.КодТаблицы)) || (таблица.Наименование == таблица.ИмяЛиста)) && (((this.ОтчетнаяФорма != null) && (this.ОтчетнаяФорма.Метаструктура != null)) && this.ОтчетнаяФорма.Метаструктура.Таблицы.ContainsKey(таблица.КодТаблицы)))
                        {
                            str4 = this.ОтчетнаяФорма.Метаструктура.Таблицы[таблица.КодТаблицы].Наименование;
                            if (str4.Trim().ToLower() != "таблица")
                            {
                                таблица.Наименование = str4;
                            }
                        }
                        if (this.СписокОграниченийНаЭлементы.ContainsKey(key))
                        {
                            таблица.ПроанализироватьЗапретыДоступаКЭлементамФормы(this.СписокОграниченийНаЭлементы[key]);
                        }
                        if (таблица.РазмещатьНаЗакладке || (this.РежимРаботы == РежимРаботыЭкраннойФормы.РедакторУвязок))
                        {
                            таблица.СоздатьЗеркалоДанных();
                        }
                        таблица.ПослеУстановкиЗначенияЯчейкиСправочника += new СобытиеПослеУстановкиЗначенияЯчейки(this.ОбработчикПослеУстановкиЗначенияЯчейкиСправочника);
                        this.ДобавитьДинамическуюТаблицу(таблица);
                        goto Label_0606;
                    }
                }
                else
                {
                    if (this.ОтчетнаяФорма.Метаструктура != null)
                    {
                        this.Шапка = new ШапкаЭкраннойФормы(this.ОтчетнаяФорма.Метаструктура.СвободныеЯчейки, this.ОтчетнаяФорма.Данные.СвободныеЯчейки);
                    }
                    else
                    {
                        this.Шапка = new ШапкаЭкраннойФормы();
                    }
                    this.Шапка.ЭкраннаяФорма = this;
                    GridFormulaEngine.RegisterGridAsSheet("Шапка", this.Шапка.Представление, this.КодГруппыТаблиц);
                    this.Шапка.ЗагрузитьИзXML(Навигатор.CreateNavigator());
                    if (this.СписокОграниченийНаЭлементы.ContainsKey("Шапка"))
                    {
                        this.Шапка.ПроанализироватьЗапретыДоступаКЭлементамФормы(this.СписокОграниченийНаЭлементы["Шапка"]);
                    }
                    this.Шапка.ПослеУстановкиЗначенияЯчейкиСправочника += new СобытиеПослеУстановкиЗначенияЯчейки(this.ОбработчикПослеУстановкиЗначенияЯчейкиСправочника);
                    goto Label_0606;
                }
            }
            throw new Exception("Неизвестный формат ");
        Label_0606:
            if (Навигатор.MoveToNext())
            {
                goto Label_004A;
            }
        }

        public void ЗагрузкаНастроекВкладок(System.Type ТипФормы)
        {
            if (((this != null) && (this.РежимРаботы == РежимРаботыЭкраннойФормы.Просмотр)) && (ТипФормы != null))
            {
                System.Type type = ТипФормы;
                try
                {
                    new НастройкиГруппыВкладок(this.xtraTabControl_ГруппаЗакладок, type, this.ОтчетнаяФорма).ДесериализоватьНастройки(this.xtraTabControl_ГруппаЗакладок);
                    this.comboBoxEdit_Закладки.Properties.Items.Clear();
                    foreach (XtraTabPage page in this.xtraTabControl_ГруппаЗакладок.TabPages)
                    {
                        this.comboBoxEdit_Закладки.Properties.Items.Add(page.Text);
                    }
                    this.comboBoxEdit_Закладки.SelectedIndex = this.xtraTabControl_ГруппаЗакладок.SelectedTabPageIndex;
                }
                catch (Exception)
                {
                }
            }
        }

        private void ИзменениеДанныхТаблицы(object Таблица)
        {
            this.ДанныеИзменились = true;
        }

        public void ОбновитьДанные()
        {
            ФормаИндикаторПроцесса процесса = new ФормаИндикаторПроцесса();
            процесса.Заголовок = "Обновление данных...";
            процесса.Минимум = 0;
            процесса.Максимум = this.ОтображаемыеТаблицы.Count + 1;
            процесса.Шаг = 1;
            процесса.Показать();
            try
            {
                try
                {
                    foreach (ИнтерфейсОтображаемойТаблицыЭкраннойФормы формы in this.отображаемыеТаблицы)
                    {
                        процесса.СледующийШаг("Таблица " + формы.Заголовок);
                        Application.DoEvents();
                        формы.ОбновитьЗначения();
                    }
                    Application.DoEvents();
                    процесса.СледующийШаг("Шапка формы");
                    if (this.Шапка != null)
                    {
                        this.Шапка.ОбновитьЗначения();
                    }
                    this.Refresh();
                }
                catch (Exception exception)
                {
                    Сообщение.ПоказатьИсключительнуюСитуацию("Ошибка обновления данных", exception);
                }
            }
            finally
            {
                процесса.Закрыть();
            }
        }

        private void ОбработчикВыбораИмпортера(object sender, ItemClickEventArgs e)
        {
            if (((e == null) || (e.Item == null)) || (e.Item.Tag == null))
            {
                Сообщение.ПоказатьОшибку("Ошибка определения класса-импортера.");
            }
            else
            {
                ИмпортерДанныхОтчетныхФорм tag = (ИмпортерДанныхОтчетныхФорм) e.Item.Tag;
                if (tag.ВыбратьФайл())
                {
                    ФормаИндикаторПроцесса процесса = new ФормаИндикаторПроцесса();
                    bool flag = false;
                    try
                    {
                        процесса.Показать();
                        this.Cursor = Cursors.WaitCursor;
                        Application.DoEvents();
                        процесса.УстановитьЗначениеИндикатора(0x19, "Выполнение импорта...");
                        if (tag.Импортировать())
                        {
                            процесса.УстановитьЗначениеИндикатора(0x4b, "Обновление данных...");
                            this.ОбновитьДанные();
                            процесса.УстановитьЗначениеИндикатора(100, "Пересчет автоблоков...");
                            this.ПересчитатьАвтоблоки();
                            flag = true;
                            процесса.Закрыть();
                        }
                        if (flag)
                        {
                            tag.ОбработатьРезультатыВыполненияИмпорта();
                        }
                    }
                    finally
                    {
                        if (!процесса.IsDisposed)
                        {
                            процесса.Закрыть();
                        }
                        this.Cursor = Cursors.Default;
                    }
                    if ((flag && (tag.Форма != null)) && (Сообщение.ПоказатьВопрос("Сохранить данные формы ?", "Импорт данных отчетной формы") == РезультатДиалога.Да))
                    {
                        this.СохранитьДанные();
                    }
                }
            }
        }

        private void ОбработчикВыбораПечатнойФормы(object Отправитель, ItemClickEventArgs Аргументы)
        {
            if (Аргументы.Item != null)
            {
                BarItem item = Аргументы.Item;
                if (item.Tag != null)
                {
                    this.simpleButton_применить.Focus();
                    Application.DoEvents();
                    if (this.РедактированиеРазрешено || this.ПеременнаяОбъявлена("Предпросмотр_ПараметрыЗаданы"))
                    {
                        this.ПересчитатьАвтоблоки();
                    }
                    else
                    {
                        this.ПересчитатьАвтоблоки(true);
                    }
                    МетаописаниеПечатнойФормы tag = (МетаописаниеПечатнойФормы) item.Tag;
                    Application.DoEvents();
                    if (this.РежимРаботы == РежимРаботыЭкраннойФормы.ПредварительныйПросмотр)
                    {
                        string path = this.ПолучитьЗначениеПеременной("Предпросмотр_ПутьКФайлуФормы", "", true).ToString();
                        if (File.Exists(path))
                        {
                            ПечатнаяФормаОтчета отчета = new ПечатнаяФормаОтчета(this.ОтчетнаяФорма);
                            отчета.АссоциированныйМакрос = tag.АссоциированныйМакрос;
                            отчета.ВнешнийШаблон = Path.Combine(Path.GetDirectoryName(path), tag.ФайлШаблона);
                            отчета.Печать();
                        }
                    }
                    else
                    {
                        this.ОтчетнаяФорма.Печать(tag);
                    }
                }
            }
        }

        private void ОбработчикВыбораЭкспортера(object sender, ItemClickEventArgs e)
        {
            if (((e == null) || (e.Item == null)) || (e.Item.Tag == null))
            {
                Сообщение.ПоказатьОшибку("Ошибка определения класса-экспортера.");
            }
            else
            {
                ЭкспортерДанныхОтчетныхФорм tag = (ЭкспортерДанныхОтчетныхФорм) e.Item.Tag;
                if (tag.ВыбратьКаталог())
                {
                    ФормаИндикаторПроцесса процесса = new ФормаИндикаторПроцесса();
                    try
                    {
                        процесса.Показать();
                        процесса.УстановитьЗначениеИндикатора(50, "Выполнение экспорта...");
                        tag.Экспортировать();
                        процесса.УстановитьЗначениеИндикатора(100, "Завершение...");
                    }
                    finally
                    {
                        процесса.Закрыть();
                    }
                }
            }
        }

        private void ОбработчикЗапускаДополнительнойОбработки(object Отправитель, ItemClickEventArgs Аргументы)
        {
            if ((this.РежимРаботы == РежимРаботыЭкраннойФормы.Просмотр) || (Сообщение.ПоказатьВопрос("В Н И М А Н И Е!!!\n\nВыполнение дополнительных обработок в режиме предварительного просмотра чревато возникновением критических ошибок.\nРазработчики снимают с себя ответственность за Ваши дальнейшие действия!\n\nВы действительно хотите выполнить данную обработку ?") == РезультатДиалога.Да))
            {
                ПунктМенюМетаструктуры tag = null;
                if (Аргументы.Item.Tag != null)
                {
                    tag = Аргументы.Item.Tag as ПунктМенюМетаструктуры;
                }
                this.Cursor = Cursors.WaitCursor;
                try
                {
                    base.Update();
                    Application.DoEvents();
                    if (!(this.РедактированиеРазрешено || this.ПеременнаяОбъявлена("Предпросмотр_ПараметрыЗаданы")))
                    {
                        this.ПересчитатьАвтоблоки(true);
                    }
                    if (tag != null)
                    {
                        РезультатВыполненияОперации операции = this.ОтчетнаяФорма.ВыполнитьОперациюМакроса(tag.Макрос, tag.Макрос, tag.ТребоватьПодключение);
                        if ((this.РедактированиеРазрешено || this.ПеременнаяОбъявлена("Предпросмотр_ПараметрыЗаданы")) && (((операции != null) && операции.НеобходимоОбновитьДанные) || (операции == null)))
                        {
                            this.ОбновитьДанные();
                            this.ПересчитатьАвтоблоки();
                            this.ДанныеИзменились = true;
                        }
                    }
                    else
                    {
                        Сообщение.ПоказатьПредупреждение("С данным пунктом меню не связана ни одна обработка формы.");
                    }
                }
                catch (Exception exception)
                {
                    Сообщение.ПоказатьИсключительнуюСитуацию("Не удалось выполнить дополнительную обработку '" + tag.Макрос + "'.", exception);
                }
                this.Cursor = Cursors.Default;
            }
        }

        private void ОбработчикПослеУстановкиЗначенияЯчейкиСправочника(object Отправитель, АргументыПослеУстановкиЗначенияЯчейки Аргументы)
        {
            this.ОтчетнаяФорма.ОбработатьСобытие_ПослеУстановкиЗначенияЯчейкиСправочника(Аргументы);
        }

        private void ОтключитьПунктыМеню(bool Отключить)
        {
            if (Отключить)
            {
                foreach (BarItem item in this.barManager_main.Items)
                {
                    if (!(item is BarLinksHolder))
                    {
                        item.Enabled = !Отключить;
                    }
                }
                foreach (BarItemLink link in this.barSubItem_ПечатныеФормы.ItemLinks)
                {
                    link.Item.Enabled = true;
                }
            }
        }

        public void ОткрытьМетодическийСправочник(ИнтерфейсОтображаемойТаблицыЭкраннойФормы таблица)
        {
            if ((this.режимРаботы == РежимРаботыЭкраннойФормы.Просмотр) || this.ПеременнаяОбъявлена("Предпросмотр_ПараметрыЗаданы"))
            {
                МетодическийСправочник справочник = null;
                if (таблица == null)
                {
                    справочник = new МетодическийСправочник(this.ОтчетнаяФорма.Метаструктура.СсылкаНаМетодическийСправочник);
                }
                else if (!(!(таблица is ДинамическаяТаблица) || string.IsNullOrEmpty((таблица as ДинамическаяТаблица).ТаблицаМетаструктуры.СсылкаНаМетодическийСправочник)))
                {
                    справочник = new МетодическийСправочник((таблица as ДинамическаяТаблица).ТаблицаМетаструктуры.СсылкаНаМетодическийСправочник);
                }
                else if (!(!(таблица is ТаблицаОтчетнойФормы) || string.IsNullOrEmpty((таблица as ТаблицаОтчетнойФормы).ТаблицаМетаструктуры.СсылкаНаМетодическийСправочник)))
                {
                    справочник = new МетодическийСправочник((таблица as ТаблицаОтчетнойФормы).ТаблицаМетаструктуры.СсылкаНаМетодическийСправочник);
                }
                else
                {
                    справочник = new МетодическийСправочник(this.ОтчетнаяФорма.Метаструктура.СсылкаНаМетодическийСправочник);
                }
                справочник.ОткрытьСправочник(this);
            }
        }

        public void ОтобразитьПанельПоиска()
        {
            this.FindTool.Visible = true;
        }

        public void ОтобразитьПодсказку(string Подсказка)
        {
            this.barToolTip.Enabled = true;
            if (string.IsNullOrEmpty(Подсказка))
            {
                this.barToolTip.Caption = "";
            }
            else
            {
                Подсказка = Подсказка.Replace("\r\n", "  ");
                if (Подсказка.Length > 150)
                {
                    Подсказка = Подсказка.Substring(0, 150) + "...";
                }
                this.barToolTip.Caption = Подсказка;
            }
        }

        private void ОтчетнаяФорма_ПриОкончанииОбработкиСобытия(object объект, АргументыОкончанияОбработкиСобытия аргумент)
        {
            if (аргумент.МодульФормы != null)
            {
                ПротоколВыполненияОперации операции = аргумент.МодульФормы.Протокол;
                if (операции == null)
                {
                    операции = аргумент.МодульФормы.РезультатВыполненияОперации as ПротоколВыполненияОперации;
                }
                if ((операции != null) && (операции.Записи.Count > 0))
                {
                    new ПечатнаяФормаПротоколаВыполненияОперации(операции).Печать();
                }
                РезультатыВыполненияСверкиДанных данных = аргумент.МодульФормы.РезультатВыполненияОперации as РезультатыВыполненияСверкиДанных;
                if ((данных != null) && (данных.СтрокиСверки.Count > 0))
                {
                    new ФормаОтчетаСверкиДанных(данных).Показать();
                }
            }
        }

        public void ОчиститьСписокПеременных()
        {
            if (this.списокПеременных != null)
            {
                this.списокПеременных.Clear();
            }
        }

        public bool ПеременнаяОбъявлена(string ИмяПеременной)
        {
            return this.ПеременнаяОбъявлена(ИмяПеременной, true);
        }

        public bool ПеременнаяОбъявлена(string ИмяПеременной, bool НезависимоОтРегистра)
        {
            if ((string.IsNullOrEmpty(ИмяПеременной) || (this.списокПеременных == null)) || (this.списокПеременных.Count == 0))
            {
                return false;
            }
            string key = НезависимоОтРегистра ? ИмяПеременной.Trim().ToLower() : ИмяПеременной.Trim();
            return this.списокПеременных.ContainsKey(key);
        }

        public void ПересчитатьАвтоблоки()
        {
            this.ПересчитатьАвтоблоки(false);
        }

        public void ПересчитатьАвтоблоки(bool ПересчитатьТолькоСтатические)
        {
            ФормаИндикаторПроцесса процесса = new ФормаИндикаторПроцесса();
            процесса.Заголовок = "Пересчет автоблоков...";
            процесса.Минимум = 0;
            процесса.Максимум = this.Таблицы.Count + 1;
            if (!ПересчитатьТолькоСтатические)
            {
                процесса.Максимум += this.ДинамическиеТаблицы.Count;
            }
            процесса.Показать();
            this.РазрешеноЗакрытьФорму = false;
            try
            {
                try
                {
                    if (!ПересчитатьТолькоСтатические)
                    {
                        foreach (ДинамическаяТаблица таблица in this.ДинамическиеТаблицы.Values)
                        {
                            процесса.СледующийШаг("Таблица " + таблица.Заголовок);
                            таблица.ПересчитатьАвтоблоки();
                        }
                    }
                    foreach (ТаблицаОтчетнойФормы формы in this.Таблицы)
                    {
                        процесса.СледующийШаг("Таблица " + формы.Заголовок);
                        формы.ПересчитатьАвтоблоки();
                    }
                    Application.DoEvents();
                    процесса.СледующийШаг("Шапка формы");
                    if (this.Шапка != null)
                    {
                        this.Шапка.ПересчитатьАвтоблоки();
                    }
                }
                catch (Exception exception)
                {
                    Сообщение.ПоказатьИсключительнуюСитуацию("Ошибка пересчета автоблоков.", exception);
                }
            }
            finally
            {
                this.РазрешеноЗакрытьФорму = true;
                процесса.Закрыть();
            }
        }

        public void ПоказатьФорму()
        {
            this.ПостроитьФорму();
            this.ПостроитьМенюПечатныхФорм();
            this.ПостроитьМенюДополнительныхОбработокФормы();
            this.ПостроитьМенюИмпортаЭкспорта();
            this.ПостроитьМенюТеста();
            if (this.ВариантОткрытия == ВариантОткрытияФормы.Чтение)
            {
                this.Text = this.Text + " (ТОЛЬКО ПРОСМОТР)";
                this.barSubItem_Архив.Enabled = false;
            }
            bool flag = (this.РежимРаботы != РежимРаботыЭкраннойФормы.Просмотр) && !this.ПеременнаяОбъявлена("Предпросмотр_ПараметрыЗаданы");
            this.ОтключитьПунктыМеню(flag);
            Application.DoEvents();
            base.Show();
        }

        public object ПолучитьЗначениеПеременной(string ИмяПеременной, object ЗначениеПоУмолчанию)
        {
            return this.ПолучитьЗначениеПеременной(ИмяПеременной, ЗначениеПоУмолчанию, true);
        }

        public object ПолучитьЗначениеПеременной(string ИмяПеременной, object ЗначениеПоУмолчанию, bool НезависимоОтРегистра)
        {
            if ((string.IsNullOrEmpty(ИмяПеременной) || (this.списокПеременных == null)) || (this.списокПеременных.Count == 0))
            {
                return ЗначениеПоУмолчанию;
            }
            string str = НезависимоОтРегистра ? ИмяПеременной.Trim().ToLower() : ИмяПеременной.Trim();
            if (!this.ПеременнаяОбъявлена(ИмяПеременной, НезависимоОтРегистра))
            {
                return ЗначениеПоУмолчанию;
            }
            return this.списокПеременных[str];
        }

        private void ПостроитьМенюДополнительныхОбработокФормы()
        {
            if (this.ОтчетнаяФорма.Метаструктура.Меню.СписокЭлементов.Count > 0)
            {
                bool flag = false;
                foreach (ЭлементМенюМетаструктуры метаструктуры in this.ОтчетнаяФорма.Метаструктура.Меню.СписокЭлементов)
                {
                    if (метаструктуры is РазделительМенюМетаструктуры)
                    {
                        flag = true;
                    }
                    else
                    {
                        this.ДобавитьЭлементМенюМетаструктуры(метаструктуры, null, flag);
                        flag = false;
                    }
                }
            }
            this.ПанельИнструментов.Visible = this.ПанельИнструментов.ItemLinks.Count > 0;
            this.barSubItem_дополнительно.Visibility = (this.barSubItem_дополнительно.ItemLinks.Count > 0) ? BarItemVisibility.Always : BarItemVisibility.Never;
        }

        private void ПостроитьМенюИмпортаЭкспорта()
        {
            BarButtonItem item;
            this.barSubItem_ИмпортДанных.Enabled = false;
            if (this.ВариантОткрытия != ВариантОткрытияФормы.Чтение)
            {
                List<ИмпортерДанныхОтчетныхФорм> list = null;
                try
                {
                    list = СписокИмпортеровДанныхОтчетныхФорм.ПолучитьСписокИмпортеровДляФормы(this.ОтчетнаяФорма);
                }
                catch
                {
                    list = null;
                }
                if ((list != null) && (list.Count > 0))
                {
                    foreach (ИмпортерДанныхОтчетныхФорм форм in list)
                    {
                        item = new BarButtonItem();
                        item.Caption = форм.Наименование();
                        item.Hint = форм.ПодсказкаДляПунктаМеню();
                        if (!string.IsNullOrEmpty(форм.ИконкаДляПунктаМеню()))
                        {
                            item.Glyph = Image.FromStream(new MemoryStream(Convert.FromBase64String(форм.ИконкаДляПунктаМеню())));
                        }
                        item.Tag = форм;
                        if (this.РежимРаботы == РежимРаботыЭкраннойФормы.Просмотр)
                        {
                            item.ItemClick += new ItemClickEventHandler(this.ОбработчикВыбораИмпортера);
                        }
                        this.barSubItem_ИмпортДанных.AddItem(item);
                    }
                    this.barSubItem_ИмпортДанных.Enabled = true;
                }
            }
            this.barSubItem_ЭкспортДанных.Enabled = false;
            List<ЭкспортерДанныхОтчетныхФорм> list2 = null;
            try
            {
                list2 = СписокЭкспортеровДанныхОтчетныхФорм.ПолучитьСписокЭкспортеровДляФормы(this.ОтчетнаяФорма);
            }
            catch
            {
                list2 = null;
            }
            if ((list2 != null) && (list2.Count > 0))
            {
                foreach (ЭкспортерДанныхОтчетныхФорм форм2 in list2)
                {
                    item = new BarButtonItem();
                    item.Caption = форм2.Наименование();
                    item.Hint = форм2.ПодсказкаДляПунктаМеню();
                    if (!string.IsNullOrEmpty(форм2.ИконкаДляПунктаМеню()))
                    {
                        item.Glyph = Image.FromStream(new MemoryStream(Convert.FromBase64String(форм2.ИконкаДляПунктаМеню())));
                    }
                    item.Tag = форм2;
                    if (this.РежимРаботы == РежимРаботыЭкраннойФормы.Просмотр)
                    {
                        item.ItemClick += new ItemClickEventHandler(this.ОбработчикВыбораЭкспортера);
                    }
                    this.barSubItem_ЭкспортДанных.AddItem(item);
                }
                this.barSubItem_ЭкспортДанных.Enabled = true;
            }
        }

        private void ПостроитьМенюПечатныхФорм()
        {
            СписокМетаописанийПечатныхФорм форм = new СписокМетаописанийПечатныхФорм();
            try
            {
                if (this.РежимРаботы == РежимРаботыЭкраннойФормы.ПредварительныйПросмотр)
                {
                    string path = this.ПолучитьЗначениеПеременной("Предпросмотр_ПутьКФайлуФормы", "", true).ToString();
                    if (File.Exists(path))
                    {
                        форм.Загрузить(ПровайдерФайловФормы.ПолучитьПутьКФайлуМетаописанийПечатныхФорм(path));
                    }
                }
                else if (this.РежимРаботы == РежимРаботыЭкраннойФормы.Просмотр)
                {
                    форм.Загрузить(this.отчетнаяФорма.Метаструктура.Идентификатор);
                }
            }
            catch
            {
            }
            this.barSubItem_ПечатныеФормы.ItemLinks.Clear();
            if (форм.Count <= 0)
            {
                this.barSubItem_ПечатныеФормы.Visibility = BarItemVisibility.Never;
            }
            else
            {
                foreach (МетаописаниеПечатнойФормы формы in форм)
                {
                    BarButtonItem item = new BarButtonItem();
                    item.Caption = формы.Наименование;
                    item.Hint = формы.Описание;
                    item.Tag = формы;
                    if ((this.РежимРаботы == РежимРаботыЭкраннойФормы.Просмотр) || (this.РежимРаботы == РежимРаботыЭкраннойФормы.ПредварительныйПросмотр))
                    {
                        item.ItemClick += new ItemClickEventHandler(this.ОбработчикВыбораПечатнойФормы);
                    }
                    ТипЭлементаЦепочки цепочки = this.ОтчетнаяФорма.Данные.Идентификатор.ЭлементЦепочки;
                    if (((цепочки == ТипЭлементаЦепочки.Офис) || (цепочки == ТипЭлементаЦепочки.ЦентральныйОфис)) && формы.ОтображатьТолькоДляАбонентов)
                    {
                        item.Visibility = BarItemVisibility.Never;
                    }
                    else if (((цепочки == ТипЭлементаЦепочки.Абонент) || (цепочки == ТипЭлементаЦепочки.ПассивныйАбонент)) && формы.ОтображатьТолькоДляОфисов)
                    {
                        item.Visibility = BarItemVisibility.Never;
                    }
                    item.Glyph = this.barSubItem_ПечатныеФормы.Glyph;
                    this.barSubItem_ПечатныеФормы.AddItem(item);
                }
            }
        }

        private void ПостроитьМенюТеста()
        {
            if (((this.РежимРаботы == РежимРаботыЭкраннойФормы.Просмотр) && (this.ВариантОткрытия != ВариантОткрытияФормы.Чтение)) && (Приложение.Параметры.ПараметрЗадан("РежимРазработчика") || Приложение.Параметры.ПараметрЗадан("РежимТестировщика")))
            {
                this.barButtonItem_тестирование.Visibility = BarItemVisibility.Always;
            }
        }

        public void ПостроитьФорму()
        {
            this.mainLayout.BeginInit();
            this.mainLayout.SuspendLayout();
            this.mainGroup.BeginInit();
            base.SuspendLayout();
            LayoutControlItem item = null;
            if (this.Шапка != null)
            {
                bool flag = true;
                if ((this.СписокОграниченийНаЭлементы.ContainsKey("Шапка") && this.СписокОграниченийНаЭлементы["Шапка"].ContainsKey("Таблица")) && (this.СписокОграниченийНаЭлементы["Шапка"]["Таблица"] == "ЗапретПросмотра"))
                {
                    flag = false;
                }
                if (flag)
                {
                    item = this.mainGroup.AddItem("", this.Шапка);
                    item.Name = "Шапка";
                    item.TextVisible = false;
                    item.Move(this.layoutControlItem_закладки, InsertType.Top);
                }
                this.Шапка.ПриИзмененииДанных += new СобытиеИзмененияТаблицыДанных(this.ИзменениеДанныхТаблицы);
            }
            if ((this.Таблицы.Count != 0) || (this.ДинамическиеТаблицы.Count != 0))
            {
                if ((this.Шапка != null) && (item != null))
                {
                    SplitterItem item2 = new SplitterItem();
                    this.mainGroup.Add(item2);
                    item2.Move(this.layoutControlItem_закладки, InsertType.Top);
                }
                this.xtraTabControl_ГруппаЗакладок.BeginInit();
                this.xtraTabControl_ГруппаЗакладок.TabPages.Clear();
                foreach (ИнтерфейсОтображаемойТаблицыЭкраннойФормы формы in this.отображаемыеТаблицы)
                {
                    формы.ПриИзмененииДанных += new СобытиеИзмененияТаблицыДанных(this.ИзменениеДанныхТаблицы);
                    формы.ЭлементУправления.Dock = DockStyle.Fill;
                    bool flag2 = false;
                    if ((this.СписокОграниченийНаЭлементы.ContainsKey(формы.КодТаблицы) && this.СписокОграниченийНаЭлементы[формы.КодТаблицы].ContainsKey("ДинамическаяТаблица")) && (this.СписокОграниченийНаЭлементы[формы.КодТаблицы]["Таблица"] != "ЗапретПросмотра"))
                    {
                        flag2 = true;
                    }
                    if (!flag2)
                    {
                        this.xtraTabControl_ГруппаЗакладок.TabPages.Add(формы.Заголовок).Controls.Add(формы.ЭлементУправления);
                    }
                }
                if (this.xtraTabControl_ГруппаЗакладок.TabPages.Count == 1)
                {
                    this.xtraTabControl_ГруппаЗакладок.ShowTabHeader = DefaultBoolean.False;
                    this.layoutItem_выборВкладки.HideToCustomization();
                }
                this.xtraTabControl_ГруппаЗакладок.EndInit();
                this.xtraTabControl_ГруппаЗакладок.SelectedTabPageIndex = 0;
            }
            else
            {
                this.xtraTabControl_ГруппаЗакладок.Visible = false;
                this.layoutControlItem_закладки.Visibility = LayoutVisibility.Never;
            }
            this.mainLayout.EndInit();
            this.mainLayout.ResumeLayout(false);
            this.mainGroup.EndInit();
            base.ResumeLayout(false);
            if ((this.Шапка != null) && (item != null))
            {
                item.Height = Math.Min(this.Шапка.ПолучитьВысотуТаблицы(), (int) (this.mainGroup.Height * 0.4));
            }
            base.ResumeLayout(false);
        }

        public string ПроанализироватьСсылкиНаКонстанты(string ИсходноеЗначение)
        {
            if (!string.IsNullOrEmpty(ИсходноеЗначение) && ИсходноеЗначение.Contains("#"))
            {
                if (((this.РежимРаботы != РежимРаботыЭкраннойФормы.Просмотр) && (this.РежимРаботы != РежимРаботыЭкраннойФормы.ПредварительныйПросмотр)) && !this.ПеременнаяОбъявлена("Предпросмотр_ПараметрыЗаданы"))
                {
                    return ИсходноеЗначение;
                }
                if (this.ОтчетнаяФорма != null)
                {
                    return this.ОтчетнаяФорма.ПроанализироватьСсылкиНаКонстанты(ИсходноеЗначение, this.РежимРаботы == РежимРаботыЭкраннойФормы.Просмотр);
                }
            }
            return ИсходноеЗначение;
        }

        private bool ПроверитьКорректностьДанных(ref List<СтрокаОтчетаСверкиДанных> списокОшибокЗаполнения, ref int количествоОшибок, ref int количествоПредупреждений)
        {
            СтрокаОтчетаСверкиДанных данных;
            МатрицаЗначений значений;
            if (списокОшибокЗаполнения == null)
            {
                списокОшибокЗаполнения = new List<СтрокаОтчетаСверкиДанных>();
            }
            bool flag = true;
            Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
            if ((this.Шапка != null) && !this.Шапка.ПроверитьЗаполненностьДанных(out dictionary))
            {
                foreach (KeyValuePair<string, List<string>> pair in dictionary)
                {
                    foreach (string str in pair.Value)
                    {
                        данных = new СтрокаОтчетаСверкиДанных();
                        данных.Форма = this.ОтчетнаяФорма.Метаструктура.Наименование;
                        данных.Столбец = pair.Key;
                        данных.Строка = str;
                        данных.Субтаблица = "Шапка";
                        данных.ТипОшибки = "Незаполненная ячейка";
                        данных.СохранениеРазрешено = false;
                        списокОшибокЗаполнения.Add(данных);
                    }
                }
            }
            foreach (ИнтерфейсОтображаемойТаблицыЭкраннойФормы формы in this.Таблицы)
            {
                if (!формы.ПроверитьЗаполненностьДанных(out dictionary))
                {
                    значений = формы.ТаблицаДанных.МатрицаЗначений;
                    foreach (KeyValuePair<string, List<string>> pair2 in dictionary)
                    {
                        foreach (string str in pair2.Value)
                        {
                            данных = new СтрокаОтчетаСверкиДанных();
                            данных.Форма = this.ОтчетнаяФорма.Метаструктура.Наименование;
                            данных.Столбец = pair2.Key;
                            данных.Строка = str;
                            данных.Субтаблица = формы.Наименование;
                            данных.ТипОшибки = "Незаполненная ячейка";
                            данных.СохранениеРазрешено = false;
                            значений[данных.Строка, данных.Столбец].Статус.ТекстОшибкиЗаполнения = данных.ТипОшибки;
                            значений[данных.Строка, данных.Столбец].Статус.ЯчейкуМожноСохранить = данных.СохранениеРазрешено;
                            значений[данных.Строка, данных.Столбец].Статус.ЯчейкаЗаполненаВерно = false;
                            списокОшибокЗаполнения.Add(данных);
                        }
                    }
                }
            }
            foreach (ИнтерфейсОтображаемойТаблицыЭкраннойФормы формы in this.ДинамическиеТаблицы.Values)
            {
                if (!формы.ПроверитьЗаполненностьДанных(out dictionary))
                {
                    значений = формы.ТаблицаДанных.МатрицаЗначений;
                    foreach (KeyValuePair<string, List<string>> pair2 in dictionary)
                    {
                        foreach (string str in pair2.Value)
                        {
                            данных = new СтрокаОтчетаСверкиДанных();
                            данных.Форма = this.ОтчетнаяФорма.Метаструктура.Наименование;
                            данных.Столбец = pair2.Key;
                            данных.Строка = str;
                            данных.Субтаблица = формы.Наименование;
                            данных.ТипОшибки = "Незаполненная ячейка";
                            данных.СохранениеРазрешено = false;
                            значений[данных.Строка, данных.Столбец].Статус.ТекстОшибкиЗаполнения = данных.ТипОшибки;
                            значений[данных.Строка, данных.Столбец].Статус.ЯчейкуМожноСохранить = данных.СохранениеРазрешено;
                            значений[данных.Строка, данных.Столбец].Статус.ЯчейкаЗаполненаВерно = false;
                            списокОшибокЗаполнения.Add(данных);
                        }
                    }
                }
            }
            списокОшибокЗаполнения.AddRange(this.ОтчетнаяФорма.Данные.ПроверитьУникальностьСтрокДинамическихТаблиц());
            if (this.ОтчетнаяФорма.КомпонентОтчетногоПериода.ОтчетныйПериод.ДатаНачала >= Конвертер.ВДату("01.01.2009"))
            {
                СверкаКлассификаторов классификаторов = this.ОтчетнаяФорма.ПолучитьОбъектСверкиКлассификаторов();
                классификаторов.ПроверятьНаПустоту = false;
                классификаторов.УдалятьНеверныеСтроки = false;
                списокОшибокЗаполнения.AddRange(классификаторов.Проверить(this.ОтчетнаяФорма.Данные));
            }
            foreach (СтрокаОтчетаСверкиДанных данных2 in списокОшибокЗаполнения)
            {
                if (данных2.СохранениеРазрешено)
                {
                    количествоПредупреждений++;
                }
                else
                {
                    количествоОшибок++;
                    flag = false;
                }
            }
            return flag;
        }

        public void СброситьНастройкиВкладок(System.Type ТипФормы)
        {
            if (((this != null) && (this.РежимРаботы == РежимРаботыЭкраннойФормы.Просмотр)) && (ТипФормы != null))
            {
                System.Type type = ТипФормы;
                try
                {
                    new НастройкиГруппыВкладок(this.xtraTabControl_ГруппаЗакладок, type, this.ОтчетнаяФорма).СброситьНастройки();
                    this.настройкиСброшены = true;
                    Сообщение.Показать("Настройки вкладок сброшены. Настройки установленные по умолчанию вступят в силу после следующего открытия этой формы.");
                }
                catch (Exception)
                {
                }
            }
        }

        public void СверитьСЗеркалом()
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                new БазовыйОбработчикСводнойФормы(this.отчетнаяФорма).СверкаСЗеркаломДанных();
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void сдвинутьВКонецToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedTabPageIndex = this.xtraTabControl_ГруппаЗакладок.SelectedTabPageIndex;
            if (selectedTabPageIndex != (this.xtraTabControl_ГруппаЗакладок.TabPages.Count - 1))
            {
                XtraTabPage selectedTabPage = this.xtraTabControl_ГруппаЗакладок.SelectedTabPage;
                this.xtraTabControl_ГруппаЗакладок.TabPages.RemoveAt(selectedTabPageIndex);
                this.xtraTabControl_ГруппаЗакладок.TabPages.Add(selectedTabPage);
                this.xtraTabControl_ГруппаЗакладок.SelectedTabPageIndex = this.xtraTabControl_ГруппаЗакладок.TabPages.Count - 1;
                this.comboBoxEdit_Закладки.Properties.Items.RemoveAt(selectedTabPageIndex);
                this.comboBoxEdit_Закладки.Properties.Items.Insert(this.xtraTabControl_ГруппаЗакладок.SelectedTabPageIndex, this.xtraTabControl_ГруппаЗакладок.SelectedTabPage.Text);
            }
        }

        private void сдвинутьВлевоНаОдинШагToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedTabPageIndex = this.xtraTabControl_ГруппаЗакладок.SelectedTabPageIndex;
            if (selectedTabPageIndex != 0)
            {
                XtraTabPage selectedTabPage = this.xtraTabControl_ГруппаЗакладок.SelectedTabPage;
                this.xtraTabControl_ГруппаЗакладок.TabPages.Move(selectedTabPageIndex - 1, selectedTabPage);
                this.xtraTabControl_ГруппаЗакладок.SelectedTabPageIndex = selectedTabPageIndex - 1;
                this.comboBoxEdit_Закладки.Properties.Items.RemoveAt(selectedTabPageIndex);
                this.comboBoxEdit_Закладки.Properties.Items.Insert(this.xtraTabControl_ГруппаЗакладок.SelectedTabPageIndex, this.xtraTabControl_ГруппаЗакладок.SelectedTabPage.Text);
            }
        }

        private void сдвинутьВначалоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedTabPageIndex = this.xtraTabControl_ГруппаЗакладок.SelectedTabPageIndex;
            if (selectedTabPageIndex != 0)
            {
                XtraTabPage selectedTabPage = this.xtraTabControl_ГруппаЗакладок.SelectedTabPage;
                this.xtraTabControl_ГруппаЗакладок.TabPages.Move(0, selectedTabPage);
                this.xtraTabControl_ГруппаЗакладок.SelectedTabPageIndex = 0;
                this.comboBoxEdit_Закладки.Properties.Items.RemoveAt(selectedTabPageIndex);
                this.comboBoxEdit_Закладки.Properties.Items.Insert(this.xtraTabControl_ГруппаЗакладок.SelectedTabPageIndex, this.xtraTabControl_ГруппаЗакладок.SelectedTabPage.Text);
            }
        }

        private void сдвинутьВправоНаОдинШагToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedTabPageIndex = this.xtraTabControl_ГруппаЗакладок.SelectedTabPageIndex;
            if (selectedTabPageIndex != (this.xtraTabControl_ГруппаЗакладок.TabPages.Count - 1))
            {
                XtraTabPage page = this.xtraTabControl_ГруппаЗакладок.TabPages[selectedTabPageIndex + 1];
                this.xtraTabControl_ГруппаЗакладок.TabPages.Move(selectedTabPageIndex, page);
                this.xtraTabControl_ГруппаЗакладок.SelectedTabPageIndex = selectedTabPageIndex + 1;
                this.comboBoxEdit_Закладки.Properties.Items.RemoveAt(selectedTabPageIndex);
                this.comboBoxEdit_Закладки.Properties.Items.Insert(this.xtraTabControl_ГруппаЗакладок.SelectedTabPageIndex, this.xtraTabControl_ГруппаЗакладок.SelectedTabPage.Text);
            }
        }

        public void СкрытьПанельПоиска()
        {
            this.FindTool.Visible = false;
        }

        public void СобратьФорму()
        {
            БазовыйОбработчикСводнойФормы формы = null;
            формы = (БазовыйОбработчикСводнойФормы) this.ОтчетнаяФорма.ВыполнитьМетодМакроса("ПолучитьОбработчикСводнойФормы", new object[] { this.отчетнаяФорма });
            if (формы == null)
            {
                формы = new БазовыйОбработчикСводнойФормы(this.отчетнаяФорма);
            }
            this.СобратьФорму(формы);
        }

        private void СобратьФорму(БазовыйОбработчикСводнойФормы обработчик)
        {
            if ((обработчик != null) && обработчик.СобратьФорму())
            {
                this.ОбновитьДанные();
                this.ПересчитатьАвтоблоки();
                if (Сообщение.ПоказатьВопрос("Сохранить данные сводной формы ?", "Формирование сводной формы") == РезультатДиалога.Да)
                {
                    List<СтрокаОтчетаСверкиДанных> list = new List<СтрокаОтчетаСверкиДанных>();
                    if (обработчик.ПолучитьСписокОшибок().Count == 0)
                    {
                        this.СохранитьДанные(new КонтекстСохраненияОтчетнойФормы(true, true));
                    }
                    else
                    {
                        РезультатыВыполненияСверкиДанных данных = new РезультатыВыполненияСверкиДанных();
                        данных.Заголовок = string.Format("Во время проверки данных были обнаружены ошибки", new object[0]);
                        данных.Подзаголовок = "Данные не будут сохранены";
                        данных.СтрокиСверки.AddRange(обработчик.ПолучитьСписокОшибок());
                        данных.СброситьСписокСтолбцов();
                        данных.ДобавитьОтображаемыйСтолбец("Таблица", "Субтаблица");
                        данных.ДобавитьОтображаемыйСтолбец("Столбец", "Столбец");
                        данных.ДобавитьОтображаемыйСтолбец("Строка", "Строка");
                        данных.ДобавитьОтображаемыйСтолбец("Условие", "Условие");
                        данных.ДобавитьОтображаемыйСтолбец("Ошибка", "ТипОшибки");
                        данных.ДобавитьОтображаемыйСтолбец("Сохранение разрешено", "СохранениеРазрешено");
                        new ФормаОтчетаСверкиДанных(данных).ПоказатьДиалог();
                    }
                    this.ОтчетнаяФорма.ЗаблокироватьФорму();
                }
            }
        }

        public void СохранениеНастроекВкладок(System.Type ТипФормы)
        {
            if ((((this != null) && (this.РежимРаботы == РежимРаботыЭкраннойФормы.Просмотр)) && !this.настройкиСброшены) && (ТипФормы != null))
            {
                System.Type type = ТипФормы;
                try
                {
                    new НастройкиГруппыВкладок(this.xtraTabControl_ГруппаЗакладок, type, this.ОтчетнаяФорма).СериализоватьНастройки();
                }
                catch (Exception)
                {
                }
            }
        }

        public void СохранитьДанные()
        {
            this.СохранитьДанные(false);
        }

        public void СохранитьДанные(bool продублироватьВЗеркало)
        {
            this.СохранитьДанные(new КонтекстСохраненияОтчетнойФормы(продублироватьВЗеркало, false));
        }

        public void СохранитьДанные(КонтекстСохраненияОтчетнойФормы контекстСохранения)
        {
            foreach (ДинамическаяТаблица таблица in this.ДинамическиеТаблицы.Values)
            {
                if (!(!таблица.РазмещатьНаЗакладке || this.ОтчетнаяФорма.ОбработатьСобытие_СохраненияСубтаблицы(таблица.КодТаблицы, таблица.Данные.ТаблицаДанных)))
                {
                    таблица.СохранитьЗеркалоДанных();
                    таблица.ЭкраннаяФорма.ОтчетнаяФорма.ОбработатьСобытие_ПослеСохраненияСубтаблицы(таблица.КодТаблицы, таблица.Данные.ТаблицаДанных);
                }
            }
            this.ОтчетнаяФорма.СохранитьДанные(контекстСохранения);
            this.ДанныеИзменились = false;
        }

        public XmlDocument СохранитьФорму()
        {
            XmlElement element3;
            XmlDocument document3;
            XmlDocument document = new XmlDocument();
            document.PreserveWhitespace = false;
            XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "utf-8", "");
            document.AppendChild(newChild);
            XmlElement element = document.CreateElement("ЭкраннаяФорма");
            document.AppendChild(element);
            if (this.Шапка != null)
            {
                XmlElement element2 = document.CreateElement("Шапка");
                document.DocumentElement.AppendChild(element2);
                XmlDocument document2 = this.Шапка.СериализоватьВXML();
                element2.InnerXml = document2.DocumentElement.OuterXml;
            }
            List<string> list = new List<string>();
            foreach (ИнтерфейсОтображаемойТаблицыЭкраннойФормы формы in this.отображаемыеТаблицы)
            {
                list.Add(формы.КодТаблицы + формы.Наименование);
                if (формы is ТаблицаОтчетнойФормы)
                {
                    element3 = document.CreateElement("Закладка");
                    document.DocumentElement.AppendChild(element3);
                    element3.SetAttribute("Код", формы.КодТаблицы);
                    element3.SetAttribute("Наименование", формы.Наименование);
                    element3.SetAttribute("ИмяЛиста", формы.ИмяЛиста);
                    document3 = формы.СериализоватьВXML();
                    element3.InnerXml = document3.DocumentElement.OuterXml;
                }
                else if (формы is ДинамическаяТаблица)
                {
                    element3 = document.CreateElement("ДинамическаяТаблица");
                    document.DocumentElement.AppendChild(element3);
                    element3.SetAttribute("Код", формы.КодТаблицы);
                    element3.SetAttribute("Наименование", формы.Наименование);
                    element3.SetAttribute("ИмяЛиста", формы.ИмяЛиста);
                    element3.SetAttribute("РазмещатьНаЗакладке", формы.РазмещатьНаЗакладке ? "Да" : "Нет");
                    document3 = формы.СериализоватьВXML();
                    element3.InnerXml = document3.DocumentElement.OuterXml;
                }
            }
            foreach (ТаблицаОтчетнойФормы формы2 in this.Таблицы)
            {
                if (!list.Contains(формы2.КодТаблицы + формы2.Наименование))
                {
                    element3 = document.CreateElement("Закладка");
                    document.DocumentElement.AppendChild(element3);
                    element3.SetAttribute("Код", формы2.КодТаблицы);
                    element3.SetAttribute("Наименование", формы2.Наименование);
                    element3.SetAttribute("ИмяЛиста", формы2.ИмяЛиста);
                    document3 = формы2.СериализоватьВXML();
                    element3.InnerXml = document3.DocumentElement.OuterXml;
                }
            }
            foreach (ДинамическаяТаблица таблица in this.ДинамическиеТаблицы.Values)
            {
                if (!list.Contains(таблица.КодТаблицы + таблица.Наименование))
                {
                    element3 = document.CreateElement("ДинамическаяТаблица");
                    document.DocumentElement.AppendChild(element3);
                    element3.SetAttribute("Код", таблица.КодТаблицы);
                    element3.SetAttribute("Наименование", таблица.Наименование);
                    element3.SetAttribute("ИмяЛиста", таблица.ИмяЛиста);
                    element3.SetAttribute("РазмещатьНаЗакладке", таблица.РазмещатьНаЗакладке ? "Да" : "Нет");
                    document3 = таблица.СериализоватьВXML();
                    element3.InnerXml = document3.DocumentElement.OuterXml;
                }
            }
            return document;
        }

        public void СохранитьФорму(string ПутьКФайлу)
        {
            this.СохранитьФорму().Save(ПутьКФайлу);
        }

        private void СформироватьСловарьОграниченийНаЭлементы()
        {
            if (!string.IsNullOrEmpty(this.ОграниченияНаЭлементы))
            {
                this.ОграниченияНаЭлементы = this.ОграниченияНаЭлементы.Trim();
                int startIndex = 0;
                try
                {
                    while (startIndex < this.ОграниченияНаЭлементы.Length)
                    {
                        string str = "";
                        string key = "";
                        string str3 = "";
                        int index = -1;
                        index = this.ОграниченияНаЭлементы.IndexOf('@', startIndex);
                        if (index == -1)
                        {
                            throw new Exception("@ не найдено начиная с позиции" + startIndex.ToString());
                        }
                        str = this.ОграниченияНаЭлементы.Substring(startIndex, index - startIndex);
                        startIndex = index + 1;
                        index = this.ОграниченияНаЭлементы.IndexOf('|', startIndex);
                        if (index == -1)
                        {
                            throw new Exception("| не найдена начиная с позиции" + startIndex.ToString());
                        }
                        int num3 = this.ОграниченияНаЭлементы.IndexOf(',', startIndex, index - startIndex);
                        if (num3 == -1)
                        {
                            num3 = index;
                        }
                        while (num3 != -1)
                        {
                            int num4 = this.ОграниченияНаЭлементы.IndexOf(':', startIndex, num3 - startIndex);
                            if (num4 == -1)
                            {
                                key = this.ОграниченияНаЭлементы.Substring(startIndex, num3 - startIndex);
                                str3 = "Таблица";
                            }
                            else
                            {
                                key = this.ОграниченияНаЭлементы.Substring(startIndex, num4 - startIndex);
                                str3 = this.ОграниченияНаЭлементы.Substring(num4 + 1, (num3 - num4) - 1);
                            }
                            if (str != "ПолныйДоступ")
                            {
                                if (!this.СписокОграниченийНаЭлементы.ContainsKey(key))
                                {
                                    this.СписокОграниченийНаЭлементы.Add(key, new Dictionary<string, string>());
                                }
                                if (str == "ЗапретРедактирования")
                                {
                                    if (str3 == "Таблица")
                                    {
                                        Dictionary<string, string> dictionary = new Dictionary<string, string>();
                                        foreach (KeyValuePair<string, string> pair in this.СписокОграниченийНаЭлементы[key])
                                        {
                                            if (pair.Value == "ЗапретПросмотра")
                                            {
                                                dictionary.Add(pair.Key, pair.Value);
                                            }
                                        }
                                        this.СписокОграниченийНаЭлементы[key] = dictionary;
                                        if (!this.СписокОграниченийНаЭлементы[key].ContainsKey(str3))
                                        {
                                            this.СписокОграниченийНаЭлементы[key].Add(str3, str);
                                        }
                                    }
                                    else if (!this.СписокОграниченийНаЭлементы[key].ContainsKey("Таблица") && !this.СписокОграниченийНаЭлементы[key].ContainsKey(str3))
                                    {
                                        this.СписокОграниченийНаЭлементы[key].Add(str3, str);
                                    }
                                }
                                else
                                {
                                    if (!(str == "ЗапретПросмотра"))
                                    {
                                        throw new Exception("Права доступа какие то левые для таблицы с кодом" + key);
                                    }
                                    if (str3 == "Таблица")
                                    {
                                        this.СписокОграниченийНаЭлементы[key].Clear();
                                        this.СписокОграниченийНаЭлементы[key].Add(str3, str);
                                    }
                                    else if (!this.СписокОграниченийНаЭлементы[key].ContainsKey("Таблица"))
                                    {
                                        if (!this.СписокОграниченийНаЭлементы[key].ContainsKey(str3))
                                        {
                                            this.СписокОграниченийНаЭлементы[key].Add(str3, str);
                                        }
                                        else
                                        {
                                            this.СписокОграниченийНаЭлементы[key][str3] = str;
                                        }
                                    }
                                    else if (this.СписокОграниченийНаЭлементы[key]["Таблица"] != "ЗапретПросмотра")
                                    {
                                        if (!this.СписокОграниченийНаЭлементы[key].ContainsKey(str3))
                                        {
                                            this.СписокОграниченийНаЭлементы[key].Add(str3, str);
                                        }
                                        else
                                        {
                                            this.СписокОграниченийНаЭлементы[key][str3] = str;
                                        }
                                    }
                                }
                            }
                            startIndex = num3 + 1;
                            if (startIndex < index)
                            {
                                num3 = this.ОграниченияНаЭлементы.IndexOf(',', startIndex, index - startIndex);
                                if (num3 == -1)
                                {
                                    num3 = index;
                                }
                            }
                            else
                            {
                                num3 = -1;
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    this.СписокОграниченийНаЭлементы.Clear();
                }
            }
        }

        public void УстановитьЗначениеПеременной(string ИмяПеременной, object ЗначениеПеременной)
        {
            this.УстановитьЗначениеПеременной(ИмяПеременной, ЗначениеПеременной, true);
        }

        public void УстановитьЗначениеПеременной(string ИмяПеременной, object ЗначениеПеременной, bool НезависимоОтРегистра)
        {
            if (!string.IsNullOrEmpty(ИмяПеременной) && (this.списокПеременных != null))
            {
                string key = НезависимоОтРегистра ? ИмяПеременной.Trim().ToLower() : ИмяПеременной.Trim();
                if (this.ПеременнаяОбъявлена(ИмяПеременной, НезависимоОтРегистра))
                {
                    this.списокПеременных[key] = ЗначениеПеременной;
                }
                else
                {
                    this.списокПеременных.Add(key, ЗначениеПеременной);
                }
            }
        }

        public void ФокусироватьНаЯчейке(string КодТаблицы, string КодСтроки, string КодСтолбца)
        {
            if (!string.IsNullOrEmpty(КодТаблицы) || (!string.IsNullOrEmpty(КодСтроки) && !string.IsNullOrEmpty(КодСтолбца)))
            {
                ТаблицаОтчетнойФормы формы = null;
                foreach (XtraTabPage page in this.xtraTabControl_ГруппаЗакладок.TabPages)
                {
                    Control control = page.Controls[0];
                    if (control is ТаблицаОтчетнойФормы)
                    {
                        формы = control as ТаблицаОтчетнойФормы;
                        if (!string.IsNullOrEmpty(КодТаблицы))
                        {
                            if (формы.КодТаблицы == КодТаблицы)
                            {
                                this.xtraTabControl_ГруппаЗакладок.SelectedTabPage = page;
                                page.Focus();
                                break;
                            }
                        }
                        else if (формы.ТаблицаМетаструктуры.Строки.ContainsKey(КодСтолбца))
                        {
                            this.xtraTabControl_ГруппаЗакладок.SelectedTabPage = page;
                            page.Focus();
                            break;
                        }
                    }
                }
                if (формы != null)
                {
                    формы.Focus();
                    for (int i = 1; i <= формы.RowCount; i++)
                    {
                        for (int j = 1; j <= формы.ColCount; j++)
                        {
                            GridStyleInfo info = формы[i, j];
                            if (info.Tag != null)
                            {
                                string str = info.Tag.ToString();
                                ТипЯчейки ячейки = формы.ТаблицаДанных[str];
                                if ((формы.ТаблицаДанных[str] != null) && (str == ("$" + КодСтроки + ":" + КодСтолбца + "$")))
                                {
                                    формы.CurrentCell.MoveTo(i, j, GridSetCurrentCellOptions.SetFocus | GridSetCurrentCellOptions.ScrollInView);
                                    формы.Refresh();
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void ЭкраннаяФорма_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.РазрешеноЗакрытьФорму)
            {
                e.Cancel = true;
            }
            else if (this.ОтчетнаяФорма.ОбработатьСобытие_ЗакрытиеФормы(base.DialogResult))
            {
                e.Cancel = true;
            }
            else
            {
                if ((this.ВариантОткрытия == ВариантОткрытияФормы.Чтение) || (this.РежимРаботы != РежимРаботыЭкраннойФормы.Просмотр))
                {
                    base.DialogResult = DialogResult.Cancel;
                }
                else
                {
                    bool flag = false;
                    this.simpleButton_применить.Focus();
                    if (base.DialogResult == DialogResult.OK)
                    {
                        flag = true;
                    }
                    else if (this.ДанныеИзменились)
                    {
                        DialogResult result = XtraMessageBox.Show("Вы желаете сохранить изменения?", "Сохранение формы", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        if (result == DialogResult.Cancel)
                        {
                            e.Cancel = true;
                            return;
                        }
                        if ((result == DialogResult.No) && this.ОтчетнаяФорма.ОбработатьСобытие_ЗакрытиеФормыСОтказомОтСохранения())
                        {
                            e.Cancel = true;
                            return;
                        }
                        if (result == DialogResult.Yes)
                        {
                            if (this.ОтчетнаяФорма.ОбработатьСобытие_ЗакрытиеФормыССохранением())
                            {
                                e.Cancel = true;
                                return;
                            }
                            flag = true;
                        }
                    }
                    if (flag)
                    {
                        РезультатыВыполненияСверкиДанных данных;
                        List<СтрокаОтчетаСверкиДанных> list = new List<СтрокаОтчетаСверкиДанных>();
                        int num = 0;
                        int num2 = 0;
                        if (!this.ПроверитьКорректностьДанных(ref list, ref num, ref num2))
                        {
                            flag = false;
                            данных = new РезультатыВыполненияСверкиДанных();
                            данных.Заголовок = string.Format("Во время проверки данных были обнаружены ошибки и предупреждения (количество ошибок: {0}, количество предупреждений: {1})", num, num2);
                            данных.Подзаголовок = "Так как форма содержит ошибки, форма сохранена не будет.";
                            данных.СтрокиСверки.AddRange(list);
                            данных.СброситьСписокСтолбцов();
                            данных.ДобавитьОтображаемыйСтолбец("Таблица", "Субтаблица");
                            данных.ДобавитьОтображаемыйСтолбец("Столбец", "Столбец");
                            данных.ДобавитьОтображаемыйСтолбец("Строка", "Строка");
                            данных.ДобавитьОтображаемыйСтолбец("Условие", "Условие");
                            данных.ДобавитьОтображаемыйСтолбец("Ошибка", "ТипОшибки");
                            данных.ДобавитьОтображаемыйСтолбец("Сохранение разрешено", "СохранениеРазрешено");
                            ФормаОтчетаСверкиДанных данных2 = new ФормаОтчетаСверкиДанных(данных);
                            данных2.ПоказатьДиалог();
                            base.DialogResult = DialogResult.None;
                            e.Cancel = true;
                            return;
                        }
                        if ((list != null) && (list.Count > 0))
                        {
                            данных = new РезультатыВыполненияСверкиДанных();
                            данных.Заголовок = string.Format("Во время проверки данных были обнаружены предупреждения (количество предупреждений: {0})", num2);
                            данных.Подзаголовок = "Так как форма содержит только предупреждения, форма будет сохранена.";
                            данных.СтрокиСверки.AddRange(list);
                            данных.СброситьСписокСтолбцов();
                            данных.ДобавитьОтображаемыйСтолбец("Таблица", "Субтаблица");
                            данных.ДобавитьОтображаемыйСтолбец("Столбец", "Столбец");
                            данных.ДобавитьОтображаемыйСтолбец("Строка", "Строка");
                            данных.ДобавитьОтображаемыйСтолбец("Условие", "Условие");
                            данных.ДобавитьОтображаемыйСтолбец("Ошибка", "ТипОшибки");
                            данных.ДобавитьОтображаемыйСтолбец("Сохранение разрешено", "СохранениеРазрешено");
                            new ФормаОтчетаСверкиДанных(данных).ПоказатьДиалог();
                        }
                    }
                    if (flag)
                    {
                        Exception exception;
                        try
                        {
                            new АвтосохранениеДанныхОтчетнойФормы(this.ОтчетнаяФорма).СохранитьДанныеФормыАвтоматически(true);
                        }
                        catch (Exception)
                        {
                        }
                        this.Cursor = Cursors.WaitCursor;
                        bool flag2 = true;
                        try
                        {
                            flag2 = !this.ОтчетнаяФорма.ОбработатьСобытие_ДоСохраненияФормы();
                        }
                        catch (Exception exception2)
                        {
                            exception = exception2;
                            Сообщение.ПоказатьИсключительнуюСитуацию("В методе 'Обработать событие до сохранения формы' возникла исключительная ситуация", exception);
                            flag2 = false;
                            base.DialogResult = DialogResult.None;
                            e.Cancel = true;
                        }
                        if (flag2)
                        {
                            this.ОбновитьДанные();
                            Application.DoEvents();
                            this.ПересчитатьАвтоблоки();
                            Application.DoEvents();
                            ФормаИндикаторПроцесса процесса = new ФормаИндикаторПроцесса();
                            процесса.TopMost = false;
                            процесса.УстановитьЗначениеИндикатора(0, "Сохранение данных...");
                            процесса.Show(this);
                            try
                            {
                                try
                                {
                                    МенеджерБД.ПроверитьСоединение();
                                    this.СохранитьДанные();
                                    процесса.Закрыть();
                                }
                                catch (Exception exception3)
                                {
                                    exception = exception3;
                                    процесса.Закрыть();
                                    base.DialogResult = DialogResult.None;
                                    e.Cancel = true;
                                    if ((exception.Message.Contains("ORA-03135") || exception.Message.Contains("ORA-03114")) || exception.Message.Contains("ORA-12571"))
                                    {
                                        if (МенеджерБД.ПроверитьСоединение())
                                        {
                                            Сообщение.ПоказатьОшибку("Во время выполения операции произошел обрыв связи.\r\nВ данный момент соединение восстановленно.\r\nДля сохранения данных формы в базу данных повторите операцию.");
                                        }
                                        else
                                        {
                                            Сообщение.ПоказатьОшибку("Во время выполения операции произошел обрыв связи и восстановить соединение не удалось.\r\nВсе ваши данные были сохранены, и могут быть восстановлены через пункт \"Функции-Восстановление данных\".");
                                        }
                                    }
                                    else
                                    {
                                        Сообщение.ПоказатьИсключительнуюСитуацию("Во время сохранения данных произошла ошибка.\r\nВсе ваши данные были сохранены, и могут быть восстановлены через пункт \"Функции-Восстановление данных\".", exception);
                                    }
                                }
                            }
                            finally
                            {
                                процесса.УстановитьЗначениеИндикатора(100, "Сохранение данных...");
                                процесса.Закрыть();
                                this.Cursor = Cursors.Default;
                            }
                        }
                    }
                }
                if (!e.Cancel)
                {
                    try
                    {
                        GridFormulaEngine.UnregisterGridAsSheet("Шапка", this.Шапка.Представление);
                        this.Шапка.Dispose();
                        if (!Приложение.Параметры.ПараметрЗадан("РежимТестировщика"))
                        {
                            this.СохранениеНастроекВкладок(base.GetType());
                        }
                        foreach (ИнтерфейсОтображаемойТаблицыЭкраннойФормы формы2 in this.отображаемыеТаблицы)
                        {
                            if (формы2 is ТаблицаОтчетнойФормы)
                            {
                                GridFormulaEngine.UnregisterGridAsSheet(формы2.ИмяЛиста, (формы2 as ТаблицаОтчетнойФормы).Представление);
                            }
                            else if (((формы2 is ДинамическаяТаблица) && (this.РежимРаботы != РежимРаботыЭкраннойФормы.ПредварительныйПросмотр)) && !Приложение.Параметры.ПараметрЗадан("РежимТестировщика"))
                            {
                                (формы2 as ДинамическаяТаблица).СохранениеНастроек(base.GetType(), формы2.Заголовок);
                            }
                        }
                        foreach (ТаблицаОтчетнойФормы формы3 in this.таблицы)
                        {
                            формы3.Dispose();
                        }
                        foreach (ДинамическаяТаблица таблица in this.динамическиеТаблицы.Values)
                        {
                            таблица.Dispose();
                        }
                        this.отображаемыеТаблицы.Clear();
                        this.таблицы.Clear();
                        this.динамическиеТаблицы.Clear();
                    }
                    catch (Exception)
                    {
                    }
                    foreach (BarButtonItemLink link in this.barSubItem_ИмпортДанных.ItemLinks)
                    {
                        ИмпортерДанныхОтчетныхФорм tag = link.Item.Tag as ИмпортерДанныхОтчетныхФорм;
                        tag.Форма = null;
                    }
                    foreach (BarButtonItemLink link in this.barSubItem_ЭкспортДанных.ItemLinks)
                    {
                        ЭкспортерДанныхОтчетныхФорм форм2 = link.Item.Tag as ЭкспортерДанныхОтчетныхФорм;
                        форм2.Форма = null;
                    }
                    this.timer_автосохранение.Stop();
                }
            }
        }

        private void ЭкраннаяФорма_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.F))
            {
                this.ОтобразитьПанельПоиска();
            }
            else if (e.KeyCode == Keys.F1)
            {
                ИнтерфейсОтображаемойТаблицыЭкраннойФормы формы = this.ТаблицаТекущейЗакладки;
                this.ОткрытьМетодическийСправочник(формы);
            }
            else
            {
                int selectedTabPageIndex;
                int num2;
                if (e.Control && (e.KeyCode == Keys.Next))
                {
                    selectedTabPageIndex = this.xtraTabControl_ГруппаЗакладок.SelectedTabPageIndex;
                    num2 = this.xtraTabControl_ГруппаЗакладок.TabPages.Count - 1;
                    if (selectedTabPageIndex < num2)
                    {
                        selectedTabPageIndex++;
                        this.xtraTabControl_ГруппаЗакладок.SelectedTabPageIndex = selectedTabPageIndex;
                        this.xtraTabControl_ГруппаЗакладок.Refresh();
                    }
                }
                else if (e.Control && (e.KeyCode == Keys.Prior))
                {
                    selectedTabPageIndex = this.xtraTabControl_ГруппаЗакладок.SelectedTabPageIndex;
                    if (selectedTabPageIndex > 0)
                    {
                        selectedTabPageIndex--;
                        this.xtraTabControl_ГруппаЗакладок.SelectedTabPageIndex = selectedTabPageIndex;
                        this.xtraTabControl_ГруппаЗакладок.Refresh();
                    }
                }
                else if (e.Control && (e.KeyCode == Keys.Home))
                {
                    this.xtraTabControl_ГруппаЗакладок.SelectedTabPageIndex = 0;
                    this.xtraTabControl_ГруппаЗакладок.Refresh();
                }
                else if (e.Control && (e.KeyCode == Keys.End))
                {
                    num2 = this.xtraTabControl_ГруппаЗакладок.TabPages.Count - 1;
                    this.xtraTabControl_ГруппаЗакладок.SelectedTabPageIndex = num2;
                    this.xtraTabControl_ГруппаЗакладок.Refresh();
                }
            }
        }

        private void ЭкраннаяФорма_Load(object sender, EventArgs e)
        {
            if (МенеджерБД.МенеджерИнициализирован)
            {
                this.barSubItem_Архив.Visibility = BarItemVisibility.Always;
                this.barButtonItem_ВосстановитьИзЗеркала.Visibility = BarItemVisibility.Always;
                this.barButtonItem_сверитьСЗеркалом.Visibility = BarItemVisibility.Always;
            }
            else
            {
                this.barSubItem_Архив.Visibility = BarItemVisibility.Never;
                this.barButtonItem_ВосстановитьИзЗеркала.Visibility = BarItemVisibility.Never;
                this.barButtonItem_сверитьСЗеркалом.Visibility = BarItemVisibility.Never;
            }
            if ((this.ОтчетнаяФорма.Данные.Идентификатор.ЭлементЦепочки != ТипЭлементаЦепочки.Абонент) && (this.ОтчетнаяФорма.Данные.Идентификатор.ЭлементЦепочки != ТипЭлементаЦепочки.ПассивныйАбонент))
            {
                if (МенеджерБД.МенеджерИнициализирован)
                {
                    bool flag = (this.ВариантОткрытия == ВариантОткрытияФормы.СводнаяФорма) || (this.ВариантОткрытия == ВариантОткрытияФормы.СводнаяФормаБезРедактирования);
                    this.barButtonItem_собрать.Enabled = flag;
                    this.barButtonItemСобратьПоКритериям.Enabled = flag;
                    this.barButtonItem_ВосстановитьИзЗеркала.Enabled = flag;
                    this.barButtonItem_ОбновитьЗеркало.Enabled = flag;
                    this.barButtonItem_сверитьСЗеркалом.Enabled = true;
                }
                else
                {
                    this.barSubItem_Сводная.Visibility = BarItemVisibility.Never;
                }
            }
            else
            {
                this.barSubItem_Сводная.Visibility = BarItemVisibility.Never;
            }
            this.СкрытьПанельПоиска();
            if ((this.РежимРаботы == РежимРаботыЭкраннойФормы.Просмотр) && ((this.ВариантОткрытия != ВариантОткрытияФормы.Чтение) || (this.ВариантОткрытия != ВариантОткрытияФормы.СводнаяФормаБезРедактирования)))
            {
                this.timer_автосохранение.Start();
                this.ОтчетнаяФорма.ПриОкончанииОбработкиСобытия += new ОбработчикОкончанияОбработкиСобытия(this.ОтчетнаяФорма_ПриОкончанииОбработкиСобытия);
            }
            if ((this.ВариантОткрытия == ВариантОткрытияФормы.Чтение) || (this.РежимРаботы != РежимРаботыЭкраннойФормы.Просмотр))
            {
                this.simpleButton_применить.Enabled = false;
                this.simpleButton_сохранитьИЗакрыть.Enabled = false;
            }
            this.contextMenuStrip_КонтекстноеМенюТабов.ImageList = this.imageList_наборРисунков;
            List<string> list = new List<string>();
            list.Add("Сиреневый");
            list.Add("Красный");
            list.Add("Желтый");
            list.Add("Зеленый");
            list.Add("Голубой");
            list.Add("Синий");
            list.Add("Белый");
            list.Add("Черный");
            ToolStripMenuItem item = new ToolStripMenuItem("Нет цвета");
            this.ToolStripMenuItem_выставитьЦвет.DropDown.Items.Add(item);
            item.Click += new EventHandler(this.toolStripMenuItem_наименованиеЦвета_Click);
            foreach (string str in list)
            {
                item = new ToolStripMenuItem(str);
                this.ToolStripMenuItem_выставитьЦвет.DropDown.Items.Add(item);
                item.Image = this.imageList_наборРисунков.Images[str + ".ico"];
                item.TextImageRelation = TextImageRelation.ImageBeforeText;
                item.Click += new EventHandler(this.toolStripMenuItem_наименованиеЦвета_Click);
            }
            if (this.РежимРаботы != РежимРаботыЭкраннойФормы.ПредварительныйПросмотр)
            {
                this.ЗагрузкаНастроекВкладок(base.GetType());
                foreach (ДинамическаяТаблица таблица in this.ДинамическиеТаблицы.Values)
                {
                    if (таблица.РазмещатьНаЗакладке && !Приложение.Параметры.ПараметрЗадан("РежимТестировщика"))
                    {
                        таблица.ЗагрузкаНастроек(base.GetType(), таблица.Заголовок);
                    }
                }
            }
            if (!Приложение.Параметры.ПараметрЗадан("РежимТестировщика"))
            {
                this.ЗагрузкаНастроекВкладок(base.GetType());
            }
        }

        public ВариантОткрытияФормы ВариантОткрытия
        {
            get
            {
                return this.вариантОткрытия;
            }
            set
            {
                this.вариантОткрытия = value;
            }
        }

        internal bool ДанныеИзменились
        {
            get
            {
                return this.данныеИзменились;
            }
            set
            {
                this.данныеИзменились = value;
            }
        }

        public Dictionary<string, ДинамическаяТаблица> ДинамическиеТаблицы
        {
            get
            {
                return this.динамическиеТаблицы;
            }
            set
            {
                this.динамическиеТаблицы = value;
            }
        }

        public int КодГруппыТаблиц
        {
            get
            {
                if (this.кодГруппыТаблиц == -1)
                {
                    this.кодГруппыТаблиц = GridFormulaEngine.CreateSheetFamilyID();
                }
                return this.кодГруппыТаблиц;
            }
        }

        public int КоличествоОтображаемыхФорм
        {
            get
            {
                return ((this.отображаемыеТаблицы == null) ? 0 : this.отображаемыеТаблицы.Count);
            }
        }

        public string ОграниченияНаЭлементы
        {
            get
            {
                return this.ограниченияНаЭлементы;
            }
            set
            {
                this.ограниченияНаЭлементы = value;
            }
        }

        public List<ИнтерфейсОтображаемойТаблицыЭкраннойФормы> ОтображаемыеТаблицы
        {
            get
            {
                return this.отображаемыеТаблицы;
            }
            set
            {
                this.отображаемыеТаблицы = value;
            }
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

        public bool РазрешеноЗакрытьФорму
        {
            get
            {
                return this.разрешеноЗакрытьФорму;
            }
            set
            {
                this.разрешеноЗакрытьФорму = value;
            }
        }

        public bool РедактированиеРазрешено
        {
            get
            {
                return ((this.РежимРаботы == РежимРаботыЭкраннойФормы.Просмотр) && ((this.ВариантОткрытия != ВариантОткрытияФормы.Чтение) && (this.ВариантОткрытия != ВариантОткрытияФормы.СводнаяФормаБезРедактирования)));
            }
        }

        public РежимРаботыЭкраннойФормы РежимРаботы
        {
            get
            {
                return this.режимРаботы;
            }
            set
            {
                this.режимРаботы = value;
            }
        }

        public Dictionary<string, Dictionary<string, string>> СписокОграниченийНаЭлементы
        {
            get
            {
                return this.списокОграниченийНаЭлементы;
            }
        }

        public ИнтерфейсОтображаемойТаблицыЭкраннойФормы ТаблицаТекущейЗакладки
        {
            get
            {
                if (((this.отображаемыеТаблицы.Count == 0) || (this.xtraTabControl_ГруппаЗакладок.SelectedTabPageIndex < 0)) || (this.xtraTabControl_ГруппаЗакладок.SelectedTabPageIndex >= this.отображаемыеТаблицы.Count))
                {
                    return null;
                }
                return this.отображаемыеТаблицы[this.xtraTabControl_ГруппаЗакладок.SelectedTabPageIndex];
            }
        }

        public List<ТаблицаОтчетнойФормы> Таблицы
        {
            get
            {
                return this.таблицы;
            }
            set
            {
                this.таблицы = value;
            }
        }

        public ШапкаЭкраннойФормы Шапка
        {
            get
            {
                return this.шапка;
            }
            set
            {
                this.шапка = value;
            }
        }
    }
}

