using System;
using System.Collections.Generic;
using System.Text;

namespace Барс.Клиент.СообщениеКлиенту
{
	public interface IСообщениеКлиенту
	{
		void ПослатьСообщениеКлиенту(string кто, string текстСообщения, string host);
	}
}
