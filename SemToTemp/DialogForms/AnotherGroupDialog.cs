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
                    "������� \"{0}\" ��� ���������� � �����������. ��� ����������� � ������ \"{1}\", ���� �� ������� ����� ������� ����������� ������ \"{2}\". ��������� ������� � ������ �� �������� �����?",
                    pos.Title, oldGroup.Name, newGroup.Name);
        }

        public AnotherGroupDialog(Position pos)
            : this()
        {
            lMessage.Text =
                string.Format(
                    "������� \"{0}\" ��� ���������� � �����������. ��������� ������������ ������� ���������� �� ���������� �� �������� �����. �������� ���������?",
                    pos.Title);
        }

        public AnotherGroupDialog(GroupElement groupElement)
            : this()
        {
            lMessage.Text =
                string.Format(
                    "������ \"{0}\" ��� ���������� � �����������. ��������� ������������ ������ ���������� �� ���������� �� �������� �����. �������� ���������?",
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