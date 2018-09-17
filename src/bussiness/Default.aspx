<%@ Page Title="Home Page" Language="C#" MasterPageFile="SiteMain.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="atk" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Content/style.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="col-md-12" style="text-align: center">
        <asp:DataList ID="dtlistcategory" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal" CssClass="row">
            <ItemStyle ForeColor="Black" />
            <ItemTemplate>
                <asp:LinkButton CssClass="btn btn-primary" ID="lnkCategory" runat="server" Text='<%# Bind("category") %>' OnClick="lnkCategory_Onclick" Font-Size="13" ForeColor="White"></asp:LinkButton>
                <asp:Label ID="lblcategid" Font-Size="13px" runat="server" Text='<%# Bind("category") %>' Visible="False"></asp:Label>
                <asp:Label ID="lblcategory" Font-Size="13px" runat="server" Text='<%# Bind("category") %>' Visible="False"></asp:Label>
            </ItemTemplate>
        </asp:DataList>
    </div>

    <div class="col-md-12">
        <br />
        <div class="form-group">
            <div class="input-group">
                <span class="input-group-addon"><span class="fa fa-search"></span></span>
                <asp:TextBox ID="txtSearch" Font-Size="15px" class="form-control" ToolTip="Búsqueda por nombre de artículo, categoría..."
                    placeholder="Búsqueda por nombre de artículo, categoría..." AutoPostBack="true" runat="server"
                    OnTextChanged="txtSearch_TextChanged"></asp:TextBox>
                <span class="input-group-btn">
                    <asp:Button class="btn btn-warning" ID="btnGo" runat="server" Text="Buscar" />
                </span>
            </div>
        </div>
    </div>


    <!-- Page Features -->
    <%--<div class="row text-center">  --%>
    <asp:Panel ID="Panelallitemlist" class="col-md-12" runat="server">
        <br />
        <asp:DataList ID="dtlistgrid" runat="server" Font-Names="Verdana" Font-Size="Small" RepeatLayout="Flow" RepeatDirection="Horizontal" CssClass="row">
            <ItemStyle ForeColor="Black" />
            <ItemTemplate>
                <div class="col-md-4">
                    <div id="pricePlansmsg">
                        <ul id="plans">
                            <li class="plan">
                                <%--<asp:HyperLink ID="HyperLink1" ToolTip="Show Details"   runat="server" NavigateUrl='<%# string.Format("~/Global/ItemDetails.aspx?ID={0}", Eval("product_id")) %>'>--%>
                                <ul class="planContainer">
                                    <li class="title">
                                        <asp:Label Visible="false" ID="lblpid" runat="server" Text='<%# Bind("product_id") %>'></asp:Label>
                                        <asp:Label ID="LblQty" Visible="false" runat="server" Text='<%# Eval("product_quantity") %>'></asp:Label>
                                        <asp:Label ID="LblPrice" Visible="false" runat="server" Text='<%# Eval("retail_price") %>'></asp:Label>
                                        <asp:Label ID="LblDisc" Visible="false" runat="server" Text='<%# Eval("discount") %>'></asp:Label>
                                        <asp:Label ID="LblDescriptions" Visible="false" runat="server" Text='<%# Eval("description") %>'></asp:Label>
                                        <h5>
                                            <asp:Label ID="lblitemname" Font-Size="13px" runat="server" Text='<%# Bind("product_name") %>'></asp:Label>
                                        </h5>
                                    </li>
                                    <li class="title">
                                        <%--<asp:Image ID="Image1" runat="server" Width="96px" Height="96px" class="img-circle"    ImageUrl='<%# "data:Image/png;base64,"   + Convert.ToBase64String((byte[])Eval("imagename")) %>' />--%>
                                        <asp:Image ID="imgPhoto" ToolTip='<%# Bind("product_name") %>' runat="server" class="img-circle" Width="96px" Height="96px" ImageUrl='<%# "data:Image/png;base64," + Convert.ToBase64String((byte[])Eval("imagename")) %>' />
                                        <br />
                                        <%--   </asp:HyperLink>--%>
                                    </li>
                                    <li>
                                        <ul class="options">
                                            <li>
                                                <asp:Label ID="Label8" ToolTip="Porciento de descuento" ForeColor="#999999" Font-Size="11px" runat="server" Text='<%# Bind("discount") %>'></asp:Label>% Descuento
                                                <asp:Label ToolTip="Precio" Font-Bold="true" ID="Label9" runat="server" Text='<%# Bind("retail_price") %>' Font-Size="14"></asp:Label>
                                            </li>

                                            <%--  <atk:NumericUpDownExtender ID="NumericUpDownExtender1" runat="server" 
                                                        TargetControlID="txtqty" Minimum="1" Maximum="999" Width="70"  ViewStateMode="Enabled">  </atk:NumericUpDownExtender>
                                                        <asp:TextBox ID="txtqty" runat="server" Font-Size="11px"  Text="1" Width="25px" ToolTip="Item Quantity"></asp:TextBox> <p></p> --%>
                                            <asp:Button ID="btnPopuOptions" runat="server" Text="Agregar al carrito"
                                                ValidationGroup="vG2" Font-Size="12px" ToolTip="Agregar al carrito" class="btn btn-warning btn-xs" OnClick="btnPopuOptions_Goclick" />
                                            <br />
                                        </ul>
                                    </li>
                                </ul>
                            </li>
                        </ul>
                    </div>
                </div>
            </ItemTemplate>
        </asp:DataList>
        <center>
            <asp:Button ID="btnviewmore" class="btn btn-default btn-sm" runat="server" Text="Ver más" OnClick="btnviewmore_click" />
        </center>
    </asp:Panel>

    <asp:Panel ID="pnlCartPanel" class="col-md-4" runat="server">
        <br />
        <br />
        <div class="panel panel-default">
            <div class="panel-heading">
                <h4>Su pedido</h4>
            </div>
            <div class="panel-bodyss">
                <asp:DataList ID="dtlistcartitems" runat="server" Font-Names="Verdana" Font-Size="Small" RepeatLayout="Flow" RepeatDirection="Horizontal" CssClass="row">
                    <ItemStyle ForeColor="Black" />
                    <ItemTemplate>
                        <div class="col-md-12">
                            <div id="pricePlansmsg">
                                <ul id="plans">
                                    <li class="plan">
                                        <ul class="planContainer">
                                            <div class="col-lg-2">
                                                <br />
                                                <asp:Image ID="imgPhoto" ToolTip='<%# Bind("ItemName") %>' runat="server" class="img-circle" Width="40px" Height="40px" ImageUrl='<%# Bind("image") %>' />
                                                <br />
                                            </div>
                                            <div class="col-md-5" style="text-align: left">
                                                <br />
                                                <asp:Label Visible="false" ID="lblid" runat="server" Text='<%# Bind("Code") %>'></asp:Label>
                                                <asp:Label ID="LblPrice" Visible="false" runat="server" Text='<%# Eval("Price") %>'></asp:Label>
                                                <asp:Label ID="LblDescriptions" Visible="false" runat="server" Text='<%# Eval("Total") %>'></asp:Label>
                                                <asp:Label ForeColor="Black" ToolTip="Cantidad" Font-Bold="true" ID="Label1" runat="server" Text='<%# Bind("Qty") %>'></asp:Label>
                                                -
                                                <asp:Label ID="lblitemname" Font-Size="12px" runat="server" Text='<%# Bind("ItemName") %>' ForeColor="#0084B4"></asp:Label>
                                                <br />
                                                Opciones: <asp:Label ForeColor="Black" ToolTip="Size" Font-Bold="true" ID="Label3" runat="server" Text='<%# Bind("Options") %>'></asp:Label>
                                                <br />
                                            </div>
                                            <div class="col-md-3">
                                                <br />
                                                <asp:Label ID="Label16" runat="server" Text="Descuento" Font-Size="5"></asp:Label>
                                                <asp:Label ForeColor="Silver" ID="Label15" runat="server" Text='<%# Bind("Disc") %>' Font-Size="9"></asp:Label>%    
                                            
                                                <asp:Label ForeColor="Black" ToolTip="Precio" Font-Bold="true" ID="Label9" runat="server" Text='<%# Bind("Total") %>' Font-Size="13px"></asp:Label>
                                            </div>
                                            <div class="col-lg-2" style="text-align: right">
                                                <br />
                                                <br />
                                                <asp:LinkButton ID="LinkDele" runat="server" ForeColor="Red" Font-Size="19px" ToolTip="Eliminar artículo" class="fa fa-times" OnClick="btnDeleteitem_Click" />
                                            </div>
                                        </ul>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:DataList>
            </div>
            <div class="panel-footer" style="text-align: right">
                Subtotal = 
                <asp:Label ID="lblsubTotal" runat="server" Font-Bold="true" Text="0"></asp:Label><br />
                <asp:Label ID="Label1" Font-Size="11px" runat="server" Text="Vat"></asp:Label>
                (<asp:Label ID="lblVatRate" Font-Size="9px" runat="server" Text="0"></asp:Label>% )
                        <asp:Label ID="lblVat" Font-Size="11px" runat="server" Text="0"></asp:Label><br />
                Costo de envío =
                <asp:Label ID="lblshippingcost" runat="server" Font-Bold="true" Text="10"></asp:Label><br />
                Total =    
                <asp:Label ID="lbltotal" runat="server" Font-Bold="true" Text="0"></asp:Label>
                <br />

                <asp:Label ID="Label7" Font-Size="11px" runat="server" Text="Total de artículos"> </asp:Label>
                <asp:Label ID="lblTotalQty" Font-Size="11px" runat="server" Text="0"></asp:Label>
                <br />
                <br />
                <div>
                    <asp:Button ID="btnsuspen" runat="server" class="btn btn-danger btn-lg" Text="Cerrar" OnClick="btnsuspen_Click" />
                    <asp:Button ID="btnPayment" runat="server" class="btn btn-warning btn-lg" Text="Pagar" OnClick="btnPayment_Click" />
                </div>
            </div>
        </div>
    </asp:Panel>

    <%--    selected items cart --%>
    <div class="col-md-4">
        <asp:Panel ID="pnlAddeditems" class="col-lg-12" runat="server" Height="350px">
            <div class="panel panel-primary">
                <asp:Panel ID="panel1" runat="server" ScrollBars="Vertical" Height="200px">
                    <asp:GridView ID="grdSelectedItem" class="table table-striped table-hover" OnRowDataBound="grdSelectedItem_RowDataBound"
                        runat="server" OnRowDeleting="grdSelectedItem_RowDeleting">
                        <Columns>
                            <asp:TemplateField HeaderText="Eliminar">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkDel" runat="server" ForeColor="Red" Font-Size="17px" CommandName="Delete" ToolTip="Eliminar artículo" class="fa fa-times" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
            </div>
            <br />
        </asp:Panel>
    </div>
    <%--   Horizontalstyle  not used --%>
    <div class="col-md-12">
        <asp:DataList ID="dtlisthoristyle" runat="server" Font-Names="Verdana" Font-Size="Small" RepeatColumns="1" RepeatDirection="Horizontal">
            <ItemStyle ForeColor="Black" />
            <ItemTemplate>
                <div id="pricePlansmsg">
                    <ul id="plans">
                        <li class="plan" style="height: 140px">
                            <table>
                                <tr>
                                    <td class="col-md-4">
                                        <ul class="planContainer">
                                            <li class="title">
                                                <asp:Label Visible="false" ID="lblpid" runat="server" Text='<%# Bind("product_id") %>'></asp:Label>
                                                <asp:Label ID="LblQty" Visible="false" runat="server" Text='<%# Eval("product_quantity") %>'></asp:Label>
                                                <asp:Label ID="LblPrice" Visible="false" runat="server" Text='<%# Eval("retail_price") %>'></asp:Label>
                                                <asp:Label ID="LblDisc" Visible="false" runat="server" Text='<%# Eval("discount") %>'></asp:Label>
                                                <asp:Label ID="LblDescriptions" Visible="false" runat="server" Text='<%# Eval("description") %>'></asp:Label>
                                            </li>
                                            <li>
                                                <%--<asp:HyperLink ID="HyperLink1" ToolTip="Show "   runat="server" NavigateUrl='<%# string.Format("~/Users/EmployeesProfile.aspx?ID={0}", Eval("product_id")) %>'   >--%>
                                                <asp:Image ID="imgPhoto" ToolTip='<%# Bind("product_name") %>' runat="server" class="img-circle" Width="100px" Height="100px" ImageUrl='<%# "data:Image/png;base64,"   + Convert.ToBase64String((byte[])Eval("imagename")) %>' />
                                                <%--   </asp:HyperLink>    --%>
                                            </li>
                                        </ul>
                                    </td>
                                    <td class="col-md-6">
                                        <h1>
                                            <asp:Label ID="lblitemname" Font-Size="24px" runat="server" Text='<%# Bind("product_name") %>'></asp:Label>
                                        </h1>
                                        <span>
                                            <asp:Label ID="lbldescription" Font-Size="12px" runat="server" Text='<%# Bind("description") %>'></asp:Label>
                                        </span>
                                        <br />
                                    </td>
                                    <td class="col-md-1">
                                        <span>
                                            <asp:Label ID="Label4" Font-Size="22px" runat="server" Text='<%# Bind("retail_price") %>'></asp:Label>
                                        </span>
                                        <br />
                                    </td>
                                    <td class="col-md-1">
                                        <asp:Button ID="btnPopuOptions" runat="server" Text="Agregar al carrito"
                                            ValidationGroup="vG2" Font-Size="12px" ToolTip="Agregar al carrito" class="btn btn-warning btn-xs" OnClick="btnPopuOptions_Goclick" />
                                        <br />
                                    </td>
                                </tr>
                            </table>
                        </li>
                    </ul>
                </div>
                <br />
            </ItemTemplate>
        </asp:DataList>
    </div>

    <%--<<<<<<<<<<<<<<<<<<<<< --------------- Options  panel Popup --- not use----------- >>>>>>>>>>>>>>>>>>>>>>>>>>  ScrollBars="Vertical" Height="440px" >>>>>--%>
    <asp:Button ID="btnshowOptins" runat="server" Style="display: none" />
    <atk:ModalPopupExtender ID="ModalPopupOptions" runat="server" TargetControlID="btnshowOptins" BehaviorID="mdlpopup" PopupControlID="pnloptionspopup" CancelControlID="btncloseOptionsPopup" BackgroundCssClass="modalBackground">
    </atk:ModalPopupExtender>

    <asp:Panel ID="pnloptionspopup" class="panel panel-primary" runat="server" BackColor="White" Style="display: none; text-align: left; overflow: hidden" DefaultButton="btn_GoAddtocart">
        <div class="panel-heading" style="text-align: left">
        </div>

        <div class="panel-footer" style="text-align: center">
        </div>
    </asp:Panel>


    <%--<<<<<<<<<<<<<<<<<<<<< ---------------   Options view      Start --------------    >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>--%>

    <!-- Modal -->
    <div class="modal fade" id="detailsmodal" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">X&times;</button>
                    <h4 class="modal-title">
                        <span class="fa fa-check" style="font-size: 25px"></span>
                        <asp:Label ID="Label14" runat="server" Font-Size="15px" Text="Selecciona tus opciones"></asp:Label>
                    </h4>
                </div>
                <div class="modal-body">
                    <asp:Panel ID="Panel2" runat="server" ScrollBars="Vertical" Height="430px">
                        <div class="col-md-7">
                            <asp:Label ID="lblItemname" Font-Bold="true" runat="server" Text="-"></asp:Label>
                            <br />
                            <asp:Label ID="lbldescriptionsPop" runat="server" ForeColor="#8e969d" Text="-"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            Cantidad
                            <br />
                            <atk:NumericUpDownExtender ID="NumericUpDownExtender1" runat="server"
                                TargetControlID="txtqty" Minimum="1" Maximum="999" Width="70" ViewStateMode="Enabled">
                            </atk:NumericUpDownExtender>
                            <asp:TextBox ID="txtqty" runat="server" Font-Size="14px" Text="1" Width="25px" ToolTip="Cantidad de artículos"></asp:TextBox>
                        </div>
                        <div class="col-md-2" style="text-align: right">
                            <br />
                            <asp:Label ID="lblitemPrice" Font-Bold="true" Font-Size="14px" runat="server" Text="0"></asp:Label>
                        </div>
                        <div class="col-md-12">
                            <br />
                            Opciones de salsa<br />
                            <asp:CheckBoxList ID="chkoptionslist" class="table-hover" runat="server" CellPadding="5"
                                CellSpacing="5" Font-Size="13" />
                        </div>
                    </asp:Panel>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btncloseOptionsPopup" class="btn btn-danger btn-sm" runat="server" data-dismiss="modal" Text="Seguir navegando" />
                    <asp:Button ID="btn_GoAddtocart" class="btn btn-primary btn-sm" runat="server" Text="Agregar al carrito" OnClick="btn_Goclick" />
                </div>
            </div>
        </div>
    </div>

    <%--<<<<<<<<<<<<<<<<<<<<<END --------------- Details view   Popup  END -------------- >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>--%>



    <%--<<<<<<<<<<<<<<<<<<<<< --------------- payment panel Popup -------------- >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>--%>
    <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
    <atk:ModalPopupExtender ID="ModalPopupPayment" runat="server" TargetControlID="btnShowPopup"
        PopupControlID="pnlpopupPayment" CancelControlID="btnClosePayment" BackgroundCssClass="modalBackground">
    </atk:ModalPopupExtender>

    <div class="col-md-12">
        <asp:Panel ID="pnlpopupPayment" runat="server" class="panel panel-primary" BackColor="White" Style="display: none; text-align: left" DefaultButton="bntPay">
            <div class="panel-heading" style="text-align: left">
                <asp:Label ID="Label2" runat="server" Font-Size="19px" Text="Pago"></asp:Label>
            </div>
            <asp:Panel ID="pnBody" class="panel-body" runat="server">
                <div class="col-md-6">
                    <asp:Label ID="Label3" runat="server" Font-Size="16px" Text="Información del pedido"></asp:Label>
                    <br />
                        Total a pagar:                               
                        <asp:Label ID="lbltotalpay" runat="server" Font-Bold="true" Font-Size="15px" Text="-"></asp:Label>
                    <br />

                    <br />
                    <asp:Label ID="Label5" runat="server" ToolTip="Opcional" Text="Nota/Condición"></asp:Label>
                    <asp:Label ID="Label6" runat="server" Font-Size="8px" ToolTip="Opcional" Text="Opcional"></asp:Label>
                    <br />
                    <asp:TextBox ID="txtNote" placeholder="Nota" runat="server" ToolTip="Opcional" Height="140px" class="form-control" TextMode="MultiLine"></asp:TextBox>
                </div>

                <div class="col-md-6">
                    <asp:Label ID="Label10" runat="server" Font-Size="16px" Text="Su dirección"></asp:Label>
                    <br />

                    <asp:Label ID="Label11" runat="server" ToolTip="Agrega tu nombre" Text="Nombre"></asp:Label>
                    <asp:RequiredFieldValidator ForeColor="Red" ControlToValidate="txtcustomername" ValidationGroup="vr12" Font-Size="11px"
                        ID="RequiredFieldValidator1" runat="server" ErrorMessage="Por favor agrega tu nombre"></asp:RequiredFieldValidator>
                    <br />
                    <asp:TextBox ID="txtcustomername" placeholder="Nombre" runat="server" ToolTip="Nombre" class="form-control"></asp:TextBox>
                    <br />

                    <asp:Label ID="Label13" runat="server" ToolTip="Agrega tu teléfono" Text="Teléfono"></asp:Label>
                    <asp:RequiredFieldValidator ForeColor="Red" ControlToValidate="txtphone" ValidationGroup="vr12" Font-Size="11px"
                        ID="RequiredFieldValidator2" runat="server" ErrorMessage="Por favor agrega tu teléfono"></asp:RequiredFieldValidator>
                    <br />
                    <asp:TextBox ID="txtphone" placeholder="Teléfono" runat="server" ToolTip="Teléfono" class="form-control"></asp:TextBox>
                    <br />


                    <asp:Label ID="Label12" runat="server" ToolTip="Dirección de entrega" Text="Dirección de entrega:"></asp:Label>
                    <asp:RequiredFieldValidator ForeColor="Red" ControlToValidate="txtAddress" ValidationGroup="vr12" Font-Size="11px"
                        ID="RequiredFieldValidator3" runat="server" ErrorMessage="Por favor agrega tu dirección de entrega"></asp:RequiredFieldValidator>
                    <asp:TextBox ID="txtAddress" placeholder="Dirección de entrega" runat="server" ToolTip="Opcional" class="form-control" Height="70px" TextMode="MultiLine"></asp:TextBox>
                </div>
            </asp:Panel>
            <div class="panel-footer" style="text-align: center">
                <asp:Button ID="btnClosePayment" class="btn btn-danger btn-sm" runat="server" Text="Atrás" />
                <asp:Button ID="bntPay" class="btn btn-primary btn-sm" runat="server" ValidationGroup="vr12" Text="Proceder a pagar" OnClick="bntPay_Goclick" />
            </div>
        </asp:Panel>
    </div>
    <%--</div>--%>
    <%--     <!-- Title -->
        <div class="row">
            <div class="col-lg-12">
                <h3>Our Bestsellers</h3>        
            </div>
        </div>--%>
</asp:Content>
