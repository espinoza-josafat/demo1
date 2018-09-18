using System;
using System.Data;
using System.Globalization;
using System.Resources;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RestPOS.Customer
{
    /// <summary>
    /// Interaction logic for CustomerList.xaml
    /// </summary>
    public partial class CustomerList : Window
    {
        const double topOffset = 180;
        const double leftOffset = 780;
        readonly GrowlNotifiactions growlNotifications = new GrowlNotifiactions();
        ResourceManager res_man;
        CultureInfo cul;

        public CustomerList()
        {
            InitializeComponent();
            growlNotifications.Top = SystemParameters.WorkArea.Top + topOffset;
            growlNotifications.Left = SystemParameters.WorkArea.Left + SystemParameters.WorkArea.Width - leftOffset;
        }

        public void CustomerDatabind()
        {
            if (CombPeopleType.SelectedValue.ToString() == "All")
            {
                var sqlCmd = "Select * from  customercredit ";
                DataAccess.ExecuteSQL(sqlCmd);
                var dt1 = DataAccess.GetDataTable(sqlCmd);
                lstvwCustomerslist.ItemsSource = dt1.DefaultView;
                lblItemcount.Text = lstvwCustomerslist.Items.Count.ToString() + " Encontrado(s)";
            }
            else
            {
                var sqlCmd = "Select * from  customercredit  where peopletype  = '" + CombPeopleType.SelectedValue.ToString() + "'";
                DataAccess.ExecuteSQL(sqlCmd);
                var dt1 = DataAccess.GetDataTable(sqlCmd);
                lstvwCustomerslist.ItemsSource = dt1.DefaultView;
                lblItemcount.Text = lstvwCustomerslist.Items.Count.ToString() + " " + CombPeopleType.SelectedValue.ToString() + " Encontrado(s)";

                if (tabcontrolpanel.SelectedItem != tabCustomerslist)
                {
                    tabcontrolpanel.SelectedItem = tabCustomerslist;
                }
            }
        }

        void CustomerWindows(object sender, RoutedEventArgs e)
        {
            try
            {
                btnAddnew.Visibility = Visibility.Hidden;
                btnDelete.Visibility = Visibility.Hidden;
                CustomerDatabind();
                txtSearch.Focus();
                switch_language();
            }
            catch
            {
            }
        }

        void switch_language()
        {
            res_man = new ResourceManager("RestPOS.Resource.Res", typeof(Home).Assembly);

            if (language.ID == "1")
            {
                cul = CultureInfo.CreateSpecificCulture(language.languagecode);
                lblsearchtitle.Text = res_man.GetString("lblsearchtitle", cul);
                lblpeopletypetitle.Text = res_man.GetString("lblpeopletypetitle", cul);
                lblalltitle.Text = res_man.GetString("lblalltitle", cul);
                tabCustomerslist.Header = res_man.GetString("tabCustomerslist", cul);
                tabCustomersview.Header = res_man.GetString("tabCustomersview", cul);
                lblNametitle.Text = res_man.GetString("lblNametitle", cul);
                lblcontacttitle.Text = res_man.GetString("lblcontacttitle", cul);
                lblemailtitle.Text = res_man.GetString("lblemailtitle", cul);
                lblcitytitle.Text = res_man.GetString("lblcitytitle", cul);
                lbltypetitle.Text = res_man.GetString("lbltypetitle", cul);
                lbladdresstitle.Text = res_man.GetString("lbladdresstitle", cul);
                btnSave.Content = res_man.GetString("btnSave", cul);
                btnAddnew.Content = res_man.GetString("btnAddnew", cul);
                btnDelete.Content = res_man.GetString("btnDelete", cul);
            }
            else
            {
                // englishToolStripMenuItem.Checked = true;
            }
        }

        void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var sqlCmd = " Select * from  customercredit " +
                             " where name  like  '%" + txtSearch.Text + "%' or " +
                             " id like  '" + txtSearch.Text + "%'  or " +
                             "  Mobile  like  '" + txtSearch.Text + "%' or " +
                             " city  like  '" + txtSearch.Text + "%'  or " +
                             " emailaddress  like  '" + txtSearch.Text + "%'";
                DataAccess.ExecuteSQL(sqlCmd);
                var dt1 = DataAccess.GetDataTable(sqlCmd);
                lstvwCustomerslist.ItemsSource = dt1.DefaultView;
                lblItemcount.Text = lstvwCustomerslist.Items.Count.ToString() + " Cliente(s) encontrado(s)";
                if (tabcontrolpanel.SelectedItem != tabCustomerslist)
                {
                    tabcontrolpanel.SelectedItem = tabCustomerslist;
                }
            }
            catch
            {
            }
        }

        void CombPeopleType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                lblalltitle.Text = CombPeopleType.SelectedValue.ToString();
                CustomerDatabind();
            }
            catch
            {
            }
        }

        void lstvwCustomerslist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var Barcode = string.Empty;

            foreach (DataRowView row in lstvwCustomerslist.SelectedItems)
            {
                Barcode = row.Row[0].ToString();
            }

            tabcontrolpanel.SelectedItem = tabCustomersview;
            tabCustomersview.Header = "Vista de detalles del cliente";
            lblCustID.Text = Barcode;
            lblmsg.Text = "-";
            DetailsData(lblCustID.Text);
        }

        void lstvwCustomerslist_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var Barcode = string.Empty;

            foreach (DataRowView row in lstvwCustomerslist.SelectedItems)
            {
                Barcode = row.Row[0].ToString();
            }

            tabcontrolpanel.SelectedIndex = 1;
            tabCustomersview.Header = "Vista de detalles del cliente";
            lblCustID.Text = Barcode;
            DetailsData(lblCustID.Text);
        }

        void DetailsData(string vle)
        {
            var sqlCmd = "Select * from  customercredit  where id  = '" + vle.ToString() + "'";
            DataAccess.ExecuteSQL(sqlCmd);
            var dt1 = DataAccess.GetDataTable(sqlCmd);
            txtCustomerName.Text = dt1.Rows[0].ItemArray[1].ToString();
            txtPhone.Text = dt1.Rows[0].ItemArray[2].ToString();
            txtCustomerAddress.Text = dt1.Rows[0].ItemArray[3].ToString();
            txtEmailAddress.Text = dt1.Rows[0].ItemArray[4].ToString();
            txtCity.Text = dt1.Rows[0].ItemArray[5].ToString();
            CombType.Text = dt1.Rows[0].ItemArray[6].ToString();
            btnAddnew.Visibility = Visibility.Visible;
            btnDelete.Visibility = Visibility.Visible;
            btnSave.Content = "Actualizar";
        }

        void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lblCustID.Text == "-")
                {
                    if (txtCustomerName.Text.Trim() == string.Empty)
                    {
                        growlNotifications.AddNotification(new Notification { Title = "Mensaje de Alerta", Message = "Por favor inserte el nombre del cliente", ImageUrl = "pack://application:,,,/Notifications/Radiation_warning_symbol.png" });
                        txtCustomerName.Focus();
                    }
                    else if (txtPhone.Text.Trim() == string.Empty)
                    {
                        growlNotifications.AddNotification(new Notification { Title = "Mensaje de Alerta", Message = "Por favor inserte Contacto", ImageUrl = "pack://application:,,,/Notifications/Radiation_warning_symbol.png" });
                        txtPhone.Focus();
                    }
                    else if (CombType.Text.Trim() == string.Empty)
                    {
                        growlNotifications.AddNotification(new Notification { Title = "Mensaje de Alerta", Message = "Por favor inserte el Tipo", ImageUrl = "pack://application:,,,/Notifications/Radiation_warning_symbol.png" });
                        CombType.Focus();
                    }
                    else if (txtCity.Text.Trim() == string.Empty)
                    {
                        growlNotifications.AddNotification(new Notification { Title = "Mensaje de Alerta", Message = "Por favor inserte la Ciudad", ImageUrl = "pack://application:,,,/Notifications/Radiation_warning_symbol.png" });
                        txtCity.Focus();
                    }
                    else if (txtCustomerAddress.Text.Trim() == string.Empty)
                    {
                        growlNotifications.AddNotification(new Notification { Title = "Mensaje de Alerta", Message = "Por favor inserte la dirección", ImageUrl = "pack://application:,,,/Notifications/Radiation_warning_symbol.png" });
                        txtCustomerAddress.Focus();
                    }
                    else
                    {
                        var sqlCmd = "insert into tbl_customer (name, emailaddress, phone, address, city, peopletype ) " +
                                        " values ('" + txtCustomerName.Text + "', '" + txtEmailAddress.Text + "', '" + txtPhone.Text + "', '" + txtCustomerAddress.Text + "', "
                                        + " '" + txtCity.Text + "', '" + CombType.SelectedValue.ToString() + "')";
                        DataAccess.ExecuteSQL(sqlCmd);
                        lblmsg.Text = "Guardado con éxito";
                        CustomerDatabind();
                        clearform();
                    }
                }
                else  //Update 
                {
                    var sqlUpdateCmd = "update tbl_customer set name = '" + txtCustomerName.Text + "', emailaddress= '" + txtEmailAddress.Text + "', " +
                        " address = '" + txtCustomerAddress.Text + "', phone = '" + txtPhone.Text + "', city = '" + txtCity.Text + "' , " +
                        " peopletype = '" + CombType.SelectedValue.ToString() + "'   where id = '" + lblCustID.Text + "'";
                    DataAccess.ExecuteSQL(sqlUpdateCmd);
                    growlNotifications.AddNotification(new Notification { Title = "Wow", Message = "Actualizado exitosamente", ImageUrl = "pack://application:,,,/Notifications/notification-icon.png" });

                    // lblmsg.Text = "Actualizado exitosamente";                    
                    DetailsData(lblCustID.Text);
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show("Lo sentimos\r\n este identificador ya ha sido agregado \n\n " + exp.Message);
            }
        }

        void AddNew_Click(object sender, RoutedEventArgs e)
        {
            clearform();
        }

        void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (lblCustID.Text == "-")
            {
                growlNotifications.AddNotification(new Notification { Title = "Mensaje de Alerta", Message = "No puedes borrar este elemento", ImageUrl = "pack://application:,,,/Notifications/Radiation_warning_symbol.png" });
            }
            else
            {
                try
                {
                    if (MessageBox.Show("¿Estas seguro que quieres borrar este elemento?", "Pregunta", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        var sql = "delete from tbl_customer where  id = '" + lblCustID.Text + "' ";
                        DataAccess.ExecuteSQL(sql);
                        lblmsg.Text = "Ha sido eliminado";
                        clearform();
                        //  tabcontrolpanel.SelectedItem = tabCustomerslist;
                        CustomerDatabind();
                    }
                }
                catch //(Exception exp)
                {
                    // MessageBox.Show("Sorry\r\n You have to Check the Data" + exp.Message);
                }
            }
        }

        void clearform()
        {
            CombType.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtCustomerName.Text = string.Empty;
            txtCustomerAddress.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtEmailAddress.Text = string.Empty;
            btnSave.Content = "Guardar";
            lblCustID.Text = "-";
        }

        void btnHomeMenuLink_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Hidden;
            var go = new Home();
            go.Show();
        }
    }
}