<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/TemplateMasterAdmin_PMS.master" CodeFile="AddExpensesType.aspx.cs" Inherits="AddExpensesType" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

     <asp:HiddenField ID="ULBFundId" runat="server" />
    <asp:HiddenField ID="ULBID" runat="server" />
    <asp:HiddenField ID="FYID" runat="server" />
   
   

    <link href="assets/css/CalendarStyle.css" rel="stylesheet" />
    <div class="main-content">
        <div class="page-content">
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>
                    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
                    </cc1:ToolkitScriptManager>
                    <div class="container-fluid">

                         <div class="row">
                            <div class="col-12">
                                <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                                    <h4 class="mb-sm-0" id="HeadingSec" runat="server">Add ULB Expense </h4>
                                    <div class="page-title-right">
                                        <ol class="breadcrumb m-0">
                                            <li class="breadcrumb-item"><a href="javascript: void(0);">Home</a></li>
                                            <li class="breadcrumb-item">Annual Action Plan</li>
                                            <li class="breadcrumb-item active">Add ULB Expense </li>
                                        </ol>
                                    </div>
                                </div>
                            </div>
                            <!--end col-->
                        </div>

                          <div class="row">
                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">Add ULB Expense</h4>
                                         <a href="ListOfAllULB_ExpenseType.aspx"  class="filter-btn" style="float:right"><i class="icon-download"></i>  ULB Expense List</a>

                                    </div>
                                    <!-- end card header -->
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-4">
                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divZone" runat="server">
                                                        <asp:Label ID="lblZoneH" runat="server" Text="State*" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlZone" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divCircle" runat="server">
                                                        <asp:Label ID="lblCircleH" runat="server" Text="District*" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlCircle" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCircle_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divDivision" runat="server">
                                                        <asp:Label ID="lblDivisionH" runat="server" Text="ULB*" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divFY" runat="server">
                                                        <asp:Label ID="lblFY" runat="server" Text="Select Financial Year*" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlFY" runat="server" CssClass="form-select"></asp:DropDownList>
                                                    </div>
                                                </div>

                                              
                                                <div class="col-xxl-3  col-md-6">
                                                    <div>
                                                        <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                                                    </div>
                                                </div>
                                             
                                            </div>
                                            <!--end row-->
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>


                        <div class="row">

                                <div class="col-lg-12">
                                <div class="card" id="AddSection" runat="server">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1" id="HeadingSec2" runat="server">ULB Expense List</h4>
                                       
                                    </div>
                                    <!-- end card header -->
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-4">
                                                <asp:GridView runat="server" ID="GrdULBFund" CssClass="display table table-bordered" AutoGenerateColumns="False" OnRowDataBound="GrdULBFund_RowDataBound" >
                                                    <Columns>

                                                         <asp:BoundField DataField="ULBExpenseType_Id" HeaderText="ULBExpenseType_Id">
                                                                    <HeaderStyle CssClass="displayStyle" />
                                                                    <ItemStyle CssClass="displayStyle" />
                                                                    <FooterStyle CssClass="displayStyle" />
                                                                </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Sr. No.">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="ULBExpenseType_Name" HeaderText="Expense Type" />
                                                      
                                                      
                                                        <asp:TemplateField HeaderText="New Work (Amount in lacs) ">
                                                           
                                                            <ItemTemplate>
                                                                
                                                               <asp:TextBox ID="NewWorkAmount" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);"></asp:TextBox>
                                                            <asp:RequiredFieldValidator  ID="rfvNewWorkAmount"  runat="server" ControlToValidate="NewWorkAmount"  ErrorMessage="Amount is required" CssClass="text-danger" Display="Dynamic">
                                                                </asp:RequiredFieldValidator>
                                                                </ItemTemplate>
                                                        </asp:TemplateField>
                                                       <asp:TemplateField HeaderText="Maintenance Work(Amount in lacs)">
                                                            <ItemTemplate>
                                                               <asp:TextBox ID="MaintenanceAmount" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);"></asp:TextBox>
                                                            <asp:RequiredFieldValidator  ID="rfvMaintenanceAmount"  runat="server" ControlToValidate="MaintenanceAmount"  ErrorMessage="Amount is required" CssClass="text-danger" Display="Dynamic">
                                                                </asp:RequiredFieldValidator>
                                                            
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        <tr>
                                                            <td colspan="15" style="text-align: center; font-weight: bold; color: red;">No records found</td>
                                                        </tr>
                                                    </EmptyDataTemplate>
                                                </asp:GridView>


                                               



                                            </div>
                                            
                                             <div class="col-md-12  col-xxl-12">
                                                 <asp:Button ID="ButtonUpdate" runat="server" style="float:right" CommandName="UpdateData" Text="Update" Visible="false" OnCommand="ButtonUpdate_Command" CssClass="btn btn-success" OnClientClick="return confirm('Are you sure you want to Update the data?');"  />
                                                 <asp:Button ID="BtnSubmit" runat="server" style="float:right" CommandName="SaveData" Text="Save Data" OnCommand="BtnSubmit_Command" CssClass="btn btn-success" OnClientClick="return confirm('Are you sure you want to Save the data?');"  />
                                             </div>

                                        </div>
                                    </div>
                                </div>

                                     
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
          

            </div>
        </div>
    
        <style>
        .filter-btn {
    display: flex;
    padding: 8px 10px;
    border-radius: 5px;
    font-size: 14px;
    font-weight: 500;
    color: #ffffff;
    background-color: #f85420;
    width: 137px;
    border: 0;
    background: -webkit-gradient(linear, left top, right top, from(#FF5722), to(#D84315));
    background: linear-gradient(88deg, #FF5722 0%, #D84315);
    text-align: center;
    align-items: center;
    justify-content: center;
    height: 39px;
    line-height: 22px;
    transition: all .35s ease-Out;
}
    </style>
    </asp:Content>