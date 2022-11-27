using InterfataUtilizator;
using LibrarieModele;
using NivelAccesDate;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DotNetOracle
{
    public partial class FormaPlanDeInvatamant : Form
    {
        private const int PRIMA_COLOANA = 0;
        private const bool SUCCES = true;

        IStocareDiscipline stocareDiscipline = (IStocareDiscipline)new StocareFactory().GetTipStocare(typeof(Disciplina));
        IStocarePlanInvatamant stocarePlanInvatamant = (IStocarePlanInvatamant)new StocareFactory().GetTipStocare(typeof(PlanInvatamant));
        IStocareProgrameDeStudiu stocareProgrameDeStudiu = (IStocareProgrameDeStudiu)new StocareFactory().GetTipStocare(typeof(ProgramDeStudiu));

        public FormaPlanDeInvatamant()
        {
            InitializeComponent();

            if (stocareDiscipline == null || stocarePlanInvatamant == null || stocareProgrameDeStudiu == null)
            {
                MessageBox.Show("Eroare la initializare");
            }
        }

        private void dataGridViewDiscipline_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int currentRowIndex = dataGridViewDiscipline.CurrentCell.RowIndex;
            var idDisciplina = int.Parse(dataGridViewDiscipline[PRIMA_COLOANA, currentRowIndex].Value.ToString());

            try
            {
                Disciplina disciplina = stocareDiscipline.GetDisciplina(idDisciplina);

                if (disciplina != null)
                {
                    labelDisciplinaPlanInvatamantId.Text = disciplina.IdPlanInvatamant.ToString();
                    textBoxDisciplinaNume.Text = disciplina.Nume;
                    textBoxDisciplinaTip.Text = disciplina.TipDisciplina;
                    textBoxDisciplinaNumarCredite.Text = disciplina.NumarCredite.ToString();
                    textBoxDisciplinaAn.Text = disciplina.An;
                    textBoxDisciplinaSemestru.Text = disciplina.Semestru.ToString();
                    textBoxDisciplinaCodDisciplina.Text = disciplina.CodDisciplina;
                    textBoxDisciplinaNumarOreSeminar.Text = disciplina.NumarOreSeminar.ToString();
                    textBoxDisciplinaNumarOreLaborator.Text = disciplina.NumarOreLaborator.ToString();
                    textBoxDisciplinaNumarOreCurs.Text = disciplina.NumarOreCurs.ToString();
                    textBoxDisciplinaFormaDeVerificare.Text = disciplina.FormaDeVerificare;
                    textBoxDisciplinaTotalOreStudiuIndividual.Text = disciplina.TotalOreStudiuIndividual.ToString();

                    cmbPlanDeInvatamant.SelectedIndex = cmbPlanDeInvatamant.Items.IndexOf(new ComboItem(
                        disciplina.PlanInvatamant.Nume, int.Parse(labelDisciplinaPlanInvatamantId.Text)));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

            buttonModificaDisciplina.Enabled = true;
        }

        private void FormaPlanInvatamant_Load(object sender, EventArgs e)
        {
            buttonModificaDisciplina.Enabled = false;

            AfiseazaDiscipline();
            IncarcaPlanuriInvatamant();
            IncarcaProgrameDeStudiu();
        }

        private void FormaPlanInvatamant(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void IncarcaPlanuriInvatamant()
        {
            try
            {
                cmbPlanDeInvatamant.Items.Clear();

                var planuriInvatamant = stocarePlanInvatamant.GetPlanuriInvatamant();

                if (planuriInvatamant != null && planuriInvatamant.Any())
                {
                    foreach (var planInvatamant in planuriInvatamant)
                    {
                        cmbPlanDeInvatamant.Items.Add(new ComboItem(planInvatamant.Nume, (Int32)planInvatamant.IdProgramDeStudiu));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void AfiseazaDiscipline()
        {
            dataGridViewDiscipline.DataSource = null;

            try
            {
                var discipline = stocareDiscipline.GetDiscipline();

                if (discipline != null && discipline.Any())
                {
                    dataGridViewDiscipline.DataSource = discipline.Select(
                        disciplina => new 
                        { 
                            disciplina.IdDisciplina,
                            disciplina.IdPlanInvatamant, 
                            disciplina.Nume,
                            disciplina.TipDisciplina,
                            disciplina.NumarCredite,
                            disciplina.An,
                            disciplina.Semestru,
                            disciplina.CodDisciplina,
                            disciplina.NumarOreSeminar,
                            disciplina.NumarOreLaborator,
                            disciplina.NumarOreCurs,
                            disciplina.FormaDeVerificare,
                            disciplina.TotalOreStudiuIndividual
                        })
                        .ToList();

                    dataGridViewDiscipline.Columns["IdDisciplina"].Visible = false;
                    dataGridViewDiscipline.Columns["IdPlanInvatamant"].Visible = false;
                    dataGridViewDiscipline.Columns["TipDisciplina"].HeaderText = "Tip";
                    dataGridViewDiscipline.Columns["NumarCredite"].HeaderText = "Credite";
                    dataGridViewDiscipline.Columns["CodDisciplina"].HeaderText = "Cod";
                    dataGridViewDiscipline.Columns["NumarOreSeminar"].HeaderText = "Ore seminar";
                    dataGridViewDiscipline.Columns["NumarOreLaborator"].HeaderText = "Ore laborator";
                    dataGridViewDiscipline.Columns["NumarOreCurs"].HeaderText = "Ore curs";
                    dataGridViewDiscipline.Columns["FormaDeVerificare"].HeaderText = "Verificare";
                    dataGridViewDiscipline.Columns["TotalOreStudiuIndividual"].HeaderText = "Studiu Individual";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void buttonModificaDisciplina_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidareDiscipline())
                {
                    MessageBox.Show("Eroare validare date");
                    ResetareControaleDiscipline();

                    return;
                }

                var disciplina = new Disciplina(
                   ((ComboItem)cmbPlanDeInvatamant.SelectedItem).Value,
                   textBoxDisciplinaNume.Text,
                   textBoxDisciplinaTip.Text,
                   Convert.ToInt32(textBoxDisciplinaNumarCredite.Text),
                   textBoxDisciplinaAn.Text,
                   Convert.ToInt32(textBoxDisciplinaSemestru.Text),
                   textBoxDisciplinaCodDisciplina.Text,
                   Convert.ToInt32(textBoxDisciplinaNumarOreSeminar.Text),
                   Convert.ToInt32(textBoxDisciplinaNumarOreLaborator.Text),
                   Convert.ToInt32(textBoxDisciplinaNumarOreCurs.Text),
                   textBoxDisciplinaFormaDeVerificare.Text,
                   Convert.ToInt32(textBoxDisciplinaTotalOreStudiuIndividual.Text));

                int currentRowIndex = dataGridViewDiscipline.CurrentCell.RowIndex;
                disciplina.IdDisciplina = int.Parse(dataGridViewDiscipline[PRIMA_COLOANA, currentRowIndex].Value.ToString());
                
                var rezultat = stocareDiscipline.UpdateDisciplina(disciplina);

                if (rezultat == SUCCES)
                {
                    MessageBox.Show("Disciplina actualizata");

                    AfiseazaDiscipline();
                }
                else
                {
                    MessageBox.Show("Eroare la actualizare disciplina");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exceptie" + ex.Message);
            }
        }

        private void buttonAdaugareDisciplina_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidareDiscipline())
                {
                    MessageBox.Show("Eroare validare date");
                    ResetareControaleDiscipline();

                    return;
                }

                var disciplina = new Disciplina(
                   ((ComboItem)cmbPlanDeInvatamant.SelectedItem).Value,
                   textBoxDisciplinaNume.Text,
                   textBoxDisciplinaTip.Text,
                   Convert.ToInt32(textBoxDisciplinaNumarCredite.Text),
                   textBoxDisciplinaAn.Text,
                   Convert.ToInt32(textBoxDisciplinaSemestru.Text),
                   textBoxDisciplinaCodDisciplina.Text,
                   Convert.ToInt32(textBoxDisciplinaNumarOreSeminar.Text),
                   Convert.ToInt32(textBoxDisciplinaNumarOreLaborator.Text),
                   Convert.ToInt32(textBoxDisciplinaNumarOreCurs.Text),
                   textBoxDisciplinaFormaDeVerificare.Text,
                   Convert.ToInt32(textBoxDisciplinaTotalOreStudiuIndividual.Text));

                var rezultat = stocareDiscipline.AddDisciplina(disciplina);

                if (rezultat == SUCCES)
                {
                    MessageBox.Show("Disciplina adaugata");

                    AfiseazaDiscipline();
                }
                else
                {
                    MessageBox.Show("Eroare la adaugare disciplina");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exceptie" + ex.Message);
            }
        }

        private void AfiseazaPlanuriDeInvatamant()
        {
            dataGridViewPlanuriInvatamant.DataSource = null;

            try
            {
                var planuriDeInvatamant = stocarePlanInvatamant.GetPlanuriInvatamant();

                if (planuriDeInvatamant != null && planuriDeInvatamant.Any())
                {
                    dataGridViewPlanuriInvatamant.DataSource = planuriDeInvatamant.Select(
                        planInvatamant => new
                        {
                            planInvatamant.IdPlanInvatamant,
                            planInvatamant.IdProgramDeStudiu,
                            planInvatamant.Nume,
                            planInvatamant.Valabilitate,
                            planInvatamant.TitluAbsolvire,
                            planInvatamant.NivelDeStudiu,
                            planInvatamant.CompetenteProfesionale,
                            planInvatamant.An
                        })
                        .ToList();

                    dataGridViewPlanuriInvatamant.Columns["IdPlanInvatamant"].Visible = false;
                    dataGridViewPlanuriInvatamant.Columns["IdProgramDeStudiu"].Visible = false;
                    dataGridViewPlanuriInvatamant.Columns["Nume"].HeaderText = "Nume";
                    dataGridViewPlanuriInvatamant.Columns["TitluAbsolvire"].HeaderText = "Titlu absolvire";
                    dataGridViewPlanuriInvatamant.Columns["NivelDeStudiu"].HeaderText = "Nivel de studiu";
                    dataGridViewPlanuriInvatamant.Columns["CompetenteProfesionale"].HeaderText = "Competente";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

            groupBoxPlanuriDeInvatamant.Visible = true;
            groupBoxPlanInvatamant.Visible = true;
        }

        private void buttonAfisarePlanuriInvatamant_Click(object sender, EventArgs e)
        {
            AfiseazaPlanuriDeInvatamant();
        }

        private void buttonAdaugarePlanDeInvatamant_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarePlanDeInvatamant())
                {
                    MessageBox.Show("Eroare validare");
                    ResetareControalePlanDeInvatamant();

                    return;
                }

                var planInvatamant = new PlanInvatamant(
                    ((ComboItem)cmbProgrameStudiu.SelectedItem).Value,
                    textBoxPlanInvatamantNume.Text,
                    dateTimePickerPlanInvatamantValabilitate.Value,
                    textBoxPlanInvatamantTitluAbsolvire.Text,
                    textBoxPlanInvatamantNivelDeStudiu.Text,
                    textBoxPlanInvatamantCompetenteProfesionale.Text,
                    dateTimePickerPlanInvatamantAn.Value);

                var rezultat = stocarePlanInvatamant.AddPlanInvatamant(planInvatamant);

                if (rezultat == SUCCES)
                {
                    MessageBox.Show("Plan de invatamant adaugat");

                    AfiseazaPlanuriDeInvatamant();
                }
                else
                {
                    MessageBox.Show("Eroare la adaugare plan de invatamant");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exceptie" + ex.Message);
            }
        }

        private void dataGridViewPlanuriInvatamant_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int currentRowIndex = dataGridViewPlanuriInvatamant.CurrentCell.RowIndex;
            var idPlanInvatamant = int.Parse(dataGridViewPlanuriInvatamant[PRIMA_COLOANA, currentRowIndex].Value.ToString());

            var discipline = stocareDiscipline.GetDiscipline(idPlanInvatamant);

            if (discipline != null && discipline.Any())
            {
                dataGridViewDiscipline.DataSource = discipline.Select(
                    disciplina => new
                    {
                        disciplina.IdDisciplina,
                        disciplina.IdPlanInvatamant,
                        disciplina.Nume,
                        disciplina.TipDisciplina,
                        disciplina.NumarCredite,
                        disciplina.An,
                        disciplina.Semestru,
                        disciplina.CodDisciplina,
                        disciplina.NumarOreSeminar,
                        disciplina.NumarOreLaborator,
                        disciplina.NumarOreCurs,
                        disciplina.FormaDeVerificare,
                        disciplina.TotalOreStudiuIndividual
                    })
                    .ToList();

                dataGridViewDiscipline.Columns["IdDisciplina"].Visible = false;
                dataGridViewDiscipline.Columns["IdPlanInvatamant"].Visible = false;
                dataGridViewDiscipline.Columns["TipDisciplina"].HeaderText = "Tip";
                dataGridViewDiscipline.Columns["NumarCredite"].HeaderText = "Credite";
                dataGridViewDiscipline.Columns["CodDisciplina"].HeaderText = "Cod";
                dataGridViewDiscipline.Columns["NumarOreSeminar"].HeaderText = "Ore seminar";
                dataGridViewDiscipline.Columns["NumarOreLaborator"].HeaderText = "Ore laborator";
                dataGridViewDiscipline.Columns["NumarOreCurs"].HeaderText = "Ore curs";
                dataGridViewDiscipline.Columns["FormaDeVerificare"].HeaderText = "Verificare";
                dataGridViewDiscipline.Columns["TotalOreStudiuIndividual"].HeaderText = "Ore impuse";
            }

            try
            {
                PlanInvatamant planInvatamant = stocarePlanInvatamant.GetPlanInvatamant(idPlanInvatamant);

                if (planInvatamant != null)
                {
                    labelPlanInvatamantFacultateId.Text = planInvatamant.IdProgramDeStudiu.ToString();
                    textBoxPlanInvatamantNume.Text = planInvatamant.Nume;
                    dateTimePickerPlanInvatamantValabilitate.Value = planInvatamant.Valabilitate;
                    textBoxPlanInvatamantTitluAbsolvire.Text = planInvatamant.TitluAbsolvire;
                    textBoxPlanInvatamantNivelDeStudiu.Text = planInvatamant.NivelDeStudiu;
                    textBoxPlanInvatamantCompetenteProfesionale.Text = planInvatamant.CompetenteProfesionale;
                    dateTimePickerPlanInvatamantAn.Value = planInvatamant.An;

                    cmbProgrameStudiu.SelectedIndex = cmbProgrameStudiu.Items.IndexOf(new ComboItem(planInvatamant.ProgramDeStudiu.Nume, int.Parse(labelPlanInvatamantFacultateId.Text)));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

            groupBoxPlanInvatamant.Visible = true;
        }

        private void IncarcaProgrameDeStudiu()
        {
            try
            {
                cmbProgrameStudiu.Items.Clear();

                var programeDeStudiu = stocareProgrameDeStudiu.GetProgrameDeStudiu();

                if (programeDeStudiu != null && programeDeStudiu.Any())
                {
                    foreach (var programDeStudiu in programeDeStudiu)
                    {
                        cmbProgrameStudiu.Items.Add(new ComboItem(programDeStudiu.Nume, (Int32)programDeStudiu.IdProgramDeStudiu));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void buttonModificaPlanDeInvatamant_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarePlanDeInvatamant())
                {
                    MessageBox.Show("Eroare validare");
                    ResetareControalePlanDeInvatamant();

                    return;
                }

                var planInvatamant = new PlanInvatamant(
                    ((ComboItem)cmbProgrameStudiu.SelectedItem).Value,
                    textBoxPlanInvatamantNume.Text,
                    dateTimePickerPlanInvatamantValabilitate.Value,
                    textBoxPlanInvatamantTitluAbsolvire.Text,
                    textBoxPlanInvatamantNivelDeStudiu.Text,
                    textBoxPlanInvatamantCompetenteProfesionale.Text,
                    dateTimePickerPlanInvatamantAn.Value);

                int currentRowIndex = dataGridViewPlanuriInvatamant.CurrentCell.RowIndex;
                planInvatamant.IdPlanInvatamant = int.Parse(dataGridViewPlanuriInvatamant[PRIMA_COLOANA, currentRowIndex].Value.ToString());

                var rezultat = stocarePlanInvatamant.UpdatePlanInvatamant(planInvatamant);

                if (rezultat == SUCCES)
                {
                    MessageBox.Show("Plan invatamant actualizat");

                    AfiseazaPlanuriDeInvatamant();

                    IncarcaPlanuriInvatamant();
                }
                else
                {
                    MessageBox.Show("Eroare la actualizare plan invatamant");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exceptie" + ex.Message);
            }
        }

        private void buttonAdaugaFacultate_Click(object sender, EventArgs e)
        {
            FormaAdaugareFacultate formaAdaugareFacultate = new FormaAdaugareFacultate();
            formaAdaugareFacultate.Show();

            this.Hide();
        }

        private void buttonStergereDisciplinaLogic_Click(object sender, EventArgs e)
        {
            try
            {
                int currentRowIndex = dataGridViewDiscipline.CurrentCell.RowIndex;
                var idDisciplina = int.Parse(dataGridViewDiscipline[PRIMA_COLOANA, currentRowIndex].Value.ToString());

                var rezultat = stocareDiscipline.SoftDeleteDisciplina(idDisciplina);

                if (rezultat == SUCCES)
                {
                    MessageBox.Show("Disciplina a fost stearsa");

                    AfiseazaDiscipline();
                }
                else
                {
                    MessageBox.Show("Eroare la stergere disciplina");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exceptie" + ex.Message);
            }
        }

        private void buttonStergereDisciplinaFizic_Click(object sender, EventArgs e)
        {
            try
            {
                int currentRowIndex = dataGridViewDiscipline.CurrentCell.RowIndex;
                var idDisciplina = int.Parse(dataGridViewDiscipline[PRIMA_COLOANA, currentRowIndex].Value
                    .ToString());

                var rezultat = stocareDiscipline.DeleteDisciplina(idDisciplina);

                if (rezultat == SUCCES)
                {
                    MessageBox.Show("Disciplina a fost stearsa");

                    AfiseazaDiscipline();
                }
                else
                {
                    MessageBox.Show("Eroare la stergere disciplina");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exceptie" + ex.Message);
            }
        }

        private void buttonStergerePlanDeInvatamantLogic_Click(object sender, EventArgs e)
        {
            try
            {
                int currentRowIndex = dataGridViewPlanuriInvatamant.CurrentCell.RowIndex;
                var idPlanInvatamant = int.Parse(dataGridViewPlanuriInvatamant[PRIMA_COLOANA, currentRowIndex].Value.ToString());

                var rezultat = stocarePlanInvatamant.SoftDeletePlanInvatamant(idPlanInvatamant);

                if (rezultat == SUCCES)
                {
                    MessageBox.Show("Plan de invatamant sters");

                    AfiseazaPlanuriDeInvatamant();
                }
                else
                {
                    MessageBox.Show("Eroare la stergere plan de invatamant");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exceptie" + ex.Message);
            }
        }

        private void buttonStergerePlanInvatamantFizic_Click(object sender, EventArgs e)
        {
            try
            {
                int currentRowIndex = dataGridViewPlanuriInvatamant.CurrentCell.RowIndex;
                var idPlanInvatamant = int.Parse(dataGridViewPlanuriInvatamant[PRIMA_COLOANA, currentRowIndex].Value.ToString());

                var rezultat = stocarePlanInvatamant.DeletePlanInvatamant(idPlanInvatamant);

                if (rezultat == SUCCES)
                {
                    MessageBox.Show("Plan de invatamant sters");

                    AfiseazaPlanuriDeInvatamant();
                }
                else
                {
                    MessageBox.Show("Eroare la stergere plan de invatamant");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exceptie" + ex.Message);
            }
        }

        private bool ValidareDiscipline()
        {
            bool OK = false;

            if (textBoxDisciplinaNume.Text == string.Empty)
            {
                labelDisciplinaNume.ForeColor = Color.Red;
                textBoxDisciplinaNume.BackColor = Color.Yellow;
                OK = true;
            }

            if (textBoxDisciplinaTip.Text == string.Empty)
            {
                labelDisciplinaTip.ForeColor = Color.Red;
                textBoxDisciplinaTip.BackColor = Color.Yellow;
                OK = true;
            }

            if (textBoxDisciplinaNumarCredite.Text == string.Empty)
            {
                labelDisciplinaCredite.ForeColor = Color.Red;
                textBoxDisciplinaNumarCredite.BackColor = Color.Yellow;
                OK = true;
            }

            if (textBoxDisciplinaAn.Text == string.Empty)
            {
                labelDisciplinaAn.ForeColor = Color.Red;
                textBoxDisciplinaAn.BackColor = Color.Yellow;
                OK = true;
            }

            if (textBoxDisciplinaSemestru.Text == string.Empty)
            {
                labelDisciplinaSemestru.ForeColor = Color.Red;
                textBoxDisciplinaSemestru.BackColor = Color.Yellow;
                OK = true;
            }

            if (textBoxDisciplinaCodDisciplina.Text == string.Empty)
            {
                labelCodDisciplina.ForeColor = Color.Red;
                textBoxDisciplinaCodDisciplina.BackColor = Color.Yellow;
                OK = true;
            }

            if (textBoxDisciplinaNumarOreSeminar.Text == string.Empty)
            {
                labelDisciplinaOreSeminar.ForeColor = Color.Red;
                textBoxDisciplinaNumarOreSeminar.BackColor = Color.Yellow;
                OK = true;
            }

            if (textBoxDisciplinaNumarOreLaborator.Text == string.Empty)
            {
                labelDisciplinaOreLaborator.ForeColor = Color.Red;
                textBoxDisciplinaNumarOreLaborator.BackColor = Color.Yellow;
                OK = true;
            }

            if (textBoxDisciplinaNumarOreCurs.Text == string.Empty)
            {
                labelDisciplinaOreCurs.ForeColor = Color.Red;
                textBoxDisciplinaNumarOreCurs.BackColor = Color.Yellow;
                OK = true;
            }

            if (textBoxDisciplinaFormaDeVerificare.Text == string.Empty)
            {
                labelDisciplinaFormaVerificare.ForeColor = Color.Red;
                textBoxDisciplinaFormaDeVerificare.BackColor = Color.Yellow;
                OK = true;
            }

            if (textBoxDisciplinaTotalOreStudiuIndividual.Text == string.Empty)
            {
                labelDisciplinaTotalOreStudiuIndividual.ForeColor = Color.Red;
                textBoxDisciplinaTotalOreStudiuIndividual.BackColor = Color.Yellow;
                OK = true;
            }

            if (OK)
            {
                return false;
            }

            return true;
        }

        private void ResetareControaleDiscipline()
        {
            textBoxDisciplinaNume.Text 
                = textBoxDisciplinaTip.Text 
                = textBoxDisciplinaNumarCredite.Text 
                = textBoxDisciplinaAn.Text 
                = textBoxDisciplinaSemestru.Text
                = textBoxDisciplinaCodDisciplina.Text
                = textBoxDisciplinaNumarOreSeminar.Text
                = textBoxDisciplinaNumarOreLaborator.Text
                = textBoxDisciplinaNumarOreCurs.Text
                = textBoxDisciplinaFormaDeVerificare.Text
                = textBoxDisciplinaTotalOreStudiuIndividual.Text
                = string.Empty;

            textBoxDisciplinaNume.BackColor
                = textBoxDisciplinaTip.BackColor
                = textBoxDisciplinaNumarCredite.BackColor
                = textBoxDisciplinaAn.BackColor
                = textBoxDisciplinaSemestru.BackColor
                = textBoxDisciplinaCodDisciplina.BackColor
                = textBoxDisciplinaNumarOreSeminar.BackColor
                = textBoxDisciplinaNumarOreLaborator.BackColor
                = textBoxDisciplinaNumarOreCurs.BackColor
                = textBoxDisciplinaFormaDeVerificare.BackColor
                = textBoxDisciplinaTotalOreStudiuIndividual.BackColor
                = Color.Empty;

            labelDisciplinaNume.ForeColor
                = labelDisciplinaTip.ForeColor
                = labelDisciplinaCredite.ForeColor
                = labelDisciplinaAn.ForeColor
                = labelDisciplinaSemestru.ForeColor
                = labelCodDisciplina.ForeColor
                = labelDisciplinaOreSeminar.ForeColor
                = labelDisciplinaOreLaborator.ForeColor
                = labelDisciplinaOreCurs.ForeColor
                = labelDisciplinaFormaVerificare.ForeColor
                = labelDisciplinaTotalOreStudiuIndividual.ForeColor
                = Color.Empty;
        }

        private bool ValidarePlanDeInvatamant()
        {
            bool OK = false;

            if (textBoxPlanInvatamantNume.Text == string.Empty)
            {
                labelPlanInvatamantNume.ForeColor = Color.Red;
                textBoxPlanInvatamantNume.BackColor = Color.Yellow;
                OK = true;
            }

            if (OK)
            {
                return false;
            }

            return true;
        }

        private void ResetareControalePlanDeInvatamant()
        {
            textBoxPlanInvatamantNume.Text 
                = string.Empty;

            textBoxPlanInvatamantNume.BackColor
                = Color.Empty;

            labelPlanInvatamantNume.ForeColor
                = labelPlanInvatamantValabilitate.ForeColor
                = Color.Empty;
        }

        private void buttonAdaugaProgramDeStudiu_Click(object sender, EventArgs e)
        {
            FormaAdaugareProgramDeStudiu formaAdaugareProgramDeStudiu = new FormaAdaugareProgramDeStudiu();
            formaAdaugareProgramDeStudiu.Show();

            this.Hide();
        }
    }
}
