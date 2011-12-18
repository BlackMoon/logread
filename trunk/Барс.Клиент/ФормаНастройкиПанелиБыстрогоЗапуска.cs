using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Xml;

namespace Барс.Клиент
{
	public partial class ФормаНастройкиПанелиБыстрогоЗапуска : XtraForm
	{

		private List<ЭлементыМеню> списокЭлементовМеню = new List<ЭлементыМеню>();

		public List<ЭлементыМеню> СписокЭлементовМеню
		{
			get { return списокЭлементовМеню; }
			set { списокЭлементовМеню = value; }
		}

		private ImageList imageList_ДеревоМеню = new ImageList();

		public ImageList ImageList_ДеревоМеню
		{
			get { return imageList_ДеревоМеню; }
			set { imageList_ДеревоМеню = value; }
		}

		public ФормаНастройкиПанелиБыстрогоЗапуска()
		{
			InitializeComponent();
		}

		private void ФормаНастройкиПанелиБыстрогоЗапуска_Load(object sender, EventArgs e)
		{
			treeList_ЭлементыМеню.Appearance.Row.Font = this.Font;
			treeList_ЭлементыМеню.Appearance.HeaderPanel.Font = this.Font;
			contextMenuStrip1.Font = this.Font;
			simpleButton2.Font = this.Font;
			simpleButton1.Font = this.Font;
			
			treeList_ЭлементыМеню.ParentFieldName = "Родитель";
			treeList_ЭлементыМеню.KeyFieldName = "ID";
			treeList_ЭлементыМеню.DataSource = СписокЭлементовМеню;
			treeList_ЭлементыМеню.SelectImageList = ImageList_ДеревоМеню;
			treeList_ЭлементыМеню.ImageIndexFieldName = "ИндексИконки";

			if (treeList_ЭлементыМеню.Nodes.Count > 0)
			{
				treeList_ЭлементыМеню.Nodes[0].Expanded = true;

				for (int i = 0; i < treeList_ЭлементыМеню.Nodes[0].Nodes.Count; i++)
				{
					treeList_ЭлементыМеню.Nodes[0].Nodes[i].Expanded = true;
				}
			}
		}

		private void toolStripMenuItem1_Click(object sender, EventArgs e)
		{
			foreach (ЭлементыМеню текущийЭлемент in СписокЭлементовМеню)
			{
				текущийЭлемент.Переключатель = false;
			}

			treeList_ЭлементыМеню.RefreshDataSource();
		}
	}
}