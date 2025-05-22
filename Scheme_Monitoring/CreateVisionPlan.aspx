<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/TemplateMasterAdmin_PMS.master" CodeFile="CreateVisionPlan.aspx.cs" Inherits="CreateVisionPlan" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="VisionPlanID" runat="server" />
    <asp:HiddenField ID="ULBID" runat="server" />
    <asp:HiddenField ID="FYID" runat="server" />
    <link href="assets/css/CalendarStyle.css" rel="stylesheet" />
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
                                    <h4 class="mb-sm-0" id="HeadingSec" runat="server">Add Karya Yojna(कार्य योजना)</h4>
                                    <div class="page-title-right">
                                        <ol class="breadcrumb m-0">
                                            <li class="breadcrumb-item"><a href="javascript: void(0);">Home</a></li>
                                            <li class="breadcrumb-item">CMVNY Scheme</li>
                                            <li class="breadcrumb-item active">Add Karya Yojna(कार्य योजना)</li>
                                        </ol>
                                    </div>
                                </div>
                            </div>
                            <!--end col-->
                        </div>

                        <div class="row">
                            <div class="col-lg-12">
                                <div class="card" id="sectionFilter" runat="server">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">Add Karya Yojna(कार्य योजना)</h4>
                                        <a href="VisionPlan.aspx" class="filter-btn" style="float: right; width: 155px"><i class="icon-download"></i>Karya Yojna(कार्य योजना) List</a>
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
                                                        <asp:DropDownList ID="ddlDivision" runat="server" AutoPostBack="true" CssClass="form-select"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divFY" runat="server">
                                                        <asp:Label ID="lblFY" runat="server" Text=" Financial Year*" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlFY" runat="server" CssClass="form-select" AutoPostBack="true"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divProj" runat="server">
                                                        <asp:Label ID="lblProj" runat="server" Text="Project type*" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="DDLProj" runat="server" CssClass="form-select"></asp:DropDownList>
                                                        <asp:HiddenField ID="hfScalarValue" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="col-xxl-3 col-md-6 " id="">
                                                    <div id="div9" runat="server">
                                                        <asp:Label ID="Label11" runat="server" Text="Project Name" CssClass="form-label"></asp:Label>
                                                        <asp:TextBox ID="TxtProject" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="div10" runat="server">
                                                        <asp:Label ID="lblProjectCost" runat="server" Text="Project Cost(In Lakhs)*" CssClass="form-label"></asp:Label>
                                                        <asp:TextBox ID="txtProjectCost" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event, true);" TextMode="Number"></asp:TextBox>
                                                        <asp:RequiredFieldValidator
                                                            ID="rfvProjectCost"
                                                            runat="server"
                                                            ControlToValidate="txtProjectCost"
                                                            ErrorMessage="Project Cost is required."
                                                            CssClass="text-danger"
                                                            Display="Dynamic"
                                                            SetFocusOnError="true" />
                                                        <asp:RangeValidator
                                                            ID="rvProjectCost"
                                                            runat="server"
                                                            ControlToValidate="txtProjectCost"
                                                            MinimumValue="0.01"
                                                            MaximumValue="10000.00"
                                                            Type="Double"
                                                            ErrorMessage="Project Cost must be between 0.01 and 10000.00 Lakhs."
                                                            CssClass="text-danger"
                                                            Display="Dynamic"
                                                            SetFocusOnError="true" />
                                                    </div>
                                                </div>
                                                <div class="col-xxl-3 col-md-6 d-none">
                                                    <div id="div11" runat="server">
                                                        <asp:Label ID="lblQuantity" runat="server" Text="Quantity/Capacity(Only Number)*" CssClass="form-label"></asp:Label>
                                                        <asp:TextBox ID="txtQuantity" Text="0" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event, false);" TextMode="Number"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xxl-3 col-md-6 d-none">
                                                    <div id="div12" runat="server">
                                                        <asp:Label ID="lblSiteArea" runat="server" Text="Site Area(In Square Meter)*" CssClass="form-label"></asp:Label>
                                                        <asp:TextBox ID="txtSiteArea" Text="0" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event, false);" TextMode="Number"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xxl-3 col-md-6" id="sectionLocation" runat="server">
                                                    <div id="div6" runat="server">
                                                        <asp:Label ID="Label9" runat="server" Text="Location (Ward Name)*" CssClass="form-label"></asp:Label>
                                                        <asp:TextBox ID="Location" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xxl-3 col-md-6 d-none" id="Div13" runat="server">
                                                    <div id="div14" runat="server">
                                                        <asp:Label ID="Label12" runat="server" Text="Location (Ward Name)*" CssClass="form-label"></asp:Label>
                                                        <asp:TextBox ID="txtApprovedProjCost" runat="server" onkeypress="return isNumberKey(event, false);" Visible="false" Text="0" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row gy-4">
                                                <div class="col-xxl-3 col-md-6">
                                                    <div runat="server" id="NewConstruction">
                                                        <asp:Label ID="Label1" runat="server" Text="Is Constructed ?*" CssClass="form-label"></asp:Label>
                                                       <asp:RadioButton ID="RadioButton1" AutoPostBack="true" OnCheckedChanged="RadioButton_CheckedChanged" runat="server" GroupName="IsConstructed" Text="Constructed " Value="1" />
                                                        <asp:RadioButton ID="RadioButton2" AutoPostBack="true" OnCheckedChanged="RadioButton_CheckedChanged" runat="server" GroupName="IsConstructed" Text="Under Construction " Value="2" />
                                                        <asp:RadioButton ID="RadioButton3" AutoPostBack="true" OnCheckedChanged="RadioButton_CheckedChanged" runat="server" GroupName="IsConstructed" Text="Under Sanction " Value="3" />
                                                    </div>
                                                </div>
                                                <div class="col-xxl-3 col-md-6">
                                                    <div runat="server" id="divIsHeritage">
                                                        <asp:Label ID="Label13" runat="server" Text="Is Heritage Building ?*" CssClass="form-label"></asp:Label>
                                                        <asp:RadioButton ID="RadioButton11" AutoPostBack="true" OnCheckedChanged="RadioButton_CheckedChanged" runat="server" GroupName="IsHeritage" Text="Yes" Value="1" />
                                                        <asp:RadioButton ID="RadioButton12" AutoPostBack="true" OnCheckedChanged="RadioButton_CheckedChanged" runat="server" GroupName="IsHeritage" Text="No" Value="0" />
                                                    </div>
                                                </div>
                                                <div class="col-xxl-3 col-md-6 validsec" id="sectionyear" runat="server">
                                                    <div id="div2" runat="server">
                                                        <asp:Label ID="Label2" runat="server" Text="Enter year*" CssClass="form-label"></asp:Label>
                                                        <asp:TextBox ID="TxtYear" placeholder="yyyy" runat="server" CssClass="form-control" maxlength="4" onkeypress="return isYearKey(event);" onblur="validateYear(this);"></asp:TextBox>
                                                        <asp:RegularExpressionValidator
                                                            ID="revYear"
                                                            runat="server"
                                                            ControlToValidate="TxtYear"
                                                            ValidationExpression="^\d{4}$"
                                                            ErrorMessage="Enter a valid year in YYYY format."
                                                            CssClass="text-danger"
                                                            Display="Dynamic"
                                                            SetFocusOnError="true" />
                                                    </div>
                                                </div>
                                                <div class="col-xxl-3 col-md-6 validsec" id="sectionCond" runat="server">
                                                    <div id="condition" runat="server">
                                                        <asp:Label ID="Label3" runat="server" Text="Condition *" CssClass="form-label"></asp:Label>
                                                        <asp:RadioButton ID="RadioButton4" runat="server" GroupName="Condition" Text="Good " Value="1" />
                                                        <asp:RadioButton ID="RadioButton5" runat="server" GroupName="Condition" Text="Need Renovation" Value="2" /><br />
                                                        <asp:RadioButton ID="RadioButton6" runat="server" GroupName="Condition" Text="Need Redevelopement" Value="3" />
                                                    </div>
                                                </div>
                                                <div class="col-xxl-3 col-md-6 validsec" id="secUser" runat="server">
                                                    <div id="UserCharge" runat="server">
                                                        <asp:Label ID="Label4" runat="server" Text="User Charge *" CssClass="form-label"></asp:Label>
                                                        <asp:RadioButton ID="RadioButton7" AutoPostBack="true" OnCheckedChanged="RadioButton_CheckedChanged" runat="server" GroupName="usercharge" Text="Yes" Value="1" />
                                                        <asp:RadioButton ID="RadioButton8" AutoPostBack="true" OnCheckedChanged="RadioButton_CheckedChanged" runat="server" GroupName="usercharge" Text="No" Value="0" /><br />
                                                    </div>
                                                </div>
                                                <div class="col-xxl-3 col-md-6 validsec" runat="server" id="sectionusercharge">
                                                    <div id="div1" runat="server">
                                                        <asp:Label ID="Label5" runat="server" Text="Amount of User Charge(in rupees)*" CssClass="form-label"></asp:Label>
                                                        <asp:TextBox ID="Amounts" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row gy-4">
                                                <div class="col-xxl-3 col-md-6 validsec" runat="server" id="sectionuOwner">
                                                    <div id="Div3" runat="server">
                                                        <asp:Label ID="Label6" runat="server" Text="Is Owner Nagar Nigam or ULB *" CssClass="form-label"></asp:Label>
                                                        <asp:RadioButton ID="RadioButton9" AutoPostBack="true" OnCheckedChanged="RadioButton_CheckedChanged" runat="server" GroupName="Owner" Text="Yes " Value="1" />
                                                        <asp:RadioButton ID="RadioButton10" AutoPostBack="true" OnCheckedChanged="RadioButton_CheckedChanged" runat="server" GroupName="Owner" Text="No" Value="0" /><br />
                                                    </div>
                                                </div>
                                                <div class="col-xxl-3 col-md-6 validsec" runat="server" id="secOtherown">
                                                    <div id="div4" runat="server">
                                                        <asp:Label ID="Label7" runat="server" Text="Owner Department*" CssClass="form-label"></asp:Label>
                                                        <asp:TextBox ID="OtherDepartment" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row gy-4">

                                                <div class="col-xxl-3 col-md-6" id="sectionpriority" runat="server">
                                                    <div id="div7" runat="server">
                                                        <asp:Label ID="Label10" runat="server" Text="Self-Priority (5 is the highest, 1 is the lowest)" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="DdlPriority" runat="server" CssClass="form-select">
                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="1">1 - Very Low Priority</asp:ListItem>
                                                            <asp:ListItem Value="2">2 - Low Priority</asp:ListItem>
                                                            <asp:ListItem Value="3">3 - Medium Priority</asp:ListItem>
                                                            <asp:ListItem Value="4">4 - High Priority</asp:ListItem>
                                                            <asp:ListItem Value="5">5 - Highest Priority</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6" id="sectionSimilar" runat="server">
                                                    <div id="div5" runat="server">
                                                        <asp:Label ID="Label8" runat="server" Text="Number Of Similar Existing Projects in City*" CssClass="form-label"></asp:Label>
                                                        <asp:TextBox ID="similarProj" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-10  col-md-10">
                                                    <div>
                                                        <asp:Button ID="BtnSave" Text="Submit" OnClick="BtnSave_Click" runat="server" Style="float: right" CssClass="btn bg-success text-white"></asp:Button>
                                                        <asp:Button ID="BtnUpdate" Text="Update" Visible="false" OnClick="BtnUpdate_Click" runat="server" Style="float: right" CssClass="btn bg-success text-white"></asp:Button>
                                                    </div>
                                                </div>
                                            </div>
                                            <!--end row-->
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="ddlCircle" />
                    <asp:PostBackTrigger ControlID="ddlDivision" />
                    <asp:PostBackTrigger ControlID="ddlFY" />
                    <asp:PostBackTrigger ControlID="BtnUpdate" />
                    <asp:PostBackTrigger ControlID="BtnSave" />
                    <asp:PostBackTrigger ControlID="RadioButton1" />
                    <asp:PostBackTrigger ControlID="RadioButton2" />
                    <asp:PostBackTrigger ControlID="RadioButton3" />
                    <asp:PostBackTrigger ControlID="RadioButton4" />
                    <asp:PostBackTrigger ControlID="RadioButton5" />
                    <asp:PostBackTrigger ControlID="RadioButton6" />
                    <asp:PostBackTrigger ControlID="RadioButton7" />
                    <asp:PostBackTrigger ControlID="RadioButton8" />
                    <asp:PostBackTrigger ControlID="RadioButton9" />
                    <asp:PostBackTrigger ControlID="RadioButton10" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>
    <script>
        $(document).ready(function () {
            $('#<%= DDLProj.ClientID %>').change(function () {
                var selectedValue = $(this).val();
                var selectedText = $(this).find("option:selected").text();

                $.ajax({
                    type: "POST",
                    url: "CreateVisionPlan.aspx/DDLProj_SelectedIndexChanged",
                    data: JSON.stringify({ id: selectedValue }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        var parsedResponse = JSON.parse(response.d);
                        var scalarValue = parsedResponse.scalarValue;
                        $('#modalContent').html(scalarValue);
                        $('#ProjectTypeText').text('Guide Lines to fill form for Project Type: ' + selectedText);
                        // Update modal content
                        // Show the modal
                        $('#myModal').modal('show');
                    },
                    error: function (xhr, status, error) {
                        console.error("Error: " + error);
                    }
                });
            });
        });
    </script>
    <script type="text/javascript">
        // Allow only numbers (and optionally one decimal point)
        function isNumberKey(evt, allowDecimal) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            // Allow backspace, tab, delete, arrows
            if (charCode == 8 || charCode == 9 || charCode == 46 || (charCode >= 37 && charCode <= 40))
                return true;
            // Allow numbers
            if (charCode >= 48 && charCode <= 57)
                return true;
            // Allow one decimal point if allowed
            if (allowDecimal && charCode == 46) {
                var input = evt.target.value;
                if (input.indexOf('.') === -1) return true;
            }
            return false;
        }

        // Allow only numbers, max 4 digits for year
        function isYearKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            // Allow backspace, tab, delete, arrows
            if (charCode == 8 || charCode == 9 || charCode == 46 || (charCode >= 37 && charCode <= 40))
                return true;
            // Allow numbers only
            if (charCode >= 48 && charCode <= 57)
                return true;
            return false;
        }

        // Validate year on blur
        function validateYear(input) {
            var year = input.value;
            if (!/^\d{4}$/.test(year)) {
                alert('Please enter a valid year in YYYY format.');
                input.value = '';
                input.focus();
            }
        }
</script>

    <div class="modal fade" id="myModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 id="ProjectTypeText"></h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <p id="modalContent"></p>
                    .
                    <a href="PDFs/GuideLines/Cost%20Estimate.pdf" target="_blank" class="btn btn-sm bg-success text-white">Click here</a> to See Full Guidelines
                </div>
                <div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
