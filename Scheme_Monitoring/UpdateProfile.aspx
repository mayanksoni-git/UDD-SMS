<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true" CodeFile="UpdateProfile.aspx.cs" Inherits="UpdatePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .profile-images-card
        {
         height: 200px;
    width: 200px;
    border: 5px solid #f67d37;
    border-radius: 50%;
    margin-top: 4vh;
}
      .profile-images-card  .header-profile-user {
    height: 191px;
    width: 191px;
}
      .custom-file input[type='file'] {
   /* display: none;*/
   margin:15px;
}
    </style>
 <div class="main-content">
        <div class="main-content-inner">
            <div class="page-content">
                <div class="container-fluid">
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
            </cc1:ToolkitScriptManager>
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>
                    <div class="row">
                                <div class="col-12 mb-3">
                                    <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                                        <h4 class="mb-sm-0">Update Profile</h4>
                                        <div class="page-title-right">
                                            <ol class="breadcrumb m-0">
                                                <li class="breadcrumb-item"><a href="javascript: void(0);">Home</a></li>
                                                <li class="breadcrumb-item">Profile</li>
                                                <li class="breadcrumb-item active">Update Profile</li>
                                            </ol>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12">

                                    <div class="card">
                                        <div class="card-body">
                                            <div class="live-preview">
                                                <div class="row gy-4">
                                                    <div class="col-xxl-5 col-md-6">
                                                        <div class="row">
                                                            <div class="col-md-5">
                                                              

                                                            </div>
                                                            <div class="col-md-6">
                                                               <div class="profile-images-card" style="margin-top:10px;">
                                                                    <div class="profile-images">
                                                                     <img class="rounded-circle header-profile-user" src="assets/images/users/avatar-1.jpg" id="uploadimg" runat="server">

                                                                     </div>
                                                                  <div class="custom-file">
                                                                        <asp:FileUpload ID="fileupload" runat="server"  /> 
                                                                       <asp:HiddenField ID="ProfileUrl" runat="server"></asp:HiddenField>
                                                                 </div>

                                                              </div>
                                                            </div>
                                                            <div class="col-md-"></div>
                                                        </div>
                                                       
                                                        </div>
                                                     <div class="col-xxl-7 col-md-6" style="margin-top:50px">
                                                         <div class="row">
                                                          
                                                       <div class="col-xxl-8 col-md-6">
                                                        <div>
                                                            <label class="control-label no-padding-right">
                                                                Employee Name  
                                                                <span style="color: red; font-weight: bold;">*</span></label>
                                                            <asp:TextBox ID="txtPersonName" runat="server" CssClass="form-control" onfocusout="checkDataFilled(this);"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <!--end col-->
                                                    <div class="col-xxl-8 col-md-6">
                                                        <div>
                                                            <label class="control-label no-padding-right">Father's Name</label>
                                                            <asp:TextBox ID="txtPersonFName" runat="server" CssClass="form-control" onfocusout="checkDataFilled(this);"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <!--end col-->
                                                   <%-- <div class="col-xxl-8 col-md-6">
                                                        <div>
                                                            <label class="control-label no-padding-right">Mobile No*</label>
                                                            <asp:TextBox ID="txtMobileNo1" runat="server" CssClass="form-control" MaxLength="10" onkeyup="isNumericVal(this);" onfocusout="checkDataFilled(this);"></asp:TextBox>
                                                        </div>
                                                    </div>--%>
                                                    <!--end col-->
                                                    <div class="col-xxl-8 col-md-6">
                                                        <div>
                                                            <label class="control-label no-padding-right">Alternate Mobile No</label>
                                                            <asp:TextBox ID="txtMobileNo2" runat="server" CssClass="form-control" MaxLength="10" onkeyup="isNumericVal(this);"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <!--end col-->
                                                    <div class="col-xxl-8 col-md-6">
                                                        <div>
                                                            <label class="control-label no-padding-right">Land Line No (If Any)</label>
                                                            <asp:TextBox ID="txtLandLine" runat="server" CssClass="form-control" MaxLength="11" onkeyup="isNumericVal(this);"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <!--end col-->
                                                    
                                                    <!--end col-->
                                                    <div class="col-xxl-8 col-md-6">
                                                        <div>
                                                            <label class="control-label no-padding-right">Address</label>
                                                            <asp:TextBox ID="txtAddress"  runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                             <div class="col-sm-10">
                                                                 <br />
                                                            <asp:Button ID="btnChange" runat="server" Text="Update" CssClass="width-65 pull-right btn btn-sm btn-success" OnClick="btnChange_Click" />

                                                                <%-- <asp:button class="btn btn-danger" >Save</asp:button>--%>
                                                             </div>


                                                         </div>
                                                        </div>
                                                   
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                    
                        </div>
                   
                </ContentTemplate>
                 <Triggers>
                        <asp:PostBackTrigger ControlID="btnChange" />
       
                    </Triggers>
            </asp:UpdatePanel>
            <asp:UpdateProgress ID="UpdateProgress1" DynamicLayout="true" runat="server" AssociatedUpdatePanelID="up">
                <ProgressTemplate>
                    <div style="position: fixed; z-index: 999; height: 100%; width: 100%; top: 0; background-color: Black; filter: alpha(opacity=60); opacity: 0.6; -moz-opacity: 0.8; cursor: not-allowed;">
                        <div style="z-index: 1000; margin: 300px auto; padding: 10px; width: 130px; background-color: White; border-radius: 10px; filter: alpha(opacity=100); opacity: 1; -moz-opacity: 1;">
                            <img src="assets/images/mb/mbloader.gif" style="height: 100px; width: 100px;" />
                        </div>
                    </div>
                </ProgressTemplate>
                
            </asp:UpdateProgress>

        </div>
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
     </div>
</asp:Content>

