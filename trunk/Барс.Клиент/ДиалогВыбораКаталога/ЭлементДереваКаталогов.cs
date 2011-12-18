using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Барс.Интерфейс
{
    public enum ТипЭлементаДереваКаталогов
    { 
        НеОпределен,
        МойКомпьютер,
        Диск,
        CDДиск,
        Флопи,
        Каталог,
        Файл
    }

    public enum ТипСодержанияПодчиненныхЭлементов
    {
        НеИзвестно,
        НеСодержитПодчиненныхЭлементов,
        СодержитКаталогиИФайлы,
        СодержитКаталоги,
        СодержитФайлы
    }

	public class ЭлементДереваКаталогов
	{
        private FileSystemInfo объектФайловойСистемы = null;
        private List<ЭлементДереваКаталогов> списокПодчиненныхЭлементов = new List<ЭлементДереваКаталогов>();

        #region Родительский элемент

        private ЭлементДереваКаталогов родитель = null;
        public ЭлементДереваКаталогов Родитель
        {
            get { return родитель; }
            set { родитель = value; }
        }


        #endregion

        #region Флаг состояния загрузки объекта

        public bool ПодчиненныеЭлементыЗагружены
        {
            get { return (списокПодчиненныхЭлементов.Count > 0); }
        }

        #endregion

        #region Тип элемента

        private ТипЭлементаДереваКаталогов типЭлемента = ТипЭлементаДереваКаталогов.НеОпределен;
        public ТипЭлементаДереваКаталогов ТипЭлемента
        { get { return типЭлемента; } }

        #endregion

        #region Проверка является ли объект файловой системы каталогом/файлом/диском

        public bool ЯвляетсяКаталогом
        {
            get { return (объектФайловойСистемы is DirectoryInfo); }
        }

        public bool ЯвляетсяФайлом
        {
            get { return (объектФайловойСистемы is FileInfo); }
        }

        public bool ЯвляетсяДиском
        {
            get
            {
                return ЯвляетсяКаталогом &&
                  (
                      (типЭлемента == ТипЭлементаДереваКаталогов.CDДиск) ||
                      (типЭлемента == ТипЭлементаДереваКаталогов.Диск) ||
                      (типЭлемента == ТипЭлементаДереваКаталогов.Флопи));
            }
        }

        #endregion

        #region Получение объекта файловой системы как каталог/файл/диск

        public DirectoryInfo КакКаталог
        {
            get 
            {
                if (ЯвляетсяКаталогом)
                {
                    return (DirectoryInfo)объектФайловойСистемы;
                }
                else
                {
                    return null;
                }
            }
        }

        public FileInfo КакФайл
        {
            get 
            {
                if (ЯвляетсяФайлом)
                {
                    return (FileInfo)объектФайловойСистемы;
                }
                else
                {
                    return null;
                }
            }
        }

        public DriveInfo КакДиск
        {
            get
            {
                if (ЯвляетсяДиском)
                {
                    return new DriveInfo(Наименование.Substring(0, 1));
                }
                else
                {
                    return null;
                }
            }
        }

        #endregion

        #region Может ли элемент содержать подэлементы

        public bool МожетСодержатьЭлементы
        {
            get { return ЯвляетсяКаталогом || (типЭлемента == ТипЭлементаДереваКаталогов.МойКомпьютер); }
        }

        public ТипСодержанияПодчиненныхЭлементов СодержитЭлементы
        {
            get 
            {
                if (!МожетСодержатьЭлементы)
                {
                    return ТипСодержанияПодчиненныхЭлементов.НеСодержитПодчиненныхЭлементов;
                }

                try
                {
                    if (типЭлемента == ТипЭлементаДереваКаталогов.МойКомпьютер)
                    {
                        return ТипСодержанияПодчиненныхЭлементов.СодержитКаталоги;
                    }

                    ТипСодержанияПодчиненныхЭлементов результат = ТипСодержанияПодчиненныхЭлементов.НеСодержитПодчиненныхЭлементов;
                    if (КакКаталог.GetDirectories().Length > 0)
                    {
                        результат = ТипСодержанияПодчиненныхЭлементов.СодержитКаталоги;

                        if (КакКаталог.GetFiles().Length > 0)
                        {
                            результат = ТипСодержанияПодчиненныхЭлементов.СодержитКаталогиИФайлы;
                        }
                    }
                    else if( КакКаталог.GetFiles().Length > 0 ) 
                    {
                        результат = ТипСодержанияПодчиненныхЭлементов.СодержитФайлы;
                    }

                    return результат;
                }
                catch
                {
                    return ТипСодержанияПодчиненныхЭлементов.НеИзвестно;
                }
            }
        }

        public bool СодержитФайлы
        {
            get
            {
                try
                {
                    if (!ЯвляетсяКаталогом)
                    {
                        return false;
                    }

                    return (КакКаталог.GetFiles().Length > 0);
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool СодержитКаталоги
        {
            get
            {
                try
                {
                    if (типЭлемента == ТипЭлементаДереваКаталогов.МойКомпьютер)
                    {
                        return (DriveInfo.GetDrives().Length > 0);
                    }

                    if (!ЯвляетсяКаталогом)
                    {
                        return false;
                    }

                    return (КакКаталог.GetDirectories().Length > 0);
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool СодержитФайлыИлиКаталоги
        {
            get { return (СодержитФайлы || СодержитКаталоги); }
        }

        public bool СодержитФайлыИКаталоги
        {
            get { return (СодержитФайлы && СодержитКаталоги); }
        }

        #endregion

        #region Обновление списка подчиненных элементов

        public void ОбновитьСписокПодчиненныхЭлементов(bool УчитыватьФайлы)
        {
            списокПодчиненныхЭлементов.Clear();

            if (!ЯвляетсяКаталогом && (типЭлемента != ТипЭлементаДереваКаталогов.МойКомпьютер))
            {
                return;
            }

            try
            {
                if (типЭлемента == ТипЭлементаДереваКаталогов.МойКомпьютер)
                {
                    foreach (DriveInfo диск in DriveInfo.GetDrives())
                    {
                        списокПодчиненныхЭлементов.Add(new ЭлементДереваКаталогов(диск, this));
                    }
                }
                else
                {

                    foreach (DirectoryInfo каталог in КакКаталог.GetDirectories())
                    {
                        списокПодчиненныхЭлементов.Add(new ЭлементДереваКаталогов(каталог, this));
                    }

                    if (УчитыватьФайлы)
                    {
                        foreach (FileInfo файл in КакКаталог.GetFiles())
                        {
                            списокПодчиненныхЭлементов.Add(new ЭлементДереваКаталогов(файл, this));
                        }
                    }
                }
            }
            catch (System.UnauthorizedAccessException ex)
            {
                System.Windows.Forms.MessageBox.Show("Возможно у Вас отсутствуют права доступа к данному элементу.\n" + ex.Message, "Ошибка получения списка подчиненных элементов", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            catch (System.Security.SecurityException ex)
            {
                System.Windows.Forms.MessageBox.Show("Возможно у Вас отсутствуют права доступа к данному элементу.\n" + ex.Message, "Ошибка получения списка подчиненных элементов", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            catch
            {

            }

        }

        #endregion

        #region Получение списка подчиненных элементов

        public List<ЭлементДереваКаталогов> ПолучитьСписокПодчиненныхЭлементов( bool УчитыватьФайлы )
        {
            if (СодержитФайлыИлиКаталоги && !ПодчиненныеЭлементыЗагружены)
            {
                ОбновитьСписокПодчиненныхЭлементов(УчитыватьФайлы);
            }

            return списокПодчиненныхЭлементов;
        }

        #endregion

        #region Загрузка подчиненного пути

        public void ЗагрузитьПодчиненныйПуть(string Путь, bool УчитыватьФайлы)
        {
            string искомыйПуть = Путь.Trim();
            if (!искомыйПуть.EndsWith("\\"))
            {
                искомыйПуть += "\\";
            }

            ОбновитьСписокПодчиненныхЭлементов(УчитыватьФайлы);

            foreach (ЭлементДереваКаталогов элемент in списокПодчиненныхЭлементов)
            {
                string текущийПуть = элемент.ПолноеНаименование;

                if (!текущийПуть.EndsWith("\\"))
                {
                    текущийПуть += "\\";
                }

                if (искомыйПуть.Trim().ToUpper().StartsWith(текущийПуть.Trim().ToUpper()))
                {
                    элемент.ЗагрузитьПодчиненныйПуть(Путь, УчитыватьФайлы);
                    break;
                }
            }

        }

        #endregion

        #region Наименование

        public string Наименование
        {
            get
            {
                if (типЭлемента == ТипЭлементаДереваКаталогов.МойКомпьютер)
                {
                    return "Мой компьютер";
                }

                if (ЯвляетсяКаталогом)
                {
                    return КакКаталог.Name;
                }
                else if (ЯвляетсяФайлом)
                {
                    return КакФайл.Name;
                }
                else
                {
                    return "";
                }
            }
            set
            {
                if( string.IsNullOrEmpty( value ) )
                {
                    System.Windows.Forms.MessageBox.Show("Некорректно передано новое имя элемента.", "Изменение имени элемента", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    return;
                }

                string новыйПуть = Path.Combine( Родитель.ПолноеНаименование, value );
                if( ( ЯвляетсяКаталогом && Directory.Exists( новыйПуть ) ) || ( ЯвляетсяФайлом && File.Exists( новыйПуть ) ) )
                {
                    System.Windows.Forms.MessageBox.Show("Элемент с таким имененм уже присутствует в системе.", "Изменение имени элемента", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    return;
                }

                if ( ( Родитель == null ) || ЯвляетсяДиском || (типЭлемента == ТипЭлементаДереваКаталогов.МойКомпьютер))
                {
                    System.Windows.Forms.MessageBox.Show("Изменение имени данного элемента запрещено.", "Изменение имени элемента", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    if (ЯвляетсяКаталогом)
                    {
                        КакКаталог.MoveTo(новыйПуть);
                    }
                    else if (ЯвляетсяФайлом)
                    {
                        КакФайл.MoveTo(новыйПуть);
                    }

                    Родитель.ОбновитьСписокПодчиненныхЭлементов(ЯвляетсяФайлом);
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Ошибка изменения наименования элемента.\n" + ex.Message, "Изменение имени элемента", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error); 
                }
            }
        }

        #endregion

        #region Полное наименование

        public string ПолноеНаименование
        {
            get
            {
                if (типЭлемента == ТипЭлементаДереваКаталогов.МойКомпьютер)
                {
                    return "Мой компьютер";
                }

                if (ЯвляетсяКаталогом)
                {
                    return КакКаталог.FullName;
                }
                else if (ЯвляетсяФайлом)
                {
                    return КакФайл.FullName;
                }
                else
                {
                    return "";
                }
            }
        }

        #endregion

        #region Конструкторы

        public ЭлементДереваКаталогов(DirectoryInfo Каталог)
		{
			объектФайловойСистемы = Каталог;
            типЭлемента = ТипЭлементаДереваКаталогов.Каталог;
		}

        public ЭлементДереваКаталогов(FileInfo Файл)
        {
            объектФайловойСистемы = Файл;
            типЭлемента = ТипЭлементаДереваКаталогов.Файл;
        }

		public ЭлементДереваКаталогов(string Путь)
		{
            if (Путь.Trim().ToUpper() == "МОЙ КОМПЬЮТЕР")
            {
                типЭлемента = ТипЭлементаДереваКаталогов.МойКомпьютер;
                return;
            }

            if (File.Exists(Путь))
            {
                объектФайловойСистемы = new FileInfo(Путь);
                типЭлемента = ТипЭлементаДереваКаталогов.Файл;
            }
            else if (Directory.Exists(Путь))
            {
                объектФайловойСистемы = new DirectoryInfo(Путь);
                типЭлемента = ТипЭлементаДереваКаталогов.Каталог;
            }
		}

        public ЭлементДереваКаталогов(DriveInfo Диск) : this( new DirectoryInfo( Диск.Name ) )
        {
            if (Диск.DriveType == DriveType.CDRom)
            {
                типЭлемента = ТипЭлементаДереваКаталогов.CDДиск;
            }
            else if (Диск.Name.ToUpper().StartsWith("A"))
            {
                типЭлемента = ТипЭлементаДереваКаталогов.Флопи;
            }
            else
            {
                типЭлемента = ТипЭлементаДереваКаталогов.Диск;
            }
        }

        public ЭлементДереваКаталогов(Environment.SpecialFolder Каталог) : this(Environment.GetFolderPath(Каталог)) { }


        public ЭлементДереваКаталогов(DirectoryInfo Каталог, ЭлементДереваКаталогов Родитель):this(Каталог)
        {
            this.Родитель = Родитель;
        }

        public ЭлементДереваКаталогов(FileInfo Файл, ЭлементДереваКаталогов Родитель):this( Файл )
        {
            this.Родитель = Родитель;
        }

        public ЭлементДереваКаталогов(string Путь, ЭлементДереваКаталогов Родитель):this(Путь)
        {
            this.Родитель = Родитель;
        }

        public ЭлементДереваКаталогов(DriveInfo Диск, ЭлементДереваКаталогов Родитель):this( Диск )
        {
            this.Родитель = Родитель;
        }

        public ЭлементДереваКаталогов(Environment.SpecialFolder Каталог, ЭлементДереваКаталогов Родитель) : this(Каталог) 
        {
            this.Родитель = Родитель;
        }

        #endregion

        #region Преобразование в строку

        public override string ToString()
		{
            return ПолноеНаименование;
        }

        #endregion
    }
}
