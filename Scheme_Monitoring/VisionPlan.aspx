<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/TemplateMasterAdmin_PMS.master" CodeFile="VisionPlan.aspx.cs" Inherits="VisionPlan" %>


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
                                    <h4 class="mb-sm-0" id="HeadingSec" runat="server">Vision Plan</h4>
                                    <div class="page-title-right">
                                        <ol class="breadcrumb m-0">
                                            <li class="breadcrumb-item"><a href="javascript: void(0);">Home</a></li>
                                            <li class="breadcrumb-item">CMVNY Scheme</li>
                                            <li class="breadcrumb-item active">Vision Plan</li>
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
                                        <h4 class="card-title mb-0 flex-grow-1">Filter :</h4>
                                         <a href="CreateVisionPlan.aspx"  class="filter-btn" style="float:right;width:155px"><i class="icon-download"></i> Create Vision Plan</a>

                                    </div>
                                    <!-- end card header -->
                                     <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-4">
                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divZone" runat="server">
                                                        <asp:Label ID="lblZoneH" runat="server" Text="State" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlZone" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divCircle" runat="server">
                                                        <asp:Label ID="lblCircleH" runat="server" Text="District" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlCircle" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCircle_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divDivision" runat="server">
                                                        <asp:Label ID="lblDivisionH" runat="server" Text="ULB" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-select"></asp:DropDownList>
                                                        <%--<asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged"></asp:DropDownList>--%>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divFY" runat="server">
                                                        <asp:Label ID="lblFY" runat="server" Text="Financial Year" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlFY" runat="server" CssClass="form-select" AutoPostBack="true" ></asp:DropDownList>
                                                    </div>
                                                </div>
                                                 <div class="col-xxl-3 col-md-6" id="sectionpriority"  runat="server" >
                                                    <div id="div7" runat="server">
                                                        <asp:Label ID="Label10" runat="server" Text="Self Priority" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="DdlPriority" runat="server" CssClass="form-select" >
                                                            <asp:ListItem value="0">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="1">1</asp:ListItem>
                                                            <asp:ListItem Value="2">2</asp:ListItem>
                                                            <asp:ListItem Value="3">3</asp:ListItem>
                                                            <asp:ListItem Value="4">4</asp:ListItem>
                                                            <asp:ListItem Value="5">5</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                               <%-- <div class="col-xxl-3 col-md-6">
                                                    <div id="divULBType" runat="server">
                                                        <asp:Label ID="Label1" runat="server" Text="ULB Type" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlULBType" runat="server" CssClass="form-select">
                                                            <asp:ListItem Value="">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="NN">Nagar Nigam</asp:ListItem>
                                                            <asp:ListItem Value="NPP">Nagar Palika Parishad</asp:ListItem>
                                                            <asp:ListItem Value="NP">Nagar Panchayat</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>--%>

                                                <div class="col-xxl-3  col-md-6">
                                                    <div>
                                                        <br />
                                                        <asp:Button ID="BtnSearch" Text="Search" OnClick="BtnSearch_Click" runat="server" style="float:right"  CssClass="btn bg-success text-white"></asp:Button>

                                                       
                                                    </div>
                                                </div>
                                              
                                            </div>
                                            <!--end row-->
                                        </div>
                                    </div>
                                </div>

                                <div class="card"  runat="server">
                                <div class="card-body"  runat="server" style="">
                                            <table id="sample-table-2" class="table table-striped table-bordered table-hover table-responsive">


                                                <thead>
                                                    <tr class="table-primary">

                                                        <th style="text-align:center;font-size:18px" >Sr. No.</th>
                                                       <th  style="text-align:center;font-size:18px" colspan="3">ULB Details</th>
                                                        <th  style="text-align:center;font-size:18px">Project Name</th>
                                                       <th  style="text-align:center;font-size:18px" colspan="2">Existing</th>
                                                        <th  style="text-align:center;font-size:18px" ">Condition</th>
                                                       
                                                        <th  style="text-align:center;font-size:18px">User Charge</th>
                                                        <th  style="text-align:center;font-size:18px" >Ownership</th>
                                                      
                                                        <th  style="text-align:center;font-size:18px" colspan="2">Location</th>
                                                        <th  style="text-align:center;font-size:18px" >Priority</th>
                                                         <th style="text-align:center;font-size:18px">Action</th>
                                                    </tr>
                                                     <tr class="table-primary">

                                                       <th>#</th>
                                                        <th  style="text-align:center">District</th>
                                                        <th  style="text-align:center">ULB Name</th>
                                                        <th  style="text-align:center">Population</th>
                                                        <th  style="text-align:center">Project Name</th>
                                                        <th  style="text-align:center">Construction </th>
                                                      <%--  <th  style="text-align:center">Under Construction(Y/N)</th>
                                                        <th  style="text-align:center">Under Sanction (Y/N)</th>--%>
                                                        <th  style="text-align:center">Year of Construction for Constructed Building</th>
                                                       <%-- <th  style='text-align:center">Good </th>
                                                        <th  style="text-align:center">Need Renovaton</th>
                                                        <th  style="text-align:center">Need Redevelopement</th>--%>
                                                        <th  style="text-align:center">Condition</th>
                                                        <th  style="text-align:center">User Charge</th>
                                                        <%--<th  style="text-align:center">Nagar Nigam</th>--%>
                                                        <th  style="text-align:center">Ownership</th>
                                                        <th  style="text-align:center">No Of Similar Project</th>
                                                        <th  style="text-align:center">Ward Name (Ward No.)</th>
                                                        <th  style="text-align:center">(on a scale of 1 to 5, 5 being the highest)</th>
                                                         <th></th>
                                                    </tr>
                                                </thead>
                                       
                                     <asp:Repeater ID="rptSearchResult" runat="server" OnItemCommand="rptSearchResult_ItemCommand">
                                        <ItemTemplate>
                                            <tr>
                                                <td><%# Container.ItemIndex + 1 %></td>
                                              
                                             

                                              
                                                <td><%# DataBinder.Eval(Container.DataItem, "Circle_Name") %></td>
                                                <td><%# DataBinder.Eval(Container.DataItem, "Division_Name") %></td>
                                                <td><%# DataBinder.Eval(Container.DataItem, "population") %></td>
                                                <td><%# DataBinder.Eval(Container.DataItem, "ProjectName") %></td>
                                               <%-- <td><%# DataBinder.Eval(Container.DataItem, "Constructed") %></td>
                                                <td><%# DataBinder.Eval(Container.DataItem, "UnderConstruction") %></td>
                                                <td><%# DataBinder.Eval(Container.DataItem, "Sanction") %></td>--%>
                                               

                                                <td><%# DataBinder.Eval(Container.DataItem, "Construction") %></td>
                                                <td><%# DataBinder.Eval(Container.DataItem, "constructedYear") %></td>
                                               <%-- <td><%# DataBinder.Eval(Container.DataItem, "GoodCondition") %></td>
                                                <td><%# DataBinder.Eval(Container.DataItem, "NeedRenovation") %></td>
                                                <td><%# DataBinder.Eval(Container.DataItem, "needredevelopement") %></td>--%>
                                                <td><%# DataBinder.Eval(Container.DataItem, "Condition") %></td>

                                               
                                                <td><%# DataBinder.Eval(Container.DataItem, "Usercharge") %></td>
                                                <td><%# DataBinder.Eval(Container.DataItem, "OwnerShips") %></td>
                                                <%--<td><%# DataBinder.Eval(Container.DataItem, "OtherOwner") %></td>--%>
                                                <td><%# DataBinder.Eval(Container.DataItem, "NoOfSameProjInCity") %></td>
                                                <td><%# DataBinder.Eval(Container.DataItem, "Loactions") %></td>
                                                <td><%# DataBinder.Eval(Container.DataItem, "selfPriority") %></td>


                                                <td>
                                                    <%--  <asp:LinkButton ID="btnEdit"  Text="Edit" CssClass="btn bg-warning" runat="server" ToolTip="Click to Edit Record" CommandName="edit"
                                                class="icon-pencil bigger-130 green" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "VisionPlanID") + "|" + DataBinder.Eval(Container.DataItem, "PlanName") %>' />--%>
                                                    <asp:LinkButton ID="btnEdit" runat="server" Text="Edit" CssClass="btn bg-warning icon-pencil bigger-130 green" ToolTip="Click to Edit Record" CommandName="edit"
                                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "VisionPlanID") + "|" + DataBinder.Eval(Container.DataItem, "distId")+ "|" + DataBinder.Eval(Container.DataItem, "ULBID")+ "|" + DataBinder.Eval(Container.DataItem, "FYID") %>' />
                                                    <asp:LinkButton ID="btnDelete" CssClass="btn bg-danger" Text="Delete" runat="server" ToolTip="Click to Delete Record" CommandName="delete" class="icon-trash bigger-130 red" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "VisionPlanID") %>'
                                                        OnClientClick="return ConfirmDeletion();" />

                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                           
                                        </FooterTemplate>
                                        
                                    </asp:Repeater>
                                                <asp:Panel ID="NoRecordsPanel" runat="server" Visible="False">
                                                    <p style="color:red;">Record Not Found</p>
                                                </asp:Panel>
                                   </table>
                                </div>
                                </div>

                            </div>
                        </div>


                       
                    </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="BtnSearch" />
                </Triggers>

                </asp:UpdatePanel>
          

            </div>
        </div>
    
        <style>
            tbale thead tr th{
                text-align:center;
            }
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
     <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>
    <script>
        $(document).ready(function () {
            $("#sample-table-2").DataTable();
        })
    </script>
    </asp:Content>