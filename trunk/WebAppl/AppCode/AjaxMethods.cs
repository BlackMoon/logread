using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using ����.�����.�������������;
using ����.�����;
using ����;

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
            
            List<string> �������������� = new List<string>();

            foreach (string ���� in HttpContext.Current.Session.Keys)
            {
                if (! (����.ToUpper().StartsWith("����") || ����.ToUpper().StartsWith("WEB�����")) )
                {
                    ��������������.Add(����);
                }
            }

            // ������� ���������� ���������� ����
            foreach (string ���� in ��������������)
            {
                HttpContext.Current.Session.Remove(����);
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
                    if (element.Value is ������������������������)
                    {
                        �������������������������.�������������������(element.Value as ������������������������);

                        if (SaveForm)
                        {
                            �������������������������������.����������������������������(element.Value as ������������������������);
                        }
                        else
                        {
                            �������������������������������.��������������������������(element.Value as ������������������������);
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
        /// ���������� ���������������� ���������� �������������� � ������ ��������.
        /// ���������� ������, ��� ������� ������ ���� ������� �������������.
        /// </summary>
        /// <param name="ID"></param>
        public static void AddBlockID(string WindowID, ������������������������ BlockID)
        {
            Hashtable blockList = GetBlockList(WindowID);

            if (!blockList.ContainsKey(BlockID.��������������������))
            {
                blockList.Add(BlockID.��������������������, BlockID);
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
            �������������������� ������������ = ��������������������(ID, Session, DataSourceIndex);

            if (�������������������������.������������������(������������.�����������.���������������������������()))
            {
                string ��������� = string.Empty;

                MemoryStream stream = ����.����.����.����������������(������������.�����������, "����������", ������������.�����������.������������);

                if (stream != null)
                {
                    stream.Seek(0, SeekOrigin.Begin);

                    byte[] buf = new byte[stream.Length];

                    if (stream.Read(buf, 0, (int)stream.Length) == stream.Length)
                    {
                        ��������� = System.Text.Encoding.UTF8.GetString(buf);
                    }

                    stream.Close();
                }

                return ���������;
            }

            return "";
        }

        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        public static string GetSign(string ID, string Session, string DataSourceIndex)
        {
            �������������������� ������������ = ��������������������(ID, Session, DataSourceIndex);

            if (�������������������������.������������������(������������.�����������.���������������������������()))
            {
                string ��������� = string.Empty;

                MemoryStream stream = ����.����.����.����������������(������������.�����������, "���");

                if (stream != null)
                {
                    stream.Seek(0, SeekOrigin.Begin);

                    byte[] buf = new byte[stream.Length];

                    if (stream.Read(buf, 0, (int)stream.Length) == stream.Length)
                    {
                        ��������� = System.Text.Encoding.UTF8.GetString(buf);
                    }

                    stream.Close();
                }

                return ���������;
            }

            return "";
        }

        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        public static bool DelSign(string ID, string Session, string DataSourceIndex)
        {
            �������������������� ������������ = ��������������������(ID, Session, DataSourceIndex);

            bool ��������� = false;

            ��������� = ������������.�����������.����������();

            return ���������;
        }

        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        public static bool CheckSign(string ID, string Session, string DataSourceIndex)
        {
            �������������������� ������������ = ��������������������(ID, Session, DataSourceIndex);

            return ������������.�����������.������������();
        }

        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        public static void SaveSign(string ID, string Session, string DataSourceIndex, string Sign)
        {
            �������������������� ������������ = ��������������������(ID, Session, DataSourceIndex);

            ������������������� �������������� = �������������������.�������������������(������������.�����������.���������������������������());

            if (�������������� != null)
            {
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(Sign);

                MemoryStream stream = new MemoryStream(buffer, false);
                ����.����.����.����������������(stream, ��������������, "���");

                stream.Close();

                ��������������.��������� = true;

                ��������������.���������();

                ��������������.���������������();

                �������������������������.�������������������(��������������.���������������������������());
            }
        }

        private static ������������������� ���������������������(string ID, string Session, string DataSourceIndex)
        {
            ������������������� ������������� = null;

            object �������������������������� = ����.�������.����������������������������.��������������������������(ID, Session);

            if (�������������������������� is �������������������������)
            {
                try
                {
                    int �������������������� = -1;

                    if (int.TryParse(DataSourceIndex, out ��������������������))
                    {
                        �������������������� ������������ = (�������������������������� as �������������������������)[��������������������];

                        if (������������ != null)
                        {
                            ������������������������������ ������������� = ������������.��������������������������������������();

                            ���������������������.�����������������������������(�������������);

                            ������������� = new �������������������();
                            �������������.����������������������(�������������);

                            �������������.������������������������� = ������������.�������������������������;
                            �������������.���������� = ������������.����������;
                            �������������.�������������� = ������������.��������������.������������������;

                            �������������������.�����������������������(�������������.������.�������������);

                            �������������.���������������();
                        }
                    }
                }
                catch
                {
                }
            }

            return �������������;
        }

        private static �������������������� ��������������������(string ID, string Session, string DataSourceIndex)
        {
            object �������������������������� = ����.�������.����������������������������.��������������������������(ID, Session);

            if (�������������������������� is �������������������������)
            {
                try
                {
                    int �������������������� = -1;

                    �������������������� ������������ = null;

                    if (int.TryParse(DataSourceIndex, out ��������������������))
                    {
                        ������������ = (�������������������������� as �������������������������)[��������������������];
                    }

                    return ������������;
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
