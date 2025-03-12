<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/TemplateMasterAdmin_PMS.master" CodeFile="AddCMFellowDetail.aspx.cs" Inherits="AddCMFellowDetail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hfCMFellowDetailId" runat="server" />
    <style>
        .profile-images-card {
            height: 200px;
            width: 200px;                 
            border: 5px solid #f67d37;
            border-radius: 50%;
            margin-top: 4vh;
        }
        .profile-images-card .header-profile-user {
            height: 191px;
            width: 191px;
        }
        .custom-file input[type='file'] {
            margin: 15px;
        }

       .profile-images {
    display: flex;
    align-items: center;
    height: 85px;
    padding-top: 22px;
}
       .header-profile-user {
    height: 60px;
    width: 60px;
}
    </style>

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
                                    <h4 class="mb-sm-0" id="head1" runat="server">Add CM Fellow Detail</h4>
                                    <div class="page-title-right">
                                        <ol class="breadcrumb m-0">
                                            <li class="breadcrumb-item"><a href="javascript: void(0);">Home</a></li>
                                            <li class="breadcrumb-item">Akanshi Yojna</li>
                                            <li class="breadcrumb-item active">Add CM Fellow Detail</li>
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
                                        <h4 class="card-title mb-0 flex-grow-1" id="head2" runat="server">Add CM Fellow Detail<label id="message" runat="server" style="float: right; color: red; font-weight: bold"></label></h4>
                                        <a href="CMFellowDetail.aspx" class="filter-btn" style="float: right"><i class="icon-download"></i>CM Fellow List</a>
                                    </div>
                                    <!-- end card header -->
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-4">
                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divZone" runat="server">
                                                        <asp:Label ID="lblZoneH" runat="server" Text="State*" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:DropDownList ID="ddlZone" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divCircle" runat="server">
                                                        <asp:Label ID="lblCircleH" runat="server" Text="District*" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:DropDownList ID="ddlCircle" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCircle_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divDivision" runat="server">
                                                        <asp:Label ID="lblDivisionH" runat="server" Text="ULB*" CssClass="form-label fw-bold me-1"></asp:Label>
                                                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divCMFellowName" runat="server">
                                                        <asp:Label ID="lblCMFellowName" runat="server" Text="CM Fellow Name" CssClass="form-label"></asp:Label>
                                                        <asp:TextBox ID="txtCMFellowName" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div> 
                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divEducationalDetail" runat="server">
                                                        <asp:Label ID="lblEducationalDetail" runat="server" Text="Educational Detail(In 200 Words)" CssClass="form-label"></asp:Label>
                                                        <asp:TextBox ID="txtEducationalDetail" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divProfessionalDetail" runat="server">
                                                        <asp:Label ID="lblProfessionalDetail" runat="server" Text="Professional Detail(In 200 Words)" CssClass="form-label"></asp:Label>
                                                        <asp:TextBox ID="txtProfessionalDetail" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divExperience" runat="server">
                                                        <asp:Label ID="lblExperience" runat="server" Text="Experience Share(About Akanshi Nagar Yojna)" CssClass="form-label"></asp:Label>
                                                        <asp:TextBox ID="txtExperience" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div class="profile-images">
                                                        <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank">
                                                            <img class="rounded-circle header-profile-user" src="assets/images/users/avatar-1.jpg" id="uploadimg" runat="server">
                                                        </asp:HyperLink>
                                                        <div class="custom-file">
                                                            <asp:FileUpload ID="fileupload" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <asp:Label ID="Label1" runat="server" Text="Uploaded Image" CssClass="form-label"></asp:Label>
                                                    <asp:HyperLink ID="hypCMFellowImage" runat="server" Target="_blank" Visible="false">
                                                      <asp:Image ID="imgCMFellow" runat="server" CssClass="rounded-circle header-profile-user" AlternateText="CM Fellow Image" />
                                                     </asp:HyperLink>
                                                    <asp:HiddenField ID="hfImageUrl" runat="server" />
                                                </div>

                                                <div class="col-xxl-3  col-md-6">
                                                    <div>
                                                        <label class="d-block">&nbsp;</label>
                                                        <asp:Button ID="btnUpdate" Text="Update" OnClick="BtnUpdate_Click" runat="server" Visible="false" CssClass="btn bg-success text-white"></asp:Button>
                                                        <asp:Button ID="btnSave" Text="Save" OnClick="btnsave_Click" runat="server" CssClass="btn bg-success text-white"></asp:Button>
                                                        <asp:Button ID="btnCancel" Text="Cancel / Reset" OnClick="btnCancel_Click" runat="server" CssClass="btn bg-secondary text-white"></asp:Button>
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
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>
   <script>
       $(function () {
           $("#<%= fileupload.ClientID %>").change(function(event) {
            var x = URL.createObjectURL(event.target.files[0]);
            document.getElementById("<%= uploadimg.ClientID %>").src = x;
             });
     });
   </script>
</asp:Content>
