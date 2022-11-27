using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DotNetOracle
{
    public partial class FormaDeStart : Form
    {
        public FormaDeStart()
        {
            InitializeComponent();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            FormaPlanDeInvatamant FormaPlanDeInvatamant = new FormaPlanDeInvatamant();
            FormaPlanDeInvatamant.Show();

            this.Hide();
        }

        private void FormaDeStart_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
