using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class Order_module_Order_Invoice : System.Web.UI.Page
{
    string ConnectionString = ConfigurationManager.ConnectionStrings["Restposconstr"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["invoice"] != null)
            {
                this.Title = "Order_Invoice_" + Session["invoice"].ToString();
                SystemInfo();
                ItemsListDataBind();
                //InvoiceInfo();
            }
            else
            {
                Response.Redirect("~/Default.aspx"); 
            }
        }
    }

    //header part  System information
    public void SystemInfo()
    {
        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_RestPOS_Databind_systeminfo", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            cmd.Parameters.AddWithValue("@InvoiceNo", Session["invoice"].ToString());

            SqlDataReader sdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(sdr);


            lblshopTitle.Text = dt.Rows[0]["companyname"].ToString();
            lblshopAddress.Text = dt.Rows[0]["location"].ToString();
            lblcustname.Text = dt.Rows[0]["name"].ToString();
            lblcustaddr.Text = dt.Rows[0]["address"].ToString();

            lblPhone.Text = dt.Rows[0]["phone"].ToString();
            lblcomphone.Text = dt.Rows[0]["phone"].ToString();
            lblcustphone.Text=  dt.Rows[0]["phone1"].ToString();
            lblwebAddress.Text =   dt.Rows[0]["web"].ToString();
            lblInvoiceNo.Text = Session["invoice"].ToString();
            lbldate.Text = dt.Rows[0]["sales_time"].ToString();
            lblComment.Text = dt.Rows[0]["comment"].ToString();
            lblInvoiceNoTop.Text = Session["invoice"].ToString();
            cn.Close();
        }
        catch
        {
        }
    }
    
    // ///////  Item list Databind
    public void ItemsListDataBind()
    {
        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("[SP_RestPOS_DataBind_OrderItemList]", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@InvoiceNo", Session["invoice"].ToString());
            cn.Open();

            grdItemList.DataSource = cmd.ExecuteReader();
            grdItemList.EmptyDataText = "No se encontrarón datos";
            grdItemList.DataBind();
            cn.Close();
          //  lbtotalRow.Text = "Total : " + Convert.ToString(grdItemList.Rows.Count) + " Records found" + "<br />";

        }
        catch
        {
            //lbtotalRow.Text = "No Records Found";
        }
    }
     
    // Total calculation
    decimal total = 0;

    protected void grdItemList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            total = total + Convert.ToDecimal(e.Row.Cells[5].Text); 
           // e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;            
          //  e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[4].Text = "Subtotal = ";

            //  Total Calculation
            e.Row.Cells[5].Text = total.ToString("");
            string totalat = total.ToString("c");
            int totalati = totalat.IndexOf('$');
            string totalatd = totalat.Substring(totalati + 1);
            e.Row.Cells[5].Text =  totalatd + " /-";
            e.Row.Cells[4].Font.Size = 10;
            e.Row.Cells[5].Font.Size = 10;

            e.Row.Font.Bold = true;
            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
           // e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
            lblsubTotal.Text = totalatd;
            decimal totalcost = Convert.ToDecimal(totalatd) + Convert.ToDecimal(lblshippingcost.Text);
            lbltotal.Text = totalcost.ToString();
        }
    }
}