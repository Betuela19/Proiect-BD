using InterfataUtilizator;
using LibrarieModele;
using NivelAccesDate;
using System;
using System.Linq;
using System.Windows.Forms;

namespace DotNetOracle
{
    public partial class FormaAdaugareProgramDeStudiu : Form
    {
        private const int PRIMA_COLOANA = 0;
        private const bool SUCCES = true;

        IStocareProgrameDeStudiu stocareProgrameDeStudiu = (IStocareProgrameDeStudiu)new StocareFactory().GetTipStocare(typeof(ProgramDeStudiu));
        IStocareFacultati stocareFacultati = (IStocareFacultati)new StocareFactory().GetTipStocare(typeof(Facultate));

        public FormaAdaugareProgramDeStudiu()
        {
            InitializeComponent();

            if (stocareProgrameDeStudiu == null)
            {
                MessageBox.Show("Eroare la initializare");
            }
        }

        private void FormaAdaugareProgramDeStudiu_Load(object sender, System.EventArgs e)
        {
            AfiseazaProgrameDeStudiu();
            IncarcaFacultati();
        }

        private void AfiseazaProgrameDeStudiu()
        {
            dataGridViewProgramDeStudiu.DataSource = null;

            try
            {
                var programeDeStudiu = stocareProgrameDeStudiu.GetProgrameDeStudiu();

                if (programeDeStudiu != null && programeDeStudiu.Any())
                {
                    dataGridViewProgramDeStudiu.DataSource = programeDeStudiu.Select(
                        programDeStudiu => new
                        {
                            programDeStudiu.IdProgramDeStudiu,
                            programDeStudiu.IdFacultate,
                            programDeStudiu.Nume,
                            programDeStudiu.DurataStudiilor,
                            programDeStudiu.FormaInvatamant
                        })
                        .ToList();

                    dataGridViewProgramDeStudiu.Columns["IdProgramDeStudiu"].Visible = false;
                    dataGridViewProgramDeStudiu.Columns["IdFacultate"].Visible = false;
                    dataGridViewProgramDeStudiu.Columns["DurataStudiilor"].HeaderText = "Durata Studiilor";
                    dataGridViewProgramDeStudiu.Columns["FormaInvatamant"].HeaderText = "Forma Invatamant";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dataGridViewProgramDeStudiu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int currentRowIndex = dataGridViewProgramDeStudiu.CurrentCell.RowIndex;
            var idProgramDeStudiu = int.Parse(dataGridViewProgramDeStudiu[PRIMA_COLOANA, currentRowIndex].Value.ToString());

            try
            {
                ProgramDeStudiu programDeStudiu = stocareProgrameDeStudiu.GetProgramStudiu(idProgramDeStudiu);

                if (programDeStudiu != null)
                {
                    labelProgramDeStudiuFacultateId.Text = programDeStudiu.IdFacultate.ToString();
                    textBoxProgramDeStudiuNume.Text = programDeStudiu.Nume;
                    textBoxProgramDeStudiuDurataStudiilor.Text = programDeStudiu.DurataStudiilor.ToString();
                    textBoxProgramDeStudiuFormaInvatamant.Text = programDeStudiu.FormaInvatamant;

                    cmbFacultati.SelectedIndex = cmbFacultati.Items.IndexOf(
                        new ComboItem(programDeStudiu.Facultate.Nume, int.Parse(labelProgramDeStudiuFacultateId.Text)));
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

        private void IncarcaFacultati()
        {
            try
            {
                cmbFacultati.Items.Clear();

                var facultati = stocareFacultati.GetFacultati();

                if (facultati != null && facultati.Any())
                {
                    foreach (var facultate in facultati)
                    {
                        cmbFacultati.Items.Add(new ComboItem(facultate.Nume, (Int32)facultate.IdFacultate));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void buttonModificaProgramDeStudiu_Click(object sender, EventArgs e)
        {
            try
            {
                var programDeStudiu = new ProgramDeStudiu(
                    ((ComboItem)cmbFacultati.SelectedItem).Value,
                    textBoxProgramDeStudiuNume.Text,
                    Convert.ToInt32(textBoxProgramDeStudiuDurataStudiilor.Text),
                    textBoxProgramDeStudiuFormaInvatamant.Text);

                int currentRowIndex = dataGridViewProgramDeStudiu.CurrentCell.RowIndex;
                programDeStudiu.IdProgramDeStudiu = int.Parse(dataGridViewProgramDeStudiu[PRIMA_COLOANA, currentRowIndex].Value.ToString());

                var rezultat = stocareProgrameDeStudiu.UpdateProgramDeStudiu(programDeStudiu);

                if (rezultat == SUCCES)
                {
                    MessageBox.Show("Program de studiu actualizat");

                    AfiseazaProgrameDeStudiu();
                }
                else
                {
                    MessageBox.Show("Eroare la actualizare program de studiu");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exceptie" + ex.Message);
            }
        }

        private void buttonAdaugaProgramDeStudiu_Click(object sender, EventArgs e)
        {
            try
            {
                var programDeStudiu = new ProgramDeStudiu(
                    ((ComboItem)cmbFacultati.SelectedItem).Value,
                    textBoxProgramDeStudiuNume.Text,
                    Convert.ToInt32(textBoxProgramDeStudiuDurataStudiilor.Text),
                    textBoxProgramDeStudiuFormaInvatamant.Text);

                var rezultat = stocareProgrameDeStudiu.AddProgramStudiu(programDeStudiu);

                if (rezultat == SUCCES)
                {
                    MessageBox.Show("Program de studiu adaugat");

                    AfiseazaProgrameDeStudiu();
                }
                else
                {
                    MessageBox.Show("Eroare la adaugare program de studiu");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exceptie" + ex.Message);
            }
        }

        private void buttonStergereProgramDeStudiuLogic_Click(object sender, EventArgs e)
        {
            try
            {
                int currentRowIndex = dataGridViewProgramDeStudiu.CurrentCell.RowIndex;
                var idProgramDeStudiu = int.Parse(dataGridViewProgramDeStudiu[PRIMA_COLOANA, currentRowIndex].Value.ToString());

                var rezultat = stocareProgrameDeStudiu.SoftDeleteProgramDeStudiu(idProgramDeStudiu);

                if (rezultat == SUCCES)
                {
                    MessageBox.Show("Program de studiu sters");

                    AfiseazaProgrameDeStudiu();
                }
                else
                {
                    MessageBox.Show("Eroare la stergere program de studiu");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exceptie" + ex.Message);
            }
        }

        private void buttonStergereProgramDeStudiuFizic_Click(object sender, EventArgs e)
        {
            try
            {
                int currentRowIndex = dataGridViewProgramDeStudiu.CurrentCell.RowIndex;
                var idProgramDeStudiu = int.Parse(dataGridViewProgramDeStudiu[PRIMA_COLOANA, currentRowIndex].Value
                    .ToString());

                var rezultat = stocareProgrameDeStudiu.DeleteProgramDeStudiu(idProgramDeStudiu);

                if (rezultat == SUCCES)
                {
                    MessageBox.Show("Program de studiu sters.");

                    AfiseazaProgrameDeStudiu();
                    IncarcaFacultati();

                }
                else
                {
                    MessageBox.Show("Eroare la stergere program de studiu");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exceptie" + ex.Message);
            }
        }

        private void FormaAdaugareProgramDeStudiu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
