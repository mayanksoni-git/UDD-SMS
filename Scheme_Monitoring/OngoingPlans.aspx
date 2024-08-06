<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/TemplateMasterAdmin_PMS.master" CodeFile="OngoingPlans.aspx.cs" Inherits="OngoingPlans" %>
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
                                    <h4 class="mb-sm-0">On Going Plan</h4>
                                    <div class="page-title-right">
                                        <ol class="breadcrumb m-0">
                                            <li class="breadcrumb-item"><a href="javascript: void(0);">Home</a></li>
                                            <li class="breadcrumb-item">Annual Action Plan</li>
                                            <li class="breadcrumb-item active">On Going Plan</li>
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
                                        <h4 class="card-title mb-0 flex-grow-1">On Going Plan <label id="message" runat="server" style="float:right;color:red;font-weight:bold"></label></h4>
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
                                                    <div id="div4" runat="server">
                                                        <asp:Label ID="ProjectMaster" runat="server" Text="Select  Scheme *" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:DropDownList ID="ddlProjectMaster" runat="server" CssClass="form-select"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="div3" runat="server">
                                                        <asp:Label ID="lblRemark" runat="server" Text="Project Name" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:TextBox ID="ProjectName" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="div1" runat="server">
                                                        <asp:Label ID="Label1" runat="server" Text="sanctioned amount(in Rupees)" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:TextBox ID="Cost" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>

                                                </div>
                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="div9" runat="server">
                                                        <asp:Label ID="Label7" runat="server" Text="Received amount(in Rupees)" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:TextBox ID="ReceivedAmn" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>

                                                </div>
                                               
                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="div2" runat="server">
                                                        <asp:Label ID="Label2" runat="server" Text="Physical Progress %" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:TextBox  ID="prgPhysical" runat="server"  class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                 
                                                  <div class="col-xxl-3 col-md-6">
                                                    <div id="div10" runat="server">
                                                        <asp:Label ID="Label8" runat="server" Text="Estimate Complition Date" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:TextBox ID="EstimateDate" runat="server"  CssClass="form-control" TextMode="Date"></asp:TextBox>
                                                    </div>
                                                </div>

                                                 <div class="col-xxl-3 col-md-6">
                                                    <div id="div8" runat="server">
                                                        <asp:Label ID="Label6" runat="server" Text="Upload Doc" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:FileUpload ID="fileupload" runat="server" CssClass="form-control"  /> 
                                                         <a href="" target="_blank" id="UpladedDoc" runat="server"></a>
                                                    </div>
                                                </div>
                                                 <div class="col-xxl-3 col-md-6">
                                                    <div id="div5" runat="server">
                                                        <asp:Label ID="Label3" runat="server" Text="Detail Of Project" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:TextBox ID="detailOfProject" runat="server"  CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                    </div>
                                                </div>

                                                  <div class="col-xxl-3 col-md-6">
                                                    <div id="div6" runat="server">
                                                        <asp:Label ID="Label4" runat="server" Text="Remark" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:TextBox ID="Remarks" runat="server"  CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                    </div>
                                                </div>
                                                 <%-- <div class="col-xxl-3 col-md-6">
                                                    <div id="div7" runat="server">
                                                        <asp:Label ID="Label5" runat="server" Text="Convergence Detail" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:TextBox ID="convergence" runat="server"  CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                    </div>
                                                </div>--%>
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
                                        <h4 class="card-title mb-0 flex-grow-1">On Going Plan List</h4>
                                      
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
                                                        <asp:BoundField HeaderText="Project Name" DataField="ProjectName" />
                                                        <asp:BoundField HeaderText="Project Detail" DataField="ProjectDetail" />
                                                        <asp:BoundField HeaderText="Sanctioned Cost(in lacs)" DataField="costinlacks" />
                                                        <asp:BoundField HeaderText="Recieced Amount(in lacs)" DataField="RecievedAmnInLac" />
                                                        <asp:BoundField HeaderText="Physical Progress" DataField="PhysicalProgress" />
                                                        <asp:BoundField HeaderText="Estimated Completion Date" DataField="EstimatedCompletionOnlyDate" />
                                                        <asp:BoundField HeaderText="Remark" DataField="Remark" />                                            
                                                         <asp:TemplateField HeaderText="Docs">
                                                            <ItemTemplate>
                                                                 <a href="<%# Eval("Documents") %>" target="_blank">Doc</a>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Edit">
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnEdit" runat="server" Text='Edit' CommandName="EditAnnualAction" OnCommand="Edit_Command" CommandArgument='<%# Eval("OnPlanId") %>' CssClass="btn btn-primary drill_btn" />
                                                            </ItemTemplate>                                                             
                                                        </asp:TemplateField>
                                                       <asp:TemplateField HeaderText="Delete">
                                                        <ItemTemplate>
                                                             <asp:Button ID="btnDelete" runat="server" Text='Delete' CommandName="DeleteAnnualAction" OnCommand="btnDelete_Command" CommandArgument='<%# Eval("OnPlanId") %>' CssClass="btn btn-danger drill_btn" />
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
