<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/TemplateMasterAdmin_PMS.master" CodeFile="AnnualActionplan.aspx.cs" Inherits="AnnualActionplan" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hdnplanId" runat="server" />

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
                                    <h4 class="mb-sm-0">Projects for Annual Action Plan</h4>
                                    <div class="page-title-right">
                                        <ol class="breadcrumb m-0">
                                            <li class="breadcrumb-item"><a href="javascript: void(0);">Home</a></li>
                                            <li class="breadcrumb-item">Annual Action Plan</li>
                                            <li class="breadcrumb-item active">Project for Annual Action Plan</li>
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
                                        <h4 class="card-title mb-0 flex-grow-1">Filter : </h4>
                                         <a href="CreateAnnualActionplan.aspx"  class="filter-btn" style="float:right"><i class="icon-download"></i> Create New</a>
                                    </div>
                                    <!-- end card header -->
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-4">
                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divZone" runat="server">
                                                        <asp:Label ID="lblZoneH" runat="server" Text="Zone*" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:DropDownList ID="ddlZone" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divCircle" runat="server">
                                                        <asp:Label ID="lblCircleH" runat="server" Text="Circle*" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:DropDownList ID="ddlCircle" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCircle_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divDivision" runat="server">
                                                        <asp:Label ID="lblDivisionH" runat="server" Text="Division*" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                
                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divFY" runat="server">
                                                        <asp:Label ID="lblFY" runat="server" Text="Financial Year*" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:DropDownList ID="ddlFY" runat="server" CssClass="form-select"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                 <div class="col-xxl-3 col-md-6">
                                                    <div id="div4" runat="server">
                                                        <asp:Label ID="ProjectMaster" runat="server" Text="Scheme *" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:DropDownList ID="ddlProjectMaster" runat="server" CssClass="form-select"></asp:DropDownList>
                                                    </div>
                                                </div>

                                               
                                                <div class="col-xxl-1 col-md-3">
                                                    <div>
                                                        <label class="d-block">&nbsp;</label>
                                                           <asp:Button ID="BtnSearch" Text="Search" OnClick="BtnSearch_Click" runat="server"  CssClass="btn bg-success text-white"></asp:Button>
                                                       
                                                    </div>
                                                </div>
                                            </div>
                                            <!--end row-->
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="BtnSearch" />
                    <asp:PostBackTrigger ControlID="ddlDivision" />
                    <asp:PostBackTrigger ControlID="ddlCircle" />
                    <asp:PostBackTrigger ControlID="ddlZone" />
                    
                </Triggers>
            </asp:UpdatePanel>
             <div class="container-fluid">
                 <div class="row">
                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">Projects For Annual Action Plan</h4>
                                      
                                    </div>
                                    <!-- end card header -->
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-12">
                                                   <div class="clearfix" id="dtOptions" runat="server">
                                                        <div class="pull-right tableTools-container"></div>
                                                    </div>
                                                 <div style="overflow: auto">
                                                <asp:GridView runat="server" ID="grdPost" CssClass="display table table-bordered"   AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPost_PreRender" OnRowDataBound="grdPost_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sr. No.">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="Scheme" DataField="Project_Name" />
                                                        <asp:BoundField HeaderText="District" DataField="Circle_Name" />
                                                        <asp:BoundField HeaderText="ULB Name" DataField="Division_Name" />
                                                        <asp:BoundField HeaderText="Financial Year" DataField="FinancialYear_Comments" />
                                                        <asp:BoundField HeaderText="Project Name" DataField="ProjectName" />
                                                        <asp:BoundField HeaderText="Project Detail" DataField="ProjectDetail" />
                                                        <asp:BoundField HeaderText="Cost (in Lakhs)" DataField="costinlacks" />
                                                        <asp:BoundField HeaderText="Priority No" DataField="PrivorityNo" />
                                                        <asp:BoundField HeaderText="Reason For Selected" DataField="ReasonForSelected" />
                                                        <asp:BoundField HeaderText="Convergence Detail " DataField="ConvergeDetail" />                                            
                                                        <%-- <asp:TemplateField HeaderText="Docs">
                                                             <ItemTemplate>
                                                                 <asp:Label ID="lblNoDocument" runat="server" Text="No Document" Visible='<%# string.IsNullOrEmpty(Eval("Documents") as string) %>'></asp:Label>
                                                                 <asp:HyperLink ID="hlDocument" runat="server" NavigateUrl='<%# Eval("Documents") %>' Text="Doc" Visible='<%# !string.IsNullOrEmpty(Eval("Documents") as string) %>' Target="_blank"></asp:HyperLink>
                                                             </ItemTemplate>
                                                         </asp:TemplateField>--%>
                                                        <asp:TemplateField HeaderText="Edit">
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnEdit" runat="server" Text='Edit' CommandName="EditAnnualAction" OnCommand="Edit_Command" CommandArgument='<%# Eval("planId") %>' CssClass="btn btn-primary drill_btn" />
                                                            </ItemTemplate>                                                             
                                                        </asp:TemplateField>
                                                       <asp:TemplateField HeaderText="Delete">
                                                        <ItemTemplate>
                                                             <asp:Button ID="btnDelete" OnClientClick="return confirm('Are You Sure You Want to Delete this Item?')" runat="server" Text='Delete' CommandName="DeleteAnnualAction" OnCommand="btnDelete_Command" CommandArgument='<%# Eval("planId") %>' CssClass="btn btn-danger drill_btn" />
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
                                         </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
             </div>
        </div>
    </div>
   

</asp:Content>
