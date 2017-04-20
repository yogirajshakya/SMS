<form method=post action="scripts/send-email.php" name="contactUsForm" onsubmit="return validateContactForm()" enctype="multipart/form-data">
	<table cellpadding="0" cellspacing="3px">
		<tr class="tr-form">
			<td colspan="2">
				<div id="contactUsForm_errorloc" class="form-text-error"></div>
			</td>
		</tr>
		<tr class="tr-form">
			<td class="td-fieldName">
				<a class="form-text-white"><b>FULL NAME</b></a><a class="form-text-white-small"> REQUIRED :</b></a>
			</td>
			<td class="td-fieldInput">
				<input class="text-box" type=text name="fname" size="50">
			</td>
		</tr>
		<tr class="tr-form">
			<td class="td-fieldName">
				<a class="form-text-white"><b>LAST NAME</b></a><a class="form-text-white-small"> REQUIRED :</b></a>
			</td>
			<td class="td-fieldInput">
				<input class="text-box" type=text name="lname" size="50">
			</td>
		</tr>
		<tr class="tr-form">
			<td class="td-fieldName">
				<a class="form-text-white"><b>ADDRESS</b></a><a class="form-text-white-small"> REQUIRED :</b></a>
			</td>
			<td class="td-fieldInput">
				<input class="text-box" type=text name="address" size="50">
			</td>
		</tr>
		<tr class="tr-form">
			<td class="td-fieldName">
				<a class="form-text-white"><b>CITY</b></a><a class="form-text-white-small"> REQUIRED :</b></a>
			</td>
			<td class="td-fieldInput">
				<input class="text-box" type=text name="city" size="50">
			</td>
		</tr>
		<tr class="tr-form">
			<td class="td-fieldName">
				<a class="form-text-white"><b>STATE</b></a><a class="form-text-white-small"> REQUIRED :</b></a>
			</td>
			<td class="td-fieldInput">
				<input class="text-box" type=text name="state" size="50">
			</td>
		</tr>
		<tr class="tr-form">
			<td class="td-fieldName">
				<a class="form-text-white"><b>COUNTRY</b></a><a class="form-text-white-small"> REQUIRED :</b></a>
			</td>
			<td class="td-fieldInput">
				<input class="text-box" type=text name="country" value="INDIA" size="50" readonly>
			</td>
		</tr>
		<tr class="tr-form">
			<td class="td-fieldName">
				<a class="form-text-white"><b>CONTACT NUMBER</b></a><a class="form-text-white-small"> REQUIRED :</b></a>
			</td>
			<td class="td-fieldInput">
				<input class="text-box" type=text name="contactno" size="50">
			</td>
		</tr>
		<tr class="tr-form">
			<td class="td-fieldName">
				<a class="form-text-white"><b>ALTERNATE CONTACT NUMBER</b></a><a class="form-text-white-small"> OPTIONAL :</b></a>
			</td>
			<td class="td-fieldInput">
				<input class="text-box" type=text name="altcontactno" size="50">
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
				<a class="form-text-white"><b>ALTERNATE EMAIL</b></a><a class="form-text-white-small"> OPTIONAL :</b></a>
			</td>
			<td class="td-fieldInput">
				<input class="text-box" type="text" name="altemail" size="50">
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
	var frmvalidator = new Validator("contactUsForm");
	frmvalidator.EnableOnPageErrorDisplaySingleBox();
	frmvalidator.EnableMsgsTogether();
	frmvalidator.addValidation("fname", "req", "First Name is a mandatory field.");
	frmvalidator.addValidation("fname", "alphabetic_space", "First Name should contain only alphabets and spaces.");
	frmvalidator.addValidation("fname", "maxlen=25","Maximum length for First Name is 25.");
	
	frmvalidator.addValidation("lname", "req", "Last Name is a mandatory field.");
	frmvalidator.addValidation("lname", "alphabetic_space", "Last Name should contain only alphabets and spaces.");
	frmvalidator.addValidation("lname", "maxlen=25", "Maximum length for Last Name is 25.");

	frmvalidator.addValidation("address", "req", "Address is a mandatory field.");
	frmvalidator.addValidation("address", "alphanumeric_space", "Address should contain only alphanumeric characters and spaces.");
	
	frmvalidator.addValidation("city", "req", "City is a mandatory field.");
	frmvalidator.addValidation("city", "alphabetic_space", "City should contain only alphabets and spaces.");
	frmvalidator.addValidation("city", "maxlen=25", "Maximum length for City is 25.");

	frmvalidator.addValidation("contactno", "req", "Contact Number is a mandatory field.");
	frmvalidator.addValidation("contactno", "numeric","Contact Number should contain only numbers.");
	frmvalidator.addValidation("contactno", "maxlen=10","Contact Number only 10 numbers.");

	frmvalidator.addValidation("altcontactno", "numeric","Alternate Contact Number should contain only numbers.");
	frmvalidator.addValidation("altcontactno", "maxlen=11","Alternate Contact Number only 10 numbers.");

	frmvalidator.addValidation("email", "req","Email is a mandatory field.");
	frmvalidator.addValidation("email", "email", "Email format is invalid.");

	frmvalidator.addValidation("altemail", "email", "Alternate Email format is invalid.");
</script>