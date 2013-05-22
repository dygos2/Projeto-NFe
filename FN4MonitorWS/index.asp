<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3c.org/TR/1999/REC-html401-19991224/loose.dtd">
<!-- saved from url=(0030)http://fisconet.megaideas.net/ -->
<HTML
xmlns="http://www.w3.org/1999/xhtml"><HEAD><TITLE>:: Sistema Fisconet ::</TITLE>
<META http-equiv=Content-Type content="text/html; charset=utf-8">
<SCRIPT language=javascript>
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

</SCRIPT>

<STYLE type=text/css>BODY {
	BACKGROUND-IMAGE: url(imgs/fundo.jpg); MARGIN: 0px
}
.style1 {
	FONT-WEIGHT: bold; FONT-SIZE: 10px; COLOR: #383838; FONT-FAMILY: Tahoma
}
.style2 {
	FONT-WEIGHT: bold; FONT-SIZE: 10px; COLOR: #ffffff; FONT-FAMILY: Tahoma
}
.style3 {
	FONT-SIZE: 10px; COLOR: #383838; FONT-FAMILY: Tahoma
}
</STYLE>

<META content="MSHTML 6.00.6000.16705" name=GENERATOR></HEAD>
<BODY>
<DIV align=center>
<TABLE height=580 cellSpacing=0 cellPadding=0 width=774 border=0>
  <TBODY>
  <TR>
    <TD vAlign=top align=middle
    background=imgs/fundo_login.jpg>
      <TABLE cellSpacing=0 cellPadding=0 width=200 border=0>
        <TBODY>
        <TR>
          <TD><IMG height=84 src="imgs/topo_login.jpg"
            width=768></TD></TR>
        <TR>
          <TD vAlign=center align=left width=38 bgColor=#22435f height=38><IMG
            height=14 src="imgs/login_usuario.jpg"
            width=141></TD></TR>
        <TR>
          <TD></TD></TR>
        <TR>
          <TD height=16>&nbsp;</TD></TR></TBODY></TABLE>

	<form name="envio" action="monitor.asp" method="post">
      <TABLE cellSpacing=0 cellPadding=0 width=768 border=0>
        <TBODY>
        <TR>
          <TD width=64 height=19><SPAN
            class=style1>&nbsp;&nbsp;&nbsp;&nbsp;EMAIL:</SPAN></TD>
          <TD vAlign=center align=left colSpan=2><INPUT class=style1 id=email size=34 name=email></TD></TR>
        <TR>
          <TD height=34><SPAN
            class=style1>&nbsp;&nbsp;&nbsp;&nbsp;SENHA:</SPAN></TD>
          <TD vAlign=center align=left width=123>
		  <INPUT class=style1 id=pass type=password size=17 name=pass></TD>
          <TD vAlign=center align=left width=581>
		  <IMG style="CURSOR: hand" onclick='javascript:document.forms[0].submit();' height=17 src="imgs/but_login.jpg" width=71></TD></TR></TBODY></TABLE>
		<%
		'apontando acesso remoto por via VPN (2) ou via local/intranet (1)
		'configurar parâmetros no ini_con.asp
		if request("con") = 2 then
		%>
  		  <input type="hidden" name="con" value="2">
		<%else%>
		  <input type="hidden" name="con" value="1">
		<%end if%>
	</form>

      <P>&nbsp;</P>
      <P>&nbsp;</P>
      <P><BR>
      <%
      	if request("erro") = 1 then
      %>
      	<center><font color=red>Usuário/Senha não cadastrado!</font></center>
      <%end if%>
      <BR></P>
      <P><BR></P>
      <TABLE cellSpacing=0 cellPadding=0 width=768 border=0>
        <TBODY>
        <TR>
          <TD vAlign=center align=middle bgColor=#22435f height=70><SPAN
            class=style2>F.A.Q. | Fale Conosco | Sobre o Sistema</SPAN></TD></TR>
        <TR>
          <TD vAlign=center align=middle>
            <P class=style3><BR>Copyright ©2006- 2008 Fisconet. Todos os
            direitos
reservadosa</P></TD></TR></TBODY></TABLE></TD></TR></TBODY></TABLE></DIV></BODY></HTML>
