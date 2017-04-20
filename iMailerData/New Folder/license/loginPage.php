<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html xmlns="http://www.w3.org/TR/REC-html40">
	<head>
		<title>Login Page</title>
		<link href="styles/style.css" rel="stylesheet" type="text/css">
		<link href="styles/form.css" rel="stylesheet" type="text/css">
		<script type="text/javascript" src="scripts/commonValidation.js"></script>
	</head>

	<body>
		<form method=post action="scripts/action-validateUser.php" name="loginForm">
			<table cellpadding="0" cellspacing="3px">
				<tr class="tr-form">
					<td colspan="2">
						<h1>Login Page</h1>
					</td>
				</tr>
				<tr class="tr-form">
					<td colspan="2">
						<div id="loginForm_errorloc" class="form-text-error"></div>
					</td>
				</tr>
				<tr class="tr-form">
					<td class="td-fieldName">
						
					</td>
					<td class="td-fieldInput">
						<a class="form-text-white"><b>USERNAME</b></a><a class="form-text-white-small"> REQUIRED :</b></a><br/>
						<input class="text-box" type=text name="uname" size="50">
						<br/>
						<a class="form-text-white"><b>PASSWORD</b></a><a class="form-text-white-small"> REQUIRED :</b></a><br/>
						<input class="text-box" type=password name="passwd" size="50">
					</td>
				</tr>
				<tr class="tr-form">
					<td>
						&nbsp;
					</td>
					<td class="td-fieldInput" colspan="2">
						<input type="submit" class="button" value="Submit" style="text-align: center; float: center">
					</td>
				</tr>
			</table>
		</form>

		<script language="JavaScript" type="text/javascript">
			var frmvalidator = new Validator("loginForm");
			frmvalidator.EnableOnPageErrorDisplaySingleBox();
			frmvalidator.EnableMsgsTogether();

			frmvalidator.addValidation("uname", "req","Username is a mandatory field.");

			frmvalidator.addValidation("passwd", "req", "Password is a mandatory field.");
		</script>
	</body>
</html>