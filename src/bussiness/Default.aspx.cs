using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : Page
{
    // List<string> lv = new List<string>();
    String ConnectionString = ConfigurationManager.ConnectionStrings["Restposconstr"].ConnectionString;
    DataTable table = new DataTable();
    String sauceoptions;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                pnlAddeditems.Visible = false;
                pnlCartPanel.Visible = false;
                categoryeDDLDataBind();
                loadItemList();
                OptionschkDataBind();
                txtSearch.Focus();

                if (Session["value"] != null)
                {
                    pnlCartPanel.Visible = true;
                    Panelallitemlist.CssClass = "col-md-8";
                    table = (DataTable)Session["value"];
                    dtlistcartitems.DataSource = table;
                    dtlistcartitems.DataBind();

                    decimal sum = 0; decimal qty2 = 0;
                    foreach (DataRow dr in table.Rows)
                    {
                        sum += Convert.ToDecimal(dr["Total"]);
                        qty2 += Convert.ToDecimal(dr["Qty"]);
                    }
                    lblsubTotal.Text = sum.ToString();
                    decimal totalcost = sum + Convert.ToDecimal(lblshippingcost.Text);
                    lbltotal.Text = totalcost.ToString();
                    lblTotalQty.Text = qty2.ToString();
                }
            }
            catch
            {

            }
        }
    }

    public void categoryeDDLDataBind()
    {
        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand(" Select  'ALL' as category from purchase   union select   distinct  top 11 category from purchase ", cn);
            // cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();

            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            //adpt.Fill(dt);

            //ddlcategory.DataSource = dt;
            //ddlcategory.DataTextField = "category";
            //ddlcategory.DataValueField = "category";
            //ddlcategory.DataBind();

            dtlistcategory.DataSource = cmd.ExecuteReader();
            dtlistcategory.DataBind();
            cn.Close();
        }
        catch
        {
            // lbtotalRow.Text = "No Records Found";
        }
    }

    protected void lnkCategory_Onclick(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = sender as LinkButton;
            DataListItem item = (DataListItem)btn.NamingContainer;
            Label lblcategid = (Label)item.FindControl("lblcategid");
            Label lblcategory = (Label)item.FindControl("lblcategory");

            txtSearch.Text = lblcategory.Text;
            // Categoryfilter(lblcategid.Text);

            SqlConnection con = new SqlConnection(ConnectionString);
            string sql = "";
            if (lblcategid.Text == "ALL")
            {
                sql = " select top 12 * from purchase ";
            }
            else
            {
                sql = " select top 15 * from purchase where category like '" + lblcategid.Text + "%' ";
            }
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Connection = con;
            con.Open();

            dtlisthoristyle.Visible = false;
            dtlistgrid.Visible = true;
            dtlistgrid.DataSource = cmd.ExecuteReader();
            dtlistgrid.DataBind();
            con.Close();
        }
        catch
        {
        }
    }

    public void OptionschkDataBind()
    {
        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("select    saucename as saucename  from tbl_sauceoptions ", cn);
            // cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();

            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adpt.Fill(dt);

            chkoptionslist.DataSource = dt;
            chkoptionslist.DataTextField = "saucename";
            chkoptionslist.DataValueField = "saucename";
            chkoptionslist.DataBind();
            cn.Close();
        }
        catch
        {
            // lbtotalRow.Text = "No Records Found";
        }
    }

    //Datalist  Item List
    protected void loadItemList()
    {
        try
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("select top 12 * from purchase  order by NEWID()");
            cmd.Connection = con;
            // cmd.CommandType = CommandType.StoredProcedure;
            // cmd.Parameters.AddWithValue("@limit", "14");
            con.Open();

            //  SqlCommand cmd = new SqlCommand("Select * from tblImages", con);
            //  con.Open();
            //   SqlDataReader rdr = cmd.ExecuteReader();
            //   GridView1.DataSource = rdr;
            //     GridView1.DataBind();
            dtlistgrid.Visible = true;
            dtlistgrid.DataSource = cmd.ExecuteReader();
            dtlistgrid.DataBind();

            //dtlisthoristyle.DataSource = cmd.ExecuteReader();
            //dtlisthoristyle.DataBind();
            con.Close();
        }
        catch
        {
        }
    }

    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("select top 15 * from purchase where product_name like '%" + txtSearch.Text + "%' ", con);
            cmd.Connection = con;
            // cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@limit", "21");
            // cmd.Parameters.AddWithValue("@value", txtSearch.Text);
            con.Open();

            //dtlistgrid.Visible = false;  
            //dtlisthoristyle.Visible = true;
            //dtlisthoristyle.DataSource = cmd.ExecuteReader();
            //dtlisthoristyle.DataBind(); 

            dtlistgrid.DataSource = cmd.ExecuteReader();
            dtlistgrid.DataBind();
            con.Close();
            txtSearch.Focus();
        }
        catch
        {
            // DTusers.Visible = true;
        }
    }

    // ///// Open Options Popup panel
    protected void btnPopuOptions_Goclick(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        DataListItem item = (DataListItem)btn.NamingContainer;
        Label Lblpid = (Label)item.FindControl("lblpid");
        Label Lblitemname = (Label)item.FindControl("lblitemname");
        Label LblQty = (Label)item.FindControl("LblQty");
        Label LblPrice = (Label)item.FindControl("LblPrice");
        Label LblDisc = (Label)item.FindControl("LblDisc");
        Label LblDescriptions = (Label)item.FindControl("LblDescriptions");
        Image imgPhoto = (Image)item.FindControl("imgPhoto");
        // TextBox txtqty = (TextBox)item.FindControl("txtqty");
        // DropDownList txtqty = (DropDownList)item.FindControl("ddlQty");

        Session["Code"] = Lblpid.Text;
        Session["ItemName"] = Lblitemname.Text;
        lblItemname.Text = Lblitemname.Text;
        // Session["Qty"]  = txtqty.Text; // LblQty.Text;
        Session["QtyStock"] = Convert.ToDecimal(LblQty.Text);
        Session["Price"] = LblPrice.Text;
        Session["Disc"] = LblDisc.Text;
        Session["image"] = imgPhoto.ImageUrl;

        lblitemPrice.Text = LblPrice.Text;
        lbldescriptionsPop.Text = LblDescriptions.Text;
        chkoptionslist.SelectedIndex = -1;
        // this.ModalPopupOptions.Show();

        //  Session.Add("value", table);
        // Session.Remove("value");
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(@"<script type='text/javascript'>");
        sb.Append("$('#detailsmodal').modal('show');");
        sb.Append(@"</script>");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DetailModalScript", sb.ToString(), false);
    }

    //Click add to cart menu
    protected void btn_Goclick(object sender, EventArgs e)
    {
        pnlCartPanel.Visible = true;
        Panelallitemlist.CssClass = "col-md-8";
        string Code = Session["Code"].ToString();
        string ItemName = Session["ItemName"].ToString();
        string Qty = txtqty.Text;
        decimal QtyStock = Convert.ToDecimal(Session["QtyStock"].ToString());
        string Price = Session["Price"].ToString();
        string Disc = Session["Disc"].ToString();

        string s = string.Empty;
        string Options;
        for (int i = 0; i < chkoptionslist.Items.Count; i++)
        {
            if (chkoptionslist.Items[i].Selected)
            {
                s += chkoptionslist.Items[i].Value + ", ";
                sauceoptions = s.Substring(0, s.Length - 2);
            }
        }
        Options = sauceoptions;

        decimal Total = Math.Round((Convert.ToDecimal(Price) - (Convert.ToDecimal(Price) * Convert.ToDecimal(Disc) / 100)) * Convert.ToDecimal(Qty), 2);

        //// string Total = LblTotal.Text; 
        //decimal Total = Math.Round((Convert.ToDecimal(Price) - (Convert.ToDecimal(Price) * Convert.ToDecimal(Disc) / 100)) * Convert.ToDecimal(Qty), 2);

        //Check Item Quantity less than 1 
        if (Convert.ToDecimal(Qty) <= 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No hay productos en existencia')", true);
        }
        if (Convert.ToDecimal(Qty) > Convert.ToDecimal(QtyStock))
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('La cantidad proporcionada es mayor a la disponible en existencia')", true);
        }
        else
        {
            if (Session["value"] != null)
            {
                table = Session["value"] as DataTable;
            }
            else
            {
                //Add item from item list     
                table.Columns.Add("Code", typeof(string));
                table.Columns.Add("ItemName", typeof(string));
                table.Columns.Add("Options", typeof(string));
                table.Columns.Add("Qty", typeof(string));
                table.Columns.Add("Price", typeof(string));
                table.Columns.Add("Disc", typeof(string));
                table.Columns.Add("Total", typeof(string));
                table.Columns.Add("image", typeof(string));
                Session["value"] = table;
            }
            table.Rows.Add(Code, ItemName, Options, Qty, Price, Disc, Total, Session["image"].ToString());
            Session.Add("value", table);

            dtlistcartitems.DataSource = table;
            dtlistcartitems.DataBind();


            // double tex = (Convert.ToDouble(lblsubTotal.Text) * 5) / 100;
            double TAX = ((Convert.ToDouble(lblsubTotal.Text) * Convert.ToDouble(lblVatRate.Text)) / 100);
            // lbldisc.Text =  pricetotal - 
            lblVat.Text = Math.Round(TAX, 2).ToString();
            lbltotal.Text = (Convert.ToDouble(lblsubTotal.Text) + Convert.ToDouble(lblVat.Text)).ToString();

            decimal sum = 0; decimal qty2 = 0;
            foreach (DataRow dr in table.Rows)
            {
                sum += Convert.ToDecimal(dr["Total"]);
                qty2 += Convert.ToDecimal(dr["Qty"]);
            }
            lblsubTotal.Text = sum.ToString();
            decimal totalcost = sum + Convert.ToDecimal(lblshippingcost.Text);
            lbltotal.Text = totalcost.ToString();
            lblTotalQty.Text = qty2.ToString();
        }
        // OptionschkDataBind();        
        txtqty.Text = "1";
    }

    #region Not used this Panel
    //Fix Row Width  and Sum of total cost
    // double pricetotal = 0;
    double total = 0;
    double Disc = 0;
    double Qty = 0;

    protected void grdSelectedItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //pricetotal += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Price"));
            total += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Total"));
            Disc += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Disc%"));
            Qty += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Qty"));

            e.Row.Cells[0].Width = 10;
            e.Row.Cells[1].Width = 100;
            e.Row.Cells[2].Width = 310;
            e.Row.Cells[2].Font.Bold = true;
            e.Row.Cells[7].Font.Bold = true;
            e.Row.Cells[7].Font.Size = 10;
            //e.Row.Cells[1].Visible = false;
            //e.Row.Cells[8].Font.Size = 5;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            //  Label pricetotal = (Label)e.Row.FindControl("pricetotal");
            lblsubTotal.Text = total.ToString();
            lblTotalQty.Text = Qty.ToString();
            //lbldisc.Text = Disc.ToString();
        }
    }

    protected void grdSelectedItem_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable dt = (DataTable)Session["value"];
        dt.Rows.RemoveAt(e.RowIndex);
        grdSelectedItem.DataSource = dt;
        grdSelectedItem.DataBind();


        if (grdSelectedItem.Rows.Count == 0)
        {
            lblsubTotal.Text = "0";
            lblVat.Text = "0";
            lbltotal.Text = "0";
            lblTotalQty.Text = "0";
        }
        else
        {
            // double tex = (Convert.ToDouble(lblsubTotal.Text) * 5) / 100;
            double tex = ((Convert.ToDouble(lblsubTotal.Text) * Convert.ToDouble(lblVatRate.Text)) / 100);
            lblVat.Text = Math.Round(tex, 2).ToString();
            lbltotal.Text = (Convert.ToDouble(lblsubTotal.Text) + Convert.ToDouble(lblVat.Text)).ToString();
        }
    }
    #endregion

    protected void btnsuspen_Click(object sender, EventArgs e)
    {
        Session.Remove("value");
        dtlistcartitems.DataSource = null;
        dtlistcartitems.DataBind();
        lblsubTotal.Text = "0";
        lblVat.Text = "0";
        lbltotal.Text = "0";
        lblTotalQty.Text = "0";
        pnlAddeditems.Visible = false;
        pnlCartPanel.Visible = false;
        Panelallitemlist.CssClass = "col-md-12";
    }

    protected void btnviewmore_click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("select top 100 * from purchase  order by product_name ", con);
            cmd.Connection = con;
            con.Open();

            dtlistgrid.Visible = true;
            dtlistgrid.DataSource = cmd.ExecuteReader();
            dtlistgrid.DataBind();

            // dtlisthoristyle.DataSource = cmd.ExecuteReader();
            // dtlisthoristyle.DataBind();
            con.Close();
            txtSearch.Focus();
        }
        catch
        {
        }
    }

    // Open Payment Popup window - Checkout Button
    protected void btnPayment_Click(object sender, EventArgs e)
    {
        // Button Linkdetails = sender as Button;
        //  GridViewRow gvrow = (GridViewRow)Linkdetails.NamingContainer;

        if (lblsubTotal.Text == "0" || lblsubTotal.Text == "00" || lblsubTotal.Text == "0.00")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Por favor agrega al menos un artículo')", true);
        }
        else
        {
            //  txtPaid.Text = string.Empty;
            lbltotalpay.Text = lbltotal.Text;
            //txtPaid.Focus();
            this.ModalPopupPayment.Show();
        }
    }
    
    //Inset Multiple Row in single trXID  - Function method
    protected void SaveSaleItem()
    {
        try
        {
            table = (DataTable)Session["value"];

            for (int i = 0; i < table.Rows.Count; i++)
            {
                //   string datetime = DateTime.Now.ToString("yyyy-MM-dd"); 
                string Code = table.Rows[i].ItemArray[0].ToString(); //.Rows[i].Cells[1].Text;
                string ItemName = table.Rows[i].ItemArray[1].ToString(); //grdSelectedItem.Rows[i].Cells[2].Text;
                string Options = table.Rows[i].ItemArray[2].ToString();
                string Qty = table.Rows[i].ItemArray[3].ToString();
                string Price = table.Rows[i].ItemArray[4].ToString();
                string Disc = table.Rows[i].ItemArray[5].ToString();
                string Total = table.Rows[i].ItemArray[6].ToString();


                SqlConnection cn = new SqlConnection(ConnectionString);
                SqlCommand cmd = new SqlCommand("SP_RestPOS_Insert_SalesItems", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                cmd.Parameters.AddWithValue("@Code", Code);
                cmd.Parameters.AddWithValue("@ItemName", ItemName);
                cmd.Parameters.AddWithValue("@Qty", Qty);
                cmd.Parameters.AddWithValue("@Price", Price);
                cmd.Parameters.AddWithValue("@Disc", Disc);
                cmd.Parameters.AddWithValue("@Total", Total);
                cmd.Parameters.AddWithValue("@Options", "web: " + Options);

                cmd.Parameters.Add("@InvoiceNoOutPut", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();

                Session["InvoiceNoOutPut"] = cmd.Parameters["@InvoiceNoOutPut"].Value.ToString();
            }
        }
        catch
        {
            // lbtotalRow.Text = "No Records Found";
        }
    }

    //Insert One Row sales payment info every one trXID
    protected void SaveSalePaymentInfo()
    {
        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_RestPOS_Insert_SRsalesPayment", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();

            cmd.Parameters.AddWithValue("@payment_amount", lbltotalpay.Text);
            cmd.Parameters.AddWithValue("@due_amount", lbltotalpay.Text);
            cmd.Parameters.AddWithValue("@comment", txtNote.Text + " Envío = " + lblshippingcost.Text);
            cmd.Parameters.AddWithValue("@emp_id", "System");
            cmd.Parameters.AddWithValue("@custname", txtcustomername.Text);
            cmd.Parameters.AddWithValue("@phone", txtphone.Text);
            cmd.Parameters.AddWithValue("@address", txtAddress.Text);
            // cmd.Parameters.Add("@shopidOutput", SqlDbType.NVarChar, 150).Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();
            cn.Close();
            //  Session["shopidOutput"] = cmd.Parameters["@shopidOutput"].Value.ToString();
        }
        catch
        {
            // lbtotalRow.Text = "No Records Found";
        }
    }

    protected void bntPay_Goclick(object sender, EventArgs e)
    {
        SaveSaleItem();
        SaveSalePaymentInfo();
        //  this.ModalPopupPayment.Show();
        Session["invoice"] = Session["InvoiceNoOutPut"].ToString();
        //  Session["shopid"] = Session["shopidOutput"].ToString(); 
        Session.Remove("value");
        Response.Redirect("~/Order_Invoice.aspx");
    }
    
    protected void btnDeleteitem_Click(object sender, EventArgs e)
    {
        LinkButton btn = sender as LinkButton;
        DataListItem item = (DataListItem)btn.NamingContainer;

        int itemIndex = item.ItemIndex;
        table = (DataTable)Session["value"];
        table.Rows.RemoveAt(itemIndex);

        dtlistcartitems.DataSource = table;
        dtlistcartitems.DataBind();

        decimal sum = 0; decimal qty = 0;
        foreach (DataRow dr in table.Rows)
        {
            sum += Convert.ToDecimal(dr["Total"]);
            qty += Convert.ToDecimal(dr["Qty"]);
        }
        lblsubTotal.Text = sum.ToString();
        decimal totalcost = sum + Convert.ToDecimal(lblshippingcost.Text);
        lbltotal.Text = totalcost.ToString();
        lblTotalQty.Text = qty.ToString();

        if (table.Rows.Count == 0)
        {
            lblsubTotal.Text = "0";
            lbltotal.Text = "0";
            lblTotalQty.Text = "0";
            // lbldiscountamount.Text = "0";
            //  lbldistype.Text = "0";
        }
        else
        {
            // if (lbldistype.Text == "2") { lbldiscountamount.Text = "0"; }
            //  lbltotal.Text = (Convert.ToDecimal(lblsubTotal.Text) - Convert.ToDecimal(lbldiscountamount.Text)).ToString();
        }
    }
}