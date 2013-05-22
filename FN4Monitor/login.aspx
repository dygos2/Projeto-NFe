<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="login.aspx.vb" Inherits="FN4Monitor.index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<script language="javascript">
	function valid(){
		var log, pass
			log = document.getElementById('login').value
			pass = document.getElementById('pass').value
			
			if(log == 'adm' && pass == 'adm'){
				window.location="interna/index.asp";
			} else {
				alert("Login/Senha incorreta!");
				document.getElementById('login').value = "";
				document.getElementById('pass').value = "";
			};
			
	};

</script>
<title>:: Sistema Fisconet ::</title>
<style type="text/css">
<!--
body {
	margin-left: 0px;
	margin-top: 0px;
	margin-right: 0px;
	margin-bottom: 0px;
	background-image: url(fundo.jpg);
}
.style1 {
	font-family: Tahoma;
	font-size: 10px;
	font-weight: bold;
	color: #383838;
}
.style3 {
	font-family: Tahoma;
	font-size: 10px;
	color: #383838;
} 
-->
</style></head>

<body>
    <form id="form1" runat="server">
<div align="center">
  <table width="774" height="580" border="0" cellpadding="0" cellspacing="0">
    <tr>
      <td align="center" valign="top" background="resources/img/fundo_login.jpg"><table width="200" border="0" cellpadding="0" cellspacing="0">
        <tr>
          <td><img src="resources/img/topo_login.jpg" width="768
          " height="84" /></td>
        </tr>
        <tr>
          <td width="38" height="38" align="left" valign="middle" bgcolor="#22435f"><img src="resources/img/login_usuario.jpg" width="141" height="14" /></td>
        </tr>
        <tr>
          <td></td>
        </tr>
        <tr>
          <td height="16">&nbsp;</td>
        </tr>
        
      </table>
        <table width="768" border="0" cellpadding="0" cellspacing="0">
          <tr>
            <td width="64" height="19"><span class="style1">&nbsp;&nbsp;&nbsp;&nbsp;LOGIN:</span></td>
            <td colspan="2" align="left" valign="middle">
                <input id="loginName" type="text" runat="server" /></td>
          </tr>
          <tr>
            <td height="34"><span class="style1">&nbsp;&nbsp;&nbsp;&nbsp;SENHA:</span></td>
            <td width="123" align="left" valign="middle" id="pass">
                <input id="userPassword" type="password" runat="server"/></td>
            <td width="581" align="left" valign="middle">
                <asp:ImageButton ID="ImageButton1" runat="server" 
                    ImageUrl="~/resources/img/but_login.jpg" />
                            </td>
          </tr>
          <tr>
            <td height="34">&nbsp;</td>
            <td width="123" align="left" valign="middle" id="pass">
                <asp:Label ID="lblValidacao" runat="server" ForeColor="#FF3300"></asp:Label>
                            </td>
            <td width="581" align="left" valign="middle">
                &nbsp;</td>
          </tr>
        </table>
        <p>&nbsp;</p>
        <p>&nbsp;</p>
        <p><br />
          <br />
        </p>
        <p><br />
        </p>
        <table width="768" border="0" cellpadding="0" cellspacing="0">
          <tr>
            <td height="70" align="center" valign="middle" bgcolor="#22435F">&nbsp;</td>
          </tr>
          <tr>
            <td align="center" valign="middle"><p class="style3"><br />
              Copyright ©2006- <%=DateTime.Now.Year%> Fisconet. Todos os direitos reservadosa</p>
            </td>
          </tr>
        </table></td>
    </tr>
  </table>
</div>
    </form>
</body>
</html>

