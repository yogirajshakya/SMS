<?
/* Declare variables that contain database information */
$host = 'localhost';
$user = 'root';
$pass = 'root@123';
$db   = 'infolanc_iMailerTest';

/* Connection setup to the database, so that there is no need to reopen the connection again on the same page */
$connect = mysql_connect($host, $user, $pass);

if ( !$connect )
{
	echo "Connection to DB Server could not be established. Please check the parameters.";
}
/* Make sure that the required database is selected */
mysql_select_db($db) or die ("Connection to DB Server is successful but could not connect to database.".mysql_error());
?>