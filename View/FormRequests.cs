﻿using Logic.BindingModel;
using Logic.BusinessLogic;
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
    public partial class FormRequests : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IRequestLogic logic;
        private readonly ReportLogic report;

        public FormRequests(IRequestLogic logic, ReportLogic report)
        {
            InitializeComponent();
            this.logic = logic;
            this.report = report;
        }

        private void FormRequests_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var list = logic.Read(null);
                if (list != null)
                {
                    dataGridView.DataSource = list;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[1].Visible = false;
                    dataGridView.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dataGridView.Columns[4].Visible = false;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormCreateRequest>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = Container.Resolve<FormCreateRequest>();
                form.Id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo,
               MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                    try
                    {
                        logic.Delete(new RequestBindingModel { Id = id });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                       MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string mail = "mr.alodov@mail.ru";
            if (textBoxEmail != null)
            {
                mail = textBoxEmail.Text;
            }
            string fileName = "Заявки.docx";
            report.SaveDetailRequestsToWordlFile(fileName, mail) ;
        }

        private void ButtonSaveToExcel_Click(object sender, EventArgs e)
        {
            string mail = "mr.alodov@mail.ru";
            if (textBoxEmail != null)
            {
                mail = textBoxEmail.Text;
            }
            string fileName = "Заявки.xlsx";
            report.SaveDetailRequestsToExcellFile(fileName, mail);
        }
    }
}
