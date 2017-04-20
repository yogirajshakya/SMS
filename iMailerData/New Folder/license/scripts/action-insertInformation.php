<?php
// header("location: ../customerRegistration.php");

include("DBConfig.php");
// Variable declaration

$fname			= $_POST["fname"];
$addr1			= $_POST["addr1"];
$addr2			= $_POST["addr2"];
$city			= $_POST["city"];
$state			= $_POST["state"];
$pin			= $_POST["pin"];
$country		= $_POST["country"];
$contactno		= $_POST["contactno"];
$altcontactno	= $_POST["altcontactno"];
$email			= $_POST["email"];
$altemail		= $_POST["altemail"];
$website		= $_POST["website"];
$pname			= $_POST["pname"];
$pversion		= $_POST["pversion"];
$pkey			= generateKey($email,$pname);
$plicenses		= $_POST["plicenses"];
$aprice			= $_POST["aprice"];
$oprice			= $_POST["oprice"];
$apaid			= $_POST["apaid"];

/*
echo "Full Name : ".			$fname			."<br/>";
echo "Address Line1 : ".		$addr1			."<br/>";
echo "Address Line2 : ".		$addr2			."<br/>";
echo "City Name : ".			$city			."<br/>";
echo "State Name : ".			$state			."<br/>";
echo "PIN : ".					$pin			."<br/>";
echo "Country Name : ".			$country		."<br/>";
echo "Primary Contac# : ".		$contactno		."<br/>";
echo "Secondary Contact# : ".	$altcontactno	."<br/>";
echo "Primary EmailID : ".		$email			."<br/>";
echo "Secondary EmailID : ".	$altemail		."<br/>";
echo "Website : ".				$website		."<br/>";
echo "Product Name : ".			$pname			."<br/>";
echo "Product Version : ".		$pversion		."<br/>";
echo "Product Key : ".			$pkey			."<br/>";
echo "Purchased Licenses : ".	$plicenses		."<br/>";
echo "Activated Licenses : ".	$alicenses		."<br/>";
echo "Actual Price : ".			$aprice			."<br/>";
echo "Offered Price : ".		$oprice			."<br/>";
echo "Amount Paid : ".			$apaid			."<br/>";
*/

$strQuery = 'INSERT INTO CustomerInfo(IsAdmin,FullName,AddressLine1,AddressLine2,City,State,PIN,Country,PrimaryContactNo,SecondaryContactNo,PrimaryEmailId,SecondaryEmailId,Website,ProductName,ProductVersion,ProductLicense,PurchasedLicense,ActivatedLicense,ActualPrice,OfferedPrice,AmountPaid,LoginId,Password,CreationDateTime,LastModifiedDateTime) VALUES (\'N\',\''.$fname.'\',\''.$addr1.'\',\''.$addr2.'\',\''.$city.'\',\''.$state.'\',\''.$pin.'\',\''.$country.'\',\''.$contactno.'\',\''.$altcontactno.'\',\''.$email.'\',\''.$altemail.'\',\''.$website.'\',\''.$pname.'\',\''.$pversion.'\',\''.$pkey.'\','.$plicenses.',0'.	',\''.$aprice.'\',\''.$oprice.'\',\''.$apaid.'\',\''.'\',\'\','.'CURRENT_TIMESTAMP,CURRENT_TIMESTAMP);';

// echo $strQuery;

mysql_query($strQuery)or die("Error encountered : ".mysql_error());

function generateKey($custEmail,$productName){
	/* http://joe-riggs.com/blog/2009/08/php-create-url-safe-encrypted-string/ */

	$key1 = strtoupper(substr(md5(strstr($custEmail, '@')),0,5));
	$key2 = strtoupper(substr(md5($productName),6,5));
	$key3 = strtoupper(substr(md5(strstr($custEmail, '@', true)),11,5));
	$key4 = strtoupper(substr(md5($productName),16,5));
	$key5 = strtoupper(substr(md5(strstr($custEmail, '@')),21,5));
	return $key1.'-'.$key2.'-'.$key3.'-'.$key4.'-'.$key5;
}

mysql_close($connect);
?>