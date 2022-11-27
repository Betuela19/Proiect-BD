using InterfataUtilizator;
using LibrarieModele;
using NivelAccesDate;
using System;
using System.Linq;
using System.Windows.Forms;

namespace DotNetOracle
{
    public partial class FormaAdaugareFacultate : Form
    {
        private const int PRIMA_COLOANA = 0;
        private const bool SUCCES = true;

        IStocareFacultati stocareFacultati = (IStocareFacultati)new StocareFactory().GetTipStocare(typeof(Facultate));

        public FormaAdaugareFacultate()
        {
            InitializeComponent();

            if (stocareFacultati == null)
            {
                MessageBox.Show("Eroare la initializare");
            }
        }

        private void FormaAdaugareFacultate_Load(object sender, EventArgs e)
        {
            AfiseazaFacultati();
        }

        private void AfiseazaFacultati()
        {
            dataGridViewFacultate.DataSource = null;

            try
            {
                var facultati = stocareFacultati.GetFacultati();

                if (facultati != null && facultati.Any())
                {
                    dataGridViewFacultate.DataSource = facultati.Select(
                        facultate => new
                        {
                            facultate.IdFacultate,
                            facultate.Nume,
                            facultate.AnInfiintare,
                            facultate.Domeniu
                        })
                        .ToList();

                    dataGridViewFacultate.Columns["IdFacultate"].Visible = false;
                    dataGridViewFacultate.Columns["AnInfiintare"].HeaderText = "An";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            FormaPlanDeInvatamant formaPlanDeInvatamant = new FormaPlanDeInvatamant();
            formaPlanDeInvatamant.Show();

            this.Hide();
        }

        private void dataGridViewFacultate_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void buttonModificaFacultate_Click(object sender, EventArgs e)
        {
            try
            {
                var facultate = new Facultate(
                   textBoxFacultateNume.Text,
                   dateTimePickerFacultateAnInfiintare.Value,
                   textBoxFacultateDomeniu.Text);

                int currentRowIndex = dataGridViewFacultate.CurrentCell.RowIndex;
                facultate.IdFacultate = int.Parse(dataGridViewFacultate[PRIMA_COLOANA, currentRowIndex].Value.ToString());

                var rezultat = stocareFacultati.UpdateFacultate(facultate);

                if (rezultat == SUCCES)
                {
                    MessageBox.Show("Facultate actualizata");

                    AfiseazaFacultati();
                }
                else
                {
                    MessageBox.Show("Eroare la actualizare facultate");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exceptie" + ex.Message);
            }
        }

        private void buttonAdaugaFacultate_Click(object sender, EventArgs e)
        {
            try
            {
                var facultate = new Facultate(
                   textBoxFacultateNume.Text,
                   dateTimePickerFacultateAnInfiintare.Value,
                   textBoxFacultateDomeniu.Text);

                var facultati = stocareFacultati.GetFacultati();

                if (facultati.Any(f => f.Nume.Equals(facultate.Nume)))
                {
                    MessageBox.Show("Facultatea exista deja");

                    return;
                }

                var rezultat = stocareFacultati.AddFacultate(facultate);

                if (rezultat == SUCCES)
                {
                    MessageBox.Show("Facultate adaugata");

                    AfiseazaFacultati();
                }
                else
                {
                    MessageBox.Show("Eroare la adaugare facultate");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exceptie" + ex.Message);
            }
        }

        private void buttonStergereFacultateLogic_Click(object sender, EventArgs e)
        {
            try
            {
                int currentRowIndex = dataGridViewFacultate.CurrentCell.RowIndex;
                var idFacultate = int.Parse(dataGridViewFacultate[PRIMA_COLOANA, currentRowIndex].Value.ToString());

                var rezultat = stocareFacultati.SoftDeletefacultate(idFacultate);

                if (rezultat == SUCCES)
                {
                    MessageBox.Show("Facultate stearsa");

                    AfiseazaFacultati();
                }
                else
                {
                    MessageBox.Show("Eroare la stergere facultate");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exceptie" + ex.Message);
            }
        }

        private void buttonStergereFacultateFizic_Click(object sender, EventArgs e)
        {
            try
            {
                int currentRowIndex = dataGridViewFacultate.CurrentCell.RowIndex;
                var idFacultate = int.Parse(dataGridViewFacultate[PRIMA_COLOANA, currentRowIndex].Value
                    .ToString());

                var rezultat = stocareFacultati.DeleteFacultate(idFacultate);

                if (rezultat == SUCCES)
                {
                    MessageBox.Show("Facultate stearsa");

                    AfiseazaFacultati();
                }
                else
                {
                    MessageBox.Show("Eroare la stergere facultate");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exceptie" + ex.Message);
            }
        }

        private void dataGridViewFacultate_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int currentRowIndex = dataGridViewFacultate.CurrentCell.RowIndex;
            var idFacultate = int.Parse(dataGridViewFacultate[PRIMA_COLOANA, currentRowIndex].Value.ToString());

            try
            {
                Facultate facultate = stocareFacultati.GetFacultate(idFacultate);

                if (facultate != null)
                {
                    textBoxFacultateNume.Text = facultate.Nume;
                    dateTimePickerFacultateAnInfiintare.Value = facultate.AnInfiintare;
                    textBoxFacultateDomeniu.Text = facultate.Domeniu;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void FormaAdaugareFacultate_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
    
}
