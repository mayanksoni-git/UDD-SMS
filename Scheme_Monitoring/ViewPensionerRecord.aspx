<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin2.master" AutoEventWireup="true" CodeFile="ViewPensionerRecord.aspx.cs" Inherits="ViewPensionerRecord" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="main-content">
        <div class="main-content-inner">
            <style>
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

                .boxborder {
                    border: 1px solid lightgrey;
                    margin-top: 10px;
                    transition: 0.2s;
                }

                    .boxborder:hover {
                        transform: scale(1.01);
                        /*background: #eeff;*/
                        background: rgb(50 202 207 / 0.2);
                    }
            </style>
            <div class="page-content">
                <div class="page-header">
                    <div class="col-md-12">
                        <div class="col-md-12">
                            <h1>View Pensioner Details
                            </h1>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-12">
                        <div class="row">
                            <div class="col-xs-12">

                                <div class="row boxborder">
                                    <div class="col-xs-12">
                                        <div class="table-header">
                                            Basic Details
                                        </div>
                                    </div>
                                    <div class="col-xs-12 boxcontent">

                                        <div class="row">
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Retirement Zone:</label>
                                                    <asp:TextBox ID="txtZoneRetirement" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Retirement Division</label>
                                                    <asp:TextBox ID="txtRetirmentDivision" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Pension Zone:</label>
                                                    <asp:TextBox ID="txtZonePension" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Pension division</label>
                                                    <asp:TextBox ID="txtPensionDivision" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Name Of Pensioner:</label>
                                                    <asp:TextBox ID="txtPensionerName" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Father Name Or Husband Name:</label>
                                                    <asp:TextBox ID="txtFatherName" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Mobile Number:</label>
                                                    <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Pan Card Number:</label>
                                                    <asp:TextBox ID="txtPanCardNo" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Aadhar Card Number:</label>
                                                    <asp:TextBox ID="txtAadharNo" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Permanent Address/स्थायी निवास स्थान:</label>
                                                    <asp:TextBox ID="txtPermanentAddress" runat="server" CssClass="form-control" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Postal address/पत्र व्यवहार का पता:</label>
                                                    <asp:TextBox ID="txtPostalAddress" runat="server" CssClass="form-control" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Gender *</label><br />
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
                                                    <label class="control-label no-padding-right">Retirement Year/सेवा निवृति का वर्ष   :</label>
                                                    <asp:TextBox ID="txtRetirementYear" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Date of Birth : </label>
                                                    <asp:TextBox ID="txtDOB" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Date of Joining : </label>
                                                    <asp:TextBox ID="txtDOJ" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Date Of Retirement: </label>
                                                    <asp:TextBox ID="txtDOR" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Date Of Death: </label>
                                                    <asp:TextBox ID="txtDOD" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Date Of Regularization: </label>
                                                    <asp:TextBox ID="txtDORegularization" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Date Of Sending Pension Form By Division: </label>
                                                    <asp:TextBox ID="txtPensionFormSendingDate" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
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
                                                    <label class="control-label no-padding-right">Date Of Receiving Pension Form By HQ: </label>
                                                    <asp:TextBox ID="txtPensionFormreceivingDate" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">AAO Pension Name:</label>
                                                    <asp:TextBox ID="txtAAOPensionName" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
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
                                                    <label class="control-label no-padding-right">Alphabatically आवंटन के हिसाब से लेखाकार का नाम:</label>
                                                    <asp:TextBox ID="txtAccountantName" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Order Number: </label>
                                                    <asp:TextBox ID="txtFamilyOrderNumber" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Order Number Date: </label>
                                                    <asp:TextBox ID="txtFamilyOrderNumberDate" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
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
                                                    <label class="control-label no-padding-right">Designation/पदनाम  : </label>
                                                    <asp:TextBox ID="txtDesignation" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Pay Band : </label>
                                                    <asp:TextBox ID="txtPayBand" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Pay Scale : </label>
                                                    <asp:TextBox ID="txtPayScale" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Employee Code : </label>
                                                    <asp:TextBox ID="txtEmployeeCode" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">CR Number : </label>
                                                    <asp:TextBox ID="txtCrNo" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
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
                                                <asp:GridView ID="grdFamilyMember" runat="server" AutoGenerateColumns="False" CssClass="display table table-bordered"
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
                                                    <label class="control-label no-padding-right">Pensioner And Family Pensioner: </label>
                                                    <asp:RadioButtonList ID="rblFamilyPensioner" runat="server" RepeatDirection="Horizontal" Enabled="false">
                                                        <asp:ListItem Value="Continue_Pension" Text="Continue Pension"></asp:ListItem>
                                                        <asp:ListItem Value="Stop_Pension" Text="Stop Pension"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-xs-12">
                                                <label class="control-label no-padding-right"><b>मृत्यु होने की दशा में नामिनी का नाम तथा पता जिसे जीवनकालीन अवशेष का भुगतान किया जाए।</b></label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Nominee Name: </label>
                                                    <asp:TextBox ID="txtNomineeName" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Nominee Address: </label>
                                                    <asp:TextBox ID="txtNomineeAddress" runat="server" CssClass="form-control" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-xs-12">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Pensioner And Family Pensioner: </label>
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
                                                        <label class="control-label no-padding-right">Family Pensioner Name: </label>
                                                        <asp:TextBox ID="txtFamilyPensionerName" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Relation: </label>
                                                        <asp:TextBox ID="txtPensionerFamilyRelation" runat="server" CssClass="form-control" Enabled="false">
                                                        </asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Date Of Birth:</label>
                                                        <asp:TextBox ID="txtFamilyDOB" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Mobile Number: </label>
                                                        <asp:TextBox ID="txtFamilyMobileNumber" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Pan Card Number: </label>
                                                        <asp:TextBox ID="txtFamilyPanNumber" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Aadhar Card Number: </label>
                                                        <asp:TextBox ID="txtFamilyAadharNumber" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>




                                    </div>
                                </div>

                                <div class="row boxborder">
                                    <div class="col-xs-12">
                                        <div class="table-header">
                                            Pension Details
                                        </div>
                                    </div>
                                    <div class="col-xs-12 boxcontent">

                                        <div class="row">
                                            <div class="col-xs-12">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Pension: </label>
                                                    <%--<asp:TextBox ID="txtPension" runat="server" CssClass="form-control"></asp:TextBox>--%>
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
                                                        <label class="control-label no-padding-right">Basic Pension Amount: </label>
                                                        <asp:TextBox ID="txtBasicPensionamt" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Family Pension: </label>
                                                        <asp:TextBox ID="txtFamilyPension" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">PPO No:</label>
                                                        <asp:TextBox ID="txtPensionPPONo" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">PPO Date: </label>
                                                        <asp:TextBox ID="txtPensionPPODate" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
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
                                                        <label class="control-label no-padding-right">Reason: </label>
                                                        <asp:TextBox ID="txtPensionReason" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xs-6">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Comments: </label>
                                                        <asp:TextBox ID="txtPensionNoComments" runat="server" CssClass="form-control" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row boxborder">
                                    <div class="col-xs-12">
                                        <div class="table-header">
                                            Gratuity Details
                                        </div>
                                    </div>
                                    <div class="col-xs-12 boxcontent">

                                        <div class="row">
                                            <div class="col-xs-12">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Gratuity: </label>
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
                                                        <label class="control-label no-padding-right">GPO Amount: </label>
                                                        <asp:TextBox ID="txtGratuityAmount" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Gratuity No: </label>
                                                        <asp:TextBox ID="txtGratuityNo" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Gratuity Date: </label>
                                                        <asp:TextBox ID="txtGratuityDate" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
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
                                                        <label class="control-label no-padding-right">Gratuity Payment Order Date (Done By HQ): </label>
                                                        <asp:TextBox ID="txtGratuityPOHQDate" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Gratuity Payment Order No (Done By HQ): </label>
                                                        <asp:TextBox ID="txtGratuityPOHQNo" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
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
                                                        <label class="control-label no-padding-right">Reason: </label>
                                                        <asp:TextBox ID="txtGratuityReason" runat="server" CssClass="form-control" TextMode="MultiLine" Enabled="false"></asp:TextBox>
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
                                                            <label class="control-label no-padding-right">Gratuity Deduction Amount: </label>
                                                            <asp:TextBox ID="txtGratuityDeductionAmount" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-xs-3">
                                                        <div class="form-group">
                                                            <label class="control-label no-padding-right">Reason: </label>
                                                            <asp:TextBox ID="txtGratuityDeductionReason" runat="server" CssClass="form-control" TextMode="MultiLine" Enabled="false"></asp:TextBox>
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

                                <div class="row boxborder">
                                    <div class="col-xs-12">
                                        <div class="table-header">
                                            Leave Encashment Details
                                        </div>
                                    </div>
                                    <div class="col-xs-12 boxcontent">
                                        <div class="row">
                                            <div class="col-xs-12">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Leave Encashment: </label>
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
                                                        <label class="control-label no-padding-right">Leave Encashment Amount: </label>
                                                        <asp:TextBox ID="txtLeaveEncashmentAmount" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Leave Encashment No: </label>
                                                        <asp:TextBox ID="txtLeaveEncashmentNo" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Leave Encashment Date: </label>
                                                        <asp:TextBox ID="txtLeaveEncashmentDate" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
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
                                                        <label class="control-label no-padding-right">Leave Encashment Payment Order Date (Done By HQ): </label>
                                                        <asp:TextBox ID="txtLeaveEncashmentOrderHQDate" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xs-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Leave Encashment Payment Order No (Done By HQ): </label>
                                                        <asp:TextBox ID="txtLeaveEncashmentOrderHQNo" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
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
                                                        <label class="control-label no-padding-right">Reason: </label>
                                                        <asp:TextBox ID="txtLeaveEncashmentReason" runat="server" CssClass="form-control" TextMode="MultiLine" Enabled="false"></asp:TextBox>
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
                                                    <label class="control-label no-padding-right">Leave Encashment Deduction Amount: </label>
                                                    <asp:TextBox ID="txtLeaveEncashmentDeductionAmount" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Reason: </label>
                                                    <asp:TextBox ID="txtLeaveEncashmentDeductionReason" runat="server" CssClass="form-control" TextMode="MultiLine" Enabled="false"></asp:TextBox>
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

                                <div class="row boxborder">
                                    <div class="col-xs-12">
                                        <div class="table-header">
                                            Group Insurance Scheme (GIS) Details
                                        </div>
                                    </div>
                                    <div class="col-xs-12 boxcontent">
                                        <div class="row">
                                            <div class="col-xs-12">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">GIS: </label>
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
                                                        <label class="control-label no-padding-right">Reason: </label>
                                                        <asp:TextBox ID="txtDivisionReason" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">LIC ID क्रमांक </label>
                                                    <asp:TextBox ID="txtLICId" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">दावा संख्या </label>
                                                    <asp:TextBox ID="txtClaimNum" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">दावा संख्या दिनांक </label>
                                                    <asp:TextBox ID="txtClaimNumberDate" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
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
                                                    <label class="control-label no-padding-right">GIS  बीमा प्रकार: </label>
                                                    <asp:TextBox ID="txtGisType" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">दावा मुख्यालय को प्राप्त होने का दिनांक </label>
                                                    <asp:TextBox ID="txtClaimRecieveDate" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">दावा LIC प्रेषित किये जाने का क्रमांक  </label>
                                                    <asp:TextBox ID="txtLICSendNumber" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">दावा LIC प्रेषित किये जाने की दिनांक :  </label>
                                                    <asp:TextBox ID="txtLICSendDate" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">LIC से प्राप्त धनराशि  </label>
                                                    <asp:TextBox ID="txtLICRecieveAmount" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">LIC से धनराशि प्राप्त होने की दिनांक : </label>
                                                    <asp:TextBox ID="txtLICRecieveDate" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
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
                                                    <label class="control-label no-padding-right">धनराशि  : </label>
                                                    <asp:TextBox ID="txtPensionerAmount" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Cheque Number : </label>
                                                    <asp:TextBox ID="txtPensionerChequeNumber" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Account Number of Pensionar : </label>
                                                    <asp:TextBox ID="txtPensionerAccountNumber" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">IFSC Code : </label>
                                                    <asp:TextBox ID="txtPensionerIFSCCode" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-left">Date: </label>
                                                    <asp:TextBox ID="txtPensionPaidDate" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
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
                                                    <label class="control-label no-padding-right">Amount  : </label>
                                                    <asp:TextBox ID="txtDivisionAmountNumber" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">cheque number  : </label>
                                                    <asp:TextBox ID="txtDivisionChequeNumber" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">date  : </label>
                                                    <asp:TextBox ID="txtDivisionDateNumber" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row boxborder">
                                    <div class="col-xs-12">
                                        <div class="table-header">
                                            Nature Of Arrear
                                        </div>
                                    </div>

                                    <div class="col-xs-12 boxcontent">
                                        <div class="row">
                                            <div class="col-xs-12">
                                                <asp:GridView ID="grdNatureOfArrear" runat="server" CssClass="display table table-bordered"
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
                                                    <label class="control-label no-padding-right">Basic Pension Rate: </label>
                                                    <asp:TextBox ID="txtBasicPensionRate" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">No Of Month: </label>
                                                    <asp:TextBox ID="txtNoOfMonthDA" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>

                                <div class="row boxborder">
                                    <div class="col-xs-12">
                                        <div class="table-header">
                                            Other Details
                                        </div>
                                    </div>
                                    <div class="col-xs-12 boxcontent">

                                        <div class="row">
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Court Case No: </label>
                                                    <asp:TextBox ID="txtCourtCaseNo" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Court Case Year: </label>
                                                    <asp:TextBox ID="txtCourtCaseYear" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Contempt No: </label>
                                                    <asp:TextBox ID="txtContemptNo" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <label class="control-label no-padding-right">Contempt Year: </label>
                                                    <asp:TextBox ID="txtContemptYear" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
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
                                                    <label class="control-label no-padding-right">Regular Field Employee: </label>
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
                                                        <label class="control-label no-padding-right">Reason Of No Payment Of Pension: </label>
                                                        <asp:TextBox ID="txtNoPaymentReason" runat="server" CssClass="form-control" TextMode="MultiLine" Enabled="false"></asp:TextBox>
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
        </div>
    </div>

    <script>
        function downloadLifeCertificate(obj) {
            var path = document.getElementById('ctl00_ContentPlaceHolder1_hf_LiveCertificate').value;
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
            var path = document.getElementById('ctl00_ContentPlaceHolder1_hf_CompiledOrder').value;
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
            var path = document.getElementById('ctl00_ContentPlaceHolder1_hf_PensionOrder').value;
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
            var path = document.getElementById('ctl00_ContentPlaceHolder1_hf_PPO').value;
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
            var path = document.getElementById('ctl00_ContentPlaceHolder1_hf_Gratuity').value;
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
            var path = document.getElementById('ctl00_ContentPlaceHolder1_hf_GratuityPO').value;
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
            var path = document.getElementById('ctl00_ContentPlaceHolder1_hf_ReasonOrder').value;
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
            var path = document.getElementById('ctl00_ContentPlaceHolder1_hf_LeaveEncashmentDoc').value;
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
            var path = document.getElementById('ctl00_ContentPlaceHolder1_hf_LeaveEncashmentOrderHQ').value;
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
            var path = document.getElementById('ctl00_ContentPlaceHolder1_hf_LeaveEncashmentDeductionReason').value;
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
            var path = document.getElementById('ctl00_ContentPlaceHolder1_hf_ClaimUploadPath').value;
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
            var path = document.getElementById('ctl00_ContentPlaceHolder1_hf_DeathCeritificate').value;
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
            var path = document.getElementById('ctl00_ContentPlaceHolder1_hf_CourtCaseOrder').value;
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
            var path = document.getElementById('ctl00_ContentPlaceHolder1_hf_ContemptOrder').value;
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
            var path = document.getElementById('ctl00_ContentPlaceHolder1_hf_Photo').value;
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
</asp:Content>

