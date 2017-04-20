<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html xmlns="http://www.w3.org/TR/REC-html40">
	<head>
		<title>Activate Product Key</title>
		<link href="styles/style.css" rel="stylesheet" type="text/css">
		<link href="styles/form.css" rel="stylesheet" type="text/css">
		<script type="text/javascript" src="scripts/commonValidation.js"></script>
	</head>

	<body>
		<form method=post action="scripts/action-activateProductKey.php" name="productKeyActivationForm">
			<table cellpadding="0" cellspacing="3px">
				<tr class="tr-form">
					<td colspan="2">
						<h1>Product Key Activation Page</h1>
					</td>
				</tr>
				<tr class="tr-form">
					<td colspan="2">
						<div id="productKeyActivationForm_errorloc" class="form-text-error"></div>
					</td>
				</tr>
				<tr class="tr-form">
					<td class="td-fieldName">
						<a class="form-text-white"><b>EMAIL</b></a><a class="form-text-white-small"> REQUIRED :</b></a>
					</td>
					<td class="td-fieldInput">
						<input class="text-box" type=text name="email" size="50">
					</td>
				</tr>
				<tr class="tr-form">
					<td class="td-fieldName">
						<a class="form-text-white"><b>WORKSTATION NAME</b></a><a class="form-text-white-small"> REQUIRED :</b></a>
					</td>
					<td class="td-fieldInput">
						<input class="text-box" type=text name="wstname" size="50">
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
			var frmvalidator = new Validator("productKeyActivationForm");
			frmvalidator.EnableOnPageErrorDisplaySingleBox();
			frmvalidator.EnableMsgsTogether();

			frmvalidator.addValidation("email", "req","Email is a mandatory field.");
			frmvalidator.addValidation("email", "email", "Email format is invalid.");

			frmvalidator.addValidation("wstname", "req", "Workstation Name is a mandatory field.");
		</script>
	</body>
</html>