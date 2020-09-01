using Logic.BindingModel;
using Logic.Interfaces;
using Logic.ViewModel;
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
    public partial class FormCarDetail : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IDetailLogic logicP;

        public int Id
        {
            get { return Convert.ToInt32(comboBoxDetail.SelectedValue); }
            set { comboBoxDetail.SelectedValue = value; }
        }

        public string DetailName { get { return comboBoxDetail.Text; } }

        public int Count
        {
            get { return Convert.ToInt32(textBoxCount.Text); }
            set { textBoxCount.Text = value.ToString(); }
        }

        public decimal PlusSum;

        public FormCarDetail(IDetailLogic logicP)
        {
            InitializeComponent();
            this.logicP = logicP;
        }

        private void FormCarDetail_Load(object sender, EventArgs e)
        {
            try
            {
                var list = logicP.Read(null);
                if (list != null)
                {
                    comboBoxDetail.DisplayMember = "DetailName";
                    comboBoxDetail.ValueMember = "Id";
                    comboBoxDetail.DataSource = list;
                    comboBoxDetail.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TextBoxCount_TextChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        private void ComboBoxDetail_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        private void CalcSum()
        {
            if (comboBoxDetail.SelectedValue != null && !string.IsNullOrEmpty(textBoxCount.Text))
            {
                try
                {
                    int id = Convert.ToInt32(comboBoxDetail.SelectedValue);
                    DetailViewModel Detail = logicP.Read(new DetailBindingModel { Id = id })?[0];
                    int count = Convert.ToInt32(textBoxCount.Text);
                    decimal sum = count * Detail?.Price ?? 0;
                    textBoxPlusSum.Text = sum.ToString();
                    PlusSum = sum;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxDetail.SelectedValue == null)
            {
                MessageBox.Show("Выберите деталь", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
