<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/TemplateMasterAdmin_PMS.master" CodeFile="CreateAnnualActionplan.aspx.cs" Inherits="CreateAnnualActionplan" %>
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
                                    <h4 class="mb-sm-0" id="head1" runat="server">Create Projects for Annual Action Plan</h4>
                                    <div class="page-title-right">
                                        <ol class="breadcrumb m-0">
                                            <li class="breadcrumb-item"><a href="javascript: void(0);">Home</a></li>
                                            <li class="breadcrumb-item">Annual Action Plan</li>
                                            <li class="breadcrumb-item active">Create Project for Annual Action Plan</li>
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
                                        <h4 class="card-title mb-0 flex-grow-1" id="head2" runat="server">Create Project For Annual Action Plan </h4>
                                         <a href="AnnualActionplan.aspx"  class="filter-btn" style="float:right"><i class="icon-download"></i>Projects List</a>
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

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="div3" runat="server">
                                                        <asp:Label ID="lblRemark" runat="server" Text="Project Name*" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:TextBox ID="ProjectName" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="div1" runat="server">
                                                        <asp:Label ID="Label1" runat="server" Text="Cost(in Rupees)*" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:TextBox ID="Cost" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="div2" runat="server">
                                                        <asp:Label ID="Label2" runat="server" Text="Priority No (In range 1-5,5 is highest)*" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <input type="text" id="PriorityNo" runat="server" class="form-control" oninput="validateNumber(this)" />
                                                        <script>
                                                            function validateNumber(input) {
                                                                input.value = input.value.replace(/[^0-9]/g, '');
                                                            }
                                                        </script>
                                                    </div>
                                                </div>
                                                 
                                                 <div class="col-xxl-3 col-md-6" runat="server" visible="false">
                                                    <div id="div8" runat="server">
                                                        <asp:Label ID="Label6" runat="server" Text="Upload Doc (PDF)" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:FileUpload ID="fileupload" runat="server" CssClass="form-control" Accept=".pdf" /> 
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
                                                        <asp:Label ID="Label4" runat="server" Text="Reason For Selected" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:TextBox ID="ReasonForSelected" runat="server"  CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                    </div>
                                                </div>
                                                  <div class="col-xxl-3 col-md-6">
                                                    <div id="div7" runat="server">
                                                        <asp:Label ID="Label5" runat="server" Text="Convergence Detail" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:TextBox ID="convergence" runat="server"  CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xxl-11  col-md-11">
                                                    <div>
                                                        <label class="d-block">&nbsp;</label>
                                                        <label id="message" runat="server" style="color:red;font-weight:bold"></label>
                                                        <%--<asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnsave_Click"  convergence/>--%>
                                                        <asp:Button ID="btnCancel" Text="Cancel / Reset" OnClick="btnCancel_Click" style="float:right" runat="server" CssClass="btn bg-secondary text-white"></asp:Button>
                                                        <asp:Button ID="BtnUpdate" Text="Update" OnClick="BtnUpdate_Click" style="float:right" runat="server" Visible="false" CssClass="btn bg-success text-white"></asp:Button>
                                                        <asp:Button ID="btnSave" Text="Save" OnClick="btnsave_Click" runat="server" style="float:right" CssClass="btn bg-success text-white"></asp:Button>
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
           
        </div>
    </div>
   

</asp:Content>
