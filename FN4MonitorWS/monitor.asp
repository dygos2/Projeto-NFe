<!--#include file="inc/ini_con.asp"-->
<%
dim nome, tipo
'checando usuário e senha para logar no site
sql = "select * from usuarios where email like '$email' and senha like '$senha'"

sql = replace(sql, "$email", request("email"))
sql = replace(sql, "$senha", request("pass"))

'abrindo o RS
rs.open  sql, con,3,2

	nome = rs("nome")
	tipo = rs("tipo")

rs.close

if(cint(err.number) = 3021) then
	response.redirect("index.asp?erro=1")
end if
%>
<!--#include file="inc/close_con.asp"-->


<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" lang="en">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title>Monitor de Serviços</title>
<script language="javascript">AC_FL_RunContent = 0;</script>
<script src="AC_RunActiveContent.js" language="javascript"></script>
<style type="text/css">
<!--
body {
	background-image: url(imgs/fundo.jpg);
	margin-left: 0px;
	margin-top: 0px;
	margin-right: 0px;
	margin-bottom: 0px;
}
-->
</style></head>
<body bgcolor="#ffffff">
<div align="center">
  <table width="777" border="0" cellpadding="0" cellspacing="0" bgcolor="224460">
    <tr>
      <td><div align="center">
  <script language="javascript">
	if (AC_FL_RunContent == 0) {
		alert("This page requires AC_RunActiveContent.js.");
	} else {
		AC_FL_RunContent(
			'codebase', 'http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,0,0',
			'width', '766',
			'height', '580',
			'src', 'monitor',
			'quality', 'high',
			'pluginspage', 'http://www.macromedia.com/go/getflashplayer',
			'align', 'middle',
			'play', 'true',
			'loop', 'true',
			'scale', 'showall',
			'wmode', 'window',
			'devicefont', 'false',
			'id', 'monitor',
			'bgcolor', '#ffffff',
			'name', 'monitor',
			'menu', 'true',
			'allowFullScreen', 'true',
			'allowScriptAccess','sameDomain',
			'movie', 'monitor',
			'salign', '',
			'FlashVars', 'importa_cnpj=54800768000179-35//54800768000250-35&server=<%=server_prov%>&perfil_id=<%=tipo%>&nome=<%=nome%>&email=<%=request("email")%>'
			); //end AC code
	}
</script>
  <noscript>
    <object classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,0,0" width="766" height="580" id="monitor" align="middle">
      <param name="allowScriptAccess" value="sameDomain" />
      <param name="allowFullScreen" value="false" />
      <param name="movie" value="monitor.swf" />
      <param name="quality" value="high" />
	  <param name="FlashVars" value="importa_cnpj=54800768000179-35//54800768000250-35&server=<%=server_prov%>&perfil_id=<%=tipo%>&nome=<%=nome%>&email=<%=request("email")%>" />
      <param name="bgcolor" value="#ffffff" />
      <embed src="monitor.swf" quality="high" bgcolor="#ffffff" width="766" height="580" name="monitor" align="middle" allowScriptAccess="sameDomain" allowFullScreen="false" type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/go/getflashplayer" />
  </object>
  </noscript>
      </div></td>
    </tr>
  </table>
</div>
</body>
</html>