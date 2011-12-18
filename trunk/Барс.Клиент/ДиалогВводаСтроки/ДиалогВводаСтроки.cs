using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Барс.Интерфейс
{
    public partial class ДиалогВводаСтроки : DevExpress.XtraEditors.XtraForm
    {
        #region Конструктор

        public ДиалогВводаСтроки()
        {
            InitializeComponent();
        }

        #endregion

        #region Свойства

        public string Заголовок
        {
            get { return Text; }
            set { Text = value; }
        }

        public string Приглашение
        {
            get { return ПриглашениеКВводу.Text; }
            set { ПриглашениеКВводу.Text = value; }
        }

        public string Текст
        {
            get { return Редактор.Text; }
            set { Редактор.Text = value; }
        }

        #endregion

        #region Статические методы

        private static ДиалогВводаСтроки СоздатьФорму(string Заголовок, string Приглашение, string Текст)
        {
            ДиалогВводаСтроки диалог = new ДиалогВводаСтроки();
            диалог.Заголовок = Заголовок;
            диалог.Приглашение = Приглашение;
            диалог.Текст = Текст;

            return диалог;
        }

        private static string ПоказатьДиалог(string Заголовок, string Приглашение, string Текст)
        {
            ДиалогВводаСтроки диалог = СоздатьФорму(Заголовок, Приглашение, Текст);
            if (диалог.ShowDialog() == DialogResult.OK)
            {
                return диалог.Текст;
            }
            else
            {
                return "";
            }
        }

        public static string Показать(string Заголовок, string Приглашение, string Текст)
        {
            return ПоказатьДиалог(Заголовок, Приглашение, Текст);
        }

        public static string Показать(string Заголовок, string Приглашение)
        {
            return Показать(Заголовок, Приглашение, "");
        }

        public static string Показать(string Заголовок)
        {
            return Показать(Заголовок, "Введите текст :", "");
        }

        public static string Показать()
        {
            return Показать("");
        }

        #endregion
    }
}