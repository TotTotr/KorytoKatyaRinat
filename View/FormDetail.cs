using Logic.BindingModel;
using Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;

namespace View
{
    public partial class FormDetail : Form
    {
        [Dependency] 
        public new IUnityContainer Container { get; set; }
        public int Id { set { id = value; } }

        private readonly IDetailLogic logic;

        private int? id;

        public static int PriceDetail;

        public FormDetail(IDetailLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void FormDetail_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var view = logic.Read(new DetailBindingModel { Id = id })?[0];
                    if (view != null)
                    {
                        textBoxName.Text = view.DetailName;
                        textBoxPrice.Text = view.Price.ToString();
                        textBoxCount.Text = view.TotalAmount.ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                PriceDetail = Convert.ToInt32(textBoxPrice.Text);
                logic.CreateOrUpdate(new DetailBindingModel
                {
                    Id = id,
                    DetailName = textBoxName.Text,
                    Price = Convert.ToInt32(textBoxPrice.Text),
                    TotalAmount = Convert.ToInt32(textBoxCount.Text)
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
