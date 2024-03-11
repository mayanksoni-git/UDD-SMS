<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MasterViewPensionerData.aspx.cs" Inherits="MasterViewPensionerData" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View Pensioner record</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta charset="utf-8" />

    <link rel="icon" href="assets/images/mb/icon.png" type="image/x-icon">
    <meta name="description" content="ERP System, Jal Nigam Uttar Pradesh" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <!-- bootstrap & fontawesome -->
    <link rel="stylesheet" href="assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="assets/font-awesome/4.5.0/css/font-awesome.min.css" />

    <!-- page specific plugin styles -->
    <link rel="stylesheet" href="assets/css/colorbox.min.css">
    <link rel="stylesheet" href="assets/css/jquery-ui.custom.min.css" />
    <link rel="stylesheet" href="assets/css/chosen.min.css" />
    <link rel="stylesheet" href="assets/css/bootstrap-datepicker3.min.css" />
    <link rel="stylesheet" href="assets/css/bootstrap-timepicker.min.css" />
    <link rel="stylesheet" href="assets/css/daterangepicker.min.css" />
    <link rel="stylesheet" href="assets/css/bootstrap-datetimepicker.min.css" />
    <link rel="stylesheet" href="assets/css/bootstrap-colorpicker.min.css" />
    <link rel="stylesheet" href="assets/css/chosen.min.css" />
    <!-- text fonts -->
    <link rel="stylesheet" href="assets/css/fonts.googleapis.com.css" />

    <!-- ace styles -->
    <%--<link rel="stylesheet" href="assets/css/ace.min.css" class="ace-main-stylesheet" id="main-ace-style" />--%>
    <link rel="stylesheet" href="assets/css/ace.min.css" class="ace-main-stylesheet" />

    <!--[if lte IE 9]>
			<link rel="stylesheet" href="assets/css/ace-part2.min.css" class="ace-main-stylesheet" />
		<![endif]-->
    <link rel="stylesheet" href="assets/css/ace-skins.min.css" />
    <link rel="stylesheet" href="assets/css/ace-rtl.min.css" />
    <link rel="stylesheet" href="assets/css/myStyle.css" />

    <style>
        .btn-grad {
            background-image: linear-gradient(to right, #3D7EAA 0%, navy 51%, #3D7EAA 100%);
            margin: 8px;
            padding: 10px 35px;
            text-align: center;
            text-transform: uppercase;
            transition: 0.7s;
            background-size: 200% auto;
            box-shadow: 0 0 20px #eee;
            border-radius: 10px;
            display: block;
            color: whitesmoke;
            font-weight: bold;
            font-size: 17px;
            text-shadow: 3px 2px 1px black;
        }

            .btn-grad:hover {
                background-position: right center; /* change the direction of the change here */
                color: #fff;
                text-decoration: none;
            }

        .control-label {
            text-transform: uppercase;
            color: #383535;
            font-family: sans-serif;
            font-weight: bold;
            transition: 0.5s;
        }

            .control-label:hover {
                color: #dd0909;
                font-weight: bolder;
                letter-spacing: 0.5px;
            }

        .boxcontent {
            padding-top: 10px;
        }
    </style>

</head>
<body>
    <form runat="server">
        <div class="page-content">
            <div class="page-header">
                <div class="row">
                    <h1>Pensioner Details </h1>
                </div>
            </div>
            
            <%--Printing Page Starts --%>

           <%-- <div class="container">
                <table class="table-responsive">
                    <tr>
                        <th>Retirement Zone</th>
                        <th>Retirement Division</th>
                        <th>Pension Zone</th>
                        <th>Pension Division</th>
                        <th>Name Of Pensioner </th>
                        <th>Father Name/Husband Name</th>
                    </tr>
                    <tr>
                        <td runat="server" id="txtZoneRetirement" Enabled="false"></td>
                        <td runat="server" id="txtRetirmentDivision" Enabled="false" ></td>
                        <td runat="server" id="txtZonePension" Enabled="false" ></td>
                        <td runat="server" id="txtPensionDivision" Enabled="false" ></td>
                        <td runat="server" id="txtPensionerName" Enabled="false" ></td>
                        <td runat="server" id="txtFatherName" Enabled="false" ></td>
                    </tr>
                      <tr>
                        <th>Mobile Number</th>
                        <th>Pan Card No.</th>
                        <th>Addhar Card No.</th>
                        <th>Permanent Address</th>
                        <th>Postal Address</th>
                        <th>Gender</th>
                    </tr>
                    <tr>
                        <td runat="server" id="txtMobileNo" Enabled="false"></td>
                        <td runat="server" id="txtPanCardNo" Enabled="false" ></td>
                        <td runat="server" id="txtAadharNo" Enabled="false" ></td>
                        <td runat="server" id="txtPermanentAddress" Enabled="false" ></td>
                        <td runat="server" id="txtPostalAddress" Enabled="false" ></td>
                        <td>
                              <asp:RadioButtonList ID="rbtGender" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" Enabled="false">
                                      <asp:ListItem Value="M" Text="&nbsp; Male &nbsp;"></asp:ListItem>
                                      <asp:ListItem Value="F" Text="&nbsp; Female &nbsp;"></asp:ListItem>
                                      <asp:ListItem Value="Tg" Text="&nbsp; Trans Gender"></asp:ListItem>
                                                    </asp:RadioButtonList></td>
                    </tr>
                    <tr>
                        <th>Retirement Year</th>
                        <th>Date of Birth</th>
                        <th>Date of Joining</th>
                        <th>Date of Retirement</th>
                        <th>Date of Death</th>
                        <th>Date of Regulization</th>
                    </tr>
                    <tr>
                        <td runat="server" id="txtRetirementYear" Enabled="false"></td>
                        <td runat="server" id="txtDOB" Enabled="false" ></td>
                        <td runat="server" id="txtDOJ" Enabled="false" ></td>
                        <td runat="server" id="txtDOR" Enabled="false" ></td>
                        <td runat="server" id="txtDOD" Enabled="false" ></td>
                        <td runat="server" id="txtDORegularization" Enabled="false" ></td>
                        </tr>
                    <tr>
                        <th>Date of Sending Pension By HQ</th>
                        <th>Date of Receiving Pension form by HQ</th>
                        <th>A.A.O. Name</th>
                        <th>Designation</th>
                        <th>आवंटन के हिसाब के लेखाकार का नाम </th>
                        <th>Pension Code</th>
                    </tr>
                    <tr>
                        <td runat="server" id="txtPensionFormSendingDate" Enabled="false"></td>
                        <td runat="server" id="txtPensionFormreceivingDate" Enabled="false" ></td>
                        <td runat="server" id="txtAAOPensionName" Enabled="false" ></td>
                        <td runat="server" id="txtDesignation" Enabled="false" ></td>
                        <td runat="server" id="txtAccountantName" Enabled="false" ></td>
                        <td runat="server" id="txtPensionCode" Enabled="false" ></td>
                        </tr>
                     <tr>
                        <th>Pay Band</th>
                        <th>Pay Scale</th>
                        <th>Grade Pay</th>
                        <th>Employee Code</th>
                        <th>C.R. Number </th>
                        <th>Downlode संकलित अदेय प्रमाण पत्र </th>
                    </tr>
                    <tr>
                        <td runat="server" id="txtPayBand" Enabled="false"></td>
                        <td runat="server" id="txtPayScale" Enabled="false" ></td>
                        <td runat="server" id="txtGradePay" Enabled="false" ></td>
                        <td runat="server" id="txtEmployeeCode" Enabled="false" ></td>
                        <td runat="server" id="txtCrNo" Enabled="false" ></td>
                         <td>
                             <button class="btn btn-danger btn-sm" onclick="return downloadCompiledOrder(this);" title="Download संकलित आदेश प्रमाण पत्र" runat="server" id="aCompiledOrder">
                                 <i class="ace-icon fa fa-download icon-only"></i></button></td>
                    </tr>
                      <tr>
                        <th> Downlode पेंशन प्रपत्र </th>
                        <th>Downlode फोटो </th>
                        <th>Downlode Life Certificate</th>
                    </tr>
                    <tr>
                        <td>
                            <button class="btn btn-danger btn-sm" onclick="return downloadPensionLetter(this);" title="Download पेंशन प्रपत्र" runat="server" id="aPensionLetter">
                                  <i class="ace-icon fa fa-download icon-only"></i></button></td>
                        <td>
                            <button class="btn btn-danger btn-sm" onclick="return downloadPhoto(this);" title="Download Photo" runat="server" id="aPhoto">
                                <i class="ace-icon fa fa-download icon-only"></i></button>
                        </td>
                        <td>
                            <button class="btn btn-danger btn-sm" onclick="return downloadLifeCertificate(this);" title="Download Life Certificate" runat="server" id="aLifeCertificate">
                                <i class="ace-icon fa fa-download icon-only"></i></button></td>                       
                    </tr>
                     <tr>
                         <asp:GridView ID="grdFamilyMember" runat="server" AutoGenerateColumns="False" CssClass="table table-striped bolder table-bordered table-hover"
                                                EmptyDataText="No Records Found">
                                                <Columns>
                                                    <asp:BoundField DataField="PersonFamilyDtls_Id" HeaderText="PersonFamilyDtls Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="S No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Name of Family Member" DataField="PersonFamilyDtls_MemberName" />
                                                    <asp:BoundField HeaderText="Age" DataField="PersonFamilyDtls_Age" />
                                                    <asp:BoundField HeaderText="Relation with the allottee" DataField="PersonFamilyDtls_Relation" />
                                                    <asp:BoundField HeaderText="Date Of Birth" DataField="PersonFamilyDtls_DOB" />
                                                    <asp:BoundField HeaderText="Marital Status" DataField="PersonFamilyDtls_MaritalStatus" />
                                                    <asp:BoundField HeaderText="whether Govt. Servant or not" DataField="PersonFamilyDtls_Is_GovtServant" />
                                                </Columns>
                                            </asp:GridView>
                     </tr>
                    
                    <tr>
                        <th>Pensioner And Family Pensioner:</th>
                        <th>मृत्यु होने की दशा में नामिनी का नाम तथा पता जिसे जीवनकालीन अवशेष का भुगतान किया जाए।</th>
                        <th>Pensioner And Family Pensioner</th>
                        <th>Pension</th>
                    </tr>
                    <tr>
                        <td><asp:RadioButtonList ID="rblFamilyPensioner" runat="server" RepeatDirection="Horizontal" Enabled="false">
                            <asp:ListItem Value="Continue_Pension" Text="Continue Pension"></asp:ListItem>
                            <asp:ListItem Value="Stop_Pension" Text="Stop Pension"></asp:ListItem>
                            </asp:RadioButtonList></td>
                        <td>
                            
                        </td>
                        <td><asp:RadioButtonList ID="rblPensioner" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rblPensioner_SelectedIndexChanged" RepeatDirection="Horizontal" Enabled="false">
                            <asp:ListItem Value="YES" Text="Yes"></asp:ListItem>
                            <asp:ListItem Value="NO" Text="No"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td runat="server" id="txtPension">
                            <asp:RadioButtonList ID="rbtPensionApplicable" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rbtPensionApplicable_SelectedIndexChanged" RepeatDirection="Horizontal" Enabled="false">
                                <asp:ListItem Value="YES" Text="Yes"></asp:ListItem>
                                <asp:ListItem Value="NO" Text="No"></asp:ListItem></asp:RadioButtonList></td>
                    </tr>
                    <tr runat="server" id="divPensionYes">
                        <th>Basic Pension Amoun:</th>
                        <th>Family Pension</th>
                        <th>PPO No</th>
                        <th>PPO Date</th>
                        <th>Download PPO:</th>
                    </tr>
                    <tr>
                        <td ID="txtBasicPensionamt" runat="server" Enabled="false" ></td>
                        <td ID="txtFamilyPension" runat="server" Enabled="false" ></td>
                        <td ID="txtPensionPPONo" runat="server" Enabled="false" ></td>
                        <td ID="txtPensionPPODate" runat="server" Enabled="false" ></td>
                        <td>
                            <button class="btn btn-danger btn-sm" onclick="return downloadPPO(this);" title="Download PPO" runat="server" id="aPPO">
                                <i class="ace-icon fa fa-download icon-only"></i></button></td>
                    </tr>
                    <tr>
                        <th>Gratuity Deduction Amount:</th>
                        <th>Reason</th>
                       
                    </tr>
                    <tr>
                        <td  ID="txtGratuityDeductionAmount" runat="server" Enabled="false"></td>
                        <td  ID="txtGratuityDeductionReason" runat="server" Enabled="false"></td>
                        <td>
                            <button class="btn btn-danger btn-sm" onclick="return downloadGratuityDeductionReasonOrder(this);" title="Download Gratuity Deduction Reason Order" runat="server" id="aGratuityDeductionReasonOrder"><i class="ace-icon fa fa-download icon-only"></i></button></td>
                    </tr>
                    <tr>
                        <th> Leave Encashment</th>
                        <th> Leave Encashment Amount:</th>
                        <th> Leave Encashment No.</th>
                        <th> Leave Encashment Date </th>
                        <th> Leave Encashment Payment Order Date {Done By HQ}</th>
                        <th> Leave Encashment Payment Order No {Done By HQ}</th>
                    </tr>
                    <tr>
                        <td> <asp:RadioButtonList ID="rbtLeaveApplicable" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rbtLeaveApplicable_SelectedIndexChanged" Enabled="false">
                            <asp:ListItem Value="YES" Text="Yes" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="NO" Text="No"></asp:ListItem>
                             </asp:RadioButtonList></td>
                        <td ID="txtLeaveEncashmentAmount" runat="server" Enabled="false"></td>
                        <td ID="txtLeaveEncashmentNo" runat="server" Enabled="false"></td>
                        <td ID="txtLeaveEncashmentDate" runat="server" Enabled="false"></td>
                        <td ID="txtLeaveEncashmentOrderHQDate" runat="server" Enabled="false"></td>
                        <td ID="txtLeaveEncashmentOrderHQNo" runat="server" Enabled="false"></td>
                    </tr>
                    <tr>
                        <th>DOWNLOAD LEAVE ENCASHMENT </th>
                        <th>DOWNLOAD LEAVE ENCASHMENT ORDER (HQ) </th>
                    </tr>
                    <tr>
                        <td> <button class="btn btn-danger btn-sm" onclick="return downloadLeaveEncashment(this);" title="Download Leave Encashment" runat="server" id="aLeaveEncashment">
                            <i class="ace-icon fa fa-download icon-only"></i>
                             </button></td>
                        <td> <button class="btn btn-danger btn-sm" onclick="return downloadLeaveEncashmentOrderHQ(this);" title="Download Leave Encashment Order (HQ)" runat="server" id="aLeaveEncashmentOrderHQ">
                            <i class="ace-icon fa fa-download icon-only"></i></button></td>
                    </tr>

                    <tr>
                        <th>Leave Encashment Deduction Amount</th>
                        <th>Reason</th>
                        <th>Download Leave Encashment Deduction Reason  Order</th>
                    </tr>
                    <tr>
                        <td ID="txtLeaveEncashmentDeductionAmount" runat="server" Enabled="false" ></td>
                        <td ID="txtLeaveEncashmentDeductionReason" runat="server" Enabled="false" ></td>
                        <td><button class="btn btn-danger btn-sm" onclick="return downloadLeaveEncashmentDeductionReasonOrder(this);" title="Download Leave Encashment Deduction Reason Order" runat="server" id="aLeaveEncashmentDeductionReasonOrder"><i class="ace-icon fa fa-download icon-only"></i></button></td>
                    </tr>

                    <tr>
                        <th>GIS</th>
                        <th>LIC ID क्रमांक</th>
                        <th>दावा संख्या</th>
                        <th>दावा संख्या दिनांक</th>
                        <th>GIS बीमा प्रकार</th>
                        <th>दावा मुख्यालय को प्राप्त होने का दिनांक</th>
                        <th runat="server" id="rdtgisApplicableYes" >Reason</th>
                    </tr>
                    <tr>
                        <td><asp:RadioButtonList ID="rdtgisApplicable" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdtgisApplicable_SelectedIndexChanged" Enabled="false">
                            <asp:ListItem Value="YES" Text="Yes"></asp:ListItem>
                            <asp:ListItem Value="NO" Text="No"></asp:ListItem>
                            </asp:RadioButtonList></td>
                        <td  ID="txtLICId" runat="server" Enabled="false"></td>
                        <td  ID="txtClaimNum" runat="server" Enabled="false"></td>
                        <td  ID="txtClaimNumberDate" runat="server" Enabled="false"></td>
                        <td  ID="txtGisType" runat="server" Enabled="false"></td>
                        <td  ID="txtClaimRecieveDate" runat="server" Enabled="false"></td>
                        <td ID="txtDivisionReason" runat="server" Enabled="false"></td>
                    </tr>
                    <tr>
                        <th>दावा LIC प्रेषित किये जाने का क्रमांक</th>
                        <th>दावा LIC प्रेषित किये जाने की दिनांक </th>
                        <th>LIC से प्राप्त धनराशि</th>
                        <th>LIC से धनराशि प्राप्त होने की दिनांक </th>
                        <th>DOWNLOAD दावा अपलोड </th>
                        <th>DOWNLOAD मृतक प्रमाण पत्र </th>
                    </tr>
                    <tr>
                        <td ID="txtLICSendNumber" runat="server" Enabled="false" ></td>
                        <td ID="txtLICSendDate" runat="server" Enabled="false" ></td>
                        <td ID="txtLICRecieveAmount" runat="server" Enabled="false" ></td>
                        <td ID="txtLICRecieveDate" runat="server" Enabled="false" ></td>
                        <td>  
                            <button class="btn btn-danger btn-sm" onclick="return downloadClaimUploadPath(this);" title="Download दावा अपलोड" runat="server" id="aClaimUploadPath">
                                <i class="ace-icon fa fa-download icon-only"></i>
                            </button>
                        </td>
                        <td >
                             <button class="btn btn-danger btn-sm" onclick="return downloadDeathCeritificate(this);" title="Download मृतक प्रमाण पत्र" runat="server" id="aDeathCeritificate">
                                 <i class="ace-icon fa fa-download icon-only"></i>
                             </button>
                        </td>
                    </tr>

                    <tr>
                        <th>धनराशि</th>
                        <th>Cheque Number </th>
                        <th>Account Number of Pensionar </th>
                        <th>IFSC Code</th>
                        <th>Date</th>
                    </tr>
                    <tr>
                        <td ID="txtPensionerAmount" runat="server" Enabled="false"></td>
                        <td ID="txtPensionerChequeNumber" runat="server" Enabled="false"></td>
                        <td ID="txtPensionerAccountNumber" runat="server" Enabled="false"></td>
                        <td ID="txtPensionerIFSCCode" runat="server" Enabled="false"></td>
                        <td ID="txtPensionPaidDate" runat="server" Enabled="false"></td>
                    </tr>
                    <thead>यदि पेंशनर मृत है तो डिवीज़न को प्रेषित धनराशि का विवरण</thead>
                    <tr>
                        <th>Amount </th>
                        <th>cheque number  </th>
                        <th>date </th>
                    </tr>
                    <tr>
                        <td runat="server" Enabled="false" id="txtDivisionAmountNumber"></td>
                        <td runat="server" Enabled="false" id="txtDivisionChequeNumber"></td>
                        <td runat="server" Enabled="false" id="txtDivisionDateNumber"></td>
                    </tr>
                    <tr>
                        <asp:GridView ID="grdNatureOfArrear" runat="server" CssClass="table table-striped bolder table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="No Records Found">
                            <Columns>
                                <asp:BoundField DataField="PensionMasterArrear_Id" HeaderText="PensionMasterArrear_Id">
                                    <HeaderStyle CssClass="displayStyle" />
                                    <ItemStyle CssClass="displayStyle" />
                                    <FooterStyle CssClass="displayStyle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PensionMasterArrear_PensionMasterId" HeaderText="PensionMasterArrear_NatureArreare_Id">
                                    <HeaderStyle CssClass="displayStyle" />
                                    <ItemStyle CssClass="displayStyle" />
                                    <FooterStyle CssClass="displayStyle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PensionMasterArrear_OrderPath" HeaderText="PensionMasterArrear_OrderPath">
                                    <HeaderStyle CssClass="displayStyle" />
                                    <ItemStyle CssClass="displayStyle" />
                                    <FooterStyle CssClass="displayStyle" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="S No.">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Amount" DataField="PensionMasterArrear_ArrearAmount" />
                                <asp:BoundField HeaderText="Order Date" DataField="PensionMasterArrear_OrderDate" />
                                <asp:BoundField HeaderText="Order No" DataField="PensionMasterArrear_OrderNo" />
                            </Columns>
                        </asp:GridView>
                    </tr>

                    <thead>Details of Dearness Relief Arrear</thead>
                    <tr>
                        <th> Basic Pension Rate: </th>
                        <th> No Of Month:</th>
                    </tr>
                    <thead>Other Details</thead>
                    <tr>
                        <th>Court Case No:</th>
                        <th>Court Case Year</th>
                        <th>Contempt No</th>
                        <th>Contempt Year</th>
                        <th>Regular Field Employee:</th>
                        <th  runat="server" id="divRegularFieldEmployeeYes">Reason Of No Payment Of Pension</th>
                        <th>DOWNLOAD COURT CASE ORDER</th>
                        <th>DOWNLOAD CONTEMPT ORDER </th>
                    </tr>
                    <tr>
                        <td ID="txtCourtCaseNo" runat="server" Enabled="false" ></td>
                        <td ID="txtCourtCaseYear" runat="server" Enabled="false" ></td>
                        <td ID="txtContemptNo" runat="server" Enabled="false" ></td>
                        <td ID="txtContemptYear" runat="server" Enabled="false" ></td>
                        <td >
                            <asp:RadioButtonList ID="rbtRegularFieldEmployee" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rbtRegularFieldEmployee_SelectedIndexChanged" Enabled="false">
                                <asp:ListItem Value="YES" Text="Yes"></asp:ListItem>
                                <asp:ListItem Value="NO" Text="No"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td ID="txtNoPaymentReason" runat="server" Enabled="false" TextMode="MultiLine" ></td>
                        <td> <button class="btn btn-danger btn-sm" onclick="return downloadContemptOrder(this);" title="Download Contempt Order" runat="server" id="aContemptOrder">
                            <i class="ace-icon fa fa-download icon-only"></i>
                              </button></td>
                    </tr>
                </table>
            </div>
              <center><br /><br>
                <button onclick="window.print()" class="btn-grad">Print This Page</button></center>
        </div>--%>

            <%--Printing Page End --%>
         
            
               <div class="row">
                <div class="col-xs-12">
                    <div class="row">
                        <div class="col-xs-12">
                            <div class="row col-xs-12">
                                <table class="table-responsive table-">
                                    <h2 class="table-header">Basic Details</h2>
                                </table>
                                <div class="col-xs-12 boxcontent">
                                        <div class="row">
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                   Retirement Zone: <br />
                                                    <asp:label ID="txtZoneRetirement" runat="server"   Enabled="false" CssClass="bolder"></asp:label>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    Retirement Division : <br />
                                                    <asp:label ID="txtRetirmentDivision" runat="server" Enabled="false" CssClass="bolder"></asp:label>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                     Pension Zone : <br />
                                                    <asp:label ID="txtZonePension" runat="server"   Enabled="false" CssClass="bolder"></asp:label>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                     Pension division :<br />
                                                    <asp:label ID="txtPensionDivision" runat="server"   Enabled="false" CssClass="bolder"></asp:label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                     Name Of Pensioner: <br />
                                                    <asp:label ID="txtPensionerName" runat="server"   Enabled="false" CssClass="bolder"></asp:label>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                     Father Name Or Husband Name: <br />
                                                    <asp:label ID="txtFatherName" runat="server"   Enabled="false" CssClass="bolder"></asp:label>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                     Mobile Number: <br />
                                                    <asp:label ID="txtMobileNo" runat="server"   Enabled="false" CssClass="bolder"></asp:label>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                     Pan Card Number: <br />
                                                    <asp:label ID="txtPanCardNo" runat="server"   Enabled="false" CssClass="bolder"></asp:label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                     Aadhar Card Number: <br />
                                                    <asp:label ID="txtAadharNo" runat="server"   Enabled="false" CssClass="bolder"></asp:label>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                     Permanent Address/स्थायी निवास स्थान: <br />
                                                    <asp:label ID="txtPermanentAddress" runat="server"   TextMode="MultiLine" Enabled="false" CssClass="bolder"></asp:label>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                     Postal address/पत्र व्यवहार का पता: <br />
                                                    <asp:label ID="txtPostalAddress" runat="server"   TextMode="MultiLine" Enabled="false" CssClass="bolder"></asp:label>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                     Gender * <br />
                                                    <asp:RadioButtonList ID="rbtGender" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" Enabled="false">
                                                        <asp:ListItem Value="M" Text="&nbsp; Male &nbsp;"></asp:ListItem>
                                                        <asp:ListItem Value="F" Text="&nbsp; Female &nbsp;"></asp:ListItem>
                                                        <asp:ListItem Value="Tg" Text="&nbsp; Trans Gender"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                     Retirement Year/सेवा निवृति का वर्ष   : <br />
                                                    <asp:label ID="txtRetirementYear" runat="server"   Enabled="false" CssClass="bolder"></asp:label>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                     Date of Birth :  <br />
                                                    <asp:label ID="txtDOB" runat="server"   Enabled="false" CssClass="bolder"></asp:label>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                     Date of Joining :  <br />
                                                    <asp:label ID="txtDOJ" runat="server"   Enabled="false" CssClass="bolder"></asp:label>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                     Date Of Retirement:  <br />
                                                    <asp:label ID="txtDOR" runat="server"   Enabled="false" CssClass="bolder"></asp:label>
                                                </div>
                                            </div>
                                        </div>

                                    <div class="row">
                                        <div class="col-xs-3">
                                            <div class="form-group">
                                                Date Of Death: 
                                                <br />
                                                <asp:Label ID="txtDOD" runat="server" Enabled="false" CssClass="bolder"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-xs-3">
                                            <div class="form-group">
                                                Date Of Regularization: 
                                                <br />
                                                <asp:Label ID="txtDORegularization" runat="server" Enabled="false" CssClass="bolder"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-xs-3">
                                            <div class="form-group">
                                                Date Of Sending Pension Form By Division: 
                                                <br />
                                                <asp:Label ID="txtPensionFormSendingDate" runat="server" Enabled="false" CssClass="bolder"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-xs-3">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Download Photo :" CssClass="control-label no-padding-right"></asp:Label>
                                                <br />
                                                <button class="btn btn-danger btn-sm" onclick="return downloadPhoto(this);" title="Download Photo" runat="server" id="aPhoto">
                                                    <i class="ace-icon fa fa-download icon-only"></i>
                                                </button>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-3">
                                            <div class="form-group">
                                                Date Of Receiving Pension Form By HQ: 
                                                <br />
                                                <asp:Label ID="txtPensionFormreceivingDate" runat="server" Enabled="false" CssClass="bolder"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-xs-3">
                                            <div class="form-group">
                                                AAO Pension Name:
                                                <br />
                                                <asp:Label ID="txtAAOPensionName" runat="server" Enabled="false" CssClass="bolder"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-xs-3">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Download संकलित अदेय प्रमाण पत्र :" CssClass="control-label no-padding-right"></asp:Label>
                                                <br />
                                                <button class="btn btn-danger btn-sm" onclick="return downloadCompiledOrder(this);" title="Download संकलित आदेश प्रमाण पत्र" runat="server" id="aCompiledOrder">
                                                    <i class="ace-icon fa fa-download icon-only"></i>
                                                </button>
                                            </div>
                                        </div>
                                        <div class="col-xs-3">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Download पेंशन प्रपत्र :" CssClass="control-label no-padding-right"></asp:Label>
                                                <br />
                                                <button class="btn btn-danger btn-sm" onclick="return downloadPensionLetter(this);" title="Download पेंशन प्रपत्र" runat="server" id="aPensionLetter">
                                                    <i class="ace-icon fa fa-download icon-only"></i>
                                                </button>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-xs-3">
                                            <div class="form-group">
                                                Designation/पदनाम  : 
                                                <br />
                                                <asp:Label ID="txtDesignation" runat="server" Enabled="false" CssClass="bolder"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-xs-3">
                                            <div class="form-group">
                                                Alphabatically आवंटन के हिसाब से लेखाकार का नाम:
                                                <br />
                                                <asp:Label ID="txtAccountantName" runat="server" Enabled="false" CssClass="bolder"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-xs-3">
                                                    <div class="form-group">
                                                        Pension Code:
                                                        <br/>
                                                        <asp:Label ID="txtPensionCode" runat="server" Enabled="false" CssClass="bolder"></asp:Label>
                                                    </div>
                                                </div>
                                        <div class="col-xs-3">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Download Life Certificate :" CssClass="control-label no-padding-right"></asp:Label>
                                                <br />
                                                <button class="btn btn-danger btn-sm" onclick="return downloadLifeCertificate(this);" title="Download Life Certificate" runat="server" id="aLifeCertificate">
                                                    <i class="ace-icon fa fa-download icon-only"></i>
                                                </button>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-3">
                                            <div class="form-group">
                                                Pay Band : 
                                                <br />
                                                <asp:Label ID="txtPayBand" runat="server" Enabled="false" CssClass="bolder"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-xs-3">
                                            <div class="form-group">
                                                Pay Scale : 
                                                <br />
                                                <asp:Label ID="txtPayScale" runat="server" Enabled="false" CssClass="bolder"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-xs-3">
                                            <div class="form-group">
                                                Grade Pay : 
                                                <br />
                                                <asp:Label ID="txtGradePay" runat="server" Enabled="false" CssClass="bolder"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-xs-3">
                                            <div class="form-group">
                                                Employee Code : 
                                                <br />
                                                <asp:Label ID="txtEmployeeCode" runat="server" Enabled="false" CssClass="bolder"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-3">
                                            <div class="form-group">
                                                CR Number : 
                                                <br />
                                                <asp:Label ID="txtCrNo" runat="server" Enabled="false" CssClass="bolder"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <div class="table-header">
                                                Family Member Details 
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <asp:GridView ID="grdFamilyMember" runat="server" AutoGenerateColumns="False" CssClass="table table-striped bolder table-bordered table-hover"
                                                EmptyDataText="No Records Found">
                                                <Columns>
                                                    <asp:BoundField DataField="PersonFamilyDtls_Id" HeaderText="PersonFamilyDtls Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="S No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Name of Family Member" DataField="PersonFamilyDtls_MemberName" />
                                                    <asp:BoundField HeaderText="Age" DataField="PersonFamilyDtls_Age" />
                                                    <asp:BoundField HeaderText="Relation with the allottee" DataField="PersonFamilyDtls_Relation" />
                                                    <asp:BoundField HeaderText="Date Of Birth" DataField="PersonFamilyDtls_DOB" />
                                                    <asp:BoundField HeaderText="Marital Status" DataField="PersonFamilyDtls_MaritalStatus" />
                                                    <asp:BoundField HeaderText="whether Govt. Servant or not" DataField="PersonFamilyDtls_Is_GovtServant" />

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <div class="form-group">
                                                Pensioner And Family Pensioner: 
                                                <br />
                                                <asp:RadioButtonList ID="rblFamilyPensioner" runat="server" RepeatDirection="Horizontal" Enabled="false">
                                                    <asp:ListItem Value="Continue_Pension" Text="Continue Pension"></asp:ListItem>
                                                    <asp:ListItem Value="Stop_Pension" Text="Stop Pension"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <b>मृत्यु होने की दशा में नामिनी का नाम तथा पता जिसे जीवनकालीन अवशेष का भुगतान किया जाए।</b>
                                            <br />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-3">
                                            <div class="form-group">
                                                Nominee Name: 
                                                <br />
                                                <asp:Label ID="txtNomineeName" runat="server" Enabled="false" CssClass="bolder"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-xs-3">
                                            <div class="form-group">
                                                Nominee Address: 
                                                <br />
                                                <asp:Label ID="txtNomineeAddress" runat="server" TextMode="MultiLine" Enabled="false" CssClass="bolder"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <div class="form-group">
                                                Pensioner And Family Pensioner: 
                                                <br />
                                                <asp:RadioButtonList ID="rblPensioner" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rblPensioner_SelectedIndexChanged" RepeatDirection="Horizontal" Enabled="false">
                                                    <asp:ListItem Value="YES" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="NO" Text="No"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>
                                    <div runat="server" id="rblPensionerYes">
                                        <div class="row">
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    Family Pensioner Name: 
                                                    <br />
                                                    <asp:Label ID="txtFamilyPensionerName" runat="server" Enabled="false" CssClass="bolder"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    Relation: 
                                                    <br />
                                                    <asp:Label ID="txtPensionerFamilyRelation" runat="server" Enabled="false" CssClass="bolder">
                                                    </asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    Date Of Birth:
                                                    <br />
                                                    <asp:Label ID="txtFamilyDOB" runat="server" Enabled="false" CssClass="bolder"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    Mobile Number: 
                                                    <br />
                                                    <asp:Label ID="txtFamilyMobileNumber" runat="server" Enabled="false" CssClass="bolder"></asp:Label>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    Pan Card Number: 
                                                    <br />
                                                    <asp:Label ID="txtFamilyPanNumber" runat="server" Enabled="false" CssClass="bolder"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    Aadhar Card Number: 
                                                    <br />
                                                    <asp:Label ID="txtFamilyAadharNumber" runat="server" Enabled="false" CssClass="bolder"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>




                                </div>
                            </div>

                            <div class="row  ">
                                <div class="col-xs-12">
                                    <div class="table-header">
                                        Pension Details
                                    </div>
                                </div>
                                <div class="col-xs-12 boxcontent">

                                    <div class="row">
                                        <div class="col-xs-12">
                                            <div class="form-group">
                                                Pension:  
                                                   <asp:label ID="txtPension" runat="server"  ></asp:label>
                                                <asp:RadioButtonList ID="rbtPensionApplicable" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rbtPensionApplicable_SelectedIndexChanged" RepeatDirection="Horizontal" Enabled="false">
                                                    <asp:ListItem Value="YES" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="NO" Text="No"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>




                                    <div runat="server" id="divPensionYes">
                                        <div class="row">
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    Basic Pension Amount: 
                                                    <br />
                                                    <asp:Label ID="txtBasicPensionamt" runat="server" Enabled="false" CssClass="bolder"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    Family Pension: 
                                                    <br />
                                                    <asp:Label ID="txtFamilyPension" runat="server" Enabled="false" CssClass="bolder"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    PPO No:
                                                    <br />
                                                    <asp:Label ID="txtPensionPPONo" runat="server" Enabled="false" CssClass="bolder"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    PPO Date: 
                                                    <br />
                                                    <asp:Label ID="txtPensionPPODate" runat="server" Enabled="false" CssClass="bolder"></asp:Label>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <asp:Label runat="server" Text="Download PPO :" CssClass="control-label no-padding-right"></asp:Label>
                                                    <br />
                                                    <button class="btn btn-danger btn-sm" onclick="return downloadPPO(this);" title="Download PPO" runat="server" id="aPPO">
                                                        <i class="ace-icon fa fa-download icon-only"></i>
                                                    </button>
                                                </div>
                                            </div>
                                        </div>

                                    </div>


                                    <div runat="server" id="divPensionNo">
                                        <div class="row">
                                            <div class="col-xs-6">
                                                <div class="form-group">
                                                    Reason: 
                                                    <br />
                                                    <asp:Label ID="txtPensionReason" runat="server" Enabled="false" CssClass="bolder"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-xs-6">
                                                <div class="form-group">
                                                    Comments: 
                                                    <br />
                                                    <asp:Label ID="txtPensionNoComments" runat="server" TextMode="MultiLine" Enabled="false" CssClass="bolder"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row  ">
                                <div class="col-xs-12">
                                    <div class="table-header">
                                        Gratuity Details
                                    </div>
                                </div>
                                <div class="col-xs-12 boxcontent">

                                    <div class="row">
                                        <div class="col-xs-12">
                                            <div class="form-group">
                                                Gratuity: 
                                                <br />
                                                <asp:RadioButtonList ID="rbtGratuityApplicable" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rbtGratuityApplicable_SelectedIndexChanged" Enabled="false">
                                                    <asp:ListItem Value="YES" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="NO" Text="No"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>
                                    <div runat="server" id="divGratuityYes">
                                        <div class="row">
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    GPO Amount: 
                                                    <br />
                                                    <asp:Label ID="txtGratuityAmount" runat="server" Enabled="false" CssClass="bolder"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    Gratuity No: 
                                                    <br />
                                                    <asp:Label ID="txtGratuityNo" runat="server" Enabled="false" CssClass="bolder"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    Gratuity Date: 
                                                    <br />
                                                    <asp:Label ID="txtGratuityDate" runat="server" Enabled="false" CssClass="bolder"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <asp:Label runat="server" Text="Download GPO :" CssClass="control-label no-padding-right"></asp:Label>
                                                    <br />
                                                    <button class="btn btn-danger btn-sm" onclick="return downloadGPO(this);" title="Download GPO" runat="server" id="aGPO">
                                                        <i class="ace-icon fa fa-download icon-only"></i>
                                                    </button>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    Gratuity Payment Order Date (Done By HQ):  
                                                        <asp:Label ID="txtGratuityPOHQDate" runat="server" Enabled="false" CssClass="bolder"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    Gratuity Payment Order No (Done By HQ):  
                                                        <asp:Label ID="txtGratuityPOHQNo" runat="server" Enabled="false" CssClass="bolder"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <asp:Label runat="server" Text="Download Order (HQ) :" CssClass="control-label no-padding-right"></asp:Label>
                                                    <br />
                                                    <button class="btn btn-danger btn-sm" onclick="return downloadOrderHQ(this);" title="Download Order (HQ)" runat="server" id="aOrderHQ">
                                                        <i class="ace-icon fa fa-download icon-only"></i>
                                                    </button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div runat="server" id="divGratuityNo">
                                        <div class="row">
                                            <div class="col-xs-6">
                                                <div class="form-group">
                                                    Reason: 
                                                    <br />
                                                    <asp:Label ID="txtGratuityReason" runat="server" TextMode="MultiLine" Enabled="false" CssClass="bolder"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <div class="table-header">
                                                Details of Any Deduction From Gratuity
                                            </div>
                                        </div>
                                        <div class="col-xs-12 boxcontent">

                                            <div class="row">
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        Gratuity Deduction Amount: 
                                                        <br />
                                                        <asp:Label ID="txtGratuityDeductionAmount" runat="server" Enabled="false" CssClass="bolder"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        Reason: 
                                                        <br />
                                                        <asp:Label ID="txtGratuityDeductionReason" runat="server" TextMode="MultiLine" Enabled="false" CssClass="bolder"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <asp:Label runat="server" Text="Download Gratuity Deduction Reason Order :" CssClass="control-label no-padding-right"></asp:Label>
                                                        <br />
                                                        <button class="btn btn-danger btn-sm" onclick="return downloadGratuityDeductionReasonOrder(this);" title="Download Gratuity Deduction Reason Order" runat="server" id="aGratuityDeductionReasonOrder">
                                                            <i class="ace-icon fa fa-download icon-only"></i>
                                                        </button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row  ">
                                <div class="col-xs-12">
                                    <div class="table-header">
                                        Leave Encashment Details
                                    </div>
                                </div>
                                <div class="col-xs-12 boxcontent">
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <div class="form-group">
                                                Leave Encashment: 
                                                <br />
                                                <asp:RadioButtonList ID="rbtLeaveApplicable" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rbtLeaveApplicable_SelectedIndexChanged" Enabled="false">
                                                    <asp:ListItem Value="YES" Text="Yes" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="NO" Text="No"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>
                                    <div runat="server" id="divLeaveEncashmentYes">
                                        <div class="row">
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    Leave Encashment Amount: 
                                                    <br />
                                                    <asp:Label ID="txtLeaveEncashmentAmount" runat="server" Enabled="false" CssClass="bolder"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    Leave Encashment No: 
                                                    <br />
                                                    <asp:Label ID="txtLeaveEncashmentNo" runat="server" Enabled="false" CssClass="bolder"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    Leave Encashment Date: 
                                                    <br />
                                                    <asp:Label ID="txtLeaveEncashmentDate" runat="server" Enabled="false" CssClass="bolder"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <asp:Label runat="server" Text="Download Leave Encashment :" CssClass="control-label no-padding-right"></asp:Label>
                                                    <br />
                                                    <button class="btn btn-danger btn-sm" onclick="return downloadLeaveEncashment(this);" title="Download Leave Encashment" runat="server" id="aLeaveEncashment">
                                                        <i class="ace-icon fa fa-download icon-only"></i>
                                                    </button>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    Leave Encashment Payment Order Date (Done By HQ): 
                                                    <br />
                                                    <asp:Label ID="txtLeaveEncashmentOrderHQDate" runat="server" Enabled="false" CssClass="bolder"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    Leave Encashment Payment Order No (Done By HQ): 
                                                    <br />
                                                    <asp:Label ID="txtLeaveEncashmentOrderHQNo" runat="server" Enabled="false" CssClass="bolder"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <asp:Label runat="server" Text="Download Leave Encashment Order (HQ) :" CssClass="control-label no-padding-right"></asp:Label>
                                                    <br />
                                                    <button class="btn btn-danger btn-sm" onclick="return downloadLeaveEncashmentOrderHQ(this);" title="Download Leave Encashment Order (HQ)" runat="server" id="aLeaveEncashmentOrderHQ">
                                                        <i class="ace-icon fa fa-download icon-only"></i>
                                                    </button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div runat="server" id="divLeaveEncashmentNo">
                                        <div class="row">
                                            <div class="col-xs-6">
                                                <div class="form-group">
                                                    Reason: 
                                                    <br />
                                                    <asp:Label ID="txtLeaveEncashmentReason" runat="server" TextMode="MultiLine" Enabled="false" CssClass="bolder"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <div class="table-header">
                                                Details of Any Deduction From Leave Encashment                                                   
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row boxcontent">
                                        <div class="col-xs-3">
                                            <div class="form-group">
                                                Leave Encashment Deduction Amount: 
                                                <br />
                                                <asp:Label ID="txtLeaveEncashmentDeductionAmount" runat="server" Enabled="false" CssClass="bolder"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-xs-3">
                                            <div class="form-group">
                                                Reason: 
                                                <br />
                                                <asp:Label ID="txtLeaveEncashmentDeductionReason" runat="server" TextMode="MultiLine" Enabled="false" CssClass="bolder"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-xs-3">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Download Leave Encashment Deduction Reason Order :" CssClass="control-label no-padding-right"></asp:Label>
                                                <br />
                                                <button class="btn btn-danger btn-sm" onclick="return downloadLeaveEncashmentDeductionReasonOrder(this);" title="Download Leave Encashment Deduction Reason Order" runat="server" id="aLeaveEncashmentDeductionReasonOrder">
                                                    <i class="ace-icon fa fa-download icon-only"></i>
                                                </button>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>

                            <div class="row  ">
                                <div class="col-xs-12">
                                    <div class="table-header">
                                        Group Insurance Scheme (GIS) Details
                                    </div>
                                </div>
                                <div class="col-xs-12 boxcontent">
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <div class="form-group">
                                                GIS: 
                                                <br />
                                                <asp:RadioButtonList ID="rdtgisApplicable" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdtgisApplicable_SelectedIndexChanged" Enabled="false">
                                                    <asp:ListItem Value="YES" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="NO" Text="No"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>
                                    <div runat="server" id="rdtgisApplicableYes" style="margin-left: 13px;">
                                        <div class="row">
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    Reason: 
                                                    <br />
                                                    <asp:Label ID="txtDivisionReason" runat="server" Enabled="false" CssClass="bolder"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-3">
                                            <div class="form-group">
                                                LIC ID क्रमांक 
                                                <br />
                                                <asp:Label ID="txtLICId" runat="server" Enabled="false" CssClass="bolder"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-xs-3">
                                            <div class="form-group">
                                                दावा संख्या 
                                                <br />
                                                <asp:Label ID="txtClaimNum" runat="server" Enabled="false" CssClass="bolder"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-xs-3">
                                            <div class="form-group">
                                                दावा संख्या दिनांक 
                                                <br />
                                                <asp:Label ID="txtClaimNumberDate" runat="server" Enabled="false" CssClass="bolder"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-xs-3">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Download दावा अपलोड :" CssClass="control-label no-padding-right"></asp:Label>
                                                <br />
                                                <button class="btn btn-danger btn-sm" onclick="return downloadClaimUploadPath(this);" title="Download दावा अपलोड" runat="server" id="aClaimUploadPath">
                                                    <i class="ace-icon fa fa-download icon-only"></i>
                                                </button>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-3">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Download मृतक प्रमाण पत्र :" CssClass="control-label no-padding-right"></asp:Label>
                                                <br />
                                                <button class="btn btn-danger btn-sm" onclick="return downloadDeathCeritificate(this);" title="Download मृतक प्रमाण पत्र" runat="server" id="aDeathCeritificate">
                                                    <i class="ace-icon fa fa-download icon-only"></i>
                                                </button>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-3">
                                            <div class="form-group">
                                                GIS  बीमा प्रकार: 
                                                <br />
                                                <asp:Label ID="txtGisType" runat="server" Enabled="false" CssClass="bolder"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-xs-3">
                                            <div class="form-group">
                                                दावा मुख्यालय को प्राप्त होने का दिनांक 
                                                <br />
                                                <asp:Label ID="txtClaimRecieveDate" runat="server" Enabled="false" CssClass="bolder"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="col-xs-3">
                                            <div class="form-group">
                                                दावा LIC प्रेषित किये जाने का क्रमांक  
                                                <br />
                                                <asp:Label ID="txtLICSendNumber" runat="server" Enabled="false" CssClass="bolder"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-xs-3">
                                            <div class="form-group">
                                                दावा LIC प्रेषित किये जाने की दिनांक :  
                                                <br />
                                                <asp:Label ID="txtLICSendDate" runat="server" Enabled="false" CssClass="bolder"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-3">
                                            <div class="form-group">
                                                LIC से प्राप्त धनराशि  
                                                <br />
                                                <asp:Label ID="txtLICRecieveAmount" runat="server" Enabled="false" CssClass="bolder"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-xs-3">
                                            <div class="form-group">
                                                LIC से धनराशि प्राप्त होने की दिनांक : 
                                                <br />
                                                <asp:Label ID="txtLICRecieveDate" runat="server" Enabled="false" CssClass="bolder"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <div class="table-header">
                                                पेंशनर को भुगतान का विवरण                                                   
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row boxcontent">
                                        <div class="col-xs-3">
                                            <div class="form-group">
                                                धनराशि  : 
                                                <br />
                                                <asp:Label ID="txtPensionerAmount" runat="server" Enabled="false" CssClass="bolder"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-xs-3">
                                            <div class="form-group">
                                                Cheque Number : 
                                                <br />
                                                <asp:Label ID="txtPensionerChequeNumber" runat="server" Enabled="false" CssClass="bolder"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-xs-3">
                                            <div class="form-group">
                                                Account Number of Pensionar : 
                                                <br />
                                                <asp:Label ID="txtPensionerAccountNumber" runat="server" Enabled="false" CssClass="bolder"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-xs-3">
                                            <div class="form-group">
                                                IFSC Code : 
                                                <br />
                                                <asp:Label ID="txtPensionerIFSCCode" runat="server" Enabled="false" CssClass="bolder"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-3">
                                            <div class="form-group">
                                                Date: 
                                                <br />
                                                <asp:Label ID="txtPensionPaidDate" runat="server" Enabled="false" CssClass="bolder"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <div class="table-header">
                                                यदि पेंशनर मृत है तो डिवीज़न को प्रेषित धनराशि का विवरण 
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row boxcontent">
                                        <div class="col-xs-3">
                                            <div class="form-group">
                                                Amount  : 
                                                <br />
                                                <asp:Label ID="txtDivisionAmountNumber" runat="server" Enabled="false" CssClass="bolder"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-xs-3">
                                            <div class="form-group">
                                                cheque number  : 
                                                <br />
                                                <asp:Label ID="txtDivisionChequeNumber" runat="server" Enabled="false" CssClass="bolder"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-xs-3">
                                            <div class="form-group">
                                                date  : 
                                                <br />
                                                <asp:Label ID="txtDivisionDateNumber" runat="server" Enabled="false" CssClass="bolder"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row  ">
                                <div class="col-xs-12">
                                    <div class="table-header">
                                        Nature Of Arrear
                                    </div>
                                </div>

                                <div class="col-xs-12 boxcontent">
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <asp:GridView ID="grdNatureOfArrear" runat="server" CssClass="table table-striped bolder table-bordered table-hover"
                                                AutoGenerateColumns="False" EmptyDataText="No Records Found">
                                                <Columns>
                                                    <asp:BoundField DataField="PensionMasterArrear_Id" HeaderText="PensionMasterArrear_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PensionMasterArrear_PensionMasterId" HeaderText="PensionMasterArrear_NatureArreare_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PensionMasterArrear_OrderPath" HeaderText="PensionMasterArrear_OrderPath">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="S No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Amount" DataField="PensionMasterArrear_ArrearAmount" />
                                                    <asp:BoundField HeaderText="Order Date" DataField="PensionMasterArrear_OrderDate" />
                                                    <asp:BoundField HeaderText="Order No" DataField="PensionMasterArrear_OrderNo" />

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-xs-12">
                                            <div class="table-header">
                                                Details of Dearness Relief Arrear
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row boxcontent">
                                        <div class="col-xs-3">
                                            <div class="form-group">
                                                Basic Pension Rate: 
                                                <br />
                                                <asp:Label ID="txtBasicPensionRate" runat="server" Enabled="false" CssClass="bolder"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-xs-3">
                                            <div class="form-group">
                                                No Of Month: 
                                                <br />
                                                <asp:Label ID="txtNoOfMonthDA" runat="server" Enabled="false" CssClass="bolder"></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>

                            <div class="row  ">
                                <div class="col-xs-12">
                                    <div class="table-header">
                                        Other Details
                                    </div>
                                </div>
                                <div class="col-xs-12 boxcontent">

                                    <div class="row">
                                        <div class="col-xs-3">
                                            <div class="form-group">
                                                Court Case No: 
                                                <br />
                                                <asp:Label ID="txtCourtCaseNo" runat="server" Enabled="false" CssClass="bolder"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-xs-3">
                                            <div class="form-group">
                                                Court Case Year: 
                                                <br />
                                                <asp:Label ID="txtCourtCaseYear" runat="server" Enabled="false" CssClass="bolder"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-xs-3">
                                            <div class="form-group">
                                                Contempt No: 
                                                <br />
                                                <asp:Label ID="txtContemptNo" runat="server" Enabled="false" CssClass="bolder"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-xs-3">
                                            <div class="form-group">
                                                Contempt Year: 
                                                <br />
                                                <asp:Label ID="txtContemptYear" runat="server" Enabled="false" CssClass="bolder"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-3">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Download Court Case Order :" CssClass="control-label no-padding-right"></asp:Label>
                                                <br />
                                                <button class="btn btn-danger btn-sm" onclick="return downloadCourtCaseOrder(this);" title="Download Court Case Order" runat="server" id="aCourtCaseOrder">
                                                    <i class="ace-icon fa fa-download icon-only"></i>
                                                </button>
                                            </div>
                                        </div>
                                        <div class="col-xs-3">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Download Contempt Order :" CssClass="control-label no-padding-right"></asp:Label>
                                                <br />
                                                <button class="btn btn-danger btn-sm" onclick="return downloadContemptOrder(this);" title="Download Contempt Order" runat="server" id="aContemptOrder">
                                                    <i class="ace-icon fa fa-download icon-only"></i>
                                                </button>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <div class="form-group">
                                                Regular Field Employee: 
                                                <br />
                                                <asp:RadioButtonList ID="rbtRegularFieldEmployee" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rbtRegularFieldEmployee_SelectedIndexChanged" Enabled="false">
                                                    <asp:ListItem Value="YES" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="NO" Text="No"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>
                                    <div runat="server" id="divRegularFieldEmployeeYes">
                                        <div class="row">
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    Reason Of No Payment Of Pension: 
                                                    <br />
                                                    <asp:Label ID="txtNoPaymentReason" runat="server" TextMode="MultiLine" Enabled="false"></asp:Label>
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
        </div>

        <asp:HiddenField ID="hf_LiveCertificate" runat="server" Value="0" />
        <asp:HiddenField ID="hf_CompiledOrder" runat="server" Value="0" />
        <asp:HiddenField ID="hf_PensionOrder" runat="server" Value="0" />
        <asp:HiddenField ID="hf_PPO" runat="server" Value="0" />
        <asp:HiddenField ID="hf_Gratuity" runat="server" Value="0" />
        <asp:HiddenField ID="hf_GratuityPO" runat="server" Value="0" />
        <asp:HiddenField ID="hf_ReasonOrder" runat="server" Value="0" />
        <asp:HiddenField ID="hf_LeaveEncashmentDoc" runat="server" Value="0" />
        <asp:HiddenField ID="hf_LeaveEncashmentOrderHQ" runat="server" Value="0" />
        <asp:HiddenField ID="hf_LeaveEncashmentDeductionReason" runat="server" Value="0" />
        <asp:HiddenField ID="hf_ClaimUploadPath" runat="server" Value="0" />
        <asp:HiddenField ID="hf_DeathCeritificate" runat="server" Value="0" />
        <asp:HiddenField ID="hf_CourtCaseOrder" runat="server" Value="0" />
        <asp:HiddenField ID="hf_ContemptOrder" runat="server" Value="0" />
        <asp:HiddenField ID="hf_Photo" runat="server" Value="0" />

        <asp:HiddenField ID="hf_PensionMaster_Id" runat="server" Value="0" />
    </form>




    <script>
        function downloadLifeCertificate(obj) {
            var path = document.getElementById('hf_LiveCertificate').value;
            if (path.trim() == "") {
                obj.href = '#';
                alert('File Not Found');
                return false;
            }
            else {
                window.open(location.origin + path, "_blank", "", false);
                return false;
            }
        }
        function downloadCompiledOrder(obj) {
            var path = document.getElementById('hf_CompiledOrder').value;
            if (path.trim() == "") {
                obj.href = '#';
                alert('File Not Found');
                return false;
            }
            else {
                window.open(location.origin + path, "_blank", "", false);
                return false;
            }
        }

        function downloadPensionLetter(obj) {
            var path = document.getElementById('hf_PensionOrder').value;
            if (path.trim() == "") {
                obj.href = '#';
                alert('File Not Found');
                return false;
            }
            else {
                window.open(location.origin + path, "_blank", "", false);
                return false;
            }
        }

        function downloadPPO(obj) {
            var path = document.getElementById('hf_PPO').value;
            if (path.trim() == "") {
                obj.href = '#';
                alert('File Not Found');
                return false;
            }
            else {
                window.open(location.origin + path, "_blank", "", false);
                return false;
            }
        }

        function downloadGPO(obj) {
            var path = document.getElementById('hf_Gratuity').value;
            if (path.trim() == "") {
                obj.href = '#';
                alert('File Not Found');
                return false;
            }
            else {
                window.open(location.origin + path, "_blank", "", false);
                return false;
            }
        }

        function downloadOrderHQ(obj) {
            var path = document.getElementById('hf_GratuityPO').value;
            if (path.trim() == "") {
                obj.href = '#';
                alert('File Not Found');
                return false;
            }
            else {
                window.open(location.origin + path, "_blank", "", false);
                return false;
            }
        }

        function downloadGratuityDeductionReasonOrder(obj) {
            var path = document.getElementById('hf_ReasonOrder').value;
            if (path.trim() == "") {
                obj.href = '#';
                alert('File Not Found');
                return false;
            }
            else {
                window.open(location.origin + path, "_blank", "", false);
                return false;
            }
        }

        function downloadLeaveEncashment(obj) {
            var path = document.getElementById('hf_LeaveEncashmentDoc').value;
            if (path.trim() == "") {
                obj.href = '#';
                alert('File Not Found');
                return false;
            }
            else {
                window.open(location.origin + path, "_blank", "", false);
                return false;
            }
        }

        function downloadLeaveEncashmentOrderHQ(obj) {
            var path = document.getElementById('hf_LeaveEncashmentOrderHQ').value;
            if (path.trim() == "") {
                obj.href = '#';
                alert('File Not Found');
                return false;
            }
            else {
                window.open(location.origin + path, "_blank", "", false);
                return false;
            }
        }

        function downloadLeaveEncashmentDeductionReasonOrder(obj) {
            var path = document.getElementById('hf_LeaveEncashmentDeductionReason').value;
            if (path.trim() == "") {
                obj.href = '#';
                alert('File Not Found');
                return false;
            }
            else {
                window.open(location.origin + path, "_blank", "", false);
                return false;
            }
        }

        function downloadClaimUploadPath(obj) {
            var path = document.getElementById('hf_ClaimUploadPath').value;
            if (path.trim() == "") {
                obj.href = '#';
                alert('File Not Found');
                return false;
            }
            else {
                window.open(location.origin + path, "_blank", "", false);
                return false;
            }
        }

        function downloadDeathCeritificate(obj) {
            var path = document.getElementById('hf_DeathCeritificate').value;
            if (path.trim() == "") {
                obj.href = '#';
                alert('File Not Found');
                return false;
            }
            else {
                window.open(location.origin + path, "_blank", "", false);
                return false;
            }
        }

        function downloadCourtCaseOrder(obj) {
            var path = document.getElementById('hf_CourtCaseOrder').value;
            if (path.trim() == "") {
                obj.href = '#';
                alert('File Not Found');
                return false;
            }
            else {
                window.open(location.origin + path, "_blank", "", false);
                return false;
            }
        }

        function downloadContemptOrder(obj) {
            var path = document.getElementById('hf_ContemptOrder').value;
            if (path.trim() == "") {
                obj.href = '#';
                alert('File Not Found');
                return false;
            }
            else {
                window.open(location.origin + path, "_blank", "", false);
                return false;
            }
        }
        function downloadPhoto(obj) {
            var path = document.getElementById('hf_Photo').value;
            if (path.trim() == "") {
                obj.href = '#';
                alert('File Not Found');
                return false;
            }
            else {
                window.open(location.origin + path, "_blank", "", false);
                return false;
            }
        }

    </script>
</body>
</html>
    