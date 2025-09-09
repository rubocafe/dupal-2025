using Payroll.UI.Controls;
using Payroll.Library.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Payroll.Properties;
using Payroll.UI.Configuration;
using Payroll.Library.Model;
using Payroll.General;
using Payroll.UI.Business;
using Payroll.UI.Epf.Epf;
using Payroll.UI.Etf.Etf;
using Payroll.UI.Epf;
using Payroll.UI.Etf;
using Payroll.UI.Common;

namespace Payroll
{
    public partial class TcMainForm : Form
    {
        private static Panel staticContentPanel;
        private static TcForm currentForm;
        private static TcNavigationButton activeButton;
        private static TableLayoutPanel staticTableLayoutPanel;

        private static List<TcNavigationButton> buttons = new List<TcNavigationButton>();

        public TcMainForm()
        {
            InitializeComponent();

            staticContentPanel = contentPanel;
            staticTableLayoutPanel = tableLayoutPanel;
        }

        public static bool Show(TcForm form)
        {
            form.TopLevel = false;
            form.Dock = DockStyle.Fill;

            staticContentPanel.Controls.Clear();
            staticContentPanel.Controls.Add(form);

            form.Show();
            currentForm = form;

            return true;
        }

        private static void ShowFormAndActivateButton(TcForm form, TcNavigationButton button)
        {
            if (currentForm == null ||
                (currentForm != null && currentForm.CanLeave()))
            {
                if (Show(form))
                {
                    ActivateButton(button);
                }
            }
        }

        private static void ActivateButton(TcNavigationButton button)
        {
            if (activeButton != null)
            {
                activeButton.Inactivate();
            }

            activeButton = button;
            button.Activate();
        }

        private void TcMainForm_Load(object sender, EventArgs e)
        {
            try
            {
                ResestForms();
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        private void settingsNavigationButton_Click(object sender, EventArgs e)
        {
            try
            {
                var form = new TcConfigurationForm();
                ShowFormAndActivateButton(form, sender as TcNavigationButton);
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        private static void navigationButton_Click(object sender, EventArgs e)
        {
            try
            {
                var button = sender as TcNavigationButton;
                if (button != null)
                {
                    var customer = TcSettings.Customer;
                    TcForm form = null;

                    var name = button.Name;
                    if (name == "EPFNavigationButton")
                    {
                        form = new TcEpfControlForm();
                    }
                    else if (name == "ETFNavigationButton")
                    {
                        form = new TcEtfControlForm();
                    }
                    else
                    {
                        var business = button.Text.TrimStart();
                        form = new TcBusinessForm(customer, business);
                    }

                    if (form != null)
                    {
                        button.BindedForm = form;
                        ShowFormAndActivateButton(form, button);
                    }
                }
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        public static void ResestForms()
        {
            try
            {
                var company = TcSettings.Company;
                var yearMonth = TcSettings.WorkingYearMonth;
                TcPayrollFolderStructure config = new TcPayrollFolderStructure(TcSettings.RootDirectory);
                

                for (int i = 0; i < staticTableLayoutPanel.Controls.Count; i++)
                {
                    var control = staticTableLayoutPanel.Controls[i];
                    var button = control as TcNavigationButton;
                    if (button != null && button.Name != "settingsNavigationButton")
                    {
                        button.Click -= new System.EventHandler(navigationButton_Click);
                        staticTableLayoutPanel.Controls.RemoveAt(i);
                        i--;
                    }
                }
                buttons.Clear();

                var customer = TcSettings.Customer;
                if (customer != "None")
                {
                    int index = 2;
                    var businesses = config.GetBusinesses(company, yearMonth.ToString(), customer);
                    foreach (var business in businesses)
                    {
                        var newButton = GetNewButton(business, Resources.Payroll);
                        staticTableLayoutPanel.Controls.Add(newButton, 0, index);
                        buttons.Add(newButton);
                        index++;
                    }

                    var customers = config.GetCustomers(company, yearMonth.ToString());
                    if (customers.Count > 0)
                    {
                        index++;
                        var epfButton = GetNewButton("EPF", Resources.EPF);
                        staticTableLayoutPanel.Controls.Add(epfButton, 0, index);
                        buttons.Add(epfButton);
                        index++;

                        var etfButton = GetNewButton("ETF", Resources.ETF);
                        staticTableLayoutPanel.Controls.Add(etfButton, 0, index);
                        buttons.Add(etfButton);
                        index++;
                    }
                }
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        private static TcNavigationButton GetNewButton(string name, Bitmap image = null)
        {
            var button = new TcNavigationButton();

            button.BackColor = System.Drawing.Color.Transparent;
            button.Dock = System.Windows.Forms.DockStyle.Fill;
            button.FlatAppearance.BorderSize = 0;
            button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            button.ForeColor = System.Drawing.Color.Black;
            
            if (image == null)
            {
                button.Image = global::Payroll.Properties.Resources.Configuration;
            }
            else
            {
                button.Image = image;
            }

            button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            button.Location = new System.Drawing.Point(0, 0);
            button.Margin = new System.Windows.Forms.Padding(0);
            button.Name = string.Format("{0}NavigationButton", name);
            button.Size = new System.Drawing.Size(200, 30);
            button.TabIndex = 0;
            button.Text = string.Format(" {0}", name);
            button.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            button.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            button.UseVisualStyleBackColor = false;
            button.Click += new System.EventHandler(navigationButton_Click);

            return button;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TcAboutBox about = new TcAboutBox();
                about.ShowDialog();
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = TcMessageBox.ShowYesNoWarning("Do you want to exit?");
                if (result == DialogResult.Yes)
                {
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }
    }
}
