using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SemToTemp.DialogForms
{
    public partial class AnotherGroupDialog : Form
    {
        public AnotherGroupDialog()
        {
            InitializeComponent();
        }

        public AnotherGroupDialog(Position pos, GroupElement oldGroup, GroupElement newGroup) : this()
        {
            lMessage.Text =
                string.Format(
                    "Позиция \"{0}\" уже существует в справочнике. Она расположена в группе \"{1}\", хотя во входном файле позиция принадлежит группе \"{2}\". Перенести позицию в группу из входного файла?",
                    pos.Title, oldGroup.Name, newGroup.Name);
        }

        public AnotherGroupDialog(Position pos)
            : this()
        {
            lMessage.Text =
                string.Format(
                    "Позиция \"{0}\" уже существует в справочнике. Параметры существуюзей позиции отличаются от параметров из входного файла. Обновить параметры?",
                    pos.Title);
        }

        public AnotherGroupDialog(GroupElement groupElement)
            : this()
        {
            lMessage.Text =
                string.Format(
                    "Группа \"{0}\" уже существует в справочнике. Параметры существуюзей группы отличаются от параметров из входного файла. Обновить параметры?",
                    groupElement.Name);
        }

        private void Cancel()
        {
            
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            Cancel();
        }

        private void bYes_Click(object sender, EventArgs e)
        {

        }

        private void bAllYes_Click(object sender, EventArgs e)
        {

        }

        private void bNo_Click(object sender, EventArgs e)
        {

        }

        private void bAllNo_Click(object sender, EventArgs e)
        {

        }

        private void AnotherGroupDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            Cancel();
        }

    }
}