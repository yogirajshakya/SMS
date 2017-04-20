<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html xmlns="http://www.w3.org/TR/REC-html40">
	<head>
		<title>Customer Registration</title>
		<link href="styles/style.css" rel="stylesheet" type="text/css">
		<link href="styles/form.css" rel="stylesheet" type="text/css">
		<script type="text/javascript" src="scripts/commonValidation.js"></script>

		<script language=JavaScript>
			function reload(form){
			var val=form.state.options[form.state.options.selectedIndex].value; 
			self.location='customerRegistration.php?state=' + val ;
			}
		</script>
	</head>

	<body>
		<form method=post action="scripts/action-insertInformation.php" name="customerRegistrationForm">
			<table cellpadding="0" cellspacing="3px">
				<tr class="tr-form">
					<td colspan="2">
						<h1>Customer Registration Page</h1>
					</td>
				</tr>
				<tr class="tr-form">
					<td colspan="2">
						<div id="customerRegistrationForm_errorloc" class="form-text-error"></div>
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
						<a class="form-text-white"><b>ADDRESS LINE1</b></a><a class="form-text-white-small"> REQUIRED :</b></a>
					</td>
					<td class="td-fieldInput">
						<input class="text-box" type=text name="addr1" size="50">
					</td>
				</tr>
				<tr class="tr-form">
					<td class="td-fieldName">
						<a class="form-text-white"><b>ADDRESS LINE2</b></a><a class="form-text-white-small"> OPTIONAL :</b></a>
					</td>
					<td class="td-fieldInput">
						<input class="text-box" type=text name="addr2" size="50">
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
						<a class="form-text-white"><b>STATE</b></a><a class="form-text-white-small"> REQUIRED :</b></a>
					</td>
					<td class="td-fieldInput">
						<?php
							include("scripts/DBConfig.php");
							
							@$selectedState=$_GET["state"];
							
							echo "<select name=\"state\" onchange=\"reload(this.form)\">\n";
							echo "<option value=''>Select State</option>\n";
							
							$strQuery = "select value from MasterData where DataType='State' and ParentValue='India';";
							$rsrcResult = mysql_query($strQuery);
							
							while($arrayRow = mysql_fetch_assoc($rsrcResult))
							{
								$state = $arrayRow["value"];
								if ($state==@$selectedState)
								{
									echo "<option selected value=\"$state\">$state</option>\n";
								}
								else
								{
									echo "<option value=\"$state\">$state</option>\n";
								}
							}
							echo "</select>";
						?>
					</td>
				</tr>
				<tr class="tr-form">
					<td class="td-fieldName">
						<a class="form-text-white"><b>CITY</b></a><a class="form-text-white-small"> REQUIRED :</b></a>
					</td>
					<td class="td-fieldInput">
						<?php
							/* *** Reference website for Conditional Dropdown: http://www.plus2net.com/php_tutorial/php_drop_down_list.php *** */
							/* *** Reference website for PHP & AJAX Tutorial: http://www.php-learn-it.com/tutorials/starting_with_php_and_ajax.html *** */

							echo "<select name=\"city\">\n";
							echo "<option value=''>Select City</option>\n";
							$strQuery = "select value from MasterData where DataType='City' and ParentValue='$selectedState' order by value;";
							$rsrcResult = mysql_query($strQuery);

							while($arrayRow = mysql_fetch_assoc($rsrcResult))
							{
								$city = $arrayRow["value"];
								echo "<option value=\"$city\">$city</option>\n";
							}
							mysql_close($connect);
							echo "</select>";
						?>
					</td>
				</tr>
				<tr class="tr-form">
					<td class="td-fieldName">
						<a class="form-text-white"><b>PIN</b></a><a class="form-text-white-small"> OPTIONAL :</b></a>
					</td>
					<td class="td-fieldInput">
						<input class="text-box" type=text name="pin" size="50">
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
						<input class="text-box" type=text name="altemail" size="50">
					</td>
				</tr>
				<tr class="tr-form">
					<td class="td-fieldName">
						<a class="form-text-white"><b>WEBSITE</b></a><a class="form-text-white-small"> OPTIONAL :</b></a>
					</td>
					<td class="td-fieldInput">
						<input class="text-box" type=text name="website" size="50">
					</td>
				</tr>
				<tr class="tr-form">
					<td class="td-fieldName">
						<a class="form-text-white"><b>PRODUCT NAME</b></a><a class="form-text-white-small"> REQUIRED :</b></a>
					</td>
					<td class="td-fieldInput">
						<select type=text name="pname">
							<option value="iMailer">iMailer</option>
							<option value="iFinnet">iFinnet</option>
						</select>
					</td>
				</tr>
				<tr class="tr-form">
					<td class="td-fieldName">
						<a class="form-text-white"><b>PRODUCT VERSION</b></a><a class="form-text-white-small"> REQUIRED :</b></a>
					</td>
					<td class="td-fieldInput">
						<select type=text name="pversion">
							<option value="1.0">1.0</option>
							<option value="2.0">2.0</option>
						</select>
					</td>
				</tr>
				<tr class="tr-form">
					<td class="td-fieldName">
						<a class="form-text-white"><b>PURCHASED LICENSES</b></a><a class="form-text-white-small"> REQUIRED :</b></a>
					</td>
					<td class="td-fieldInput">
						<input class="text-box" type=text name="plicenses" size="50">
					</td>
				</tr>
				<tr class="tr-form">
					<td class="td-fieldName">
						<a class="form-text-white"><b>ACTUAL PRICE</b></a><a class="form-text-white-small"> REQUIRED :</b></a>
					</td>
					<td class="td-fieldInput">
						<input class="text-box" type=text name="aprice" size="50">
					</td>
				</tr>
				<tr class="tr-form">
					<td class="td-fieldName">
						<a class="form-text-white"><b>OFFERED PRICE</b></a><a class="form-text-white-small"> REQUIRED :</b></a>
					</td>
					<td class="td-fieldInput">
						<input class="text-box" type=text name="oprice" size="50">
					</td>
				</tr>
				<tr class="tr-form">
					<td class="td-fieldName">
						<a class="form-text-white"><b>AMOUNT PAID</b></a><a class="form-text-white-small"> REQUIRED :</b></a>
					</td>
					<td class="td-fieldInput">
						<input class="text-box" type=text name="apaid" size="50">
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
			var frmvalidator = new Validator("customerRegistrationForm");
			frmvalidator.EnableOnPageErrorDisplaySingleBox();
			frmvalidator.EnableMsgsTogether();
			frmvalidator.addValidation("fname", "req", "First Name is a mandatory field.");
			frmvalidator.addValidation("fname", "alphabetic_space", "First Name should contain only alphabets and spaces.");
			frmvalidator.addValidation("fname", "maxlen=50","Maximum length for First Name is 50.");

			frmvalidator.addValidation("addr1", "req", "Address Line1 is a mandatory field.");

			frmvalidator.addValidation("city", "req", "City is a mandatory field.");
			frmvalidator.addValidation("city", "alphabetic_space", "City should contain only alphabets and spaces.");
			frmvalidator.addValidation("city", "maxlen=20","Maximum length for City is 20.");

			frmvalidator.addValidation("state", "req", "State is a mandatory field.");
			frmvalidator.addValidation("state", "alphabetic_space", "State should contain only alphabets and spaces.");
			frmvalidator.addValidation("state", "maxlen=20","Maximum length for State is 20.");
			
			frmvalidator.addValidation("pin", "numeric","PIN should contain only numbers.");
			frmvalidator.addValidation("pin", "maxlen=6","PIN should contain 6 nunbers only.");

			frmvalidator.addValidation("contactno", "req", "Contact Number is a mandatory field.");
			frmvalidator.addValidation("contactno", "numeric","Contact Number should contain only numbers.");
			frmvalidator.addValidation("contactno", "maxlen=10","Contact Number only 10 numbers.");
			
			frmvalidator.addValidation("altcontactno", "numeric","Alternate Contact Number should contain only numbers.");
			frmvalidator.addValidation("altcontactno", "maxlen=11","Alternate Contact Number only 10 numbers.");

			frmvalidator.addValidation("email", "req","Email is a mandatory field.");
			frmvalidator.addValidation("email", "email", "Email format is invalid.");

			frmvalidator.addValidation("altemail", "email", "Alternate Email format is invalid.");

			frmvalidator.addValidation("plicenses", "req", "Purchased License is a mandatory field.");
			frmvalidator.addValidation("plicenses", "numeric","Purchased License should contain only numbers.");

			frmvalidator.addValidation("aprice", "req", "Actual Price is a mandatory field.");
			frmvalidator.addValidation("aprice", "decimal","Actual Price should contain only numbers.");

			frmvalidator.addValidation("oprice", "req", "Offered Price is a mandatory field.");
			frmvalidator.addValidation("oprice", "decimal","Offered Price should contain only numbers.");

			frmvalidator.addValidation("apaid", "req", "Amount Paid is a mandatory field.");
			frmvalidator.addValidation("apaid", "decimal","Amount Paid should contain only numbers.");
		</script>
	</body>
</html>