namespace Барс.Своды.БраузерОтчетныхФорм
{
    using DevExpress.XtraEditors;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class СписокРедактируемыхФорм : List<РедактируемаяФорма>
    {
        public bool Загрузить(string ПапкаКэша)
        {
            try
            {
                if (!Directory.Exists(ПапкаКэша))
                {
                    throw new Exception("Указанная папка кэша форм не существует!");
                }
                foreach (string str in Directory.GetFiles(ПапкаКэша, "*.xml", SearchOption.AllDirectories))
                {
                    if (!Path.GetFileName(str).StartsWith("(ЭФ).") && !Path.GetFileName(str).StartsWith("(ПФ)."))
                    {
                        РедактируемаяФорма item = new РедактируемаяФорма();
                        if (item.ПолучитьОписаниеФормы(str))
                        {
                            base.Add(item);
                        }
                    }
                }
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Невозможно загрузить список доступных форм. Проверьте настройки дизайнера.");
                return false;
            }
            return true;
        }
    }
}

