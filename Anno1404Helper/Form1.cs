using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anno1404Helper.IO;

namespace Anno1404Helper
{
    public partial class FrmAnno1404Helper : Form
    {
        TableLayoutPanel tableLayout;
        List<Building>[] buildings;
        List<Building> endOfLineBuildings;
        List<Building> workInProgressBuildings;
        public FrmAnno1404Helper()
        {
            InitializeComponent();
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {
            
        }

        private void FrmAnno1404Helper_Load(object sender, EventArgs e)
        {
            BuildingReader br = new BuildingReader();
            buildings = br.getBuildings();
            endOfLineBuildings = buildings[0];
            workInProgressBuildings = buildings[1];
            cmbGoods.DataSource = endOfLineBuildings;
            cmbGoods.DisplayMember = "Goods";
            cmbGoods.ValueMember = "Name";
            createTable();
        }

        private void cmbGoods_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                checkInputs();
                decimal[] population = new decimal[] { Convert.ToDecimal(txtBeggars.Text), Convert.ToDecimal(txtPeasants.Text), Convert.ToDecimal(txtCitizens.Text), Convert.ToDecimal(txtPatricians.Text), Convert.ToDecimal(txtNoblemen.Text), Convert.ToDecimal(txtNomads.Text), Convert.ToDecimal(txtEnvoys.Text) };
                String labelText = endOfLineBuildings[cmbGoods.SelectedIndex].Calculate(population, workInProgressBuildings, 1, null);
                lblRequirements.Text = labelText;
            }
            catch (Exception ex)
            {
                if (ex.Message == "Input string was not in a correct format.")
                {
                    MessageBox.Show("please fill in all textboxes correctly (0 is allowed)");
                }
                else
	            {
                    MessageBox.Show(ex.Message);
	            }
            }
        }

        private void checkInputs()
        {
            foreach (Control control in this.Controls)
            {
                if (control is GroupBox)
                {
                    foreach (Control C in control.Controls)
                    {
                        if (C is TextBox)
                        {
                            TextBox tb = C as TextBox;
                            if (string.IsNullOrEmpty(tb.Text))
                            {
                                tb.Text = "0";
                            }
                            if (Convert.ToInt32(tb.Text) < 0)
                            {
                                throw new Exception("negative inputs are not allowed");
                            }
                        }
                    }

                }
            }
        }

        private void createTable()
        {
            tableLayout = new TableLayoutPanel();
            tableLayout.Dock = DockStyle.Fill;
            groupBoxRequirements.Controls.Add(tableLayout);
        }

        private void addColumn(Building b)
        {
            tableLayout.ColumnCount += 1;
            String goods = b.Goods.First().ToString().ToUpper() + String.Join("", b.Goods.Skip(1));
            string imageName = "icon" + goods;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
        }

        private void cmbGoods_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
