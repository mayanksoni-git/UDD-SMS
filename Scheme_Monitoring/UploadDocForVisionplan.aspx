<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/TemplateMasterAdmin_PMS.master" CodeFile="UploadDocForVisionplan.aspx.cs" Inherits="UploadDocForVisionplan" %>
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
                                    <h4 class="mb-sm-0">Upload Documents for Annual Action Plan</h4>
                                    <div class="page-title-right">
                                        <ol class="breadcrumb m-0">
                                            <li class="breadcrumb-item"><a href="javascript: void(0);">Home</a></li>
                                            <li class="breadcrumb-item">Annual Action Plan</li>
                                            <li class="breadcrumb-item active">Upload Document for Annual Action Plan</li>
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
                                        <h4 class="card-title mb-0 flex-grow-1">Upload Document For Annual Action Plan <label id="message" runat="server" style="float:right;color:red;font-weight:bold"></label></h4>
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
                                                        <asp:Label ID="lblFY" runat="server" Text="Select Financial Year*" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:DropDownList ID="ddlFY" runat="server" CssClass="form-select"></asp:DropDownList>
                                                    </div>
                                                </div>
                                               
                                                 
                                                 <div class="col-xxl-3 col-md-6">
                                                    <div id="div8" runat="server">
                                                        <asp:Label ID="Label6" runat="server" Text="Upload Doc  (PDF)" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:FileUpload ID="fileupload" runat="server" CssClass="form-control" Accept=".pdf" /> 
                                                         <a href="" target="_blank" id="UpladedDoc" runat="server"></a>
                                                    </div>
                                                </div>
                                                 
                                                <div class="col-xxl-3  col-md-6">
                                                    <div>
                                                        <label class="d-block">&nbsp;</label>
                                                        <%--<asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnsave_Click"  convergence/>--%>
                                                        <asp:Button ID="BtnUpdate" Text="Update" OnClick="BtnUpdate_Click" runat="server" Visible="false" CssClass="btn bg-success text-white"></asp:Button>
                                                        <asp:Button ID="btnSave" Text="Save" OnClick="btnsave_Click" runat="server" CssClass="btn bg-success text-white"></asp:Button>
                                                        <asp:Button ID="btnCancel" Text="Cancel / Reset" OnClick="btnCancel_Click" runat="server" CssClass="btn bg-secondary text-white"></asp:Button>
                                                        <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                                                        <asp:HiddenField ID="hfFormApproval_Id" runat="server" />
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
                    <asp:PostBackTrigger ControlID="btnCancel" />
                    <asp:PostBackTrigger ControlID="btnSave" />
                    <asp:PostBackTrigger ControlID="BtnUpdate" />
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
                                        <h4 class="card-title mb-0 flex-grow-1">Upload Documents For Annual Action Plan</h4>
                                      
                                    </div>
                                    <!-- end card header -->
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-12">
                                                   <div class="clearfix" id="dtOptions" runat="server">
                                                        <div class="pull-right tableTools-container"></div>
                                                    </div>
                                                 <div style="overflow: auto">
                                                <asp:GridView runat="server" ID="grdPost" CssClass="display table table-bordered"   AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPost_PreRender">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sr. No.">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="State" DataField="Zone_Name" />
                                                        <asp:BoundField HeaderText="District" DataField="Circle_Name" />
                                                        <asp:BoundField HeaderText="ULBName" DataField="Division_Name" />
                                                        <asp:BoundField HeaderText="Financial Year" DataField="FinancialYear_Comments" />
                                                                                              
                                                         <asp:TemplateField HeaderText="Action Plan Doc.">
                                                             <ItemTemplate>
                                                                 <asp:Label ID="lblNoDocument" runat="server" Text="No Document" Visible='<%# string.IsNullOrEmpty(Eval("Documents") as string) %>'></asp:Label>
                                                                 <asp:HyperLink ID="hlDocument" runat="server" NavigateUrl='<%# Eval("Documents") %>' Text="Doc" Visible='<%# !string.IsNullOrEmpty(Eval("Documents") as string) %>' Target="_blank"></asp:HyperLink>
                                                             </ItemTemplate>
                                                         </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Edit">
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnEdit" runat="server" Text='Edit' CommandName="EditAnnualAction" OnCommand="Edit_Command" CommandArgument='<%# Eval("DocId") %>' CssClass="btn btn-primary drill_btn" />
                                                            </ItemTemplate>                                                             
                                                        </asp:TemplateField>
                                                       <asp:TemplateField HeaderText="Delete">
                                                        <ItemTemplate>
                                                             <asp:Button ID="btnDelete" OnClientClick="return confirm('Are You Sure !!!')" runat="server" Text='Delete' CommandName="DeleteAnnualAction" OnCommand="btnDelete_Command" CommandArgument='<%# Eval("DocId") %>' CssClass="btn btn-danger drill_btn" />
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
