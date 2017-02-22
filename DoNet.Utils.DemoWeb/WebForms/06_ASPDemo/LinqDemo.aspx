<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LinqDemo.aspx.cs" Inherits="DoNet.Utils.DemoWeb.WebForms._06_ASPDemo.LinqDemo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <b>查询所有数据:</b><br />
            <asp:GridView ID="GridViewAll" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="序号" />
                    <asp:BoundField DataField="USERNAME" HeaderText="名称" />
                    <asp:BoundField DataField="SEX" HeaderText="性别" />
                    <asp:BoundField DataField="DUTY" HeaderText="职务" />
                </Columns>
            </asp:GridView>
        </div>
        <div>
            <b>Linq:from p in UserList where p.SEX.Equals("男") select p; </b>
            <br />
            <asp:GridView ID="GridViewNan" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="序号" />
                    <asp:BoundField DataField="USERNAME" HeaderText="名称" />
                    <asp:BoundField DataField="SEX" HeaderText="性别" />
                    <asp:BoundField DataField="DUTY" HeaderText="职务" />
                </Columns>
            </asp:GridView>
        </div>
        <div> 
            <b>Linq:UserList.Where(p => p.SEX.Equals("女"));</b><br />
            <asp:GridView ID="GridViewNv" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="序号" />
                    <asp:BoundField DataField="USERNAME" HeaderText="名称" />
                    <asp:BoundField DataField="SEX" HeaderText="性别" />
                    <asp:BoundField DataField="DUTY" HeaderText="职务" />
                </Columns>
            </asp:GridView>
        </div>
        <div> 
            <b>Linq:UserList.Where(p => p.USERNAME.StartsWith("李")).OrderByDescending(p=>p.ID);</b><br />
            <asp:GridView ID="GridViewLi" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="序号" />
                    <asp:BoundField DataField="USERNAME" HeaderText="名称" />
                    <asp:BoundField DataField="SEX" HeaderText="性别" />
                    <asp:BoundField DataField="DUTY" HeaderText="职务" />
                </Columns>
            </asp:GridView>
        </div>
        <div> 
            <b>Linq:UserList.Skip(4).Take(2);</b><br />
            <asp:GridView ID="GridViewPage" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="序号" />
                    <asp:BoundField DataField="USERNAME" HeaderText="名称" />
                    <asp:BoundField DataField="SEX" HeaderText="性别" />
                    <asp:BoundField DataField="DUTY" HeaderText="职务" />
                </Columns>
            </asp:GridView>
        </div> 
    </form>
</body>
</html>
