using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Ѕарс.—воды.ќтчетна€‘орма;
using Ѕарс.—воды;
using Ѕарс;

namespace Bars
{
    public static class AjaxMethods
    {
        #region Session clear
        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        public static void RemoveListFromSession(string ID)
        {
            try
            {
                HttpContext.Current.Session.Remove(ID);
            }
            catch
            {
            }
        }

        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        public static void ClearSession()
        {
            try
            {
                HttpContext.Current.Session.Clear();
            }
            catch
            {
            }
        }
        #endregion

        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        public static void AbandSession()
        {
            AjaxMethods.RemoveAllBlocks();
            
            List<string> ключи ”далению = new List<string>();

            foreach (string ключ in HttpContext.Current.Session.Keys)
            {
                if (! (ключ.ToUpper().StartsWith("Ѕј–—") || ключ.ToUpper().StartsWith("WEB—¬ќƒџ")) )
                {
                    ключи ”далению.Add(ключ);
                }
            }

            // ќчищаем сессионные переменные форм
            foreach (string ключ in ключи ”далению)
            {
                HttpContext.Current.Session.Remove(ключ);
            }
        }

        #region Block
        private static readonly string SessionName = "UserBlockers";

        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        public static void RemoveBlock(string WindowID)
        {
            RemoveBlockParam(WindowID, true);
        }

        public static void RemoveBlockParam(string WindowID, bool RemoveParam)
        {
            RemoveBlockParam(WindowID, RemoveParam, false);
        }

        public static void RemoveBlockParam(string WindowID, bool RemoveParam, bool SaveForm)
        {
            try
            {
                Hashtable blockList = GetBlockList(WindowID);

                foreach (DictionaryEntry element in blockList)
                {
                    if (element.Value is »дентификаторƒанных‘ормы)
                    {
                        ’ранилищеЅлокировокƒанных.–азблокировать‘орму(element.Value as »дентификаторƒанных‘ормы);

                        if (SaveForm)
                        {
                            Ћогированиеќперацийѕользовател€.«афиксировать—охранение‘ормы(element.Value as »дентификаторƒанных‘ормы);
                        }
                        else
                        {
                            Ћогированиеќперацийѕользовател€.«афиксировать«акрытие‘ормы(element.Value as »дентификаторƒанных‘ормы);
                        }
                    }
                }

                if (RemoveParam)
                {
                    HttpContext.Current.Session.Remove(SessionName + WindowID);
                }
            }
            catch
            {
            }
        }

        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        public static void RemoveAllBlocks()
        {
            try
            {
                List<string> paramsToRemove = new List<string>();

                foreach (string key in HttpContext.Current.Session.Keys)
                {
                    if (key.StartsWith(SessionName))
                    {
                        RemoveBlockParam(key.Substring(SessionName.Length), false);

                        paramsToRemove.Add(key);
                    }
                }

                foreach (string paramToRemove in paramsToRemove)
                {
                    HttpContext.Current.Session.Remove(paramToRemove);
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// ƒобавление заблокированного строкового идентификатора в список значени€.
        /// Ќеобходимо учесть, что строчка должна быть заранее заблокирована.
        /// </summary>
        /// <param name="ID"></param>
        public static void AddBlockID(string WindowID, »дентификаторƒанных‘ормы BlockID)
        {
            Hashtable blockList = GetBlockList(WindowID);

            if (!blockList.ContainsKey(BlockID.»дентификатор—трокой))
            {
                blockList.Add(BlockID.»дентификатор—трокой, BlockID);
            }
        }

        private static Hashtable GetBlockList(string WindowID)
        {
            if (HttpContext.Current.Session[SessionName + WindowID] == null)
            {
                Hashtable blockList = new Hashtable();
                HttpContext.Current.Session[SessionName + WindowID] = blockList;

                return blockList;
            }
            else
            {
                Hashtable blockList = (Hashtable)HttpContext.Current.Session[SessionName + WindowID];

                return blockList;
            }
        }
        #endregion

        #region ECPData
        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        public static string GetStoredData(string ID, string Session, string DataSourceIndex)
        {
            “екуща€ќтчетна€‘орма текуща€‘орма = ѕолучить“екущую‘орму(ID, Session, DataSourceIndex);

            if (’ранилищеЅлокировокƒанных.«аблокировать‘орму(текуща€‘орма.ƒанные‘ормы.ѕолучить»дентификаторƒанных()))
            {
                string –езультат = string.Empty;

                MemoryStream stream = Ѕарс.“ипы.‘айл.—читатьѕоток»зЅƒ(текуща€‘орма.ƒанные‘ормы, "‘айлƒанных", текуща€‘орма.ƒанные‘ормы.—жимать‘айлы);

                if (stream != null)
                {
                    stream.Seek(0, SeekOrigin.Begin);

                    byte[] buf = new byte[stream.Length];

                    if (stream.Read(buf, 0, (int)stream.Length) == stream.Length)
                    {
                        –езультат = System.Text.Encoding.UTF8.GetString(buf);
                    }

                    stream.Close();
                }

                return –езультат;
            }

            return "";
        }

        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        public static string GetSign(string ID, string Session, string DataSourceIndex)
        {
            “екуща€ќтчетна€‘орма текуща€‘орма = ѕолучить“екущую‘орму(ID, Session, DataSourceIndex);

            if (’ранилищеЅлокировокƒанных.«аблокировать‘орму(текуща€‘орма.ƒанные‘ормы.ѕолучить»дентификаторƒанных()))
            {
                string –езультат = string.Empty;

                MemoryStream stream = Ѕарс.“ипы.‘айл.—читатьѕоток»зЅƒ(текуща€‘орма.ƒанные‘ормы, "Ё÷ѕ");

                if (stream != null)
                {
                    stream.Seek(0, SeekOrigin.Begin);

                    byte[] buf = new byte[stream.Length];

                    if (stream.Read(buf, 0, (int)stream.Length) == stream.Length)
                    {
                        –езультат = System.Text.Encoding.UTF8.GetString(buf);
                    }

                    stream.Close();
                }

                return –езультат;
            }

            return "";
        }

        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        public static bool DelSign(string ID, string Session, string DataSourceIndex)
        {
            “екуща€ќтчетна€‘орма текуща€‘орма = ѕолучить“екущую‘орму(ID, Session, DataSourceIndex);

            bool –езультат = false;

            –езультат = текуща€‘орма.ƒанные‘ормы.”далитьЁ÷ѕ();

            return –езультат;
        }

        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        public static bool CheckSign(string ID, string Session, string DataSourceIndex)
        {
            “екуща€ќтчетна€‘орма текуща€‘орма = ѕолучить“екущую‘орму(ID, Session, DataSourceIndex);

            return текуща€‘орма.ƒанные‘ормы.ѕроверитьЁ÷ѕ();
        }

        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        public static void SaveSign(string ID, string Session, string DataSourceIndex, string Sign)
        {
            “екуща€ќтчетна€‘орма текуща€‘орма = ѕолучить“екущую‘орму(ID, Session, DataSourceIndex);

            ’ранимыеƒанные‘ормы хранимыеƒанные = ’ранилищеƒанных‘орм.ѕолучитьƒанные‘ормы(текуща€‘орма.ƒанные‘ормы.ѕолучить»дентификаторƒанных());

            if (хранимыеƒанные != null)
            {
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(Sign);

                MemoryStream stream = new MemoryStream(buffer, false);
                Ѕарс.“ипы.‘айл.«аписатьѕоток¬Ѕƒ(stream, хранимыеƒанные, "Ё÷ѕ");

                stream.Close();

                хранимыеƒанные.—татусЁ÷ѕ = true;

                хранимыеƒанные.—охранить();

                хранимыеƒанные.—н€тьЅлокировку();

                ’ранилищеЅлокировокƒанных.–азблокировать‘орму(хранимыеƒанные.ѕолучить»дентификаторƒанных());
            }
        }

        private static ќтчетна€‘ормаƒанных ѕолучитьќтчетную‘орму(string ID, string Session, string DataSourceIndex)
        {
            ќтчетна€‘ормаƒанных отчетна€‘орма = null;

            object ќбъект—ессионнойѕеременной = Ѕарс.¬ебядро.ћенеджер—ессионныхѕеременных.ѕолучитьѕеременную»з—ессии(ID, Session);

            if (ќбъект—ессионнойѕеременной is —писок“екущихќтчетных‘орм)
            {
                try
                {
                    int »ндексќбъекта¬—писке = -1;

                    if (int.TryParse(DataSourceIndex, out »ндексќбъекта¬—писке))
                    {
                        “екуща€ќтчетна€‘орма текуща€‘орма = (ќбъект—ессионнойѕеременной as —писок“екущихќтчетных‘орм)[»ндексќбъекта¬—писке];

                        if (текуща€‘орма != null)
                        {
                            »дентификаторћетаописани€‘ормы идентификатор = текуща€‘орма.ѕолучить»дентификаторћетаописани€‘ормы();

                            ’ранилищећетаописаний.ќбновитьЋокальноећетаописание(идентификатор);

                            отчетна€‘орма = new ќтчетна€‘ормаƒанных();
                            отчетна€‘орма.«агрузитьћетаструктуру(идентификатор);

                            отчетна€‘орма. омпонентќтчетногоѕериода = текуща€‘орма. омпонентќтчетногоѕериода;
                            отчетна€‘орма.”чреждение = текуща€‘орма.”чреждение;
                            отчетна€‘орма.Ёлемент÷епочки = текуща€‘орма.Ёлемент÷епочки.“ипЁлемента÷епочки;

                            ’ранилищеƒанных‘орм.ќбновитьЋокальныеƒанные(отчетна€‘орма.ƒанные.»дентификатор);

                            отчетна€‘орма.«агрузитьƒанные();
                        }
                    }
                }
                catch
                {
                }
            }

            return отчетна€‘орма;
        }

        private static “екуща€ќтчетна€‘орма ѕолучить“екущую‘орму(string ID, string Session, string DataSourceIndex)
        {
            object ќбъект—ессионнойѕеременной = Ѕарс.¬ебядро.ћенеджер—ессионныхѕеременных.ѕолучитьѕеременную»з—ессии(ID, Session);

            if (ќбъект—ессионнойѕеременной is —писок“екущихќтчетных‘орм)
            {
                try
                {
                    int »ндексќбъекта¬—писке = -1;

                    “екуща€ќтчетна€‘орма текуща€‘орма = null;

                    if (int.TryParse(DataSourceIndex, out »ндексќбъекта¬—писке))
                    {
                        текуща€‘орма = (ќбъект—ессионнойѕеременной as —писок“екущихќтчетных‘орм)[»ндексќбъекта¬—писке];
                    }

                    return текуща€‘орма;
                }
                catch
                {
                }
            }

            return null;
        }
        #endregion
    }
}
